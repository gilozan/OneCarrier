using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class almex_almex : System.Web.UI.Page
{
    public bool toggleOn = false;
    public string pageType = "dash";
    public string sessionType = "1";
    public string supplierID = "-1";
    public string actn = "";
    public string scripts = "";
    public string prms = "";
    public System.Data.DataTable dtSuppliers = new System.Data.DataTable();
    public System.Data.DataTable dtSatCatalog = new System.Data.DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Cookies["kt_aside_toggle_state"] != null)
        {
            var value = Request.Cookies["kt_aside_toggle_state"].Value;
            if (value == "on")
                toggleOn = true;
        }
        if (!string.IsNullOrEmpty(Request.Params["p"]))
        {
            Response.Redirect("Login.aspx");
        }
        if (!string.IsNullOrEmpty(Request.Params["actn"]))
        {
            pageType = Request.Params["actn"];
            if (pageType == "auth")
                actn = "auth";
            if (pageType == "minv")
                GetSatCodes();
            if (pageType == "dash")
            {
                actn = "dash";
            }
            if (pageType == "inv")
                actn = "inv";
            if (pageType == "sinv")
                actn = "sinv";
            if (pageType == "cxc")
                actn = "cxc";
            if (pageType == "cob")
                actn = "cob";
            if (pageType == "minv")
            {
                actn = "minv";
                if (!string.IsNullOrEmpty(Request.Params["loadID"]))
                    prms = Request.Params["loadID"];
            }
            if (pageType == "minvalmx")
            {
                actn = "minvalmx";
            }

        }
        else { actn = "dash"; }
        try
        {
            if (Session["type"] == null)
                Response.Redirect("Login.aspx");
            sessionType = Session["type"].ToString();
            supplierID = Session["supplierid"].ToString();
        }
        catch
        {
            Response.Redirect("Login.aspx");
        }

    }
    public void GetSuppliers()
    {
        Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        dh.Connect();
        dh.Query("select * from suppliers where authorized='0'");
        this.dtSuppliers = dh.DataTable;
        dh.Disconnect();

    }

    public string GetSatCodes()
    {
        string search = Request.QueryString["search"];
        string result = "";
        if (!string.IsNullOrEmpty(search))
        {
            Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
            dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
            dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
            dh.Connect();
            dh.Query("select code,name from sat_catalog where name like '%search%'");
            this.dtSatCatalog = dh.DataTable;
            for (int i = 0; i < dtSatCatalog.Rows.Count; i++)
            {
                result += "{\"Results\" [{\"id\":\"" + dtSatCatalog.Rows[i]["code"] + "\",\"text\":\"" + dtSatCatalog.Rows[i]["name"] + "\"}] }";
            }
            dh.Disconnect();
        }
        return result;
    }
    [WebMethod(EnableSession = true)]
    public static string GetToken(string userid)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        DataHelper.Connect();
        DataHelper.Query("SELECT bp.legal_code,usr.login_name,bp.description FROM business_partners bp inner join users usr on bp.bpartner_id=usr.bpartner_id where usr.usr_id = " + userid);

        string result = "";
        string clienteRFC = ""; //legal_code
        string empresaRFC = ""; //legal_code
        string cuenta = "";

        if (DataHelper.exception.Message == "No Exception")
        {
            clienteRFC = DataHelper.DataTable.Rows[0]["legal_code"].ToString();
            empresaRFC = DataHelper.DataTable.Rows[0]["description"].ToString();
            cuenta = DataHelper.DataTable.Rows[0]["login_name"].ToString();
        }

        //string url = "http://166.62.93.54/ProconecttApi/Configuracion/GetParameters?ClienteRFC=" + clienteRFC.ToString() + "&EmpresaRFC=" + empresaRFC.ToString() + "&Cuenta=" + cuenta.ToString() + "";
        /*string url = "http://166.62.93.54/ProconectaApi/Configuracion/GetParameters?ClienteRFC=" +*/
        result = "CLIENTERFC=".ToUpper() + clienteRFC.ToString() + "&EmpresaRFC=".ToUpper() + empresaRFC.ToString() + "&Cuenta=".ToUpper() + cuenta.ToString() + "";
        //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        //HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        //{
        //    result = streamReader.ReadToEnd();
        //}


        //result.Replace("{\"data\":{\"Token\":", "{\"data\":{\"Token\":\"http://166.62.93.54/ProconecttWeb/pages/inicio.aspx?token=");

        return result;
    }
}