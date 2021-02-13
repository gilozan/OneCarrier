using System;
using System.Collections;
using System.Xml;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;
using System.IO;
using Npgsql;
using NpgsqlTypes;

namespace Hzone.Api.Database
{
    /// <summary>
    /// Sets the Status
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// 
        /// </summary>
        Available = 1,
        /// <summary>
        /// 
        /// </summary>
        Busy = 2
    }

    /// <summary>
    /// Sets the resultset Mode.
    /// </summary>
    public enum ResultSetMode
    {
        /// <summary>
        /// Works in Connected Mode using ResultSet to get Data from DataBase.
        /// </summary>
        DataReader = 1,
        /// <summary>
        /// Works in Disconnected Mode using Adapter to get Data from DataBase.
        /// </summary>
        DataAdapter = 2
    }
    /// <summary>
    /// Connection Type ODBC or OLEDB.
    /// </summary>
    public enum ConnectionType
    {
        OleDb = 1,
        Odbc = 2
    }
    /// <summary>
    /// Constructor for the DataHelper Class.
    /// The usage is selecting the ConnectionType, ConnectionUrl and the Default ResultSetMode is DataReader.
    /// </summary>
    public class DataHelper
    {
        #region Attributes
        public Exception exception;
        public int recordsAffected;

        //Connection settings
        bool Flagtransaction = false;
        int resultSetIndex = 0;

        //Connection Type 1
        private System.Data.OleDb.OleDbConnection connOleDb;
        private System.Data.OleDb.OleDbCommand statementOleDb;
        private System.Data.OleDb.OleDbDataReader resultSetOleDb;
        private System.Data.OleDb.OleDbTransaction transactionOleDb;
        private System.Data.OleDb.OleDbDataAdapter dataAdapterOleDb;

        //Connection Type 2
        private NpgsqlConnection connOdbc;
        private NpgsqlCommand statementOdbc;
        private NpgsqlDataReader resultSetOdbc;
        private NpgsqlTransaction transactionOdbc;
        private NpgsqlDataAdapter dataAdapterOdbc;

        private System.Data.DataTable datatable = new DataTable(); //ver 1.1
        private string bxsqlFile;
        private ResultSetMode resultSetMode = ResultSetMode.DataAdapter;
        private ConnectionType connectionType = ConnectionType.OleDb;
        private Status status = Hzone.Api.Database.Status.Available;
        private string connectionUrl;
        private XmlDocument bxsqlDocument;
        private string query = "";
        private string description;
        private Bnet.Next.Collections.Hashtable columnHashTable = new Bnet.Next.Collections.Hashtable();

        #endregion Attributes

        #region Properties
        public string QueryCommand
        {
            get { return this.query; }
            set { this.query = value; }
        }

        /// <summary>
        /// Gets a value indicating if the DataHelper is currently connected.
        /// </summary>
        [System.ComponentModel.Description("Gets a value indicating if the DataHelper is currently connected.")]
        public bool IsConnected
        {
            get
            {
                if (this.connectionType == ConnectionType.Odbc)
                {

                    if (this.connOdbc != null && this.connOdbc.State == (System.Data.ConnectionState.Open | System.Data.ConnectionState.Executing | System.Data.ConnectionState.Fetching))
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (this.connOleDb != null && this.connOleDb.State == (System.Data.ConnectionState.Open | System.Data.ConnectionState.Executing | System.Data.ConnectionState.Fetching))
                        return true;
                    else
                        return false;
                }
            }
        }

        /// <summary>
        /// Get or sets the current Hzone.Api.Database.Status of the DataHelper connection.
        /// </summary>
        [System.ComponentModel.Description("Get or sets the current Hzone.Api.Database.Status of the DataHelper connection.")]
        public Status Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }
        /// <summary>
        /// Gets or sets the description of the DataHelper.
        /// </summary>
        [System.ComponentModel.Description("Gets or sets the description of the DataHelper.")]
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        /// <summary>
        /// Gets or sets the current Datatable contained in the DataHelper.
        /// </summary>
        [System.ComponentModel.Description("Gets or sets the current Datatable contained in the DataHelper.")]
        public System.Data.DataTable DataTable
        {
            get
            {
                return this.datatable;
            }
            set
            {
                this.datatable = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the current BxsqlDocument from which the DataHelper gets its SQL commnads.
        /// </summary>
        [System.ComponentModel.Description("Gets or sets the current BxsqlFile from which the DataHelper gets its SQL commnads.")]
        public string BxsqlFile
        {
            get
            {
                return this.bxsqlFile;
            }
            set
            {
                this.bxsqlFile = value;
            }
        }
        /// <summary>
        /// Gets or sets the connection URL from which the data helper gets the Database name, port, server, the user and the password.
        /// Example: "Server='server name';Port=5432;User Id='xxxxx';Password='xxxxx';Database='database name';"
        /// </summary>
        [System.ComponentModel.Description("Gets or sets the connection URL from which the data helper gets the Database name, server and the user and password.")]
        public string ConnectionUrl
        {
            get
            {
                return this.connectionUrl;
            }
            set
            {
                this.connectionUrl = value;
            }
        }
        /// <summary>
        /// Gets or sets the current BxsqlDocument from which the DataHelper gets its SQL commnads.
        /// </summary>
        [System.ComponentModel.Description("Gets or sets the current BxsqlDocument from which the DataHelper gets its SQL commnads.")]
        public XmlDocument BxsqlDocument
        {
            get
            {
                return bxsqlDocument;
            }
            set
            {
                bxsqlDocument = value;
            }
        }
        /// <summary>
        /// Gets or sets the Hzone.Api.Database.ResultSetMode of the DataHelper
        /// </summary>
        [System.ComponentModel.Description("")]
        public ResultSetMode ResultSetMode
        {

            get
            {
                return resultSetMode;
            }
            set
            {
                this.resultSetMode = value;
            }

        }
        /// <summary>
        /// Gets or sets the Hzone.Api.Database.ConnectionType of the DataHelper
        /// </summary>
        [System.ComponentModel.Description("")]
        public ConnectionType ConnectionType
        {

            get
            {
                return connectionType;
            }
            set
            {
                this.connectionType = value;
            }

        }

        /// <summary>
        /// Gets or sets the Bnet.Next.Collections.Hashtable containing the column names and the information from the BXSQLFile of the columns to be resized and reheader. 
        /// </summary>
        [System.ComponentModel.Description("Gets or sets the Bnet.Next.Collections.Hashtable containing the column names and the information from the BXSQLFile of the columns to be resized and reheader. ")]
        public Bnet.Next.Collections.Hashtable ColumnHashTable
        {
            get
            {
                return columnHashTable;
            }
            set
            {
                this.columnHashTable = value;
            }
        }

        #endregion

        #region Contructors
        public DataHelper()
        {
            //
            // TODO: Add constructor logic here
            //
            exception = new Exception("No Exception");
        }
        #endregion Constructors

        #region transactions Statements
        /// <summary>
        /// Begins a transaction in the database.
        /// </summary>
        public void Begin()
        {
            this.Flagtransaction = true;
            if (this.ConnectionType == ConnectionType.OleDb)
            {
                transactionOleDb = this.connOleDb.BeginTransaction();
                statementOleDb = new OleDbCommand("", this.connOleDb, transactionOleDb);
                //statementOleDb.transaction=transactionOleDb;
            }
            if (this.ConnectionType == ConnectionType.Odbc)
            {
                transactionOdbc = this.connOdbc.BeginTransaction();
                statementOdbc = new NpgsqlCommand("", this.connOdbc, transactionOdbc);
                //statementOdbc.transaction=transactionOdbc;			
            }
        }
        /// <summary>
        /// Commits the changes in a transaction.
        /// </summary>
        public void Commit()
        {
            if (Flagtransaction == false)
                return;
            if (this.ConnectionType == ConnectionType.OleDb)
            {
                transactionOleDb.Commit();
                statementOleDb = null;
            }
            if (this.ConnectionType == ConnectionType.Odbc)
            {
                transactionOdbc.Commit();
                statementOdbc = null;
            }

        }
        /// <summary>
        /// Makes a rollback for a transaction in the database.
        /// </summary>
        public void Rollback()
        {
            if (Flagtransaction == false)
                return;
            if (this.ConnectionType == ConnectionType.OleDb)
            {
                transactionOleDb.Rollback();
            }
            if (this.ConnectionType == ConnectionType.Odbc)
            {
                transactionOdbc.Rollback();
            }

        }
        #endregion transaction Statements

        #region BXSQL Handling
        /// <summary>
        /// Loads an Bxsql File and sets the bxsqlFile property to the especified path.
        /// </summary>
        /// <param name="path">Path where the Bxsql File is located.</param>
        public void LoadBxsql(string path)
        {
            bxsqlFile = path;
            bxsqlDocument = new XmlDocument();
            bxsqlDocument.Load(bxsqlFile);
        }
        /// <summary>
        /// Loads an Bxsql File and requieres the path to be initialized in the bxsqlFile Property.
        /// </summary>
        public void LoadBxsql()
        {
            bxsqlDocument = new XmlDocument();
            bxsqlDocument.Load(bxsqlFile);
        }

        /// <summary>
        /// Sets a Connection String for a Bxsql.
        /// </summary>
        /// <param name="connName">The connection name to find in the Bxsql file.</param>
        public void SetConnectionBxsql(string connName)
        {
            XmlNodeList nodeConnection = bxsqlDocument.GetElementsByTagName("connection");
            for (int x = 0; x < nodeConnection.Count; x++)
            {
                if (nodeConnection[x].Attributes["name"].Value.ToString().ToLower() == connName.ToLower())
                {
                    if (nodeConnection[x].Attributes["type"].Value.ToString().ToLower() == "oledb")
                        this.ConnectionType = ConnectionType.OleDb;
                    if (nodeConnection[x].Attributes["type"].Value.ToString().ToLower() == "odbc")
                        this.ConnectionType = ConnectionType.Odbc;
                    this.ConnectionUrl = nodeConnection[x].InnerXml.ToString();
                }
            }
        }



        #endregion BXSQL Handling

        #region Database Conection Handling
        /// <summary>
        /// Sets an OleDbConnection from an already created Connection.
        /// </summary>
        /// <param name="conn">Connection that is going to be set.</param>
        public void SetConnection(OleDbConnection conn)
        {
            this.connOleDb = conn;
            this.ConnectionType = ConnectionType.OleDb;
        }
        /// <summary>
        /// Sets an OdbcConnection from an already created Connection.
        /// </summary>
        /// <param name="conn">Connection that is going to be set.</param>
        public void SetConnection(NpgsqlConnection conn)
        {
            this.connOdbc = conn;
            this.ConnectionType = ConnectionType.Odbc;
        }
        /// <summary>
        /// Gets a connection.
        /// </summary>
        /// <returns>Returns the connection object.</returns>
        public object GetConnection()
        {
            if (this.ConnectionType == ConnectionType.OleDb)
                return this.connOleDb;
            if (this.ConnectionType == ConnectionType.Odbc)
                return this.connOdbc;
            return null;
        }

        /// <summary>
        /// Connects to a specified database.
        /// </summary>
        /// <returns>Returns true if successfull.</returns>
        public bool Connect()
        {
            exception = new Exception("No Exception");
            bool status = true;
            try
            {
                if (ConnectionType == ConnectionType.OleDb)
                {
                    this.connOleDb = new System.Data.OleDb.OleDbConnection(this.ConnectionUrl);
                    connOleDb.Open();
                }
                if (ConnectionType == ConnectionType.Odbc)
                {
                    this.connOdbc = new NpgsqlConnection(this.ConnectionUrl);
                    /*connOdbc.UserId="postgres";
                    connOdbc.Password="postgres";
                    connOdbc.Host="localhost";
                    connOdbc.Database="centrales";
                    */
                    connOdbc.Open();

                }
            }
            catch (Exception e)
            {
                status = false;
                exception = e;
            }
            return status;
        }
        /// <summary>
        /// Disconnects the database connection.
        /// </summary>
        /// <returns>Returns true if successfull.</returns>
        public bool Disconnect()
        {
            exception = new Exception("No Exception");
            bool status = true;
            try
            {
                if (ConnectionType == ConnectionType.OleDb)
                {
                    if (resultSetOleDb != null)
                        resultSetOleDb.Close();
                    connOleDb.Close();
                }
                if (ConnectionType == ConnectionType.Odbc)
                {
                    if (resultSetOdbc != null)
                        resultSetOdbc.Close();
                    connOdbc.Close();
                }
            }
            catch (Exception e)
            {
                status = false;
                exception = e;
            }
            return status;
        }

        /// <summary>
        /// Closes a reader.
        /// </summary>
        /// <returns>Returns true if no errors.</returns>
        public bool CloseReader()
        {
            exception = new Exception("No Exception");
            bool status = true;
            try
            {
                if (ConnectionType == ConnectionType.OleDb)
                    resultSetOleDb.Close();
                if (ConnectionType == ConnectionType.Odbc)
                    resultSetOdbc.Close();
            }
            catch (Exception e)
            {
                status = false;
                exception = e;
            }
            return status;
        }

        #endregion Database Conection Handling


        /// <summary>
        /// Executes an SQLQuery based on a bxsqlCommand from a bxsql file that has to be loaded first using
        /// LoadBxsql().
        /// </summary>
        /// <param name="bxsqlCommand">Command to be executed from the bxsql file.</param>
        /// <param name="bxsqlParameters">Parameters that need to be pased in a Hashtable.</param>
        /// <returns>Returns true if successfull, false if not and the exception can be read from the public exception property.</returns>
        public bool ExecuteCommand(string bxsqlCommand, Bnet.Next.Collections.Hashtable bxsqlParameters)
        {
            this.columnHashTable.Clear();
            query = "";
            recordsAffected = 0;
            exception = new Exception("No Exception");
            string type = "";
            string sqlText = "";
            string sqlConditions = "";
            string sqlConditionStr = "";
            string sqlDefaultConditionStr = "";
            string outerConditionStr = "";

            try
            {

                XmlNodeList nodeBxsql = bxsqlDocument.GetElementsByTagName("command");
                for (int x = 0; x < nodeBxsql.Count; x++)
                {
                    if (nodeBxsql[x].Attributes["name"].Value.ToString().ToLower() == bxsqlCommand.ToLower())
                    {
                        // Takes out the columns with other language rather than the selected.
                        //this.FilterLanguageColumns(nodeBxsql[x]);

                        type = nodeBxsql[x].Attributes["type"].Value.ToString().ToLower();
                        //Obtains the main query line (SqlText)
                        sqlText = nodeBxsql[x].ChildNodes[0].InnerText.Trim();

                        ///
                        //This parts eliminates the params not used in an insert or
                        // update
                        ///
                        if (type == "insert")
                        {
                            //Generate an array with the parameters
                            Bnet.Next.Collections.Hashtable initParams = new Bnet.Next.Collections.Hashtable();
                            string sqlAux;
                            int firstIndex = 0, secondIndex = 0;
                            firstIndex = sqlText.IndexOf('(');
                            secondIndex = sqlText.IndexOf(')');
                            sqlAux = sqlText.Substring(firstIndex + 1, (secondIndex - firstIndex - 1));
                            string[] strInitParams = sqlAux.Split(',');
                            foreach (string par in strInitParams)
                            {
                                initParams.Add(par.Replace("$", ""), "0", "True");
                            }
                            //Run all the hash table of parameters
                            foreach (DictionaryEntry d in bxsqlParameters)
                            {
                                //Only if the parameter is not an arraylist
                                if (bxsqlParameters.GetValue(d.Key).GetType().ToString() != "System.Collections.ArrayList")
                                {
                                    string key = d.Key.ToString().Replace("@", "");
                                    //removes from the list the parameters not sent and the ones with value '' (nothing)
                                    if (initParams.ContainsKey(key) & bxsqlParameters.GetValue(d.Key).ToString() != "")
                                    {
                                        initParams.Remove(key);
                                    }
                                }
                            }
                            foreach (DictionaryEntry d in initParams)
                            {
                                string key = ",@" + d.Key.ToString() + "@";
                                string keym = "@" + d.Key.ToString() + "@";
                                string key2 = ",$" + d.Key.ToString() + "$";
                                string key2m = "$" + d.Key.ToString() + "$";
                                sqlText = sqlText.Replace(key2, "");
                                sqlText = sqlText.Replace(key2m, "");
                                sqlText = sqlText.Replace(key, "");
                                sqlText = sqlText.Replace(keym, "");
                            }
                            //If for any reason there is a colon at the begining or the end it is
                            //eliminated
                            sqlText = sqlText.Replace("(,", "(");
                            sqlText = sqlText.Replace(",)", ")");
                        }
                        if (type == "update")
                        {
                            Bnet.Next.Collections.Hashtable initParams = new Bnet.Next.Collections.Hashtable();
                            string sqlAux;
                            int firstIndex = 0, secondIndex = 0;
                            sqlAux = sqlText = sqlText.ToLower();
                            firstIndex = sqlAux.IndexOf("set") + 3;
                            secondIndex = sqlText.Length;
                            sqlAux = sqlText.Substring(firstIndex + 1, (secondIndex - firstIndex - 1));
                            string[] strInitParamsAll = sqlAux.Split(',');
                            string[] strInitParams;
                            foreach (string parAll in strInitParamsAll)
                            {
                                strInitParams = parAll.Split('=');

                                initParams.Add(strInitParams[0].Replace("$", ""), "0", "True");

                            }

                            foreach (DictionaryEntry d in bxsqlParameters)
                            {
                                //Only if the parameter is not an arraylist
                                if (bxsqlParameters.GetValue(d.Key).GetType().ToString() != "System.Collections.ArrayList")
                                {
                                    string key = d.Key.ToString().Replace("@", "");
                                    //removes from the list the parameters not sent and the ones with value '' (nothing)
                                    if (initParams.ContainsKey(key) & bxsqlParameters.GetValue(d.Key).ToString() != "")
                                    {
                                        initParams.Remove(key);
                                    }
                                }
                            }
                            foreach (DictionaryEntry d in initParams)
                            {
                                string key = ",$" + d.Key.ToString() + "$=@" + d.Key.ToString() + "@";
                                string keym = "$" + d.Key.ToString() + "$=@" + d.Key.ToString() + "@";
                                sqlText = sqlText.Replace(key, "");
                                sqlText = sqlText.Replace(keym, "");
                            }
                            //If for any reason there is a colon at the begining or the end it is
                            //eliminated
                            sqlText = sqlText.Replace("set ,", "set ");

                        }
                        if (type == "select")
                        {
                            Bnet.Next.Collections.Hashtable initParams = new Bnet.Next.Collections.Hashtable();
                            string sqlAux;
                            int firstIndex = 0, secondIndex = 0;
                            sqlAux = sqlText = sqlText.ToLower();
                            firstIndex = sqlAux.IndexOf("select") + 6;
                            secondIndex = sqlAux.IndexOf("from");
                            sqlAux = sqlText.Substring(firstIndex + 1, (secondIndex - firstIndex - 1));
                            string[] strInitParams = sqlAux.Split(',');
                            foreach (string parAll in strInitParams)
                            {
                                initParams.Add(parAll.Replace("@", "").Trim(), "0", "True");

                            }

                            foreach (DictionaryEntry d in bxsqlParameters)
                            {
                                //Only if the parameter is not an arraylist
                                if (bxsqlParameters.GetValue(d.Key).GetType().ToString() != "System.Collections.ArrayList")
                                {
                                    string key = d.Key.ToString().Replace("@", "");
                                    //removes from the list the parameters not sent and the ones with value '' (nothing)
                                    if (initParams.ContainsKey(key) & bxsqlParameters.GetValue(d.Key).ToString() != "")
                                    {
                                        initParams.Remove(key);
                                    }
                                }
                            }
                            foreach (DictionaryEntry d in initParams)
                            {
                                string key = ",@" + d.Key.ToString() + "@";
                                string keym = "@" + d.Key.ToString() + "@";
                                sqlText = sqlText.Replace(key, "");
                                sqlText = sqlText.Replace(keym, "");
                            }
                            //If for any reason there is a colon at the begining or the end it is
                            //eliminated
                            sqlText = sqlText.Replace("select ,", "select ");
                            sqlText = sqlText.Replace(", from", " from");

                        }
                        ///
                        //Ends the section that removes the parameters not specified.
                        ///
                        if (type == "select" | type == "update" | type == "delete")
                        {
                            int count = 0;
                            for (int y = 0; y < nodeBxsql[x].ChildNodes.Count; y++)
                            {
                                if (nodeBxsql[x].ChildNodes[y].Name == "column")
                                {
                                    string data = nodeBxsql[x].ChildNodes[y].InnerText.Trim();
                                    string[] str = data.Split(',');
                                    //if the width of colummn is declared on bxsql it is saved to the hashtable
                                    if (str.Length > 2)
                                    {
                                        ArrayList parameters = new ArrayList();
                                        parameters.Add(str[2]);
                                        string format = "";
                                        for (int formatCount = 3; formatCount < str.Length; formatCount++)
                                        {
                                            format = format + str[formatCount];
                                            if ((formatCount + 1) < str.Length)
                                            {
                                                format = format + ",";
                                            }
                                        }
                                        parameters.Add(format);
                                        parameters.Add(count);
                                        count++;
                                        this.columnHashTable.Add(str[0], str[1], parameters);
                                    }
                                    else
                                        columnHashTable.Add(str[0], str[1], null);
                                }

                                if (nodeBxsql[x].ChildNodes[y].Name == "default")
                                {
                                    sqlDefaultConditionStr = nodeBxsql[x].ChildNodes[y].InnerText.Trim();
                                }
                                if (nodeBxsql[x].ChildNodes[y].Name == "where")
                                {
                                    for (int w = 0; w < nodeBxsql[x].ChildNodes[y].ChildNodes.Count; w++)
                                    {
                                        string innerCondition = nodeBxsql[x].ChildNodes[y].ChildNodes[w].Attributes["innercondition"].Value.ToUpper();
                                        string outerCondition = nodeBxsql[x].ChildNodes[y].ChildNodes[w].Attributes["outercondition"].Value.ToUpper();
                                        for (int z = 0; z < nodeBxsql[x].ChildNodes[y].ChildNodes[w].ChildNodes.Count; z++)
                                        {
                                            string like = "=";
                                            string key = nodeBxsql[x].ChildNodes[y].ChildNodes[w].ChildNodes[z].Attributes["value"].Value;

                                            like = nodeBxsql[x].ChildNodes[y].ChildNodes[w].ChildNodes[z].Attributes["operator"].Value.ToUpper();

                                            if (bxsqlParameters.ContainsKey(key))
                                            {
                                                if (bxsqlParameters.GetValue(key).ToString() != "" & bxsqlParameters.GetValue(key).ToString() != "%" & bxsqlParameters.GetValue(key).ToString() != "%%")
                                                {
                                                    //If the parameter is an array list, the like property needs to be changed to "IN" instead of "=" or "like"
                                                    if (bxsqlParameters.GetValue(key).GetType().ToString() == "System.Collections.ArrayList")
                                                        like = "IN";
                                                    sqlConditions += nodeBxsql[x].ChildNodes[y].ChildNodes[w].ChildNodes[z].Attributes["name"].Value + " " + like + " " + key + " " + innerCondition + " ";
                                                }
                                            }
                                        }
                                        //If there are conditions always removes the last innerCondition and agrupate in parentheses
                                        if (sqlConditions != "")
                                        {
                                            //only add an outercondition if it´s the second time (outerConditionStr has nothing the first time)
                                            sqlConditions = sqlConditions.Remove(sqlConditions.Length - (innerCondition.Length + 1), innerCondition.Length + 1);
                                            sqlConditions = sqlConditions.Insert(0, "(");
                                            sqlConditions = sqlConditions + ")";
                                            //The outer condition is only set when the sqlconditionStr is not blank to avoid putting the condition
                                            //when theres nothing before.
                                            if (sqlConditionStr != "" | outerConditionStr == "")
                                                sqlConditionStr += outerConditionStr;

                                            sqlConditionStr += " " + sqlConditions;
                                            sqlConditions = "";
                                        }
                                        outerConditionStr = " " + outerCondition;

                                    }
                                    if (sqlDefaultConditionStr != "")
                                    {
                                        if (sqlConditionStr != "")
                                            sqlConditionStr = sqlConditionStr.Insert(0, sqlDefaultConditionStr + " AND "); //"+sqlConditionStr);
                                        else
                                            sqlConditionStr = sqlConditionStr.Insert(0, sqlDefaultConditionStr); //"+sqlConditionStr);
                                    }
                                    if (sqlConditionStr != "")
                                        sqlText += " WHERE " + sqlConditionStr;

                                }
                                else if (nodeBxsql[x].ChildNodes[y].Name == "orderby")
                                {
                                    if (nodeBxsql[x].ChildNodes[y].InnerText.Trim() != "")
                                        sqlText += " ORDER BY " + nodeBxsql[x].ChildNodes[y].InnerText.Trim();
                                }
                                else if (nodeBxsql[x].ChildNodes[y].Name == "groupby")
                                {
                                    if (nodeBxsql[x].ChildNodes[y].InnerText.Trim() != "")
                                    {
                                        string groupByText = nodeBxsql[x].ChildNodes[y].InnerText.Trim();
                                        Bnet.Next.Collections.Hashtable initParams = new Bnet.Next.Collections.Hashtable();
                                        string[] strInitParams = groupByText.Split(',');
                                        foreach (string parAll in strInitParams)
                                        {
                                            initParams.Add(parAll.Replace("@", "").Trim(), "0", "True");

                                        }

                                        foreach (DictionaryEntry d in bxsqlParameters)
                                        {
                                            //Only if the parameter is not an arraylist
                                            if (bxsqlParameters.GetValue(d.Key).GetType().ToString() != "System.Collections.ArrayList")
                                            {
                                                string key = d.Key.ToString().Replace("@", "");
                                                //removes from the list the parameters not sent and the ones with value '' (nothing)
                                                if (initParams.ContainsKey(key) & bxsqlParameters.GetValue(d.Key).ToString() != "")
                                                {
                                                    initParams.Remove(key);
                                                }
                                            }
                                        }
                                        foreach (DictionaryEntry d in initParams)
                                        {
                                            string key = ",@" + d.Key.ToString() + "@";
                                            string keym = "@" + d.Key.ToString() + "@";
                                            groupByText = groupByText.Replace(key, "");
                                            groupByText = groupByText.Replace(keym, "");
                                        }
                                        //If for any reason there is a colon at the begining or the end it is
                                        //eliminated
                                        sqlText += " GROUP BY " + groupByText;
                                        sqlText = sqlText.Replace("GROUP BY ,", "GROUP BY ");

                                    }
                                }
                            }
                        }
                        //else
                        //	sqlText=nodeBxsql[x].ChildNodes[0].InnerText.Trim();
                        //Eliminate the "$" symbol from parameters
                        sqlText = sqlText.Replace("$", "");
                        break;
                    }
                }


                if (type == "insert")
                {
                    if (Update(sqlText, bxsqlParameters))
                        return true;
                    else
                        return false;
                }
                else if (type == "update")
                {
                    if (Update(sqlText, bxsqlParameters))
                        return true;
                    else
                        return false;
                }
                else if (type == "select")
                {
                    //MessageBox.Show(sqlText);
                    if (Query(sqlText, bxsqlParameters))
                        return true;
                    else
                        return false;
                }
                else if (type == "delete")
                {
                    if (Update(sqlText, bxsqlParameters))
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                exception = e;
                return false;
            }
        }


        #region SQL Statements Execution
        /// <summary>
        /// Executes a Query and leaves the information in a resultSet.
        /// </summary>
        /// <param name="sqlQuery">Query to be executed.</param>
        /// <returns>Returns true if successfull.</returns>
        public bool Query(string sqlQuery)
        {
            query = "";
            recordsAffected = 0;
            exception = new Exception("No Exception");
            bool status = true;
            try
            {

                if (ConnectionType == ConnectionType.OleDb)
                    if (connOleDb.State == System.Data.ConnectionState.Closed)
                        connOleDb.Open();
                if (ConnectionType == ConnectionType.Odbc)
                    if (connOdbc.State == System.Data.ConnectionState.Closed)
                        connOdbc.Open();

                if (ConnectionType == ConnectionType.OleDb)
                {
                    if (ResultSetMode == ResultSetMode.DataReader)
                    {
                        if (statementOleDb == null)
                            statementOleDb = new System.Data.OleDb.OleDbCommand(sqlQuery, connOleDb);
                        else
                            statementOleDb.CommandText = sqlQuery;
                        resultSetOleDb = statementOleDb.ExecuteReader();
                    }
                    else
                    {
                        //If I create a new adapter to fill a data table, I establish the index to -1
                        resultSetIndex = -1;
                        if (this.statementOleDb == null)
                            this.statementOleDb = new OleDbCommand(sqlQuery, connOleDb);
                        else
                            this.statementOleDb.CommandText = sqlQuery;
                        this.dataAdapterOleDb = new OleDbDataAdapter(this.statementOleDb);
                        datatable = new DataTable();
                        dataAdapterOleDb.Fill(datatable);
                    }

                }
                if (ConnectionType == ConnectionType.Odbc)
                {
                    if (ResultSetMode == ResultSetMode.DataReader)
                    {
                        if (statementOdbc == null || statementOdbc.Connection.State == System.Data.ConnectionState.Closed)
                            statementOdbc = new NpgsqlCommand(sqlQuery, connOdbc);
                        else
                            statementOdbc.CommandText = sqlQuery;
                        resultSetOdbc = statementOdbc.ExecuteReader();
                    }
                    else
                    {
                        //If I create a new adapter to fill a data table, I establish the index to -1
                        resultSetIndex = -1;
                        if (statementOdbc == null || statementOdbc.Connection.State == System.Data.ConnectionState.Closed)
                            this.statementOdbc = new NpgsqlCommand(sqlQuery, connOdbc);
                        else
                            this.statementOdbc.CommandText = sqlQuery;
                        this.dataAdapterOdbc = new NpgsqlDataAdapter(this.statementOdbc);
                        datatable = new DataTable();
                        dataAdapterOdbc.Fill(datatable);
                    }
                }
                query = sqlQuery;

            }
            catch (Exception e)
            {
                if (e.Message.ToLower().Contains("broken") || e.Message.ToLower().Contains("not open"))
                {
                    try
                    {
                        if (ConnectionType == ConnectionType.OleDb)
                            connOleDb.Close();
                        if (ConnectionType == ConnectionType.Odbc)
                        {
                            connOdbc.Close();
                            statementOdbc = null;
                        }
                    }
                    catch { }
                }
                status = false;
                exception = e;
                query = sqlQuery;
            }

            return status;
        }

        /// <summary>
        /// Executes a Query and leaves the information in a resultSet.
        /// </summary>
        /// <param name="sqlQuery">Query to be executed.</param>
        /// <param name="sqlParameters">Parameters in a Bnet.Hashtable for the query, The first parameter in the Hashtable indicates de key, the second, indicates the value, and the third True if string, and False if any other datatype.</param>
        /// <returns>Returns True if successfull.</returns>
        public bool Query(string sqlQuery, Bnet.Next.Collections.Hashtable sqlParameters)
        {
            exception = new Exception("No Exception");
            bool status = true;
            string parameters = "";
            query = "";
            recordsAffected = 0;
            try
            {
                if (ConnectionType == ConnectionType.OleDb)
                    if (connOleDb.State == System.Data.ConnectionState.Closed)
                        connOleDb.Open();
                if (ConnectionType == ConnectionType.Odbc)
                    if (connOdbc.State == System.Data.ConnectionState.Closed)
                        connOdbc.Open();

                foreach (DictionaryEntry d in sqlParameters)
                {
                    if (sqlParameters.GetValue(d.Key).GetType().ToString() == "System.Collections.ArrayList")
                    {
                        ArrayList y = (ArrayList)d.Value;
                        object x = y[0];
                        {
                            foreach (object z in (ArrayList)x)
                            {
                                if ((string)sqlParameters.GetType(d.Key) == "True")
                                    parameters += "'" + z.ToString() + "',";
                                else
                                    parameters += z.ToString() + ",";
                            }
                        }
                        //Insert parentheses and remove colon.
                        parameters = parameters.Insert(0, "(");
                        parameters = parameters.Remove(parameters.Length - 1, 1);
                        parameters += ")";
                    }
                    else
                    {
                        if ((string)sqlParameters.GetType(d.Key) == "True")
                            parameters = "'" + sqlParameters.GetValue(d.Key).ToString() + "'";
                        else
                            parameters = sqlParameters.GetValue(d.Key).ToString();
                    }

                    sqlQuery = sqlQuery.Replace(d.Key.ToString(), parameters);
                    parameters = "";

                }
                query = sqlQuery;
                if (ConnectionType == ConnectionType.OleDb)
                {

                    if (ResultSetMode == ResultSetMode.DataReader)
                    {
                        if (statementOleDb == null)
                            statementOleDb = new System.Data.OleDb.OleDbCommand(sqlQuery, connOleDb);
                        else
                            statementOleDb.CommandText = sqlQuery;
                        resultSetOleDb = statementOleDb.ExecuteReader();
                    }
                    else
                    {
                        //If I create a new adapter to fill a data table, I establish the index to -1
                        resultSetIndex = -1;
                        if (this.statementOleDb == null)
                            this.statementOleDb = new OleDbCommand(sqlQuery, connOleDb);
                        else
                            this.statementOleDb.CommandText = sqlQuery;
                        this.dataAdapterOleDb = new OleDbDataAdapter(this.statementOleDb);
                        datatable = new DataTable();
                        dataAdapterOleDb.Fill(datatable);
                    }
                }
                if (ConnectionType == ConnectionType.Odbc)
                {

                    if (ResultSetMode == ResultSetMode.DataReader)
                    {
                        if (statementOdbc == null || statementOdbc.Connection.State == System.Data.ConnectionState.Closed)
                            statementOdbc = new NpgsqlCommand(sqlQuery, connOdbc);
                        else
                            statementOdbc.CommandText = sqlQuery;
                        resultSetOdbc = statementOdbc.ExecuteReader();
                    }
                    else
                    {
                        //If I create a new adapter to fill a data table, I establish the index to -1
                        resultSetIndex = -1;
                        if (statementOdbc == null || statementOdbc.Connection.State == System.Data.ConnectionState.Closed)
                            this.statementOdbc = new NpgsqlCommand(sqlQuery, connOdbc);
                        else
                            this.statementOdbc.CommandText = sqlQuery;
                        this.dataAdapterOdbc = new NpgsqlDataAdapter(this.statementOdbc);
                        datatable = new DataTable();
                        dataAdapterOdbc.Fill(datatable);
                    }
                }

            }
            catch (Exception e)
            {
                if (e.Message.ToLower().Contains("broken") || e.Message.ToLower().Contains("not open"))
                {
                    try
                    {
                        if (ConnectionType == ConnectionType.OleDb)
                            connOleDb.Close();
                        if (ConnectionType == ConnectionType.Odbc)
                        {
                            connOdbc.Close();
                            statementOdbc = null;
                        }
                    }
                    catch { }
                }
                status = false;
                exception = e;
            }

            return status;
        }

        /// <summary>
        /// Executes a query and leaves the information in a resultSet.
        /// </summary>
        /// <param name="sqlQuery">Query to be executed.</param>
        /// <returns>Returns the object of the first column and first row of the resulting data.</returns>
        public object QueryScalar(string sqlQuery)
        {
            exception = new Exception("No Exception");
            object resultValue = null;
            try
            {

                if (ConnectionType == ConnectionType.OleDb)
                    if (connOleDb.State == System.Data.ConnectionState.Closed)
                        connOleDb.Open();
                if (ConnectionType == ConnectionType.Odbc)
                    if (connOdbc.State == System.Data.ConnectionState.Closed)
                        connOdbc.Open();

                if (ConnectionType == ConnectionType.OleDb)
                {
                    if (statementOleDb == null)
                        statementOleDb = new System.Data.OleDb.OleDbCommand(sqlQuery, connOleDb);
                    else
                        statementOleDb.CommandText = sqlQuery;
                    resultValue = statementOleDb.ExecuteScalar();
                }
                if (ConnectionType == ConnectionType.Odbc)
                {
                    if (statementOdbc == null || statementOdbc.Connection.State == System.Data.ConnectionState.Closed)
                        statementOdbc = new NpgsqlCommand(sqlQuery, connOdbc);
                    else
                        statementOdbc.CommandText = sqlQuery;

                    //					statementOdbc = new NpgsqlCommand(sqlQuery,connOdbc);
                    //					statementOdbc.CommandText = sqlQuery;
                    resultValue = statementOdbc.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                exception = e;
            }
            return resultValue;
        }
        /// <summary>
        /// Executes an update based on a query and parameters.
        /// </summary>
        /// <param name="sqlUpdate">Sql Query for the update.</param>
        /// <param name="sqlParameters">Parameters in a Bnet.Hashtable for the query, The first parameter in the Hashtable indicates de key, the second, indicates the value, and the third True if string, and False if any other datatype.</param>
        /// <returns>Returns true if succesfull.</returns>
        public bool Update(string sqlUpdate, Bnet.Next.Collections.Hashtable sqlParameters)
        {
            exception = new Exception("No Exception");
            bool status = true;
            query = "";
            recordsAffected = 0;
            string parameters = "";
            try
            {
                if (ConnectionType == ConnectionType.OleDb)
                    if (connOleDb.State == System.Data.ConnectionState.Closed)
                        connOleDb.Open();
                if (ConnectionType == ConnectionType.Odbc)
                    if (connOdbc.State == System.Data.ConnectionState.Closed)
                        connOdbc.Open();

                foreach (DictionaryEntry d in sqlParameters)
                {
                    if (sqlParameters.GetValue(d.Key).GetType().ToString() == "System.Collections.ArrayList")
                    {
                        ArrayList y = (ArrayList)d.Value;
                        object x = y[0];
                        {
                            foreach (object z in (ArrayList)x)
                            {
                                if ((string)sqlParameters.GetType(d.Key) == "True")
                                    parameters += "'" + z.ToString() + "',";
                                else
                                    parameters += z.ToString() + ",";
                            }
                        }
                        //Insert parentheses and remove colon.
                        parameters = parameters.Insert(0, "(");
                        parameters = parameters.Remove(parameters.Length - 1, 1);
                        parameters += ")";
                    }
                    else
                    {
                        if ((string)sqlParameters.GetType(d.Key) == "True")
                            parameters = "'" + sqlParameters.GetValue(d.Key).ToString() + "'";
                        else
                            parameters = sqlParameters.GetValue(d.Key).ToString();
                    }

                    sqlUpdate = sqlUpdate.Replace(d.Key.ToString(), parameters);
                    parameters = "";

                }
                query = sqlUpdate;
                if (ConnectionType == ConnectionType.OleDb)
                {
                    if (statementOleDb == null)
                        statementOleDb = new System.Data.OleDb.OleDbCommand(sqlUpdate, connOleDb);
                    else
                        statementOleDb.CommandText = sqlUpdate;
                    recordsAffected = statementOleDb.ExecuteNonQuery();
                }
                if (ConnectionType == ConnectionType.Odbc)
                {
                    if (statementOdbc == null || statementOdbc.Connection.State == System.Data.ConnectionState.Closed)
                        statementOdbc = new NpgsqlCommand(sqlUpdate, connOdbc);
                    else
                        statementOdbc.CommandText = sqlUpdate;
                    recordsAffected = statementOdbc.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                if (e.Message.ToLower().Contains("broken") || e.Message.ToLower().Contains("not open"))
                {
                    try
                    {
                        if (ConnectionType == ConnectionType.OleDb)
                            connOleDb.Close();
                        if (ConnectionType == ConnectionType.Odbc)
                        {
                            connOdbc.Close();
                            statementOdbc = null;
                        }
                    }
                    catch { }
                }
                status = false;
                exception = e;
            }
            return status;
        }
        public bool Update(string sqlUpdate)
        {
            exception = new Exception("No Exception");
            query = "";
            recordsAffected = 0;
            bool status = true;
            try
            {

                if (ConnectionType == ConnectionType.OleDb)
                    if (connOleDb.State == System.Data.ConnectionState.Closed)
                        connOleDb.Open();
                if (ConnectionType == ConnectionType.Odbc)
                    if (connOdbc.State == System.Data.ConnectionState.Closed)
                        connOdbc.Open();

                if (ConnectionType == ConnectionType.OleDb)
                {
                    if (statementOleDb == null)
                        statementOleDb = new System.Data.OleDb.OleDbCommand(sqlUpdate, connOleDb);
                    else
                        statementOleDb.CommandText = sqlUpdate;
                    recordsAffected = statementOleDb.ExecuteNonQuery();
                }
                if (ConnectionType == ConnectionType.Odbc)
                {
                    if (statementOdbc == null || statementOdbc.Connection.State == System.Data.ConnectionState.Closed)
                        statementOdbc = new NpgsqlCommand(sqlUpdate, connOdbc);
                    else
                        statementOdbc.CommandText = sqlUpdate;
                    recordsAffected = statementOdbc.ExecuteNonQuery();
                }

                query = sqlUpdate;

            }
            catch (Exception e)
            {
                if (e.Message.ToLower().Contains("broken") || e.Message.ToLower().Contains("not open"))
                {
                    try
                    {
                        if (ConnectionType == ConnectionType.OleDb)
                            connOleDb.Close();
                        if (ConnectionType == ConnectionType.Odbc)
                        {
                            connOdbc.Close();
                            statementOdbc = null;
                        }
                    }
                    catch { }
                }
                status = false;
                exception = e;
            }

            return status;
        }

        #endregion SQL Statements Execution

        #region DataTable Data Retriving Functions
        /// <summary>
        /// Gets the next item in a resultSet.
        /// </summary>
        /// <returns>Returns true if successfull.</returns>
        public bool Next()
        {
            exception = new Exception("No Exception");
            bool status = false;
            try
            {

                if (this.ResultSetMode == ResultSetMode.DataReader)
                {
                    if (ConnectionType == ConnectionType.OleDb)
                    {

                        if (resultSetOleDb.Read())
                            status = true;

                    }
                    if (ConnectionType == ConnectionType.Odbc)
                    {
                        if (resultSetOdbc.Read())
                            status = true;
                    }
                }
                else
                {
                    if (datatable.Rows.Count > resultSetIndex + 1)
                    {
                        resultSetIndex++;
                        status = true;
                    }
                }

            }
            catch (Exception e)
            {
                status = false;
                exception = e;
            }

            return status;
        }

        /// <summary>
        /// Gets the values of a resultSet.
        /// </summary>
        /// <returns>Returns an array of objects with the values of a executed query.</returns>
        public object[] FieldValues()
        {
            exception = new Exception("No Exception");
            object[] values = null;
            try
            {
                if (ConnectionType == ConnectionType.OleDb)
                {
                    values = new object[resultSetOleDb.FieldCount];
                    resultSetOleDb.GetValues(values);
                }
                if (ConnectionType == ConnectionType.Odbc)
                {
                    values = new object[resultSetOdbc.FieldCount];
                    resultSetOdbc.GetValues(values);
                }
            }
            catch (Exception e)
            {
                values = new object[1];
                values[0] = "";
                exception = e;
            }
            return values;
        }
        /// <summary>
        /// Gets the object value.
        /// </summary>
        /// <param name="field">Is the name of the field for the value to be returned.</param>
        /// <returns>Returns an object value for the executed query.</returns>
        public object FieldValue(string field)
        {
            exception = new Exception("No Exception");
            object value = null;
            try
            {
                if (ResultSetMode == ResultSetMode.DataReader)
                {
                    if (ConnectionType == ConnectionType.OleDb)
                        value = resultSetOleDb.GetValue(resultSetOleDb.GetOrdinal(field));
                    if (ConnectionType == ConnectionType.Odbc)
                        value = resultSetOdbc.GetValue(resultSetOdbc.GetOrdinal(field));
                }
                else
                    value = datatable.Rows[resultSetIndex][datatable.Columns[field]];
            }
            catch (Exception e)
            {
                value = "";
                exception = e;
            }
            return value;
        }
        /// <summary>
        /// Gets the object value.
        /// </summary>
        /// <param name="position">Is the index of the field for the value to be returned.</param>
        /// <returns>Returns an object value for the executed query.</returns>	
        public object FieldValue(int position)
        {
            exception = new Exception("No Exception");
            object value = null;
            try
            {
                if (ResultSetMode == ResultSetMode.DataReader)
                {
                    if (ConnectionType == ConnectionType.OleDb)
                        value = resultSetOleDb.GetValue(position);
                    if (ConnectionType == ConnectionType.Odbc)
                        value = resultSetOdbc.GetValue(position);
                }
                else
                    value = datatable.Rows[resultSetIndex][datatable.Columns[position]];
            }
            catch (Exception e)
            {
                value = "";
                exception = e;
            }
            return value;
        }

        /// <summary>
        /// Gets the 'System.Type' of the especified column.
        /// </summary>
        /// <param name="field">string value indicating the column from which you want to get the Type.</param>
        /// <returns>string value indicating the System.Typ of the column.</returns>
        public string GetFieldType(string field)
        {
            exception = new Exception("No Exception");
            string value = "";
            try
            {
                if (ResultSetMode == ResultSetMode.DataReader)
                {
                    if (ConnectionType == ConnectionType.OleDb)
                        value = resultSetOleDb.GetValue(resultSetOleDb.GetOrdinal(field)).GetType().ToString();
                    if (ConnectionType == ConnectionType.Odbc)
                        value = resultSetOdbc.GetValue(resultSetOdbc.GetOrdinal(field)).GetType().ToString();
                }
                else
                    value = datatable.Rows[resultSetIndex][datatable.Columns[field]].GetType().ToString();
            }
            catch (Exception e)
            {
                value = "";
                exception = e;
            }
            return value;
        }

        /// <summary>
        /// Gets the 'System.Type' of the especified column.
        /// </summary>
        /// <param name="position">integer value indicating the column from which you want to get the Type.</param>
        /// <returns>string value indicating the System.Typ of the column.</returns>
        public string GetFieldType(int position)
        {
            exception = new Exception("No Exception");
            string value = "";
            try
            {
                if (ResultSetMode == ResultSetMode.DataReader)
                {
                    if (ConnectionType == ConnectionType.OleDb)
                        value = resultSetOleDb.GetValue(position).GetType().ToString();
                    if (ConnectionType == ConnectionType.Odbc)
                        value = resultSetOdbc.GetValue(position).GetType().ToString();
                }
                else
                    value = datatable.Rows[resultSetIndex][datatable.Columns[position]].GetType().ToString();
            }
            catch (Exception e)
            {
                value = "";
                exception = e;
            }
            return value;
        }

        #endregion DataTable Data Retriving Functions

        #region Cloning
        /// <summary>
        /// Gets a Clone object of this class.
        /// </summary>
        /// <returns>Hzone.Api.Database.DataHelper with a copy by value of this class.</returns>
        public Hzone.Api.Database.DataHelper Clone()
        {
            DataHelper dataHelper = new Hzone.Api.Database.DataHelper();
            dataHelper.ConnectionUrl = this.ConnectionUrl;
            dataHelper.ConnectionType = this.ConnectionType;
            return dataHelper;
        }
        #endregion Cloning

        public void saveFile(string fileName)
        {
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(new BufferedStream(fs));

                Byte[] bytes = br.ReadBytes(Convert.ToInt32(fs.Length));
                br.Close();
                fs.Close();

                if (this.connOdbc.State == ConnectionState.Closed)
                {
                    this.connOdbc.Open();
                }
                NpgsqlCommand comm = new NpgsqlCommand("insert into test(file,id) values (:bytesData,2)", this.connOdbc);
                NpgsqlParameter param = new NpgsqlParameter(":bytesData", DbType.Binary);

                param.Value = bytes;
                comm.Parameters.Add(param);
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.ToString();
                this.exception = ex;
            }
        }

        public void FileQuery(string query, System.Collections.Hashtable ht)
        {
            try
            {
                /*
                int lastindex = 0;
                string val = "";
                */
                ArrayList parameters = new ArrayList();
                NpgsqlCommand comm = new NpgsqlCommand(query, this.connOdbc);
                foreach (string s in ht.Keys)
                {
                    NpgsqlParameter param = new NpgsqlParameter(s, DbType.Binary);
                    param.Value = ht[s];
                    comm.Parameters.Add(param);
                }
                /*
                while(query.IndexOf("(:",lastindex) != -1 || query.IndexOf(",:",lastindex) != -1)
                {
                    string parameter = "";
                    int one = query.IndexOf("(:",lastindex);
                    int two = query.IndexOf(",:",lastindex);
                    lastindex = one==-1?two:one;
                    lastindex++;
                    int i = lastindex;
                    while (query[i] != ',' && query[i] != ')')
                    {
                        parameter += query[i++];
                    }
                    val = parameter.Substring(1);
                    NpgsqlParameter param = new NpgsqlParameter(parameter,DbType.Binary);
                    param.Value = ht.ContainsKey(parameter)?ht[parameter]:null;
                    parameters.Add(param);
                }
                foreach(NpgsqlParameter p in parameters)
                {
                    comm.Parameters.Add(p);
                }
                */
                if (this.connOdbc.State == ConnectionState.Closed)
                    this.connOdbc.Open();
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.ToString();
                this.exception = ex;
            }
        }

        #region Obsolete Fuctions
        /// <summary>
        /// Obsolete.
        /// </summary>
        [Obsolete("El método Buildtable() se encuentra en desuso.", false)]
        private void Buildtable()
        {
            System.Data.DataTable _table = null;
            if (ConnectionType == ConnectionType.OleDb)
            {
                _table = resultSetOleDb.GetSchemaTable();
            }
            if (ConnectionType == ConnectionType.Odbc)
            {
                _table = resultSetOdbc.GetSchemaTable();
            }
            System.Data.DataTable _dt = new System.Data.DataTable();
            System.Data.DataColumn _dc;
            System.Data.DataRow _row;
            System.Collections.ArrayList _al = new System.Collections.ArrayList();

            for (int i = 0; i < _table.Rows.Count; i++)
            {

                _dc = new System.Data.DataColumn();
                if (!_dt.Columns.Contains(_table.Rows[i]["ColumnName"].ToString()))
                {

                    _dc.ColumnName = _table.Rows[i]["ColumnName"].ToString();
                    _dc.Unique = Convert.ToBoolean(_table.Rows[i]["IsUnique"]);
                    _dc.AllowDBNull = Convert.ToBoolean(_table.Rows[i]["AllowDBNull"]);
                    _dc.ReadOnly = Convert.ToBoolean(_table.Rows[i]["IsReadOnly"]);
                    _al.Add(_dc.ColumnName);
                    _dt.Columns.Add(_dc);

                }
            }

            if (ConnectionType == ConnectionType.OleDb)
            {
                while (resultSetOleDb.Read())
                {
                    _row = _dt.NewRow();
                    for (int i = 0; i < _al.Count; i++)
                    {
                        _row[((System.String)_al[i])] = resultSetOleDb[(System.String)_al[i]];
                    }
                    _dt.Rows.Add(_row);
                }

            }
            if (ConnectionType == ConnectionType.Odbc)
            {
                while (resultSetOleDb.Read())
                {
                    _row = _dt.NewRow();
                    for (int i = 0; i < _al.Count; i++)
                    {
                        _row[((System.String)_al[i])] = resultSetOdbc[(System.String)_al[i]];
                    }
                    _dt.Rows.Add(_row);
                }

            }

            this.DataTable = _table;

        }

        #endregion Obsolete Fuctions

    }


}

