using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml;
using System.Drawing;
using Winnovative.WnvHtmlConvert;
using System.Collections;


public partial class Default : System.Web.UI.Page
{

    public bool toggleOn = false;
    public string pageType = "dash";
    public string sessionType = "1";
    public string supplierID = "-1";
    public string actn = "";
    public string scripts = "";
    public string prms="";
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

        }
        else { actn = "dash"; }
        try
        {
            if(Session["type"]==null)
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

}