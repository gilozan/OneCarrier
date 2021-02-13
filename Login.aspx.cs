using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Services;
using System.Collections;

public partial class Login : System.Web.UI.Page
{
    public Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
    public string result;
    public string lang = "ES";

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        //     Session.Timeout = 5;

        this.DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        this.DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        if(!string.IsNullOrEmpty(Request.Params["lang"]))
            lang=Request.Params["lang"];
        //authenticate(null,null);

        //if (IsPostBack) 
        //{
        //    TextBox1.Text = this.this.DataHelper.ConnectionUrl.ToString() + " " + this.DataHelper.exception.ToString() + " " + this.DataHelper.DataTable.Rows.Count.ToString();
        //}
    }


    public class Form
    {

        public string name { get; set; }
        public string value { get; set; }

    }

    public static string GetVal(Form[] formVars, string name)
    {
        var matches = formVars.Where(nv => nv.name.ToLower() == name.ToLower()).FirstOrDefault();
        if (matches != null)
            return matches.value;
        return string.Empty;
    }

    [WebMethod(EnableSession = true)]
    public static string authenticate(Form[] form)
    {
        string result = "";
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        try
        {
            if (DataHelper.Connect())
            { }
            else
            { result = "No se conecto: " + DataHelper.exception.ToString(); }
        }
        catch (Exception ex)
        { result = ex.ToString(); }

        string user = GetVal(form, "username");
        string password = GetVal(form, "password");
        if(DataHelper.ConnectionUrl.Contains("eINVOICED"))
            DataHelper.Query("select user_id,name,type,supplier_id from users u where u.username ='"
            + user + "' and u.password ='" + password + "'");
        else
            DataHelper.Query("select usr_id as user_id,u.name,u.type,usr_id as supplier_id,c.customer_id,bp.emails from users u INNER JOIN business_partners bp ON bp.bpartner_id=u.bpartner_id LEFT JOIN customers c ON c.bpartner_id=u.bpartner_id where u.login_name ='"
            + user + "' and u.password ='" + password + "' AND u.deleted='0'");
        //this.DataHelper.Disconnect();

        //    TextBox1.Text = this.DataHelper.exception.ToString();
        if (DataHelper.exception.Message != "No Exception")
            result = "No se conecto " + DataHelper.exception.Message.ToString();
        else
        {
            if (DataHelper.DataTable.Rows.Count == 1)
            {
                HttpContext.Current.Session["usuario"] = user;
                HttpContext.Current.Session["password"] = password;
                HttpContext.Current.Session["userid"] = DataHelper.DataTable.Rows[0]["user_id"].ToString();
                HttpContext.Current.Session["type"] = DataHelper.DataTable.Rows[0]["type"].ToString();
                HttpContext.Current.Session["supplierid"] = DataHelper.DataTable.Rows[0]["supplier_id"].ToString();
                HttpContext.Current.Session["customerid"] = DataHelper.DataTable.Rows[0]["customer_id"].ToString();
                HttpContext.Current.Session["nomusuario"] = DataHelper.DataTable.Rows[0]["name"].ToString();
                HttpContext.Current.Session["email"] = DataHelper.DataTable.Rows[0]["emails"].ToString();

                //Response.Redirect("Default.aspx");
                return "success";
            }
            else
            {
                result = "Nombre de usuario o contraseña incorrectos";

            }
        }
        DataHelper.Disconnect();
        return result;

    }

    [WebMethod(EnableSession = true)]
    public static string InsertSupplier(Form[] form)//Bnet.Next.Collections.Hashtable ht)
    {
        string result = "";
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        try
        {
            if (DataHelper.Connect())
            { }
            else
            { result = "No se conecto: " + DataHelper.exception.ToString(); }
        }
        catch (Exception ex)
        { result = ex.ToString(); }

        string user = GetVal(form, "username");
        string password = GetVal(form, "password");
        string company = GetVal(form, "fullname");
        string suppliernumber = GetVal(form, "suppliernumber");
        string contact = GetVal(form, "contact");
        string giro = GetVal(form, "giro");
        string tel = GetVal(form, "tel");
        string location = GetVal(form, "location");
        string msg = GetVal(form, "msg");
        string email = GetVal(form, "email");

        General.sendEmail("glozano@gmail.com", "Alta de Proveedor", "Se ha dado de alta el proveedor " + company.ToUpper() + "<p>");
        DataHelper.Update("INSERT INTO notifications (notif_id,user_id,name,created_by,updated_by,created,updated) VALUES (nextval('sq_notifications'),3,'Se agrego el proveedor " + company.ToUpper() + " ',4,4,now(),now()) ");
        DataHelper.Update("INSERT INTO suppliers (supplier_id,code,name,comments,contact,business_line,phone,email,username,password,type,activated,created_by,updated_by,created,updated,deleted) VALUES " +
        " (nextval('sq_suppliers'),'" + suppliernumber + "','" + company.ToUpper() + "','"+msg.ToUpper()+"','" + contact.ToUpper() + "','"+giro.ToUpper()+"','" + tel + "','" + email + "','" + email + "','" + password + "'," + location + ",'1',1,1,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP,'0') ");
        //this.DataHelper.Disconnect();

        //    TextBox1.Text = this.DataHelper.exception.ToString();
        if (DataHelper.exception.Message != "No Exception")
            result = "No se pudo insertar " + DataHelper.exception.Message.ToString();
        else
        {
           return "success"; 
        }
        DataHelper.Disconnect();
        return result;
    }
}
