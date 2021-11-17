using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Configuration;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using Newtonsoft.Json;
using System.Net;

public partial class GetHint : System.Web.UI.Page
{
    public Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        string search = Request.QueryString["search"];
        string result = "";
        resultss rs = new resultss();
        if (!string.IsNullOrEmpty(search))
        {
            Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
            dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
            dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
            dh.Connect();
            dh.Query("select code as id,code||'-'||name as text from sat_catalog where lower(unaccent(name)) like '%"+General.CleanString(search).ToLower()+"%' OR code like '%"+search+"%'");
            System.Data.DataTable dtSatCatalog = dh.DataTable;
            result="{\"items\":";
            //for (int i = 0; i < dtSatCatalog.Rows.Count; i++)
            //{
            //    result += "{\"id\":\"" + dtSatCatalog.Rows[i]["code"] + "\",\"text\":\"" + dtSatCatalog.Rows[i]["name"] + "\"}";
                //results r = new results();
                //r.id = dtSatCatalog.Rows[i]["code"].ToString();
                //r.text = dtSatCatalog.Rows[i]["name"].ToString();
                
                
            //}
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (System.Data.DataRow dr in dtSatCatalog.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (System.Data.DataColumn col in dtSatCatalog.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            result +=  new JavaScriptSerializer().Serialize(rows);
            result += "}";


            dh.Disconnect();
        }
        Context.Response.ContentType = "application/json; charset=utf-8";
        Response.Write(result); 
    }


    public class resultss
    {
        public results[] results { get; set; }
    }

    public class results
    {
        public string id { get; set; }
        public string text { get; set; }
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
    public static string GetInvDetail(Form[] form)//Bnet.Next.Collections.Hashtable ht)
    {
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        string result = "";
        string pID=GetVal(form,"pID");
        DataHelper.Connect();
        DataHelper.Query("select pi.uuid,pi.type,pi.legal_name,po.name as po from pch_invoices pi INNER JOIN pch_orders po ON po.pch_order_id=pi.pch_order_id where pi.pch_invoice_id=" + pID + "");
        if(DataHelper.Next())
        {
            result= "{\"response\":\"success\",\"uuid\":\"" + DataHelper.FieldValue("uuid") + "\"";
            result += ",\"suppliername\":\"" + DataHelper.FieldValue("legal_name") + " \"";
            if (DataHelper.FieldValue("type").ToString()=="I")
                result+=",\"type\":\"INGRESO \"";
            if (DataHelper.FieldValue("type").ToString() == "E")
                result += ",\"type\":\"EGRESO \"";
            if (DataHelper.FieldValue("type").ToString() == "P")
                result += ",\"type\":\"PAGO \"";
            result += ",\"po\":\"" + DataHelper.FieldValue("po") + " \"";
            result += "}";
        }
        DataHelper.Disconnect();
        return result;
    }

    [WebMethod(EnableSession = true)]
    public static string GetInvHistory(Form[] form)//Bnet.Next.Collections.Hashtable ht)
    {
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        string result = "<li class=\"kt-nav__head\">"+
							"Historial"+
							"<i class=\"flaticon2-information\" data-toggle=\"kt-tooltip\" data-placement=\"right\" title=\"Historial de la Factura...\"></i>"+
                        "</li>" +
						"<li class=\"kt-nav__separator\"></li>";
        string pID = GetVal(form, "pID");
        DataHelper.Connect();
        DataHelper.Query("select pi.created,uc.name as creby,pi.date_authorized,uau.name as autby,pi.date_programmed,upr.name as progby,date_paid,upa.name as paiby from pch_invoices pi "+
        " LEFT JOIN users uc ON uc.user_id=pi.created_by LEFT JOIN users upr ON upr.user_id=programmed_by "+
        "LEFT JOIN users upa ON upa.user_id=paid_by LEFT JOIN users uau ON uau.user_id=authorized_by "+
        "where pi.pch_invoice_id=" + pID + "");
        if (DataHelper.Next())
        {
            System.Collections.ArrayList arr = new System.Collections.ArrayList();
            if(DataHelper.FieldValue("created").ToString()!="")
                arr.Add(DataHelper.FieldValue("created")+"|"+DataHelper.FieldValue("creby")+"|Factura Registrada|danger");
            if(DataHelper.FieldValue("date_authorized").ToString()!="")
                arr.Add(DataHelper.FieldValue("date_authorized") + "|" + DataHelper.FieldValue("atuby") + "|Factura Autorizada|warning");
             if(DataHelper.FieldValue("date_programmed").ToString()!="")
                 arr.Add(DataHelper.FieldValue("date_programmed") + "|" + DataHelper.FieldValue("progby") + "|Factura Programada|brand");
            if(DataHelper.FieldValue("date_paid").ToString()!="")
                arr.Add(DataHelper.FieldValue("date_paid") + "|" + DataHelper.FieldValue("paiby") + "|Factura Pagada|success");
            for (int i = 0; i < arr.Count; i++)
            {
                string[] dataArr = arr[i].ToString().Split('|');
                result += "<div class=\"kt-widget2\">" +
                              "<div class=\"kt-widget2__item kt-widget2__item--"+dataArr[3]+"\">" +
                               "   <div class=\"kt-widget2__checkbox\">" +
                                    "<label>" +
                                     "   <span></span>" +
                                    "</label>" +
                                "</div>" +
                                "<div class=\"kt-widget2__info\">" +
                                    "<a href=\"javascript:;\" class=\"kt-widget2__title kt-link\">" +
                                     dataArr[2] +
                                    "</a>" +
                                    "<a href=\"javascript:;\" class=\"kt-widget2__username\">" +
                                     " Por el Usuario "+dataArr[1]+" el "+Convert.ToDateTime(dataArr[0]).ToString("dd-MMM-yy hh:mm tt")+" " +
                                    "</a>" +
                                "</div>" +
                                "<div class=\"kt-widget2__actions\">" +

                                "</div>" +
                             "</div>" +
                          "</div>";
            }
            
        }
        result+="<li class=\"kt-nav__separator\"></li>"+
				"<li class=\"kt-nav__foot\">"+
					
					"<a class=\"btn btn-clean btn-bold btn-sm\" href=\"javascript:;\" >Cerrar</a>"+
				"</li>";
        DataHelper.Disconnect();
        return result;
    }

    [WebMethod(EnableSession = true)]
    public static string GetNotifications(Form[] form)//Bnet.Next.Collections.Hashtable ht)
    {
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        string result = "";
        int totNotif = 0;
        string userid = HttpContext.Current.Session["userid"].ToString();
        DataHelper.Connect();
        DataHelper.Query("select * from notifications where user_id=" + userid + " and read is null order by created");
        while (DataHelper.Next())
        {
            string timeStr = "";
            TimeSpan time = DateTime.Now - Convert.ToDateTime(DataHelper.FieldValue("created"));
            if (time.TotalSeconds < 60)
                timeStr = "hace un momento";
            else if (time.TotalMinutes < 60)
                timeStr = "hace " + Convert.ToInt32(time.TotalMinutes) + " minutos";
            else if (time.TotalHours < 24)
                timeStr = "hace " + Convert.ToInt32(time.TotalHours) + " horas";
            else 
                timeStr = "hace " + Convert.ToInt32(time.TotalDays) + " días";

            result += "<a href=\"javascript:;\" onclick=\"DeleteNotification(" + DataHelper.FieldValue("notif_id") + ")\" class=\"kt-notification__item\" id=\"notif" + DataHelper.FieldValue("notif_id") + "\">" +
                    "	<div class=\"kt-notification__item-icon\">" +
                    "		<i class=\"flaticon-doc kt-font-success\"></i>" +
                    "	</div>" +
                    "	<div class=\"kt-notification__item-details\">" +
                    "		<div class=\"kt-notification__item-title\">" +
                    "			"+DataHelper.FieldValue("name")+"" +
                    "		</div>" +
                    "		<div class=\"kt-notification__item-time\">" +
                    "			"+timeStr+" " +
                    "		</div>" +
                    "	</div>" +
                    "</a>";

            totNotif++;
        }
        DataHelper.Disconnect();
        result += "|"+totNotif+"";
        return result;
    }


    [WebMethod(EnableSession = true)]
    public static string GetInvStatus(Form[] form)//Bnet.Next.Collections.Hashtable ht)
    {
        string resp="";
        string supplierid = HttpContext.Current.Session["supplierid"].ToString();
        string userid = HttpContext.Current.Session["userid"].ToString();
        string suppQry="";
        if (supplierid != "")
            suppQry="AND supplier_id=" + supplierid;
        if(userid=="6")
            suppQry = "AND supplier_id IN (1,2,3)";
        if (userid == "7")
            suppQry = "AND supplier_id IN (4,5)";

        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        DataHelper.Connect();

        DataHelper.Query("select count(pch_order_id) as cant,sum(total) as total from pch_orders where processed is null " + suppQry);
        if (DataHelper.Next())
            resp = DataHelper.FieldValue("cant").ToString() + "," + DataHelper.FieldValue("total").ToString() + ",";
        DataHelper.Query("select count(pch_invoice_id) as cant,sum(total) as total from pch_invoices where status=1"+ suppQry);
        if (DataHelper.Next())
            resp += DataHelper.FieldValue("cant").ToString()+","+DataHelper.FieldValue("total").ToString()+",";
        DataHelper.Query("select count(pch_invoice_id) as cant,sum(total) as total from pch_invoices where status=2" + suppQry);
        if (DataHelper.Next())
            resp += DataHelper.FieldValue("cant").ToString() + "," + DataHelper.FieldValue("total").ToString() + ",";
        DataHelper.Query("select count(pch_invoice_id) as cant,sum(total) as total from pch_invoices where status=3" + suppQry);
        if (DataHelper.Next())
            resp += DataHelper.FieldValue("cant").ToString() + "," + DataHelper.FieldValue("total").ToString() + ","; 
        DataHelper.Query("select count(pch_invoice_id) as cant,sum(total) as total from pch_invoices where status=4" + suppQry);
        if (DataHelper.Next())    
            resp += DataHelper.FieldValue("cant").ToString() + ","+DataHelper.FieldValue("total").ToString()+"";
        int count = 0;

        resp+="|";
        int max = 0;
        double percent = 0;
        DataHelper.Query("select mx.customer_id,c.name,max_guide,COALESCE(lastused,0) as lastused FROM "+
				"(select MAX(max_guide) as max_guide,customer_id  FROM customer_guide_range where authorized_by is not null group by customer_id ) mx "+
				"LEFT JOIN "+
				"(SELECT COALESCE(MAX(guide),0) as lastused,customer_id from subcustomer_guides group by customer_id) lu ON lu.customer_id=mx.customer_id "+
				"INNER JOIN customers c on c.customer_id=mx.customer_id ORDER BY lastused desc");

        for (int i = 0; i < DataHelper.DataTable.Rows.Count;i++ )
        {
            /*
            resp+=
            "<div class=\"kt-widget5__item\">"+
											"				<div class=\"kt-widget5__content\">"+
											"					<div class=\"kt-widget5__pic\">"+
											"						"+
											"					</div>"+
											"					<div class=\"kt-widget5__section\">"+
											"						<a href=\"#\" class=\"kt-widget5__title\">"+
																		DataHelper.DataTable.Rows[i]["name"]+
											"						</a>"+
											"					</div>"+
											"				</div>"+
											"				<div class=\"kt-widget5__content\">"+
											"					<div class=\"kt-widget5__stats\">"+
											"						<span class=\"kt-widget5__number\">"+DataHelper.DataTable.Rows[i]["max_guide"]+"</span>"+
											"						<span class=\"kt-widget5__sales\">Maximo</span>"+
											"					</div>"+
											"					<div class=\"kt-widget5__stats\">"+
											"						<span class=\"kt-widget5__number\">"+DataHelper.DataTable.Rows[i]["lastused"]+"</span>"+
											"						<span class=\"kt-widget5__votes\">Último usado</span>"+
											"					</div>"+
											"				</div>"+
											"			</div>";
             */ 
             if (i == 0)
            {
                resp += "<thead><tr><td width=\"40%\">Cliente</td><td>Máxima guía autorizada</td><td>Última guía utilizada</td></tr></thead><tbody>";
            }
            if (i==DataHelper.DataTable.Rows.Count)
            {
                resp +="</tbody>";
                break;
            }



            resp += "<tr><td>" + DataHelper.DataTable.Rows[i]["name"].ToString().Replace(",", "") + "</td><td>" + DataHelper.DataTable.Rows[i]["max_guide"].ToString() + "</td><td>" + DataHelper.DataTable.Rows[i]["lastused"].ToString() + "</td></div>" +
            "</div></td></tr>";
        }
            

        DataHelper.Disconnect();
        return resp;
    }

    [WebMethod(EnableSession = true)]
    public static string GetQuotes(Form[] form)//Bnet.Next.Collections.Hashtable ht)
    {
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;


        string supplierid = HttpContext.Current.Session["supplierid"].ToString();
        string userid= HttpContext.Current.Session["userid"].ToString();
        string sessionType = "" + HttpContext.Current.Session["type"];
        string customerid = HttpContext.Current.Session["customerid"].ToString();
        

        string dteInit = GetVal(form, "start");
        string dteEnd = GetVal(form, "end");
        string type = GetVal(form, "cmbInvType");
        string status = GetVal(form, "cmbInvStatus");
        string complement = GetVal(form, "cmbInvComplement");

        


        if (string.IsNullOrEmpty(dteInit))
        {
            dteInit = DateTime.Now.AddMonths(-1).ToString();
            dteEnd = DateTime.Now.ToString();
        }
        else
        {
            dteInit = Convert.ToDateTime(dteInit, CultureInfo.GetCultureInfo("es-MX")).ToString();
            dteEnd = Convert.ToDateTime(dteEnd, CultureInfo.GetCultureInfo("es-MX")).ToString();
        }


        Bnet.Next.Collections.Hashtable ht = new Bnet.Next.Collections.Hashtable();

        
        ht.Add("@created_min@", Convert.ToDateTime(dteInit).ToString("yyyy/MM/dd 00:00:00"), "True");
        ht.Add("@created_max@", Convert.ToDateTime(dteEnd).ToString("yyyy/MM/dd 23:59:59"), "True");
        if (customerid != "")
            ht.Add("@customer_id@", customerid, "False");
        ht.Add("@status@", status, "False");
        ht.Add("@type@", type, "True");

        DataHelper.BxsqlFile = HttpContext.Current.Server.MapPath("~/bxsql/Bnet.Suite.Core.Operations.CrmQuotationOperation.bxsql");
        DataHelper.LoadBxsql();

        DataHelper.Connect();
        DataHelper.ExecuteCommand("searchGuides", ht);
        
        string[] row = new string[10];
        object[] rows = new object[0];
        if (DataHelper.exception.Message == "No Exception")
        {
            int rw = 0;
            rows = new object[DataHelper.DataTable.Rows.Count];
            foreach (System.Data.DataRow dr in DataHelper.DataTable.Rows)
            {
                
                string img="1.png";
                row = new string[10];
                row[0] = dr["subcustomer_guide_id"].ToString();
                row[1] = "<div class=\"kt-user-card-v2\">" +
                            "<div class=\"kt-user-card-v2__pic\">" +
                                "<img src=\"assets/media/client-logos/logo" + img + "\" alt=\"photo\">" +
                            "</div>" +
                            "<div class=\"kt-user-card-v2__details\">" +
                                "<a href=\"javascript:;\" class=\"kt-user-card-v2__name\">" + dr["name"] + "&nbsp;</a>" +
                                "<span class=\"kt-user-card-v2__email\">" +
                                dr["contact"] + "</span>" +
                            "</div>" +
                        "</div>";
                row[2] = dr["custname"].ToString(); ;
                row[3] = dr["guide"].ToString();
                row[4] = Convert.ToDateTime(dr["created"]).ToString("dd-MMM-yy");
                if (!string.IsNullOrEmpty(dr["recolection_date"].ToString()))
                    row[5] = Convert.ToDateTime(dr["recolection_date"]).ToString("dd-MMM-yy");
                else
                    row[5] = "";
                row[6] = dr["packtype"].ToString();

                string color = "success";
                string stat = "Creada";
                string complpath = "";


                row[7] = dr["destination_type"].ToString();

                    string strAction = "Imprimir";
                    string linkAction = "onclick=\"downloadRecOrder(" + dr["subcustomer_guide_id"] + "); return false;\"";
                    string xmlpath = "";
                     
                    string actions="";
                    string style = "style=\"display:none\"";

                    if (sessionType == "1" || sessionType == "2" || sessionType == "3" || sessionType == "")
                    {
                        if (sessionType == "1")
                            style = "";
                        actions = "<div class=\"dropdown\">" +
                                            "<a href=\"javascript:;\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md kt-link\" data-toggle=\"dropdown\" >" +
                                            "<i class=\"fa fa-check\"></i></a>" +
                                                "<div class=\"dropdown-menu dropdown-menu-right\">" +
                                                    "<ul class=\"kt-nav\">" +
                                                        "<li class=\"kt-nav__item\">" +
                                                            "<a href=\"javascript:;\" class=\"kt-nav__link\" " + linkAction + " >" +
                                                            "<i class=\"kt-nav__link-icon flaticon2-expand\"></i>" +
                                                            "<span class=\"kt-nav__link-text\">" + strAction + "</span>" +
                                                            "</a>" +
                                                        "</li>" +
                                                    "</ul>" +
                                                "</div>" +
                                        "</div>";
                    }

                    row[8] = dr["extra_info"].ToString();
                    row[9] = "<div class=\"actions\"> " +
                                         xmlpath +
                                         complpath +
                                         actions +
                           "</div></td>"+
                           "</tr>";
                
                //row[6] = "<div class=\"actions\"> " +
                //    "<a href=\"/MediaUploader/" + dr["uuid"] + ".xml\" download=\"" + dr["uuid"] + ".xml\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md\" data-toggle=\"kt-tooltip\" data-container=\"body\" data-placement=\"top\" title=\"Descargar XML\">" +
                //                            "XML</a>" +
                //                            "<a href=\"/MediaUploader/" + dr["uuid"] + ".pdf\" download=\"" + dr["uuid"] + ".pdf\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md\" data-toggle=\"kt-tooltip\" data-container=\"body\" data-placement=\"top\" title=\"Descargar PDF\">" +
                //                            "PDF</a>" +
                //                            "<a href=\"javascript:;\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md\" data-toggle=\"kt-tooltip\" data-placement=\"top\" title=\"Cambiar Status\" onclick=\"closeQuote(" + dr["pch_invoice_id"] + "); return false;\" >" +
                //                            "<i class=\"fa fa-check\"></i></a>" +
                //                        "</div></td>" +
                //                "</tr>";

                rows[rw] = new object();
                rows[rw] = row;
                rw++;
            }

        }
         
        /*
        System.Data.DataTable dtrows = new System.Data.DataTable() ;
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        if (DataHelper.exception.Message == "No Exception")
        {
            dtrows = DataHelper.DataTable;
            Dictionary<string, object> row;
            foreach (System.Data.DataRow dr in dtrows.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (System.Data.DataColumn col in dtrows.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

        }
        */
        DataHelper.Disconnect();

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;
        string serData = serializer.Serialize(rows);
        return serData;
    }

    [WebMethod(EnableSession = true)]
    public static string GetCustomers()//Bnet.Next.Collections.Hashtable ht)
    {
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        //string dteInit = GetVal(form, "fecha_inicio");

        DataHelper.BxsqlFile = HttpContext.Current.Server.MapPath("~/bxsql/Bnet.Suite.Core.Operations.InvoiceOperation.bxsql");
        DataHelper.LoadBxsql();

        Bnet.Next.Collections.Hashtable ht = new Bnet.Next.Collections.Hashtable();

        string customerid = HttpContext.Current.Session["customerid"].ToString();
        ht.Add("@customer_id@", customerid, "False");
        ht.Add("@code@", "1=1", "False");
        DataHelper.Connect();
        DataHelper.ExecuteCommand("loadSubCustomers", ht);


        string[] row = new string[9];
        object[] rows = new object[0];
        if (DataHelper.exception.Message == "No Exception")
        {
            int rw = 0;
            rows = new object[DataHelper.DataTable.Rows.Count];
            foreach (System.Data.DataRow dr in DataHelper.DataTable.Rows)
            {

                string img = "1.png";
                row = new string[9];
                row[0] = dr["subcustomer_id"].ToString();
                row[1] = dr["code"].ToString();
                row[2] = dr["name"].ToString();
                row[3] = dr["address"].ToString();
                row[4] = dr["city"].ToString();
                row[5] = dr["state"].ToString();
                row[6] = dr["zipcode"].ToString();
                row[7] = "<a href=\"javascript:;\" onclick=getSubCustomer('"+dr["code"]+"');><i class=\"fa fa-check-square fa-2x\"></i></a>";
                

                rows[rw] = new object();
                rows[rw] = row;
                rw++;
            }

        }

        DataHelper.Disconnect();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string serData = serializer.Serialize(rows);
        return serData;
    }


    [WebMethod(EnableSession = true)]
    public static string GetCustomer(string customerID)
    {
        Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        
        string result = "";
        dh.BxsqlFile = HttpContext.Current.Server.MapPath("~/bxsql/Bnet.Suite.Core.Operations.InvoiceOperation.bxsql");
        dh.LoadBxsql();

        Bnet.Next.Collections.Hashtable ht = new Bnet.Next.Collections.Hashtable();


        ht.Add("@customer_id@", customerID, "False");
        dh.Connect();
        dh.ExecuteCommand("loadCustomers", ht);

        if (dh.exception.Message == "No Exception")
        {
            if (dh.Next())
            {
                result = "{\"response\":\"success\"";
                
                if (dh.DataTable.Rows[0]["legal_name"].ToString() == "")
                    result += ",\"name\":\"" + dh.DataTable.Rows[0]["name"] + "\"";
                else
                    result += ",\"name\":\"" + dh.DataTable.Rows[0]["legal_name"] + "\"";
                result += ",\"rfc\":\"" + dh.DataTable.Rows[0]["legal_code"].ToString() +"\"";
                //this.mskTxtAddress.Text = dh.DataTable.Rows[0]["address"].ToString();
                result += ",\"street\":\"" + dh.DataTable.Rows[0]["street"].ToString() +"\"";
                result += ",\"no_ext\":\"" + dh.DataTable.Rows[0]["no_ext"].ToString() +"\"";
                result += ",\"no_int\":\"" + dh.DataTable.Rows[0]["no_int"].ToString() +"\"";
                result += ",\"neighborhood\":\"" + dh.DataTable.Rows[0]["neigh"].ToString() +"\"";
                result += ",\"city\":\"" + dh.DataTable.Rows[0]["city_id"].ToString() +"\"";
                result += ",\"state\":\"" + dh.DataTable.Rows[0]["state_id"].ToString() +"\"";
                result += ",\"postal_code\":\"" + dh.DataTable.Rows[0]["postal_code1"].ToString() +"\"";

                result += ",\"customer_id\":\"" + dh.DataTable.Rows[0]["customer_id"].ToString() +"\"";
                result += ",\"emails\":\"" + dh.DataTable.Rows[0]["emails"].ToString() +"\"";
                //if (dh.DataTable.Rows[0]["credit_days"] != DBNull.Value)
                 //   this.mskTxtCreditDays.Text = dh.DataTable.Rows[0]["credit_days"].ToString();
                //else
                //    this.mskTxtCreditDays.Text = "0";

                if (dh.DataTable.Rows[0]["pay_method"].ToString() == "")
                    result += ",\"pay_method\":\"-1\"";
                else
                {
                    result += ",\"pay_method\":\"" + dh.DataTable.Rows[0]["pay_method"].ToString() + "\"";
                }
                //if (dh.DataTable.Rows[0]["acc_number"].ToString() == "")
                //    this.mskAccNumber.Text = "NO IDENTIFICADO";
                //else
                //    this.mskAccNumber.Text = dh.DataTable.Rows[0]["acc_number"].ToString();

                 result += ",\"pay_method_cond\":\"" + dh.DataTable.Rows[0]["pay_method_cond"].ToString() + "\"";
                result += ",\"cfdi_usage\":\"" + dh.DataTable.Rows[0]["cfdi_usage_id"].ToString() + "\"";
                result+="}";
            }
            else
                result = "{\"response\":\"error\",\"message\":\"No se pudo traer de la base de datos\",\"error\":\"No se encontró el registro\"}";
        }
        else
            result = "{\"response\":\"error\",\"message\":\"No se pudo traer de la base de datos\",\"error\":\"" +  General.parseJSON(dh.exception.Message) + "\"}";
        
            


        dh.Disconnect();

        return result;



    }

    [WebMethod(EnableSession = true)]
    public static string GetSubCustomer(string code)
    {
        Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;


        string result = "";
        dh.BxsqlFile = HttpContext.Current.Server.MapPath("~/bxsql/Bnet.Suite.Core.Operations.InvoiceOperation.bxsql");
        dh.LoadBxsql();

        Bnet.Next.Collections.Hashtable ht = new Bnet.Next.Collections.Hashtable();

        string customerid = HttpContext.Current.Session["customerid"].ToString();
        ht.Add("@customer_id@", customerid, "False");
        ht.Add("@code@", "code ilike '"+code+"'", "False");
        dh.Connect();
        dh.ExecuteCommand("loadSubCustomers", ht);

        if (dh.exception.Message == "No Exception")
        {
            if (dh.Next())
            {
                result = "{\"response\":\"success\"";

                result += ",\"name\":\"" + dh.DataTable.Rows[0]["name"] + "\"";
                result += ",\"code\":\"" + dh.DataTable.Rows[0]["code"].ToString() + "\"";
                //this.mskTxtAddress.Text = dh.DataTable.Rows[0]["address"].ToString();
                result += ",\"address\":\"" + dh.DataTable.Rows[0]["address"].ToString() + "\"";
                result += ",\"neighborhood\":\"" + dh.DataTable.Rows[0]["neighborhood"].ToString() + "\"";
                result += ",\"city\":\"" + dh.DataTable.Rows[0]["city"].ToString() + "\"";
                result += ",\"state\":\"" + dh.DataTable.Rows[0]["state"].ToString() + "\"";
                result += ",\"postal_code\":\"" + dh.DataTable.Rows[0]["zipcode"].ToString() + "\"";

                result += ",\"contact\":\"" + dh.DataTable.Rows[0]["contact"].ToString() + "\"";
                result += ",\"phone\":\"" + dh.DataTable.Rows[0]["phone"].ToString() + "\"";
              
                result += "}";
            }
            else
                result = "{\"response\":\"warning\",\"message\":\"No se pudo traer de la base de datos\",\"error\":\"No se encontró el registro\"}";
        }
        else
            result = "{\"response\":\"error\",\"message\":\"No se pudo traer de la base de datos\",\"error\":\"" + General.parseJSON(dh.exception.Message) + "\"}";




        dh.Disconnect();

        return result;



    }


    [WebMethod(EnableSession = true)]
    public static string GetLastGuide(string customerID,string auth)
    {
        Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        dh.BxsqlFile = HttpContext.Current.Server.MapPath("~/bxsql/Bnet.Suite.Core.Operations.InvoiceOperation.bxsql");
        dh.LoadBxsql();

        Bnet.Next.Collections.Hashtable ht = new Bnet.Next.Collections.Hashtable();
       
        string result = "";
        
        dh.Connect();
        if(customerID=="-1")
            ht.Add("@customer_id@", HttpContext.Current.Session["customerid"], "False");
        else
            ht.Add("@customer_id@", customerID, "False");
        if(auth=="True")
            ht.Add("@authorized@", "AND authorized_by is not null", "False");
        else
            ht.Add("@authorized@", "", "False");
        dh.ExecuteCommand("getLastGuide", ht);
        
        if (dh.exception.Message == "No Exception")
        {
            if (dh.Next())
            {
                result = "{\"response\":\"success\"";


                result += ",\"range\":\"" + dh.DataTable.Rows[0]["min_guide"] + "-" + dh.DataTable.Rows[0]["max_guide"] + "\"";
                result += ",\"lastused\":\"" + dh.DataTable.Rows[0]["lastused"].ToString() + "\"";
                result += ",\"available\":\"" + (Convert.ToInt32(dh.DataTable.Rows[0]["max_guide"])-Convert.ToInt32(dh.DataTable.Rows[0]["lastused"])) + "\"";
                result += ",\"customer\":\"" + dh.DataTable.Rows[0]["customer"].ToString() + "\"";
                result += ",\"contact\":\"" + dh.DataTable.Rows[0]["contact"].ToString() + "\"";
                result += ",\"address\":\"" + dh.DataTable.Rows[0]["address1"].ToString() + "\"";
                result += ",\"neighborhood\":\"" + dh.DataTable.Rows[0]["neighborhood"].ToString() + "\"";
                result += ",\"city\":\"" + dh.DataTable.Rows[0]["city"].ToString() + "\"";
                result += ",\"state\":\"" + dh.DataTable.Rows[0]["state"].ToString() + "\"";
                result += ",\"phone\":\"" + dh.DataTable.Rows[0]["phones"].ToString() + "\"";
                result += ",\"zipcode\":\"" + dh.DataTable.Rows[0]["postal_code"].ToString() + "\"";
                result += ",\"schedule\":\"" + dh.DataTable.Rows[0]["schedule"].ToString() + "\"";
                result += ",\"reference\":\"" + dh.DataTable.Rows[0]["address_reference"].ToString() + "\"";
                result += ",\"auth\":\"" + auth + "\"";
                result += "}";
            }
            else
                result = "{\"response\":\"success\",\"range\":\"0\",\"lastused\":\"0\",\"available\":\"0\",\"auth\":\"" + auth + "\"}";
        }
        else
            result = "{\"response\":\"error\",\"message\":\"No se pudo traer de la base de datos\",\"error\":\"" + General.parseJSON(dh.exception.Message) + "\"}";




        dh.Disconnect();

        return result;



    }

    [WebMethod(EnableSession = true)]
    public static string GetSaleInvoices(Form[] form)//Bnet.Next.Collections.Hashtable ht)
    {
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;


        //string supplierid = HttpContext.Current.Session["supplierid"].ToString();
        string sessionType = "" + HttpContext.Current.Session["type"];



        string dteInit = GetVal(form, "start");
        string dteEnd = GetVal(form, "end");
        string type = GetVal(form, "cmbInvType");
        string status = GetVal(form, "cmbInvStatus");
        string customer = GetVal(form, "customer");
        string code = GetVal(form, "code");
        string complement = GetVal(form, "cmbInvComplement");
        string rfc = GetVal(form, "rfc");

        bool nofilter = true;

        if (type != "" || status != "" || customer != "" || code != "" || complement != "" || rfc != "")
            nofilter = false;

        Bnet.Next.Collections.Hashtable ht = new Bnet.Next.Collections.Hashtable();

        if (string.IsNullOrEmpty(dteInit))
        {
            dteInit = DateTime.Now.AddMonths(-1).ToString();
            dteEnd = DateTime.Now.ToString();
            if (nofilter)
            {
                ht.Add("@created_min@", Convert.ToDateTime(dteInit).ToString("yyyy/MM/dd 00:00:00"), "True");
                ht.Add("@created_max@", Convert.ToDateTime(dteEnd).ToString("yyyy/MM/dd 23:59:59"), "True");
            }
        }
        else
        {
            dteInit = Convert.ToDateTime(dteInit, CultureInfo.GetCultureInfo("es-MX")).ToString();
            dteEnd = Convert.ToDateTime(dteEnd, CultureInfo.GetCultureInfo("es-MX")).ToString();
            ht.Add("@created_min@", Convert.ToDateTime(dteInit).ToString("yyyy/MM/dd 00:00:00"), "True");
            ht.Add("@created_max@", Convert.ToDateTime(dteEnd).ToString("yyyy/MM/dd 23:59:59"), "True");
        }

        //if (supplierid != "")
        //    ht.Add("@customer_id@", supplierid, "False");
        ht.Add("@status@", status, "False");
        if (rfc != "")
        ht.Add("@legal_code@", "%" + rfc + "%", "True");
        ht.Add("@code@", code, "True");
        if(customer!="")
            ht.Add("@name@","%"+ customer+"%", "True");
        ht.Add("@type@", type, "True");
        if (complement == "4")
            ht.Add("@complement@", "(4)", "False");
        if (complement == "0")
            ht.Add("@complement@", "(1,2,3)", "False");
        ht.Add("@remark@", "", "False");
        ht.Add("@remarkjoin@", "", "False");

        DataHelper.BxsqlFile = HttpContext.Current.Server.MapPath("~/bxsql/Bnet.Suite.Core.Operations.InvoiceOperation.bxsql");
        DataHelper.LoadBxsql();

        DataHelper.Connect();
        DataHelper.ExecuteCommand("findInvoicesByOrder", ht);

        string[] row = new string[10];
        object[] rows = new object[0];
        if (DataHelper.exception.Message == "No Exception")
        {
            int rw = 0;
            rows = new object[DataHelper.DataTable.Rows.Count];
            foreach (System.Data.DataRow dr in DataHelper.DataTable.Rows)
            {

                string img = "1.png";
                row = new string[10];
                row[0] = dr["sale_invoice_id"].ToString();
                row[1] = "<a class=\"kt-link\" href=\"javascript:;\" onclick=\"getInvoiceLines(" + dr["sale_invoice_id"] + ");\">" + dr["serie"] + "-" + dr["code"].ToString() + "</a>";
                row[2] = "<div class=\"kt-user-card-v2\">" +
                            "<div class=\"kt-user-card-v2__pic\">" +
                                "<img src=\"assets/media/client-logos/logo" + img + "\" alt=\"photo\">" +
                            "</div>" +
                            "<div class=\"kt-user-card-v2__details\">" +
                                "<a href=\"javascript:;\" onclick=\"getInvoiceLines("+dr["sale_invoice_id"]+")\" class=\"kt-user-card-v2__name kt-link\">" + dr["name"] + "</a>" +
                                "<span class=\"kt-user-card-v2__email\">" +
                                dr["legal_code"] + "</span>" +
                            "</div>" +
                        "</div>";
                row[3] = dr["typ"].ToString();
                row[4] = "<a class=\"kt-link\" href=\"javascript:;\" onclick=\"getInvoiceLines(" + dr["sale_invoice_id"] + ");\">" + dr["street"] + " " + dr["no_ext"] + " " + dr["neighborhood"].ToString() + " " + dr["postal_code"] + "</a>";
                row[5] = Convert.ToDateTime(dr["date_invoiced"]).ToString("dd-MMM-yy");
                if (!string.IsNullOrEmpty(dr["expiration_date"].ToString()))
                    row[6] = Convert.ToDateTime(dr["expiration_date"]).ToString("dd-MMM-yy");
                else
                    row[6] = "";
                row[7] = Convert.ToDouble(dr["grand_total"]).ToString("c");

                string color = "brand";
                string stat = "Pagada";
                string complpath = "";
                string cancelAction = "<li class=\"kt-nav__item\">" +
                                           "<a href=\"javascript:;\" onclick=\"cancelSaleInvoice(" + dr["sale_invoice_id"] + ");return false;\" class=\"kt-nav__link\" \" >" +
                                           "<i class=\"kt-nav__link-icon flaticon-cancel\"></i>" +
                                           "<span class=\"kt-nav__link-text\">Cancelar Factura</span>" +
                                           "</a>" +
                                       "</li>";

                if (dr["statid"].ToString() == "5")
                {
                    color = "danger";
                    stat = "Cancelada";
                    cancelAction = "";
                }
                if (dr["status"].ToString() == "2")
                {
                    color = "warning";
                    stat = "Validada";
                }
                if (dr["status"].ToString() == "3")
                {
                    color = "info";
                    stat = "Programada";
                }
                if (dr["statid"].ToString() == "4")
                {
                    color = "success";
                    complpath = "<a href=\"/CFD/" + dr["uuid"] + ".xml\" download=\"" + dr["uuid"] + ".xml\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md kt-link\" data-toggle=\"kt-tooltip\" data-container=\"body\" data-placement=\"top\" title=\"Descargar Complemento Pago\">" +
                                         "PAGO</a>";
                }

                row[8] = "<span class=\"btn btn-bold btn-sm btn-label-"+color+"\">" +
                            dr["status"] + "</span>";

                string strAction = "Autorizar";
                string linkAction = "onclick=\"updateSaleInvoice(" + dr["sale_invoice_id"] + "); return false;\"";
                string xmlpath = "<a href=\"/CFD/" + dr["serie"] + "-" + dr["code"] + ".xml\" download=\"" + dr["serie"] + "-" + dr["code"] + ".xml\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md kt-link\" data-toggle=\"kt-tooltip\" data-container=\"body\" data-placement=\"top\" title=\"Descargar XML\">" +
                                     "XML</a>";

                if (dr["status"].ToString() == "2")
                {
                    strAction = "Programar Fecha";
                    linkAction = "data-toggle=\"modal\" data-target=\"#InvActions\" onclick=\"setpInv(" + dr["pch_invoice_id"] + ");\" ";
                }
                if (dr["status"].ToString() == "3")
                {
                    strAction = "Marcar como Pagada";
                }
                if (dr["status"].ToString() == "4")
                {
                    strAction = "";
                }

                string actions = "";

                if (sessionType == "1" || sessionType == "2" || sessionType == "3")
                {
                    actions = "<div class=\"dropdown\">" +
                                        "<a href=\"javascript:;\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md kt-link\" data-toggle=\"dropdown\" >" +
                                        "<i class=\"fa fa-check\"></i></a>" +
                                            "<div class=\"dropdown-menu dropdown-menu-right\">" +
                                                "<ul class=\"kt-nav\">" +
                                                    "<li class=\"kt-nav__item\">" +
                                                        "<a href=\"Default.aspx?actn=minv&loadId="+dr["sale_invoice_id"]+"\" class=\"kt-nav__link\" \" >" +
                                                        "<i class=\"kt-nav__link-icon flaticon-interface-11\"></i>" +
                                                        "<span class=\"kt-nav__link-text\">Cargar Factura</span>" +
                                                        "</a>" +
                                                    "</li>" +
                                                    cancelAction+
                                                "</ul>" +
                                            "</div>" +
                                    "</div>";
                }

                row[9] = "<div class=\"actions\"> " +
                                     xmlpath +
                                     "<a href=\"/CFD/" + dr["serie"] + "-" + dr["code"] + ".pdf\" download=\"" + dr["serie"] + "-" + dr["code"] + ".pdf\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md kt-link\" data-toggle=\"kt-tooltip\" data-container=\"body\" data-placement=\"top\" title=\"Descargar PDF\">" +
                                     "PDF</a>" +
                                     complpath +
                                     actions +
                       "</div></td>" +
                       "</tr>";

                rows[rw] = new object();
                rows[rw] = row;
                rw++;
            }

        }

        /*
        System.Data.DataTable dtrows = new System.Data.DataTable() ;
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        if (DataHelper.exception.Message == "No Exception")
        {
            dtrows = DataHelper.DataTable;
            Dictionary<string, object> row;
            foreach (System.Data.DataRow dr in dtrows.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (System.Data.DataColumn col in dtrows.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

        }
        */
        DataHelper.Disconnect();

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;
        string serData = serializer.Serialize(rows);
        return serData;
    }

    [WebMethod(EnableSession = true)]
    public static string GetSaleInvoice(string invoiceID)//Bnet.Next.Collections.Hashtable ht)
    {
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        string sessionType = "" + HttpContext.Current.Session["type"];

        Bnet.Next.Collections.Hashtable ht = new Bnet.Next.Collections.Hashtable();


        ht.Add("@sale_invoice_id@",invoiceID, "False");
        
        ht.Add("@remark@", "", "False");
        ht.Add("@remarkjoin@", "", "False");

        DataHelper.BxsqlFile = HttpContext.Current.Server.MapPath("~/bxsql/Bnet.Suite.Core.Operations.InvoiceOperation.bxsql");
        DataHelper.LoadBxsql();

        DataHelper.Connect();
        DataHelper.ExecuteCommand("findInvoicesByOrder", ht);

       
        string pdfFile = "";
        string xmlFile = "";
        string path = HttpContext.Current.Request.ApplicationPath;
        path = HttpContext.Current.Request.ApplicationPath;
        if (path.EndsWith("/"))
            path += "CFD/";
        else
            path += "/CFD/";
        if (DataHelper.exception.Message == "No Exception")
        {
            xmlFile =  DataHelper.DataTable.Rows[0]["serie"] + "-" + DataHelper.DataTable.Rows[0]["code"] + ".xml";
            pdfFile =  DataHelper.DataTable.Rows[0]["serie"] + "-" + DataHelper.DataTable.Rows[0]["code"] + ".pdf";
        }

        DataHelper.Disconnect();

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;
        string serData = JsonConvert.SerializeObject(DataHelper.DataTable);//serializer.Serialize(rows);
        serData="{\"saleInvoice\":"+serData.Replace("[", "").Replace("]", "");
        string sInvLines = GetInvoiceLines(invoiceID);
        serData += ",\"saleInvoiceLines\":" + sInvLines + ",\"path\":\"" + path + "\",\"xmlFile\":\"" + xmlFile + "\",\"pdfFile\":\"" + pdfFile + "\"}";
        return serData;
    }

    [WebMethod]
    public static string GetInvoiceLines(string invoiceID)//Bnet.Next.Collections.Hashtable ht)
    {
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        //string dteInit = GetVal(form, "fecha_inicio");

        DataHelper.BxsqlFile = HttpContext.Current.Server.MapPath("~/bxsql/Bnet.Suite.Core.Operations.InvoiceOperation.bxsql");
        DataHelper.LoadBxsql();

        Bnet.Next.Collections.Hashtable ht = new Bnet.Next.Collections.Hashtable();


        ht.Add("@sale_invoice_id@", invoiceID, "False");
        DataHelper.Connect();
        DataHelper.ExecuteCommand("findInvoiceLines", ht);


        string[] row = new string[10];
        object[] rows = new object[0];
        if (DataHelper.exception.Message == "No Exception")
        {
            int rw = 0;
            rows = new object[DataHelper.DataTable.Rows.Count];
            foreach (System.Data.DataRow dr in DataHelper.DataTable.Rows)
            {

                string img = "1.png";
                row = new string[13];
                row[0] = dr["code"].ToString();
                row[1] = dr["qty_required"].ToString();
                row[2] = dr["product_name"].ToString();
                row[3] = dr["price_u"].ToString();
                row[4] = dr["total_sale"].ToString();
                row[5] = dr["unit"].ToString();
                row[6] = dr["sat_code"].ToString();
                row[7] = dr["cve_unit"].ToString();
                row[8] = Convert.ToDouble(dr["tax_percent"]).ToString();
                row[9] = dr["discount_amt"].ToString();

                rows[rw] = new object();
                rows[rw] = row;
                rw++;
            }

        }

        DataHelper.Disconnect();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string serData = serializer.Serialize(rows);
        return serData;
    }

    [WebMethod(EnableSession = true)]
    public static string GetReceivableAccounts(Form[] form)//Bnet.Next.Collections.Hashtable ht)
    {
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;


        //string supplierid = HttpContext.Current.Session["supplierid"].ToString();
        string sessionType = "" + HttpContext.Current.Session["type"];



        string dteInit = GetVal(form, "start");
        string dteEnd = GetVal(form, "end");
        string type = GetVal(form, "cmbInvType");
        string status = GetVal(form, "cmbInvStatus");
        string customer = GetVal(form, "customer");
        string code = GetVal(form, "code");
        string complement = GetVal(form, "cmbInvComplement");
        string rfc = GetVal(form, "rfc");

        bool nofilter = true;

        if (type != "" || status != "" || customer != "" || code != "" || complement != "" || rfc != "")
            nofilter = false;

        Bnet.Next.Collections.Hashtable ht = new Bnet.Next.Collections.Hashtable();

        if (string.IsNullOrEmpty(dteInit))
        {
            dteInit = DateTime.Now.AddMonths(-1).ToString();
            dteEnd = DateTime.Now.ToString();
            if (nofilter)
            {
                ht.Add("@created_min@", Convert.ToDateTime(dteInit).ToString("yyyy/MM/dd 00:00:00"), "True");
                ht.Add("@created_max@", Convert.ToDateTime(dteEnd).ToString("yyyy/MM/dd 23:59:59"), "True");
            }
        }
        else
        {
            dteInit = Convert.ToDateTime(dteInit, CultureInfo.GetCultureInfo("es-MX")).ToString();
            dteEnd = Convert.ToDateTime(dteEnd, CultureInfo.GetCultureInfo("es-MX")).ToString();
            ht.Add("@created_min@", Convert.ToDateTime(dteInit).ToString("yyyy/MM/dd 00:00:00"), "True");
            ht.Add("@created_max@", Convert.ToDateTime(dteEnd).ToString("yyyy/MM/dd 23:59:59"), "True");
        }

        //if (supplierid != "")
        //    ht.Add("@customer_id@", supplierid, "False");
        ht.Add("@status@", status, "False");
        if (rfc != "")
            ht.Add("@legal_code@", "%" + rfc + "%", "True");
        ht.Add("@code@", code, "True");
        if (customer != "")
            ht.Add("@name@", "%" + customer + "%", "True");
        ht.Add("@type@", type, "True");
        if (complement == "4")
            ht.Add("@complement@", "(4)", "False");
        if (complement == "0")
            ht.Add("@complement@", "(1,2,3)", "False");
        ht.Add("@remark@", "", "False");
        ht.Add("@remarkjoin@", "", "False");

        DataHelper.BxsqlFile = HttpContext.Current.Server.MapPath("~/bxsql/Bnet.Suite.Core.Operations.SalePaymentOperation.bxsql");
        DataHelper.LoadBxsql();

        DataHelper.Connect();
        DataHelper.ExecuteCommand("searchReceivableAccounts", ht);

        string[] row = new string[10];
        object[] rows = new object[0];
        if (DataHelper.exception.Message == "No Exception")
        {
            int rw = 0;
            rows = new object[DataHelper.DataTable.Rows.Count];
            foreach (System.Data.DataRow dr in DataHelper.DataTable.Rows)
            {

                string img = "1.png";
                row = new string[10];
                row[0] = dr["doc_id"].ToString();
                row[1] = dr["serie"] + "-" + dr["code"].ToString();
                row[2] = "<div class=\"kt-user-card-v2\">" +
                            "<div class=\"kt-user-card-v2__pic\">" +
                                "<img src=\"assets/media/client-logos/logo" + img + "\" alt=\"photo\">" +
                            "</div>" +
                            "<div class=\"kt-user-card-v2__details\">" +
                                "<a href=\"javascript:;\" onclick=\"getInvoiceLines(" + dr["doc_id"] + ")\" class=\"kt-user-card-v2__name kt-link\">" + dr["customer_name"] + "</a>" +
                                "<span class=\"kt-user-card-v2__email\">" +
                                dr["invoice_doc_code"] + "</span>" +
                            "</div>" +
                        "</div>";
                row[3] = dr["type_name"].ToString();
                row[4] = "";
                row[5] = Convert.ToDateTime(dr["invoiced"]).ToString("dd-MMM-yy");
                if (!string.IsNullOrEmpty(dr["expiration_date"].ToString()))
                    row[6] = Convert.ToDateTime(dr["expiration_date"]).ToString("dd-MMM-yy");
                else
                    row[6] = "";
                row[7] = Convert.ToDouble(dr["grand_total"]).ToString("c");

                string color = "brand";
                string stat = "Pagada";
                string complpath = "";
                string cancelAction = "<li class=\"kt-nav__item\">" +
                                           "<a href=\"javascript:;\" data-toggle=\"modal\" data-target=\"#saleinvpay_modal\"  onclick=\"getSalePayDocs(" + dr["customer_id"] + ");return false;\" class=\"kt-nav__link\" \" >" +
                                           "<i class=\"kt-nav__link-icon fa fa-money-check-alt\"></i>" +
                                           "<span class=\"kt-nav__link-text\">Cobrar Factura</span>" +
                                           "</a>" +
                                       "</li>";

                //if (dr["statid"].ToString() == "5")
                //{
                //    color = "danger";
                //    stat = "Cancelada";
                //    cancelAction = "";
                //}
                //if (dr["status"].ToString() == "2")
                //{
                //    color = "warning";
                //    stat = "Validada";
                //}
                //if (dr["status"].ToString() == "3")
                //{
                //    color = "info";
                //    stat = "Programada";
                //}
                //if (dr["statid"].ToString() == "4")
                //{
                //    color = "success";
                //    complpath = "<a href=\"/CFD/" + dr["uuid"] + ".xml\" download=\"" + dr["uuid"] + ".xml\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md kt-link\" data-toggle=\"kt-tooltip\" data-container=\"body\" data-placement=\"top\" title=\"Descargar Complemento Pago\">" +
                //                         "PAGO</a>";
                //}

                row[8] = Convert.ToDouble(dr["balance"]).ToString("c");

                string strAction = "Autorizar";
                string linkAction = "onclick=\"updateSaleInvoice(" + dr["doc_id"] + "); return false;\"";
                string xmlpath = "<a href=\"/CFD/" + dr["serie"] + "-" + dr["code"] + ".xml\" download=\"" + dr["serie"] + "-"+ dr["code"] + ".xml\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md kt-link\" data-toggle=\"kt-tooltip\" data-container=\"body\" data-placement=\"top\" title=\"Descargar XML\">" +
                                     "XML</a>";

                //if (dr["status"].ToString() == "2")
                //{
                //    strAction = "Programar Fecha";
                //    linkAction = "data-toggle=\"modal\" data-target=\"#InvActions\" onclick=\"setpInv(" + dr["pch_invoice_id"] + ");\" ";
                //}
                //if (dr["status"].ToString() == "3")
                //{
                //    strAction = "Marcar como Pagada";
                //}
                //if (dr["status"].ToString() == "4")
                //{
                //    strAction = "";
                //}

                string actions = "";

                if (sessionType == "1" || sessionType == "2" || sessionType == "3")
                {
                    actions = "<div class=\"dropdown\">" +
                                        "<a href=\"javascript:;\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md kt-link\" data-toggle=\"dropdown\" >" +
                                        "<i class=\"fa fa-check\"></i></a>" +
                                            "<div class=\"dropdown-menu dropdown-menu-right\">" +
                                                "<ul class=\"kt-nav\">" +
                                                    "<li class=\"kt-nav__item\">" +
                                                        "<a href=\"Default.aspx?actn=minv&loadId=" + dr["doc_id"] + "\" class=\"kt-nav__link\" \" >" +
                                                        "<i class=\"kt-nav__link-icon flaticon-interface-11\"></i>" +
                                                        "<span class=\"kt-nav__link-text\">Cargar Factura</span>" +
                                                        "</a>" +
                                                    "</li>" +
                                                    cancelAction +
                                                "</ul>" +
                                            "</div>" +
                                    "</div>";
                }

                row[9] = "<div class=\"actions\"> " +
                                     xmlpath +
                                     "<a href=\"/CFD/" + dr["serie"] + "-" + dr["code"] + ".pdf\" download=\"" + dr["serie"] + "-" + dr["code"] + ".pdf\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md kt-link\" data-toggle=\"kt-tooltip\" data-container=\"body\" data-placement=\"top\" title=\"Descargar PDF\">" +
                                     "PDF</a>" +
                                     complpath +
                                     actions +
                       "</div></td>" +
                       "</tr>";

                rows[rw] = new object();
                rows[rw] = row;
                rw++;
            }

        }
        string[] rowt = new string[10];
        object[] rowst = new object[0]; 
        DataHelper.ExecuteCommand("searchTotalReceivableAccounts", ht);
        if (DataHelper.exception.Message == "No Exception")
        {
           

            int rw = 0;
            rowst = new object[DataHelper.DataTable.Rows.Count];
            foreach (System.Data.DataRow dr in DataHelper.DataTable.Rows)
            {

                rowt = new string[6];

                rowt[0] = dr["line"].ToString();
                rowt[1] = dr["total_lines"].ToString();
                rowt[2] = Convert.ToDouble(dr["grand_total"]).ToString("c");
                rowt[3] = Convert.ToDouble(dr["paid_amt"]).ToString("c");
                rowt[4] = Convert.ToDouble(dr["pending"]).ToString("c");
                rowt[5] = dr["currency_code"].ToString();

                rowst[rw] = new object();
                rowst[rw] = rowt;
                rw++;
            }

        }


        DataHelper.Disconnect();

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;
        string serData = "{\"recact\":" + serializer.Serialize(rows) +",\"totalrecact\":"+serializer.Serialize(rowst)+"}";
        return serData;
    }

    [WebMethod(EnableSession = true)]
    public static string GetSalePayments(Form[] form)//Bnet.Next.Collections.Hashtable ht)
    {
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;


        //string supplierid = HttpContext.Current.Session["supplierid"].ToString();
        string sessionType = "" + HttpContext.Current.Session["type"];



        string dteInit = GetVal(form, "start");
        string dteEnd = GetVal(form, "end");
        string type = GetVal(form, "cmbInvType");
        string status = GetVal(form, "cmbInvStatus");
        string customer = GetVal(form, "customer");
        string code = GetVal(form, "code");
        string complement = GetVal(form, "cmbInvComplement");
        string rfc = GetVal(form, "rfc");

        bool nofilter = true;

        if (type != "" || status != "" || customer != "" || code != "" || complement != "" || rfc != "")
            nofilter = false;

        Bnet.Next.Collections.Hashtable ht = new Bnet.Next.Collections.Hashtable();

        if (string.IsNullOrEmpty(dteInit))
        {
            dteInit = DateTime.Now.AddMonths(-1).ToString();
            dteEnd = DateTime.Now.ToString();
            if (nofilter)
            {
                ht.Add("@created_date_min@", Convert.ToDateTime(dteInit).ToString("yyyy/MM/dd 00:00:00"), "True");
                ht.Add("@created_date_max@", Convert.ToDateTime(dteEnd).ToString("yyyy/MM/dd 23:59:59"), "True");
            }
        }
        else
        {
            dteInit = Convert.ToDateTime(dteInit, CultureInfo.GetCultureInfo("es-MX")).ToString();
            dteEnd = Convert.ToDateTime(dteEnd, CultureInfo.GetCultureInfo("es-MX")).ToString();
            ht.Add("@created_date_min@", Convert.ToDateTime(dteInit).ToString("yyyy/MM/dd 00:00:00"), "True");
            ht.Add("@created_date_max@", Convert.ToDateTime(dteEnd).ToString("yyyy/MM/dd 23:59:59"), "True");
        }

        //if (supplierid != "")
        //    ht.Add("@customer_id@", supplierid, "False");
        ht.Add("@status@", status, "False");
        if (rfc != "")
            ht.Add("@legal_code@", "%" + rfc + "%", "True");
        ht.Add("@code@", code, "True");
        if (customer != "")
            ht.Add("@name@", "%" + customer + "%", "True");
        ht.Add("@type@", type, "True");
        if (complement == "4")
            ht.Add("@complement@", "(4)", "False");
        if (complement == "0")
            ht.Add("@complement@", "(1,2,3)", "False");
        ht.Add("@remark@", "", "False");
        ht.Add("@remarkjoin@", "", "False");

        DataHelper.BxsqlFile = HttpContext.Current.Server.MapPath("~/bxsql/Bnet.Suite.Core.Operations.SalePaymentOperation.bxsql");
        DataHelper.LoadBxsql();

        DataHelper.Connect();
        DataHelper.ExecuteCommand("searchSalePayments", ht);

        string[] row = new string[10];
        object[] rows = new object[0];

        string[] rowt = new string[10];
        object[] rowst = new object[1];
        double subtotalMX = 0;
        double taxMX = 0;
        double totalMX = 0;

        if (DataHelper.exception.Message == "No Exception")
        {
            int rw = 0;
            rows = new object[DataHelper.DataTable.Rows.Count];
            foreach (System.Data.DataRow dr in DataHelper.DataTable.Rows)
            {

                string img = "1.png";
                row = new string[10];
                row[0] = dr["sale_payment_id"].ToString();
                row[1] = dr["code"].ToString();
                row[2] = "<div class=\"kt-user-card-v2\">" +
                            "<div class=\"kt-user-card-v2__pic\">" +
                                "<img src=\"assets/media/client-logos/logo" + img + "\" alt=\"photo\">" +
                            "</div>" +
                            "<div class=\"kt-user-card-v2__details\">" +
                                "<a href=\"javascript:;\" onclick=\"getInvoiceLines(" + dr["sale_payment_id"] + ")\" class=\"kt-user-card-v2__name kt-link\">" + dr["customer_name"] + "</a>" +
                                "<span class=\"kt-user-card-v2__email\">" +
                                dr["description"] + "</span>" +
                            "</div>" +
                        "</div>";
                row[3] = dr["type_name"].ToString();
                row[4] = "";
                row[5] = Convert.ToDateTime(dr["payment_date"]).ToString("dd-MMM-yy");
                row[6] = dr["reference"].ToString();
                row[7] = Convert.ToDouble(dr["grand_total"]).ToString("c");

                string color = "brand";
                string stat = "Pagada";
                string complpath = "";
                string cancelAction = "<li class=\"kt-nav__item\">" +
                                           "<a href=\"javascript:;\" data-toggle=\"modal\" data-target=\"#saleinvpay_modal\"  onclick=\"getSalePayDocs(" + dr["sale_payment_id"] + ");return false;\" class=\"kt-nav__link\" \" >" +
                                           "<i class=\"kt-nav__link-icon fa fa-money-check-alt\"></i>" +
                                           "<span class=\"kt-nav__link-text\">Detalle</span>" +
                                           "</a>" +
                                       "</li>";

                //if (dr["statid"].ToString() == "5")
                //{
                //    color = "danger";
                //    stat = "Cancelada";
                //    cancelAction = "";
                //}
                //if (dr["status"].ToString() == "2")
                //{
                //    color = "warning";
                //    stat = "Validada";
                //}
                //if (dr["status"].ToString() == "3")
                //{
                //    color = "info";
                //    stat = "Programada";
                //}
                //if (dr["statid"].ToString() == "4")
                //{
                //    color = "success";
                //    complpath = "<a href=\"/CFD/" + dr["uuid"] + ".xml\" download=\"" + dr["uuid"] + ".xml\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md kt-link\" data-toggle=\"kt-tooltip\" data-container=\"body\" data-placement=\"top\" title=\"Descargar Complemento Pago\">" +
                //                         "PAGO</a>";
                //}

                row[8] = Convert.ToDouble(dr["exchange_rate"]).ToString("c");

                string strAction = "Autorizar";
                string linkAction = "onclick=\"updateSaleInvoice(" + dr["sale_payment_id"] + "); return false;\"";
                string xmlpath = "<a href=\"/CFD/P-" + dr["sale_payment_id"] + ".xml\" download=\"P-" + dr["sale_payment_id"] + ".xml\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md kt-link\" data-toggle=\"kt-tooltip\" data-container=\"body\" data-placement=\"top\" title=\"Descargar XML\">" +
                                      "XML</a>";

                //if (dr["status"].ToString() == "2")
                //{
                //    strAction = "Programar Fecha";
                //    linkAction = "data-toggle=\"modal\" data-target=\"#InvActions\" onclick=\"setpInv(" + dr["pch_invoice_id"] + ");\" ";
                //}
                //if (dr["status"].ToString() == "3")
                //{
                //    strAction = "Marcar como Pagada";
                //}
                //if (dr["status"].ToString() == "4")
                //{
                //    strAction = "";
                //}

                string actions = "";

                if (sessionType == "1" || sessionType == "2" || sessionType == "3")
                {
                    actions = "<div class=\"dropdown\">" +
                                        "<a href=\"javascript:;\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md kt-link\" data-toggle=\"dropdown\" >" +
                                        "<i class=\"fa fa-check\"></i></a>" +
                                            "<div class=\"dropdown-menu dropdown-menu-right\">" +
                                                "<ul class=\"kt-nav\">" +
                                                    "<li class=\"kt-nav__item\">" +
                                                        "<a href=\"#\" class=\"kt-nav__link\" \" >" +
                                                        "<i class=\"kt-nav__link-icon flaticon-interface-11\"></i>" +
                                                        "<span class=\"kt-nav__link-text\">Detalle</span>" +
                                                        "</a>" +
                                                    "</li>" +
                                                    cancelAction +
                                                "</ul>" +
                                            "</div>" +
                                    "</div>";
                }

                row[9] = "<div class=\"actions\"> " +
                                     xmlpath +
                                     "<a href=\"/CFD/P-" + dr["sale_payment_id"] + ".pdf\" download=\"P-" + dr["sale_payment_id"] + ".pdf\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md kt-link\" data-toggle=\"kt-tooltip\" data-container=\"body\" data-placement=\"top\" title=\"Descargar PDF\">" +
                                     "PDF</a>" +
                                     complpath +
                                     actions +
                       "</div></td>" +
                       "</tr>";

                rows[rw] = new object();
                rows[rw] = row;
                rw++;


                if (dr["subtotal_base_currency"].ToString() != "")
                    subtotalMX += Convert.ToDouble(dr["subtotal_base_currency"]);
                if (dr["tax_base_currency"].ToString() != "")
                    taxMX += Convert.ToDouble(dr["tax_base_currency"]);
                if (dr["total_base_currency"].ToString() != "")
                    totalMX += Convert.ToDouble(dr["total_base_currency"]);

            }

            rowt[0] = rows.Length.ToString();
            rowt[1] = subtotalMX.ToString("c");
            rowt[2] = taxMX.ToString("c");
            rowt[3] = totalMX.ToString("c");
            rowt[4] = "MX";
        }
        

        DataHelper.Disconnect();

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        serializer.MaxJsonLength = Int32.MaxValue;
        string serData = "{\"recact\":" + serializer.Serialize(rows) + ",\"totalrecact\":" + serializer.Serialize(rowst) + "}";
        return serData;
    }

     [WebMethod(EnableSession = true)]
    public static string GetSalePaymentDocs(string custID)//Bnet.Next.Collections.Hashtable ht)
    {
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        Bnet.Next.Collections.Hashtable ht = new Bnet.Next.Collections.Hashtable();

        //string dteInit = GetVal(form, "fecha_inicio");
        //string custID = GetVal(form, "custID");
        ht.Add("@customer_id@", custID, "False");

        DataHelper.BxsqlFile = HttpContext.Current.Server.MapPath("~/bxsql/Bnet.Suite.Core.Operations.SalePaymentOperation.bxsql");
        DataHelper.LoadBxsql();

        DataHelper.Connect();
        string custName = "";
        string custEmail = "";
        DataHelper.Query("select emails from business_partners bp INNER JOIN customers c ON c.bpartner_id=bp.bpartner_id AND customer_id=" + custID + "");
        if (DataHelper.Next())
            custEmail = DataHelper.FieldValue("emails").ToString();
        DataHelper.ExecuteCommand("searchSalePaymentDocs", ht);

        

        string[] row = new string[13];
        object[] rows = new object[0];
        if (DataHelper.exception.Message == "No Exception")
        {
            int rw = 0;
            rows = new object[DataHelper.DataTable.Rows.Count];
            foreach (System.Data.DataRow dr in DataHelper.DataTable.Rows)
            {
                custName = dr["customer_name"].ToString();
                string img = "1.png";
                row = new string[13];
                row[0] = dr["serie"]+"-"+dr["code"].ToString();
                row[1] = dr["type_name"].ToString();
                row[2] = dr["grand_total"].ToString();
                row[3] = dr["paid_amt"].ToString();
                row[4] = dr["balance"].ToString();
                row[5] = "<a class=\"editable\" id=\"docln" + rw + "\" href=\"javascript:;\">0</a>";
                row[6] = dr["currency_code"].ToString();
                row[7] = dr["subtotal"].ToString();
                row[8] = dr["tax_amt"].ToString();
                row[9] = dr["doc_id"].ToString();
                row[10] = dr["type"].ToString();
                row[11] =  dr["pay_method_cond"].ToString();
                row[12] = Convert.ToDateTime(dr["created"]).ToString("yyyy-MM-dd HH:mm:ss");
                //row[10] = 
                //row[11] = "<span id=\"unitaryprice" + dr["line_id"] + "\">" + Convert.ToDouble(dr["unitary_price"]).ToString("c") + "</span>";
                //row[12] = "<span id=\"linetoedit" + dr["line_id"] + "\">" + dr["total"].ToString() + "</span>";

                rows[rw] = new object();
                rows[rw] = row;
                rw++;
            }

        }

        DataHelper.Disconnect();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string serData = "{\"salePayDocs\":" + serializer.Serialize(rows) + ",\"custID\":\"" + custID + "\",\"custName\":\"" + custName + "\",\"custEmail\":\"" + custEmail + "\"}";
        return serData;
    }


     [WebMethod(EnableSession = true)]
     public static string GetRanges(Form[] form)//Bnet.Next.Collections.Hashtable ht)
     {
         Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
         DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
         DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;


         //string supplierid = HttpContext.Current.Session["supplierid"].ToString();
         string sessionType = "" + HttpContext.Current.Session["type"];



         string dteInit = GetVal(form, "start");
         string dteEnd = GetVal(form, "end");
         string type = GetVal(form, "cmbInvType");
         string status = GetVal(form, "cmbInvStatus");
         string customer = GetVal(form, "customer");

         bool nofilter = true;

         if (type != "" || status != "" || customer != "")
             nofilter = false;

         Bnet.Next.Collections.Hashtable ht = new Bnet.Next.Collections.Hashtable();

         if (string.IsNullOrEmpty(dteInit))
         {
             dteInit = DateTime.Now.AddMonths(-1).ToString();
             dteEnd = DateTime.Now.ToString();
             if (nofilter)
             {
                 ht.Add("@created_min@", Convert.ToDateTime(dteInit).ToString("yyyy/MM/dd 00:00:00"), "True");
                 ht.Add("@created_max@", Convert.ToDateTime(dteEnd).ToString("yyyy/MM/dd 23:59:59"), "True");
             }
         }
         else
         {
             dteInit = Convert.ToDateTime(dteInit, CultureInfo.GetCultureInfo("es-MX")).ToString();
             dteEnd = Convert.ToDateTime(dteEnd, CultureInfo.GetCultureInfo("es-MX")).ToString();
             ht.Add("@created_min@", Convert.ToDateTime(dteInit).ToString("yyyy/MM/dd 00:00:00"), "True");
             ht.Add("@created_max@", Convert.ToDateTime(dteEnd).ToString("yyyy/MM/dd 23:59:59"), "True");
         }

         //if (supplierid != "")
         //    ht.Add("@customer_id@", supplierid, "False");
         if(status=="1")
             ht.Add("@authorized@", " NOT NULL ", "False");
         else if(status=="0")
             ht.Add("@authorized@", " NULL ", "False");
        
         if (customer != "-1")
             ht.Add("@customer_id@", customer, "False");
         ht.Add("@type@", type, "True");
         ht.Add("@remark@", "", "False");
         ht.Add("@remarkjoin@", "", "False");

         DataHelper.BxsqlFile = HttpContext.Current.Server.MapPath("~/bxsql/Bnet.Suite.Core.Operations.InvoiceOperation.bxsql");
         DataHelper.LoadBxsql();

         DataHelper.Connect();
         DataHelper.ExecuteCommand("getRanges", ht);

         string[] row = new string[9];
         object[] rows = new object[0];
         if (DataHelper.exception.Message == "No Exception")
         {
             int rw = 0;
             rows = new object[DataHelper.DataTable.Rows.Count];
             foreach (System.Data.DataRow dr in DataHelper.DataTable.Rows)
             {

                 string img = "1.png";
                 row = new string[10];
                 row[0] = dr["customer_guide_range_id"].ToString();
                 row[1] = "<div class=\"kt-user-card-v2\">" +
                             "<div class=\"kt-user-card-v2__pic\">" +
                                 "<img src=\"assets/media/client-logos/logo" + img + "\" alt=\"photo\">" +
                             "</div>" +
                             "<div class=\"kt-user-card-v2__details\">" +
                                 "<a href=\"javascript:;\" class=\"kt-user-card-v2__name kt-link\">" + dr["customer"] + "</a>" +
                                 "<span class=\"kt-user-card-v2__email\">" +
                                 dr["customer_id"] + "</span>" +
                             "</div>" +
                         "</div>";
                 row[2] = dr["min_guide"]+"-"+dr["max_guide"];
                 row[3] = dr["created_by"].ToString();
                 row[4] = Convert.ToDateTime(dr["created"]).ToString("dd-MMM-yy hh:mm:ss tt");
                 row[5] = dr["authorized_by"].ToString();
                 if (!string.IsNullOrEmpty(dr["authorized"].ToString()))
                     row[6] = Convert.ToDateTime(dr["authorized"]).ToString("dd-MMM-yy hh:mm:ss tt");
                 else
                     row[6] = "";
                 
                 //row[7] = Convert.ToDouble(dr["grand_total"]).ToString("c");

                 string color = "success";
                 string stat = "Autorizada";
                 string complpath = "";
                 string cancelAction = "";

             

                 if (dr["authorized_by"].ToString() == "")
                 {
                     color = "warning";
                     stat = "Por autorizar";
                 }


                 row[7] = "<span class=\"btn btn-bold btn-sm btn-label-" + color + "\">" +
                             stat + "</span>";

                 string strAction = "Autorizar";
                 string linkAction = "onclick=\"authorizeRange(" + dr["customer_guide_range_id"] + "); return false;\"";
                 string xmlpath = "";

                 string actions = "";

                 if ( ( sessionType == "2" ) & dr["authorized_by"].ToString() == "")
                 {
                     actions = "<div class=\"dropdown\">" +
                                         "<a href=\"javascript:;\" class=\"btn btn-sm btn-clean btn-icon btn-icon-md kt-link\" data-toggle=\"dropdown\" >" +
                                         "<i class=\"fa fa-check\"></i></a>" +
                                             "<div class=\"dropdown-menu dropdown-menu-right\">" +
                                                 "<ul class=\"kt-nav\">" +
                                                     "<li class=\"kt-nav__item\">" +
                                                         "<a href=\"javascript:;\" class=\"kt-nav__link\" " + linkAction + " >" +
                                                         "<i class=\"kt-nav__link-icon flaticon-interface-11\"></i>" +
                                                          "<span class=\"kt-nav__link-text\">" + strAction + "</span>" +
                                                         "</a>" +
                                                     "</li>" +
                                                     cancelAction +
                                                 "</ul>" +
                                             "</div>" +
                                     "</div>";
                 }

                 row[8] = "<div class=\"actions\"> " +
                                      complpath +
                                      actions +
                        "</div></td>" +
                        "</tr>";

                 rows[rw] = new object();
                 rows[rw] = row;
                 rw++;
             }

         }

         /*
         System.Data.DataTable dtrows = new System.Data.DataTable() ;
         List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
         if (DataHelper.exception.Message == "No Exception")
         {
             dtrows = DataHelper.DataTable;
             Dictionary<string, object> row;
             foreach (System.Data.DataRow dr in dtrows.Rows)
             {
                 row = new Dictionary<string, object>();
                 foreach (System.Data.DataColumn col in dtrows.Columns)
                 {
                     row.Add(col.ColumnName, dr[col]);
                 }
                 rows.Add(row);
             }

         }
         */
         DataHelper.Disconnect();

         System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
         serializer.MaxJsonLength = Int32.MaxValue;
         string serData = serializer.Serialize(rows);
         return serData;
     }

    [WebMethod]
    public static string GetPOLines(Form[] form)//Bnet.Next.Collections.Hashtable ht)
    {
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        //string dteInit = GetVal(form, "fecha_inicio");
        string poID = GetVal(form, "cmbPchOrder");
            

        
        DataHelper.Connect();

        DataHelper.Query("select po.code,pol.line_id,pol.delivery,pol.name,pol.description,un.code as unitcode,cu.code as currcode,pol.quantity,pol.invoiced_quantity,(pol.quantity-pol.invoiced_quantity) as pendent,"+
         "0 as delivery_quantity,unitary_price,0 as total "+
         " FROM pch_orders po INNER JOIN pch_order_lines pol ON po.pch_order_id=pol.pch_order_id INNER JOIN units un ON un.unit_id=pol.unit_id INNER JOIN currency cu ON cu.currency_id=pol.currency_id "+
        "WHERE po.pch_order_id="+poID+"");

        string[] row = new string[13];
        object[] rows = new object[0];
        if (DataHelper.exception.Message == "No Exception")
        {
            int rw = 0;
            rows = new object[DataHelper.DataTable.Rows.Count];
            foreach (System.Data.DataRow dr in DataHelper.DataTable.Rows)
            {

                string img = "1.png";
                row = new string[13];
                row[0] = dr["code"].ToString();
                row[1] = dr["line_id"].ToString();
                row[2] = dr["delivery"].ToString();
                row[3] = dr["name"].ToString();
                row[4] = dr["description"].ToString();
                row[5] = dr["unitcode"].ToString();
                row[6] = dr["currcode"].ToString();
                row[7] = Convert.ToInt32(dr["quantity"]).ToString();
                row[8] = Convert.ToInt32(dr["invoiced_quantity"]).ToString();
                row[9] = Convert.ToInt32(dr["pendent"]).ToString();
                row[10] = "<a class=\"editable\" id=\"ln" + dr["line_id"] + "\" href=\"javascript:;\">" + dr["delivery_quantity"].ToString() + "</a>";
                row[11] = "<span id=\"unitaryprice" + dr["line_id"] + "\">" + Convert.ToDouble(dr["unitary_price"]).ToString("c") + "</span>";
                row[12] = "<span id=\"linetoedit" + dr["line_id"] + "\">" + dr["total"].ToString() + "</span>";

                rows[rw] = new object();
                rows[rw] = row;
                rw++;
            }

        }

        DataHelper.Disconnect();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        string serData = serializer.Serialize(rows);
        return serData;
    }

    [WebMethod]
    public static string UploadFile(IEnumerable<HttpPostedFileBase> files)//Bnet.Next.Collections.Hashtable ht)
    {
        
        return "OK";
    }


    [WebMethod(EnableSession = true)]
    public static string UpdateInvoice(Form[] form)
    {
        Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        string userid = "" + HttpContext.Current.Session["userid"];

        string pInvID = GetVal(form, "pinvID");
        bool updatePay = true;
        if (string.IsNullOrEmpty(pInvID))
        {
            pInvID = GetVal(form, "pID");
            updatePay = false;
        }
        string dateToPay = GetVal(form, "kt_datepicker_2_modal");
        if (updatePay)
        {
            try
            {
                dateToPay = Convert.ToDateTime(dateToPay).ToString("yyyy/MM/dd");
            }
            catch
            {
                dateToPay = Convert.ToDateTime(dateToPay,CultureInfo.GetCultureInfo("es-MX")).ToString("yyyy/MM/dd");
            }
        }

        dh.Connect();
        dh.Query("select status,pch_invoice_id,uuid,legal_name,total,serie,folio from pch_invoices where pch_invoice_id="+pInvID+"");
        if (dh.Next())
        {
            if (updatePay)
            {
                if (Convert.ToInt32(dh.FieldValue("status")) == 2)
                {
                    dh.Update("update pch_invoices set status=3, date_to_pay='" + dateToPay + "',programmed_by="+userid+",date_programmed=CURRENT_TIMESTAMP  where pch_invoice_id=" + dh.FieldValue("pch_invoice_id") + "");
                    dh.Update("INSERT INTO notifications (notif_id,user_id,name,created_by,updated_by,created,updated) VALUES (nextval('sq_notifications'),4,'Factura con Fecha de pago asignada " + dh.FieldValue("serie") + "-" + dh.FieldValue("folio") + "',4,4,now(),now()) ");
                    General.sendEmail(General.sendEmailTo,"Fecha de pago asignada a factura", "Factura UUID: " + dh.FieldValue("uuid") + "<p>Proveedor: " + dh.FieldValue("legal_name") + "<p>Total: " + dh.FieldValue("total") + " <p>Fecha de Pago: " + Convert.ToDateTime(dateToPay).ToString("dd-MMM-yyyy") + "");
                }
            }
            else
            {
                string subject = "Factura autorizada";
                string updStr = "";
                if (Convert.ToInt32(dh.FieldValue("status")) == 1)
                    updStr = ",authorized_by=" + userid + ",date_authorized=CURRENT_TIMESTAMP";
                if (Convert.ToInt32(dh.FieldValue("status")) == 3)
                {
                    subject = "Factura Pagada";
                    updStr = ",paid_by=" + userid + ",date_paid=CURRENT_TIMESTAMP";
                }
                dh.Update("update pch_invoices set status=status+1 "+updStr+"  where pch_invoice_id=" + pInvID + "");
                dh.Update("INSERT INTO notifications (notif_id,user_id,name,created_by,updated_by,created,updated) VALUES (nextval('sq_notifications'),4,'"+subject+" " + dh.FieldValue("serie") + "-" + dh.FieldValue("folio") + "',4,4,now(),now()) ");
                General.sendEmail(General.sendEmailTo, subject, "Factura UUID: " + dh.FieldValue("uuid") + "<p>Proveedor: " + dh.FieldValue("legal_name") + "<p>Total: " + dh.FieldValue("total") + " <p> ");
            }

        }
        dh.Disconnect();
        //string qts = GetQuotes(ht);
        return "success";

        

    }

    [WebMethod(EnableSession = true)]
    public static string UpdateSupplier(Form[] form)
    {
        Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        string supID = GetVal(form, "supID");
        string result = "";

        dh.Connect();
       
        dh.Update("update suppliers set authorized='1'  where supplier_id=" + supID + "");
        if (dh.exception.Message != "No Exception")
            result = "{\"response\":\"error\",\"message\":\"No se pudo insertar en la base de datos\",\"error\":\"" +  General.parseJSON(dh.exception.Message) + "\"}";
        else
            result = "{\"response\":\"success\",\"message\":\"" + supID + "\"}";
        dh.Disconnect();

        return result;

        

    }


    [WebMethod(EnableSession = true)]
    public static string AuthorizeRange(string rangeid)
    {
        Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

         string userid = "" + HttpContext.Current.Session["userid"];
        string result = "";

        dh.Connect();

        dh.Update("update customer_guide_range set authorized_by=" + userid + ",authorized=CURRENT_TIMESTAMP  where customer_guide_range_id=" + rangeid + "");
        if (dh.exception.Message != "No Exception")
            result = "{\"response\":\"error\",\"message\":\"No se pudo insertar en la base de datos\",\"error\":\"" + General.parseJSON(dh.exception.Message) + "\"}";
        else
            result = "{\"response\":\"success\",\"message\":\"" + rangeid + "\"}";
        dh.Disconnect();

        return result;



    }

    [WebMethod(EnableSession = true)]
    public static string DeleteNotifications(string notifid)
    {
        Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        string result = "";

        dh.Connect();

        dh.Update("update notifications set read='1',date_read=now()  where notif_id=" + notifid + "");
        if (dh.exception.Message != "No Exception")
            result = "{\"response\":\"error\",\"message\":\"No se pudo insertar en la base de datos\",\"error\":\"" +  General.parseJSON(dh.exception.Message) + "\"}";
        else
            result = "{\"response\":\"success\",\"message\":\"" + notifid + "\"}";
        dh.Disconnect();

        return result;



    }

    [WebMethod(EnableSession = true)]
    public static string CheckZip(string zip)
    {
        Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        string result = "";

        dh.Connect();

        //dh.Query("select st.state_id,city_id,neighborhood,covered FROM valid_zipcodes vz LEFT JOIN states st ON upper(unaccent('unaccent',st.state_name)) like upper(vz.state) LEFT JOIN cities c ON upper(unaccent('unaccent',c.city_name)) like upper(vz.city)  where zipcode like '" + zip + "' AND covered='SI'");
        dh.Query("select state,city,neighborhood,covered FROM valid_zipcodes vz where zipcode like '" + zip + "' AND covered='SI'");
        if (dh.exception.Message != "No Exception")
            result = "{\"response\":\"error\",\"message\":\"Error al realizar la consulta\",\"error\":\"" + General.parseJSON(dh.exception.Message) + "\"}";
        else if (dh.Next())
        {
            if (dh.FieldValue("covered").ToString().ToUpper() == "SI")
            {
                result = "{\"response\":\"success\",\"state\":\"" + dh.FieldValue("state") + "\",\"city\":\"" + dh.FieldValue("city") + "\",\"neighborhood\":\"" + dh.FieldValue("neighborhood") + "\"}";
            }
            else
                result = "{\"response\":\"warning\",\"message\":\"El código postal no cuenta con cobertura\",\"error\":\"\"}";
        }
        else
            result = "{\"response\":\"warning\",\"message\":\"No se encontró el código postal ingresado\",\"error\":\"\"}";
        dh.Disconnect();

        return result;



    }

    [WebMethod(EnableSession = true)]
    public static string GetOcurreInfo(string id)
    {
        Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        string result = "";

        dh.Connect();

        //dh.Query("select st.state_id,city_id,neighborhood,covered FROM valid_zipcodes vz LEFT JOIN states st ON upper(unaccent('unaccent',st.state_name)) like upper(vz.state) LEFT JOIN cities c ON upper(unaccent('unaccent',c.city_name)) like upper(vz.city)  where zipcode like '" + zip + "' AND covered='SI'");
        dh.Query("select address,state,city,neighborhood,zipcode,phone FROM ocurre_stores os where ocurre_store_id= " + id + "");
        if (dh.exception.Message != "No Exception")
            result = "{\"response\":\"error\",\"message\":\"Error al realizar la consulta\",\"error\":\"" + General.parseJSON(dh.exception.Message) + "\"}";
        else if (dh.Next())
        {

            result = "{\"response\":\"success\",\"address\":\"" + dh.FieldValue("address") + "\",\"zipcode\":\"" + dh.FieldValue("zipcode") + "\",\"phone\":\"" + dh.FieldValue("phone") + "\",\"state\":\"" + dh.FieldValue("state") + "\",\"city\":\"" + dh.FieldValue("city") + "\",\"neighborhood\":\"" + dh.FieldValue("neighborhood") + "\"}";
        }
        else
            result = "{\"response\":\"warning\",\"message\":\"No se encontró información\",\"error\":\"\"}";
        dh.Disconnect();

        return result;



    }

    [WebMethod(EnableSession = true)]
    public static string SaveInvoice(Form[] form)
    {
        Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        dh.BxsqlFile = HttpContext.Current.Server.MapPath("~/bxsql/Bnet.Suite.Core.Operations.InvoiceOperation.bxsql");
        dh.LoadBxsql();

        string userid = "" + HttpContext.Current.Session["userid"];
        string result = "";
        double subtotal = Convert.ToDouble(GetVal(form, "subtotal"));
        double tax = Convert.ToDouble(GetVal(form, "tax_amt")); ;
        double total = Convert.ToDouble(GetVal(form, "total")); ;

        int initFolio = 1;
        string invcode="1";
        string sCadena = "";
        string pedimento = "";
        string ieps = "0";
        bool ivat0 = false;
        bool hasieps = false;
        double totalDiscount = 0;
        string ptotalBase = "";
        string conceptos = "";
        bool shadow = false;
        string selloSAT = "";
        string fechaSAT = "";
        string folioSAT = "";
        string cadenaSAT = "";
        string certificadoSAT = "";
        string fechaNow=DateTime.Today.ToString("yyyy-MM-dd");
        string horaNow=DateTime.Now.ToString("HH:mm:ss");
        string sello = "NOSELLODIG";
        string serie = "A";
        string folio = "";
        string cerRoute = "";
        string keyRoute = "";
        string origSerie="";
        string emitRFC="";
        string emitName="";
        string regimeID="";
        string regime="";
        string address="";
        string address2="";
        string address3="";
        string phones="";
        string webpage="";
        string csdPwd="";
        string certSerial="";
        string certData = "";
        string emitCP="";
        string ftpPassword="";
        string ftpUser="";
        string ftpRoute="";
        string timbradoUser="";
        string timbradoPassword="";
        string folioSerie2="";

        //pedimento=GetVal(form, "pedimento");
        string calle = "";string no = "";string col = "";string edo = "";string loc = "";string cp = "";string noint = "";string intstr = "INT";

        try
        {
            string currency=GetVal(form, "currency");
            string currencyName="MXN";
            if(currency=="2")
                currencyName="USD";
            string condiciones =GetVal(form, "pay_cond");
            string PMethod=GetVal(form, "pay_method_cond");
            string cfdiUsage=GetVal(form, "cfdi_usage");
            string rfc=GetVal(form, "rfc");
            string customerName=GetVal(form, "fullname");
            string payForm=GetVal(form, "pay_method");
            string comments = GetVal(form, "comments");
            no =  GetVal(form, "no_ext");
            noint =  GetVal(form, "no_ext");
            calle = General.CleanStringn(GetVal(form, "address").TrimEnd().TrimStart());
            col = General.CleanString(GetVal(form, "neighborhood").TrimEnd().TrimStart());
            loc = General.CleanString(GetVal(form, "cityname").TrimEnd().TrimStart());
            edo = General.CleanString(GetVal(form, "statename").TrimEnd().TrimStart());
            cp = GetVal(form, "postal_code").TrimStart();
            if (calle == "" | calle == " ")calle = "NA";if (no == "" | no == " ")no = "SN";if (noint == "" | noint == " ")noint = "SN";if (col == "" | col == " ")col = "NA";if (edo == "")edo = "NA";if (loc == "")loc = "NA";if (cp == "")cp = "NA";
            dh.Connect();
            System.Data.DataTable dtCfg = new System.Data.DataTable();
             dh.Query("select * from sale_invoice_config");
            if (dh.exception.Message == "No Exception" & dh.Next())
            {
                dtCfg = dh.DataTable;
                cerRoute = dtCfg.Rows[0]["cer_route"].ToString();
                keyRoute = dtCfg.Rows[0]["key_route"].ToString();
                origSerie = dtCfg.Rows[0]["serie"].ToString();
                serie = origSerie;
                initFolio = Convert.ToInt32(dtCfg.Rows[0]["folio"].ToString());
                emitRFC = dtCfg.Rows[0]["rfc"].ToString();
                emitName= dtCfg.Rows[0]["legal_name"].ToString();
                regimeID = dtCfg.Rows[0]["fiscal_regime_id"].ToString();
                regime = dtCfg.Rows[0]["fiscal_regime"].ToString();
                address = dtCfg.Rows[0]["address"].ToString();
                address2= dtCfg.Rows[0]["address2"].ToString();
                address3 = dtCfg.Rows[0]["address3"].ToString();
                phones = dtCfg.Rows[0]["telephone"].ToString();
                webpage = dtCfg.Rows[0]["webpage"].ToString();
                csdPwd = dtCfg.Rows[0]["csd_password"].ToString();
                certSerial = dtCfg.Rows[0]["cer_serie"].ToString();
                certData = dtCfg.Rows[0]["cer_data"].ToString();
                emitCP= dtCfg.Rows[0]["cp"].ToString();

                ftpPassword = dtCfg.Rows[0]["ftp_password"].ToString();
                ftpUser = dtCfg.Rows[0]["ftp_user"].ToString();
                ftpRoute = dtCfg.Rows[0]["ftp_route"].ToString();
                timbradoUser = dtCfg.Rows[0]["timbrado_user"].ToString();
                timbradoPassword = dtCfg.Rows[0]["timbrado_password"].ToString();

            }


            dh.Begin();

            #region Parametros para factura nueva

            string sale_invoice_id = "";
            Bnet.Next.Collections.Hashtable ht = new Bnet.Next.Collections.Hashtable();

            dh.Query("select COALESCE(max(code::int)+1,1) as code from sale_invoices");
            if (dh.Next())
            {
                if (initFolio > Convert.ToInt32(dh.FieldValue("code")))
                    invcode = initFolio.ToString();
                else
                    invcode = dh.FieldValue("code").ToString();
            }
            folio=invcode;
            folioSerie2 = serie+"-"+folio;

            sale_invoice_id = dh.QueryScalar("select Nextval('sq_sale_invoices')").ToString(); // //Gilo por que usaba el folio de factura como id (wooow!!)
            //this.invID = Convert.ToInt32(sale_invoice_id);

            ht.Add("@code@", invcode, "True");

            ht.Add("@sale_invoice_id@", sale_invoice_id, "False");
            ht.Add("@customer_id@", GetVal(form,"customerid"), "False");
            ht.Add("@subtotal@", subtotal, "False");
            ht.Add("@grand_total@", total, "False");
            ht.Add("@created@", "CURRENT_TIMESTAMP", "False");
            ht.Add("@updated@", "CURRENT_TIMESTAMP", "False");
            ht.Add("@created_by@", userid, "False");
            ht.Add("@updated_by@", userid, "False");
            ht.Add("@customer_name@", customerName, "True");//this.dgdInvoiceDetail.Rows[0].Cells["name"].Value.ToString(), "True");
            ht.Add("@customer_address@", calle, "True");

            ht.Add("@street@",calle, "True");
            ht.Add("@no_ext@", no, "True");
            ht.Add("@no_int@", noint, "True");
            ht.Add("@neighborhood@", col, "True");
            ht.Add("@city@", loc, "True");
            ht.Add("@state@",edo, "True");
            ht.Add("@postal_code@", cp, "True");

            ht.Add("@credit_days@", 0, "True");
            ht.Add("@date_invoiced@", DateTime.Today.ToString("yyyy-MM-dd"), "True");
            ht.Add("@expiration_date@", DateTime.Today.ToString("yyyy-MM-dd"), "True");
            ht.Add("@expiration_date_original@", DateTime.Today.ToString("yyyy-MM-dd"), "True");
            ht.Add("@promise_date@", DateTime.Today.ToString("yyyy-MM-dd"), "True");
            ht.Add("@project_id@", -1, "False");// this.Variables["service_order_id"].ToString(), "False");
            ht.Add("@type@", 1, "False");
            ht.Add("@currency_id@", currency, "False");
            double exchRt = 1;
            if (GetVal(form, "currency") != "1")
                exchRt = Convert.ToDouble(GetVal(form, "exchange_rate"));

            ht.Add("@exchange_rate@", exchRt, "False");
            double subtPes = Math.Round(subtotal * exchRt, 2);
            double totPes = Math.Round(total * exchRt, 2);
            ht.Add("@subtotal_pesos@", subtPes, "False");
            ht.Add("@total_pesos@", totPes, "False");

            ht.Add("@tax_amt@", tax, "False");
            ht.Add("@discount_amt@", GetVal(form, "discount_amt"), "False");
            ht.Add("@store_id@", 1, "False");
            ht.Add("@invoice_doc_code@", rfc, "True");
            ht.Add("@description@", comments, "True");
            ht.Add("@pay_method@", payForm, "True");
            ht.Add("@pay_method_id@", payForm, "True");
            ht.Add("@acc_number@", "", "True");
            ht.Add("@pay_method_cond@", PMethod, "True");
            ht.Add("@cfdi_usage_id@", cfdiUsage, "True");
            //ht.Add("@code@", mskTxtInvoiceId.Text, "True");
            ht.Add("@tip_amt@", 0, "False");
            ht.Add("@serie@", serie, "True");
            ht.Add("@remark@", GetVal(form, "pch_order"), "True");

            ht.Add("@status@", 0, "False");

            dh.ExecuteCommand("newInvoice", ht);

            if (dh.exception.Message != "No Exception")
                throw new Exception(dh.exception.Message);

            #endregion

            #region Obtener Lineas

            System.Collections.ArrayList prods = new System.Collections.ArrayList();
            string[] prod = new string[10];
            InvLine invLine = new InvLine();

            for (int i = 0; i < form.Length; i++)
            {
                if (form[i].name.Contains("[lncode]")) {
                    invLine.code = form[i].value; }
                if (form[i].name.Contains("[lnqty]")) { invLine.quantity = Convert.ToDouble(form[i].value); }
                if (form[i].name.Contains("[lnproduct]")) { invLine.product = form[i].value; }
                if (form[i].name.Contains("[lnprice]")) { invLine.price = Convert.ToDouble(form[i].value); }
                if (form[i].name.Contains("[lntotal]")) { invLine.total = Convert.ToDouble(form[i].value); }
                if (form[i].name.Contains("[lnunit]")) { invLine.unit = form[i].value; }
                if (form[i].name.Contains("[lnsatcode]")) { invLine.satcode = form[i].value; }
                if (form[i].name.Contains("[lnsatunit]")) { invLine.satunit = form[i].value; }
                if (form[i].name.Contains("[lntax]")) { invLine.tax = Convert.ToDouble(form[i].value); }
                if (form[i].name.Contains("[lndiscount]")) 
                {
                    invLine.discount = Convert.ToDouble(form[i].value);
                    prods.Add(invLine);
                    invLine = new InvLine();
                }
               

            }
            #endregion

            pedimento = GetVal(form, "pedimento");
            for (int i = 0; i < prods.Count;i++ )
            {
                #region Parametros para lineas de factura
                ht = new Bnet.Next.Collections.Hashtable();
                ht.Add("@sale_invoice_id@", sale_invoice_id, "False");
                ht.Add("@sale_invoiceln_id@", dh.QueryScalar("select Nextval('sq_sale_invoice_lines')"), "False");
                //ht.Add("@product_id@", dgdInvoiceDetail.Rows[i].Cells["product_id"].Value.ToString(), "False");
                ht.Add("@invoiced_product_code@", ((InvLine)prods[i]).code, "True");
                ht.Add("@qty_saled@", ((InvLine)prods[i]).quantity, "False");
                ht.Add("@description@", ((InvLine)prods[i]).product, "True");
                ht.Add("@unitary_price@", ((InvLine)prods[i]).price, "False");
                ht.Add("@subtotal@", ((InvLine)prods[i]).total, "False");
                ht.Add("@line_grand_total@", ((InvLine)prods[i]).total, "False");
                ht.Add("@updated@", "CURRENT_TIMESTAMP", "False");
                //Para unidad de medida
                ht.Add("@unit@", ((InvLine)prods[i]).unit, "True");
                ht.Add("@currency_id@", GetVal(form, "currency"), "False");
                ht.Add("@status@", 0, "False");
                
                //Gilo 24-Abr-2018 para grabar codigo SAT
                ht.Add("@sat_code@", ((InvLine)prods[i]).satcode, "True");
                ht.Add("@cve_unit@", ((InvLine)prods[i]).satunit, "True");
                if (pedimento != "")
                    ht.Add("@import_doc_code@", pedimento, "True");
                ht.Add("@tax_percent@", ((InvLine)prods[i]).tax, "True");
                ht.Add("@discount_amt@", ((InvLine)prods[i]).discount, "False");

                
                //ht.Add("@store_id@", this.cmbStore.SelectedValue.ToString(), "False");
                #endregion

                #region Preparacion de Líneas para CFDI
                if (!shadow)
                    shadow = true;
                else
                shadow = false;
                double txPct = Convert.ToDouble(General.tasaGeneral)*100;
                string tasaOcuota = Math.Round(txPct / 100, 6).ToString("n6");
                string descImpuesto = "002 IVA";
                double rowDiscount = 0;
                
                if (txPct == 0)
                    ivat0 = true;

                double desc = 0;
                string descStr = "";
                try
                {
                    if (((InvLine)prods[i]).discount != 0)
                    {

                        desc = ((InvLine)prods[i]).discount;
                        rowDiscount = desc;
                        totalDiscount += desc;
                        descStr = "Descuento=\"" + desc.ToString("n2").Replace(",", "") + "\"";

                    }
                }
                catch { }

                
                double totciva = ((((InvLine)prods[i]).price * ((InvLine)prods[i]).quantity)-desc) * (1 + (Convert.ToDouble(txPct) / 100));
                double totivared = (((InvLine)prods[i]).total-desc) + Math.Round((((InvLine)prods[i]).total-desc) * (Convert.ToDouble(txPct) / 100), 2);
                double ivarow = 0;
                ptotalBase = (((InvLine)prods[i]).total - desc).ToString("n2").Replace(",","");
                string codImpuesto = "002";
                
                //if (txPct < 0 & txPct < 16 & isAmigo)
                //{
                //    hasieps = true;
                //    codImpuesto = "003";
                //    descImpuesto = "003 IEPS";
                //}

                if (Math.Round(totciva, 2) > Math.Round(totivared, 2))
                    ivarow += Math.Round((((InvLine)prods[i]).total-desc) * (Convert.ToDouble(txPct) / 100), 2) + .01;
                else if (Math.Round(totciva, 2) < Math.Round(totivared, 2))
                    ivarow += Math.Round((((InvLine)prods[i]).total-desc) * (Convert.ToDouble(txPct) / 100), 2) - .01;
                else
                    ivarow += Math.Round((((InvLine)prods[i]).total-desc) * (Convert.ToDouble(txPct) / 100), 2);

                string pedimentoXml="";
                if(pedimento!="")
                    pedimentoXml = "<cfdi:InformacionAduanera NumeroPedimento=\"" + pedimento + "\"/>";

                string identif="NoIdentificacion=\"" + ((InvLine)prods[i]).code + "\"";
                if (((InvLine)prods[i]).code == "")
                    identif = "";
                sCadena += "<cfdi:Concepto "+identif+" ClaveProdServ=\"" + ((InvLine)prods[i]).satcode + "\" ClaveUnidad=\"" + ((InvLine)prods[i]).satunit + "\"  Cantidad=\"" + ((InvLine)prods[i]).quantity + "\" Unidad=\"" + ((InvLine)prods[i]).unit + "\" Descripcion=\"" + ((InvLine)prods[i]).product + "\" ValorUnitario=\"" + ((InvLine)prods[i]).price.ToString("n2").Replace(",", "") + "\" " + descStr + " Importe=\"" + ((InvLine)prods[i]).total.ToString("n2").Replace(",", "") + "\">" +
                            "<cfdi:Impuestos><cfdi:Traslados><cfdi:Traslado Base=\"" + ptotalBase + "\" Impuesto=\"" + codImpuesto + "\" TipoFactor=\"Tasa\" TasaOCuota=\"" + tasaOcuota + "\" Importe=\"" + ivarow + "\"/></cfdi:Traslados></cfdi:Impuestos>" + pedimentoXml + "</cfdi:Concepto>";

                 
                #endregion

                dh.ExecuteCommand("newInvoiceLine", ht);

                if (dh.exception.Message != "No Exception")
                    throw new Exception(dh.exception.Message);
            }            

            #region Generacion de CFDI

             string cadenaOriginal = "";
           
            fechaNow = DateTime.Today.ToString("yyyy-MM-dd");
            horaNow = DateTime.Now.ToString("HH:mm:ss");
            string traslados = "";
            string dctoStr = "";
            if (totalDiscount > 0)
            {
                dctoStr = " Descuento=\"" + totalDiscount.ToString("n2").Replace(",","")+"\"";
            }
           
            if (tax > 0)
            {
                traslados += "<cfdi:Traslado Impuesto=\"002\" TipoFactor=\"Tasa\" TasaOCuota=\"" + General.tasaGeneral + "\" Importe=\"" + tax.ToString("n2").Replace(",", "") + "\"/>";   
            }
            if(ivat0)
                traslados += "<cfdi:Traslado Impuesto=\"002\" TipoFactor=\"Tasa\" TasaOCuota=\"0.000000\" Importe=\"0.00\"/>";

            string relatedCfdi="";
            //if(lstRelCfdi.Items.Count>0)
            //    relatedCfdi+="<cfdi:CfdiRelacionados TipoRelacion=\""+cmbRelationType.SelectedValue+"\">";

            //for (int r = 0; r < lstRelCfdi.Items.Count; r++)
            //{
            //    relatedCfdi += "<cfdi:CfdiRelacionado UUID=\"" + lstRelCfdi.Items[r].ToString() + "\"/>";
            //    if(r==lstRelCfdi.Items.Count-1)
            //        relatedCfdi+="</cfdi:CfdiRelacionados>";
            //}

            string tipoComprobante="I";
            //if (cmbType.SelectedValue.ToString() == "3")
            //    tipoComprobante = "E";


             string sXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"+
                            "<cfdi:Comprobante xmlns:cfdi=\"http://www.sat.gob.mx/cfd/3\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"  xsi:schemaLocation=\"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd\" Version=\"3.3\" "+
                            "Serie=\"" + serie + "\" Folio=\"" + folio + "\" Fecha=\"" + fechaNow + "T" + horaNow + "\" Sello=\"" + sello + "\"  TipoDeComprobante=\""+tipoComprobante+"\" FormaPago=\"" + payForm + "\" "+
                            "CondicionesDePago=\"" + General.CleanString(condiciones) + "\" SubTotal=\"" + subtotal + "\" Total=\"" + total + "\" "+dctoStr+"  TipoCambio=\""+exchRt+"\" Moneda=\""+currencyName+"\"  MetodoPago=\"" + PMethod + "\" "+
                            "LugarExpedicion=\"" + emitCP.Trim() + "\"  NoCertificado=\"" + certSerial + "\" Certificado=\"" + certData + "\">"+relatedCfdi+""+
                            "<cfdi:Emisor Rfc=\"" + emitRFC.ToUpper().Replace("-", "").Trim() + "\" Nombre=\"" + General.CleanString(emitName.ToUpper().Replace(",", "").Replace(".", "").Trim()) + "\" RegimenFiscal=\"" + regimeID + "\"/>"+
                            "<cfdi:Receptor Rfc=\"" + rfc.Replace("-", "").Trim() + "\" Nombre=\"" + General.CleanString(customerName) + "\" UsoCFDI=\"" + cfdiUsage + "\"/>"+
                            "<cfdi:Conceptos>" + sCadena + "</cfdi:Conceptos><cfdi:Impuestos TotalImpuestosTrasladados=\"" + tax.ToString("n2").Replace(",", "") + "\"><cfdi:Traslados>" + traslados + "</cfdi:Traslados></cfdi:Impuestos></cfdi:Comprobante>";
            sXML = sXML.Replace("&", "&amp;");


            #region Generacion de Cadenas y Sellos
            string path = HttpContext.Current.Server.MapPath("~/CFD/");
            System.IO.FileStream fs = new System.IO.FileStream(path+folioSerie2+".xml", System.IO.FileMode.Create, System.IO.FileAccess.Write);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
            sw.Write(sXML);
            sw.Flush();
            sw.Close();
            fs.Close();

            StreamReader reader = new StreamReader(path + folioSerie2 + ".xml");
            XPathDocument doc = new XPathDocument(reader);
            XslCompiledTransform trans = new XslCompiledTransform();
            
            trans.Load(path+"conf/cadenaoriginal_3_3.xslt");
           
            StringWriter writer = new StringWriter();
            XmlTextWriter myWriter = new XmlTextWriter(writer);
            trans.Transform(doc, null, myWriter);
            cadenaOriginal = writer.ToString();
            //Y poner el caracter correcto en la cadena original
            cadenaOriginal = cadenaOriginal.Replace("&amp;", "&");
     
            fs = new System.IO.FileStream(path+"conf/co.txt", System.IO.FileMode.Create, System.IO.FileAccess.Write);
            sw = new System.IO.StreamWriter(fs);
            sw.Write(cadenaOriginal);
            sw.Flush();
            sw.Close();
            fs.Close();

             System.Diagnostics.ProcessStartInfo si;
             string args = "dgst -sha256 -sign "+path+"conf\\key.pem -out "+path+"conf\\co.signed.txt "+path+"conf\\co.txt";
             si = new System.Diagnostics.ProcessStartInfo(path + "\\conf\\openssl.exe", args);
             si.RedirectStandardOutput = true;
             si.UseShellExecute = false;
             si.CreateNoWindow = true;
             System.Diagnostics.Process proc = new System.Diagnostics.Process();
             proc.StartInfo = si;
             proc.Start();
             sello = proc.StandardOutput.ReadToEnd();

            si = new System.Diagnostics.ProcessStartInfo(path+"conf/openssl.exe", "base64 -in  "+path+"conf/co.signed.txt");
                si.RedirectStandardOutput = true;
                si.UseShellExecute = false;
                si.CreateNoWindow = true;
                proc = new System.Diagnostics.Process();
                proc.StartInfo = si;
                proc.Start();
                sello = proc.StandardOutput.ReadToEnd();

            sXML = sXML.Replace("NOSELLODIG",sello.Replace("\n",""));
            fs = new System.IO.FileStream(path+ folioSerie2 + ".xml", System.IO.FileMode.Truncate, System.IO.FileAccess.Write);
            sw = new System.IO.StreamWriter(fs);
            sw.Write(sXML);
            sw.Flush();
            sw.Close();
            fs.Close();
            
            #endregion Cadenas y Sellos

            #region Timbrado de CFDI

            
            RVCFDI33.GeneraCFDI cfdi3 = new RVCFDI33.GeneraCFDI();
            string XML = System.IO.File.ReadAllText(path+ folioSerie2 + ".xml");
            bool produccion = true;
            if (emitRFC == "LAN7008173R5" || emitRFC == "AAA010101AAA")
            {
                produccion = false;
            }
            cfdi3.TimbrarCfdiArchivo(path+ folioSerie2 + ".xml", timbradoUser, timbradoPassword, "http://generacfdi.com.mx/rvltimbrado/service1.asmx?WSDL", path, folioSerie2 + ".xml", produccion);

            if ((!string.IsNullOrEmpty(cfdi3.MensajeError)))
            {
                throw new Exception(cfdi3.MensajeError);
            }
            
            selloSAT = cfdi3.SelloSat;
            cadenaSAT = cfdi3.CadenaTimbre;
            fechaSAT = cfdi3.FechaTimbrado;
            certificadoSAT = cfdi3.NoCertificadoPac;
            folioSAT = cfdi3.UUID;
            
            if (fechaSAT == "")
                fechaSAT = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            dh.Update("update sale_invoices set sat_authorization=1,cadena_original='"+cadenaOriginal+"',sello='"+sello+"',fecha_cfd='"+fechaNow+" "+horaNow+"',SAT_certificate='"+certificadoSAT+"',cadena_original_SAT='"+cadenaSAT+"',sello_SAT='"+selloSAT+"',fecha_cfd_sat='"+fechaSAT.Replace("T"," ")+"',UUID='"+folioSAT+"' where sale_invoice_id="+sale_invoice_id+"");
            if (dh.exception.Message != "No Exception")
                 throw new Exception(dh.exception.Message);

            #endregion Timbrado

            #region Conversion a PDF
            System.Collections.Hashtable htextras = new System.Collections.Hashtable();
            htextras.Add("@logo@", "<img src=\"conf/logoInf.gif\" border=\"0\"  height=\"78\"/>");

            if (comments != "")
                htextras.Add("@comments@", address);
            if (address != "")
                htextras.Add ("@address@", address);
            if (address2 != "")
                htextras.Add("@address2@", address2);
            if (address3 != "")
                htextras.Add("@address3@", address3);
            if (phones != "")
                htextras.Add("@phones@", phones);
            if (webpage != "")
                htextras.Add("@web_page@", webpage);
            if (calle != "")
                htextras.Add("@Direccion@", calle);
            if(col!="")
                htextras.Add("@Colonia@", col);
            if (loc != "")
                htextras.Add("@ciudad@", loc);
            if (edo != "")
                htextras.Add("@estado@", edo);
            if (cp != "")
                htextras.Add("@CP@", cp);


            ConvertPDFProcess(path, folioSerie2+".xml", htextras);

            #endregion Conversion PDF

            

            #endregion Generacion CFDI

            string pdfFile = "";
            string xmlFile = "";
            path = HttpContext.Current.Request.ApplicationPath;
            if (path.EndsWith("/"))
                path += "CFD/";
            else
                path += "/CFD/";
            
                xmlFile = folioSerie2+".xml";
                pdfFile = folioSerie2 + ".pdf";
            
            dh.Commit();
            result = "{\"response\":\"success\",\"message\":\"" + result + "\",\"path\":\"" + path + "\",\"xmlFile\":\"" + xmlFile + "\",\"pdfFile\":\"" + pdfFile + "\",\"serie\":\"" + serie + "\",\"folio\":\"" + folio + "\"}";
        }
        catch (Exception ex)
        {
            try
            {
                dh.Rollback();
            }
            catch { }
            result = "{\"response\":\"error\",\"message\":\"No se pudo insertar en la base de datos\",\"error\":\"" + General.parseJSON(ex.Message) + "\"}";
            
                
        }
        finally
        {
            dh.Disconnect();
           
        }
        return result;

    }

    [WebMethod(EnableSession = true)]
    public static string SaveGuide(Form[] form)
    {
        Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        dh.BxsqlFile = HttpContext.Current.Server.MapPath("~/bxsql/Bnet.Suite.Core.Operations.InvoiceOperation.bxsql");
        dh.LoadBxsql();

        Bnet.Next.Collections.Hashtable ht = new Bnet.Next.Collections.Hashtable();
        string result = "";
        dh.Connect();

        string userid = "" + HttpContext.Current.Session["userid"];
        string customerID = "" + HttpContext.Current.Session["customerid"];
        ht.Add("@customer_id@", customerID, "False");
        ht.Add("@authorized@", "AND authorized_by is not null", "False");
        
        dh.ExecuteCommand("getLastGuide", ht);
        int max_guide = 0;
        int lastused = 0;
        int guide = 0;
        DateTime recDate=DateTime.Today;
        //string range = GetVal(form, "range");
        if (dh.exception.Message == "No Exception")
        {
            max_guide = Convert.ToInt32(dh.DataTable.Rows[0]["max_guide"]);
            lastused = Convert.ToInt32(dh.DataTable.Rows[0]["lastused"]);
            guide = lastused + 1;
            if ((guide) > max_guide)
                result = "{\"response\":\"warning\",\"message\":\"No hay guías disponibles para esta operación, favor de ponerse en contacto con un representante para solicitar más guías a\"}";
            else
            {
                ht = new Bnet.Next.Collections.Hashtable();
                string subguide_id = dh.QueryScalar("SELECT nextval('sq_subcustomer_guides')").ToString();
                ht.Add("@subcustomer_guide_id@", subguide_id, "False");
                ht.Add("@subcustomer_id@", 1, "False");
                ht.Add("@customer_id@", customerID, "False");
                ht.Add("@guide@", guide, "False");
                try
                {
                    string rdate = GetVal(form, "recdate");
                    recDate = Convert.ToDateTime(rdate);
                }
                catch 
                { 
                }
                ht.Add("@recolection_date@", recDate.ToString("yyyy-MM-dd"), "True");
                ht.Add("@created_by@", userid, "False");
                ht.Add("@created@", "CURRENT_TIMESTAMP", "False");
                ht.Add("@updated@", "CURRENT_TIMESTAMP", "False");
                string deltype=GetVal(form, "cmbDeliveryType");
                if (deltype == "1")
                    deltype = "DOMICILIO";
                else
                    deltype = "OCURRE";
                ht.Add("@destination_type@",deltype, "True");
                ht.Add("@zipcode@", GetVal(form, "destinyzip"), "True");
                ht.Add("@solicitant@", GetVal(form, "fullname"), "True");
                ht.Add("@schedule@", GetVal(form, "schedule"), "True");
                ht.Add("@caddress@", GetVal(form, "caddress"), "True");
                ht.Add("@cneighborhood@", GetVal(form, "cneighborhood"), "True");

                ht.Add("@czipcode@", GetVal(form, "czipcode"), "True");
                ht.Add("@creference@", GetVal(form, "creference"), "True");
                ht.Add("@ccity@", GetVal(form, "ccity"), "True");
                ht.Add("@cstate@", GetVal(form, "cstate"), "True");
                ht.Add("@cphone@", GetVal(form, "cphone"), "True");
                ht.Add("@ccontact@", GetVal(form, "ccontact"), "True");

                ht.Add("@name@", GetVal(form, "name"), "True");
                ht.Add("@address@", GetVal(form, "address"), "True");
                ht.Add("@neighborhood@", GetVal(form, "neighborhood"), "True");
                ht.Add("@state@", GetVal(form, "state"), "True");
                ht.Add("@city@", GetVal(form, "city"), "True");
                ht.Add("@contact@", GetVal(form, "contact"), "True");
                ht.Add("@phone@", GetVal(form, "phone"), "True");
                string packtype = GetVal(form, "cmbpacktype");
                if (packtype == "1")
                    packtype = "PAQUETE";
                else
                    packtype = "TARIMA";
                ht.Add("@packtype@", packtype, "True");

                ht.Add("@content@", GetVal(form, "content"), "True");
                ht.Add("@weight@", GetVal(form, "weight"), "True");
                ht.Add("@width@", GetVal(form, "width"), "True");
                ht.Add("@length@", GetVal(form, "length"), "True");
                ht.Add("@height@", GetVal(form, "height"), "True");
                string insured = GetVal(form, "cmbinsured");
                if (insured == "1")
                    insured = "SI";
                else
                    insured = "NO";
                ht.Add("@insured@", insured, "True");
                ht.Add("@value@", GetVal(form, "invoicevalue"), "True");
                ht.Add("@extra_info@", GetVal(form, "extrainfo"), "True");

                dh.ExecuteCommand("insertGuide", ht);
                if (dh.exception.Message != "No Exception")
                    result = "{\"response\":\"error\",\"message\":\"No se pudo insertar en la base de datos\",\"error\":\"" + General.parseJSON(dh.exception.Message) + "\"}";
                else
                {
                    string path = RecOrderProcess(dh, subguide_id.ToString());
                    string pdfFile = subguide_id + ".pdf";

                    ht = new Bnet.Next.Collections.Hashtable();

                    string code=GetVal(form, "shortname").ToString();
                    if(code!="")
                    {
                        
                        string subcustid = dh.QueryScalar("SELECT nextval('sq_subcustomers')").ToString();
                        ht.Add("@subcustomer_id@", subcustid, "False");
                        ht.Add("@customer_id@", customerID, "False");
                        ht.Add("@created_by@", userid, "False");
                        ht.Add("@created@", "CURRENT_TIMESTAMP", "False");
                        ht.Add("@updated@", "CURRENT_TIMESTAMP", "False");
                        ht.Add("@code@", code.ToUpper() , "True");
                        ht.Add("@name@", GetVal(form, "name"), "True");
                        ht.Add("@address@", GetVal(form, "address"), "True");
                        ht.Add("@neighborhood@", GetVal(form, "neighborhood"), "True");
                        ht.Add("@state@", GetVal(form, "state"), "True");
                        ht.Add("@city@", GetVal(form, "city"), "True");
                        ht.Add("@zipcode@", GetVal(form, "destinyzip"), "True");
                        ht.Add("@contact@", GetVal(form, "contact"), "True");
                        ht.Add("@phone@", GetVal(form, "phone"), "True");

                        dh.Query("select code from subcustomers where customer_id=" + customerID + " and lower(code) ilike '" + code + "'").ToString();
                        if (dh.Next())
                            dh.ExecuteCommand("updateSubCustomer", ht);
                        else
                            dh.ExecuteCommand("insertSubCustomer", ht);
                    }

                    result = "{\"response\":\"success\",\"message\":\"\",\"guide\":\"" + guide + "\",\"path\":\"" + path + "\",\"pdfFile\":\"" + pdfFile + "\"}";
                }
            }
        }
        else
            result = "{\"response\":\"error\",\"message\":\"No se pudo insertar en la base de datos\",\"error\":\"" + General.parseJSON(dh.exception.Message) + "\"}";

        
        
        dh.Disconnect();

        return result;
    }

    [WebMethod(EnableSession = true)]
    public static string SaveRange(Form[] form)
    {
        Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        string result = "";
        dh.Connect();

        string userid = "" + HttpContext.Current.Session["userid"];
        string customerID = GetVal(form, "customer");
        string custName = GetVal(form, "custname");
        string authGuides = GetVal(form, "toauthorize");
        string range = GetVal(form,"range");
        string min_guide = "0";
        string max_guide = "0";
        string[] rng = range.Split('-');
        if (rng.Length > 1)
        {
            max_guide = rng[1];
        }
        min_guide = (Convert.ToInt32(max_guide) + 1).ToString();
        max_guide = (Convert.ToInt32(max_guide) + Convert.ToInt32(authGuides)).ToString();
        string token = DateTime.Now.ToString("ssmmHHddMMyy");

        try
        {
            string custrange = dh.QueryScalar("SELECT nextval('sq_customer_guide_ranges')").ToString();
            dh.Update("INSERT INTO  customer_guide_range (customer_guide_range_id,customer_id,auth_guides,min_guide,max_guide,created_by,created,updated,token)" +
            "VALUES (" + custrange + "," + customerID + "," + authGuides + "," + min_guide + "," + max_guide + "," + userid + ",CURRENT_TIMESTAMP,CURRENT_TIMESTAMP,'" + token + "') ");
            if (dh.exception.Message != "No Exception")
                throw new Exception(dh.exception.Message);
            else
            {
                string email = "";
                string usrid = "";
                dh.Query("select emails,u.usr_id from business_partners bp INNER JOIN users u ON u.bpartner_id=bp.bpartner_id WHERE u.type=2");
                if (dh.Next())
                {
                    email = dh.FieldValue("emails").ToString();
                    userid = dh.FieldValue("usr_id").ToString();
                }
                result = "{\"response\":\"success\",\"message\":\"\",\"range\":\"" + min_guide + "-" + max_guide + "\"}";
                string msg = "Se requiere autorización para el cliente " + custName + "<p>";
                msg += "Guías a autorizar: " + authGuides + ", rango desde " + min_guide + " - " + max_guide + "<p>";
                msg += "Para autorizar, haga click en la siguiente liga<p>";
                msg += "https://onecarrier.inf.com.mx/Authorize.aspx?token=" + token + "&id=" + custrange + "&userid=" + userid + "";
                string sndmsg="";
                sndmsg=General.sendEmail(email, "Autorización de guías del cliente " + custName, msg);
                if(sndmsg !="success")
                    result = "{\"response\":\"warning\",\"message\":\"Se agrego el registro, pero no se pudo enviar el correo\",\"error\":\"" + General.parseJSON(sndmsg) + "\",\"range\":\"" + min_guide + "-" + max_guide + "\"}";
                    
            }
        }
        catch (Exception ex)
        {
            result = "{\"response\":\"error\",\"message\":\"No se pudo insertar en la base de datos\",\"error\":\"" + General.parseJSON(dh.exception.Message) + "\"}";
        }
        dh.Disconnect();

        return result;
    }

    [WebMethod(EnableSession = true)]
    public static string AddPayment(Form[] form, object[] docs,bool genCP)
    {
        string result = "";
        Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        dh.BxsqlFile = HttpContext.Current.Server.MapPath("~/bxsql/Bnet.Suite.Core.Operations.SalePaymentOperation.bxsql");
        dh.LoadBxsql();

        string userid = "" + HttpContext.Current.Session["userid"];
        string customerId= GetVal(form, "spcustomerid");
        string PayId = "-1";
        string code = "";
        string paymentTypeId = GetVal(form, "sppay_method");
        string formaDePago = paymentTypeId;//GetPayForm(paymentTypeId);
        double total = Convert.ToDouble(GetVal(form, "sppay_total"));
        int currencyid = Convert.ToInt32(GetVal(form, "spcurrency"));
        string currencyCode = "MXN";
        double exchange_rate = Convert.ToDouble(GetVal(form, "spexchange_rate"));
        string affDocs = "";
        bool requireCfdi = false;

        int initFolio = 1;
        string cerRoute = "";
        string keyRoute = "";
        string origSerie = "";
        string emitRFC = "";
        string emitName = "";
        string regimeID = "";
        string regime = "";
        string address = "";
        string address2 = "";
        string address3 = "";
        string phones = "";
        string webpage = "";
        string csdPwd = "";
        string certSerial = "";
        string certData = "";
        string emitCP = "";
        string ftpPassword = "";
        string ftpUser = "";
        string ftpRoute = "";
        string timbradoUser = "";
        string timbradoPassword = "";
        

        try
        {
            dh.Connect();

            currencyCode = GetCurrencyCode(currencyid, dh);

            System.Data.DataTable dtCfg = new System.Data.DataTable();
            dh.Query("select * from sale_invoice_config");
            if (dh.exception.Message == "No Exception" & dh.Next())
            {
                dtCfg = dh.DataTable;
                cerRoute = dtCfg.Rows[0]["cer_route"].ToString();
                keyRoute = dtCfg.Rows[0]["key_route"].ToString();
                //origSerie = dtCfg.Rows[0]["serie"].ToString();
                //serie = origSerie;
                initFolio = Convert.ToInt32(dtCfg.Rows[0]["folio"].ToString());
                emitRFC = dtCfg.Rows[0]["rfc"].ToString();
                emitName = dtCfg.Rows[0]["legal_name"].ToString();
                regimeID = dtCfg.Rows[0]["fiscal_regime_id"].ToString();
                regime = dtCfg.Rows[0]["fiscal_regime"].ToString();
                address = dtCfg.Rows[0]["address"].ToString();
                address2 = dtCfg.Rows[0]["address2"].ToString();
                address3 = dtCfg.Rows[0]["address3"].ToString();
                phones = dtCfg.Rows[0]["telephone"].ToString();
                webpage = dtCfg.Rows[0]["webpage"].ToString();
                csdPwd = dtCfg.Rows[0]["csd_password"].ToString();
                certSerial = dtCfg.Rows[0]["cer_serie"].ToString();
                certData = dtCfg.Rows[0]["cer_data"].ToString();
                emitCP = dtCfg.Rows[0]["cp"].ToString();

                ftpPassword = dtCfg.Rows[0]["ftp_password"].ToString();
                ftpUser = dtCfg.Rows[0]["ftp_user"].ToString();
                ftpRoute = dtCfg.Rows[0]["ftp_route"].ToString();
                timbradoUser = dtCfg.Rows[0]["timbrado_user"].ToString();
                timbradoPassword = dtCfg.Rows[0]["timbrado_password"].ToString();

            }


            dh.Begin();

            PayId = dh.QueryScalar("select Nextval('sq_sale_payments')").ToString();           
            string date = DateTime.Parse(dh.QueryScalar("select CURRENT_DATE").ToString()).ToString("yyMMdd");
            //Determinamos el Código de la nota
            code = "PAY-" + date + "-" + PayId;
            Bnet.Next.Collections.Hashtable payInfo = new Bnet.Next.Collections.Hashtable();
            Bnet.Next.Collections.Hashtable payToCheck = new Bnet.Next.Collections.Hashtable();


            decimal appliedAmt = 0;
            decimal subtotalBaseCurrency = 0;
            decimal taxBaseCurrency = 0;
            System.Collections.ArrayList income = new  System.Collections.ArrayList();
            string invoiceIDs = "";
            for (int j = 0; j < docs.Length; j++)
            {
                object[] docrow= (object[])docs[j];
               
                string val = (string)docrow[5];                     
                var initStr= val.LastIndexOf(">",val.LastIndexOf(">")-1)+1;
                var endStr = val.LastIndexOf("<");
                val = val.Substring(initStr, (endStr - initStr));

                decimal apAmt = Convert.ToDecimal(val);
                if (Convert.ToDouble(apAmt) > 0)
                {
                    appliedAmt += apAmt;
                    //Se obtiene la proporcion del pago que se esta haciendo contra el total, para poder calcular el subtotal proporcional
                    //de cada elemento, ya que puede que algunos no contengan iva.
                    decimal proportion = 0;
                    if (Convert.ToDecimal(docrow[2]) > 0)
                        proportion = apAmt / Convert.ToDecimal(docrow[2]);
                    decimal subtLine = Convert.ToDecimal(docrow[7].ToString());
                    decimal taxLine = Convert.ToDecimal(docrow[8]);
                    decimal exchRate = Convert.ToDecimal(exchange_rate);
                    subtotalBaseCurrency += subtLine * exchRate * proportion;
                    taxBaseCurrency += taxLine * exchRate * proportion;
                    //Gilo para que en el correo envie las facturas cobradas
                    affDocs += "Folio: " + docrow[0] + "\t\t Monto: " +
                    Convert.ToDecimal(docrow[2]).ToString("c") + "\t\t Cobrado: " + Convert.ToDecimal(apAmt).ToString("c") + "\n";

                    string doccode = docrow[0].ToString().Split('-')[1];
                    income.Add(doccode);
                    invoiceIDs += docrow[9] + ",";
                    
                    dh.Update("INSERT INTO sale_payment_docs (sale_payment_id,doc_id,type,total,activated,deleted,updated_by,created_by,updated,created,currency_id,exchange_rate,total_base_currency,project_id,subtotal_base_currency,tax_base_currency,code,invoice_exchange_rate) VALUES (" + PayId + ","+docrow[9]+",1,abs("+apAmt+"),'1','0'," + userid + "," + userid + ",CURRENT_TIMESTAMP,CURRENT_TIMESTAMP,"+currencyid+"," + exchange_rate + ",abs("+apAmt+")*" + exchange_rate + ",-1,abs("+subtotalBaseCurrency+"),abs("+taxBaseCurrency+"),"+doccode+","+exchange_rate+")");
                    if (dh.exception.Message != "No Exception")
                        throw new Exception(dh.exception.Message);
                }

            }
            if (invoiceIDs.Length > 0)
                invoiceIDs = invoiceIDs.Substring(0, invoiceIDs.Length - 1);
            DateTime payDate = DateTime.Now;

            payInfo.Add("@sale_payment_id@",PayId, "True");
            payInfo.Add("@code@", code, "True");
            payInfo.Add("@description@", "", "True");
            payInfo.Add("@customer_id@", customerId, "False");
            payInfo.Add("@status@", 2, "False");
            //			sPaymentInfo.Add("@payment_rule_type_id@",this.cmbAdmnPaymentRuleID.SelectedValue.ToString(),"False");
            payInfo.Add("@type@", 1, "False");
            payInfo.Add("@bank_id@", GetVal(form, "spbank"), "False");
            payInfo.Add("@bank_account_id@", GetVal(form, "spaccount"), "False");
            payInfo.Add("@reference@", GetVal(form, "spreference"), "True");
            payInfo.Add("@currency_id@", currencyid, "False");
            payInfo.Add("@grand_total@", total, "False");
            payInfo.Add("@applied_amt@", total, "False");
            payInfo.Add("@created_by@", userid, "False");
            payInfo.Add("@updated_by@", userid, "False");
            payInfo.Add("@activated@", "1", "True");
            payInfo.Add("@deleted@", "0", "True");
            payInfo.Add("@total_base_currency@", total * exchange_rate, "False");
            payInfo.Add("@exchange_rate@", exchange_rate, "False");
            payInfo.Add("@subtotal_base_currency@",subtotalBaseCurrency, "False");
            payInfo.Add("@paid_currency_id@", currencyid, "False");
            payInfo.Add("@tax_base_currency@", taxBaseCurrency, "False");
            payInfo.Add("@payment_date@", payDate.ToString("yyyy-MM-dd HH:mm:ss"), "True");
            payInfo.Add("@store_id@", 1, "False");

           
            dh.ExecuteCommand("insertSalePayment", payInfo);
            if (dh.exception.Message != "No Exception")
                throw new Exception(dh.exception.Message);

            Bnet.Next.Collections.Hashtable parameters = new Bnet.Next.Collections.Hashtable();
            int identifier = Convert.ToInt32((Bnet.Api.Utilities.Basic.SecondsFrom(2000) / 60).ToString());
            string incid = dh.QueryScalar("select Nextval('sq_treasury_incomes')").ToString();
            string localCode = 1 + "-" + identifier + "-" + incid;
            parameters.Add("@treasury_incomes_id@", incid, "False");
            parameters.Add("@local_code@", localCode, "True");
            parameters.Add("@created_by@", userid, "False");
            parameters.Add("@company_id@", 1, "False");
            parameters.Add("@income_date@", payDate.ToString("yyyy-MM-dd"), "True");
            parameters.Add("@bank_account_id@", GetVal(form, "spaccount"), "False");
            parameters.Add("@reference@", GetVal(form, "spreference"), "True");
            parameters.Add("@amount@", total * exchange_rate, "True");
            parameters.Add("@currency_id@", currencyid, "False");
            parameters.Add("@treasury_income_concept_id@",3, "False");
            parameters.Add("@treasury_income_status_id@", 1, "False");
            //parameters.Add("@comments@", "", "True");
            int bpartner = Convert.ToInt32(dh.QueryScalar("select bpartner_id from customers where customer_id=" + customerId + ""));
            parameters.Add("@bpartner_id@", bpartner, "False");
            parameters.Add("@updated_by@", userid, "False");
            //parameters.Add("@description@", , "True");
            //parameters.Add("@code@", income.Code, "True");
            parameters.Add("@payment_type_id@", paymentTypeId, "False");
            parameters.Add("@has_accountant_policy@", "0", "True");
            parameters.Add("@is_conciliated@", "0", "True");

            dh.ExecuteCommand("insertIncome", parameters);
                if (dh.exception.Message != "No Exception")
                throw new Exception(dh.exception.Message);

               parameters.Clear();
               parameters.Add("@treasury_incomes_id@", incid, "False");
               for (int i = 0; i < income.Count; i++)
               {
                   parameters.Remove("@invoice_code@");
                   parameters.Add("@invoice_code@", income[i].ToString(), "True");
                   dh.ExecuteCommand("insertIncomesInvoices", parameters);
                   if (dh.exception.Message != "No Exception")
                       throw new Exception(dh.exception.Message);
               }
               if (invoiceIDs.Length > 0)
               {
                   Bnet.Next.Collections.Hashtable invoiceParameters = new Bnet.Next.Collections.Hashtable();
                   invoiceParameters.Add("@sale_invoices_id@", invoiceIDs, "False");
                   dh.ExecuteCommand("updateInvoicesBalance", invoiceParameters);
                   if (dh.exception.Message != "No Exception")
                       throw new Exception(dh.exception.Message);
                   dh.ExecuteCommand("updatePaidInvoices", invoiceParameters);
                   if (dh.exception.Message != "No Exception")
                       throw new Exception(dh.exception.Message);
               }


            #region ProcesoCFDI

            string strPagos = "";
            string pagos = "";
            string pago = "<td class=\"variable\" valign=\"top\">" + payDate.ToString("dd-MMM-yy HH:mm:ss ") + "</td>" +
                      "<td class=\"variable\" valign=\"top\">" + formaDePago + "</td>" +
                      "<td class=\"variable\" valign=\"top\">" + Convert.ToDouble(total).ToString("c") + "</td>" +
                      "<td class=\"variable\" valign=\"top\">" + exchange_rate + "</td>" +
                      "</tr>";

            string tipoCambio = "";
            if (currencyid == 2)
                tipoCambio = "TipoCambioP=\"" + exchange_rate + "\"";
            strPagos = "<pago10:Pago FechaPago=\"" + payDate.ToString("yyyy-MM-ddTHH:mm:ss") + "\" FormaDePagoP=\"" + formaDePago + "\" MonedaP=\"" + currencyCode + "\" Monto=\"" + Convert.ToDouble(total).ToString("n2").Replace(",", "") + "\" " + tipoCambio + ">";
            string uuidmsg = "";
            
            bool genCfdi = genCP;
            string parcialidad = "1";
            double notaCredito = 0;
            //for (int j = 0; j < this.DataTablePaymentLines.Rows.Count; j++)
            //{
            //    if (this.DataTablePaymentLines.Rows[j].RowState != System.Data.DataRowState.Deleted)
            //    {
            //        if (this.DataTablePaymentLines.Rows[j]["selected"].ToString() == "1" & this.DataTablePaymentLines.Rows[j]["type"].ToString() == "3")
            //        {
            //            notaCredito += Math.Round(Convert.ToDouble(this.DataTablePaymentLines.Rows[j]["pay_amt"]), 2);
            //        }
            //    }
            //}
            for (int j = 0; j < docs.Length; j++)
            {
                object[] docrow = (object[])docs[j];

                string val = (string)docrow[5];
                var initStr = val.LastIndexOf(">", val.LastIndexOf(">") - 1) + 1;
                var endStr = val.LastIndexOf("<");
                val = val.Substring(initStr, (endStr - initStr));

                decimal apAmt = Convert.ToDecimal(val);
                if (Convert.ToDouble(apAmt) > 0 & docrow[10] != "3")
                {
                    string uuid = "";
                    string doccode = docrow[0].ToString().Split('-')[1];
                    string seriedoc = docrow[0].ToString().Split('-')[0];
                    dh.Query("select uuid from sale_invoices where sale_invoice_id=" + docrow[9] + "").ToString();
                    if (dh.Next())
                        uuid = dh.FieldValue("uuid").ToString();
                    string tasaOcuota = Math.Round(16.0 / 100, 6).ToString("n6");

                    dh.Query("select count(doc_id) as parcialidad from sale_payment_docs spd INNER JOIN  sale_payments sp ON sp.sale_payment_id=spd.sale_payment_id where doc_id=" + docrow[9] + " AND spd.deleted='0' AND sp.status!=3").ToString();
                    if (dh.Next())
                    {
                        parcialidad = dh.FieldValue("parcialidad").ToString();
                        if (parcialidad != "")
                            parcialidad = (Convert.ToInt32(parcialidad)).ToString();
                    }
                    if (docrow[11] == "PPD")
                    {
                        requireCfdi = true;
                    }
                    if (uuid == "")
                    {
                        uuidmsg += "La factura con Folio: " + doccode + " no tiene UUID\n";
                    } //CtaOrdenante=\"12345678901\"
                    //<pago10:Impuestos TotalImpuestosTrasladados=\"" + Convert.ToDouble(this.DataTablePaymentLines.Rows[j]["tax_amt"]).ToString("n6") + "\"><pago10:Traslados><pago10:Traslado Impuesto=\"002\" TipoFactor=\"Tasa\" TasaOCuota=\""+tasaOcuota+"\" Importe=\""+Convert.ToDouble(this.DataTablePaymentLines.Rows[j]["tax_amt"]).ToString("n6")+"\" /></pago10:Traslados></pago10:Impuestos>
                    double saldoAnt = Math.Round(Convert.ToDouble(docrow[4]), 2);
                    double amountPaid = Math.Round(Convert.ToDouble(apAmt), 2);
                    double saldoPend = Math.Round(saldoAnt - amountPaid, 2);
                    saldoAnt += notaCredito;
                    amountPaid += notaCredito;
                    if (docrow[10] != "3")
                    {
                        //saldoAnt = Math.Round(Convert.ToDouble(this.DataTablePaymentLines.Rows[j]["balance"]), 2)*-1;
                        //amountPaid = Math.Round(Convert.ToDouble(this.DataTablePaymentLines.Rows[j]["pay_amt"]), 2)*-1;
                        //saldoPend = Math.Round(saldoAnt - amountPaid, 2);
                        strPagos += "<pago10:DoctoRelacionado IdDocumento=\"" + uuid + "\" Serie=\"" + seriedoc + "\" Folio=\"" + doccode + "\" MonedaDR=\"" + currencyCode + "\" MetodoDePagoDR=\"PPD\" NumParcialidad=\"" + parcialidad + "\" ImpSaldoAnt=\"" + saldoAnt + "\" ImpPagado=\"" + amountPaid + "\" ImpSaldoInsoluto=\"" + saldoPend + "\" />";
                        pagos += "<td class=\"variable\" valign=\"top\">" + uuid + "</td>" +
                                  "<td class=\"variable\" valign=\"top\">" + seriedoc + "-" + doccode + "</td>" +
                                  "<td class=\"variable\" valign=\"top\">PPD</td>" +
                                  "<td class=\"variable\" valign=\"top\">" + Convert.ToDouble(total).ToString("c") + "</td>" +
                                  "<td class=\"variable\" valign=\"top\">" + saldoAnt.ToString("c") + "</td>" +
                                  "<td class=\"variable\" valign=\"top\">" + saldoPend.ToString("c") + "</td> " +
                                  "<td class=\"variable\" valign=\"top\">" + amountPaid.ToString("c") + "</td>" +
                                  "<td class=\"variable\" valign=\"top\">" + currencyCode + "</td>" +
                                  "</tr>";
                    }
                }
            }

            string pdfFile = "";
            string xmlFile = "";
            string serie = "";
            string folioSerie2 = "";
            string path = "";

            //if (requireCfdi)
            //{
            //    genCfdi = true;
            //    if (uuidmsg != "")
            //        throw new Exception(uuidmsg);


            //}
            //else
            //{
            //    //if (MessageBox.Show("No es necesario generar complemento de pago\nDesea generalo de todas maneras?", "Generar CFDI", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //        genCfdi = true;
            //}

            if (genCfdi)
            {
                #region CFDI

                dh.Query("SELECT a.name,a.sales_rep_id,c.legal_code,/*b.customer_account_id,b.credit_limit,b.credit_balance,b.balance,b.available_credit,b.extra_credit,b.bank_account_id,b.bank_reference,*/c.city_id,c.state_id," +
                "c.country_id,c.city_name,c.state_name,c.country_name,c.address,c.neighborhood,c.postal_code,c.emails " +
                "FROM customers a " +
                //"LEFT JOIN customer_accounts b ON a.customer_id = b.customer_id " +
                "LEFT JOIN business_partners c ON a.bpartner_id = c.bpartner_id " +
                "WHERE a.customer_id = " + customerId + "");

                string rfc = "";
                string customerName = "";
                string postalCode = "";
                if (dh.Next()) { rfc = dh.FieldValue("legal_code").ToString(); customerName = dh.FieldValue("name").ToString(); postalCode = dh.FieldValue("postal_code").ToString(); }

                string sello = "NOSELLODIG";
                serie = "P";
                string fechaNow = DateTime.Today.ToString("yyyy-MM-dd");
                string horaNow = DateTime.Now.ToString("HH:mm:ss");
                string selloSAT = "";
                string fechaSAT = "";
                string folioSAT = "";
                string cadenaSAT = "";
                string certificadoSAT = "";
                string sXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><cfdi:Comprobante xmlns:cfdi=\"http://www.sat.gob.mx/cfd/3\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd http://www.sat.gob.mx/Pagos http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos10.xsd\"  xmlns:pago10=\"http://www.sat.gob.mx/Pagos\" Version=\"3.3\" Serie=\"" + serie + "\" Folio=\"" + PayId + "\" Fecha=\"" + fechaNow + "T" + horaNow + "\" Sello=\"" + sello + "\"  TipoDeComprobante=\"P\" SubTotal=\"0\" Total=\"0\" Moneda=\"XXX\" LugarExpedicion=\"64970\"  NoCertificado=\"" + certSerial + "\" Certificado=\"" + certData + "\"><cfdi:Emisor Rfc=\"" + emitRFC.ToUpper().Replace("-", "").Trim() + "\" Nombre=\"" + General.CleanString(emitName.ToUpper().Replace(",", "").Replace(".", "").Trim()) + "\" RegimenFiscal=\"" + regimeID + "\"/><cfdi:Receptor Rfc=\"" + rfc.Replace("-", "").Trim() + "\" Nombre=\"" + General.CleanString(customerName) + "\" UsoCFDI=\"P01\"/><cfdi:Conceptos><cfdi:Concepto ClaveProdServ=\"84111506\" Cantidad=\"1\" ClaveUnidad=\"ACT\" Descripcion=\"Pago\" ValorUnitario=\"0\" Importe=\"0\"/></cfdi:Conceptos><cfdi:Complemento> " +
                "<pago10:Pagos Version=\"1.0\" xmlns:pago10=\"http://www.sat.gob.mx/Pagos\" xsi:schemaLocation=\"http://www.sat.gob.mx/Pagos http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos10.xsd\">" + strPagos + "</pago10:Pago></pago10:Pagos></cfdi:Complemento></cfdi:Comprobante>";

                sXML = sXML.Replace("&", "&amp;");

                folioSerie2 = serie + "-" + PayId;

                path = HttpContext.Current.Server.MapPath("~/CFD/");
                System.IO.FileStream fs = new System.IO.FileStream(path + folioSerie2 + ".xml", System.IO.FileMode.Create, System.IO.FileAccess.Write);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
                sw.Write(sXML);
                sw.Flush();
                sw.Close();
                fs.Close();

                StreamReader reader = new StreamReader(path + folioSerie2 + ".xml");
                XPathDocument doc = new XPathDocument(reader);
                XslCompiledTransform trans = new XslCompiledTransform();

                trans.Load(path + "conf/cadenaoriginal_3_3.xslt");
                string cadenaOriginal = "";
                StringWriter writer = new StringWriter();
                XmlTextWriter myWriter = new XmlTextWriter(writer);
                trans.Transform(doc, null, myWriter);
                cadenaOriginal = writer.ToString();
                //Y poner el caracter correcto en la cadena original
                cadenaOriginal = cadenaOriginal.Replace("&amp;", "&");

                fs = new System.IO.FileStream(path + "conf/co.txt", System.IO.FileMode.Create, System.IO.FileAccess.Write);
                sw = new System.IO.StreamWriter(fs);
                sw.Write(cadenaOriginal);
                sw.Flush();
                sw.Close();
                fs.Close();

                try
                {
                    System.Diagnostics.ProcessStartInfo si;

                    string args = "dgst -sha256 -sign " + path + "conf\\key.pem -out " + path + "conf\\co.signed.txt " + path + "conf\\co.txt";
                    si = new System.Diagnostics.ProcessStartInfo(path + "\\conf\\openssl.exe", args);
                    si.RedirectStandardOutput = true;
                    si.UseShellExecute = false;
                    si.CreateNoWindow = true;
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo = si;
                    proc.Start();
                    sello = proc.StandardOutput.ReadToEnd();

                    si = new System.Diagnostics.ProcessStartInfo(path + "conf/openssl.exe", "base64 -in  " + path + "conf/co.signed.txt");
                    si.RedirectStandardOutput = true;
                    si.UseShellExecute = false;
                    si.CreateNoWindow = true;
                    proc = new System.Diagnostics.Process();
                    proc.StartInfo = si;
                    proc.Start();
                    sello = proc.StandardOutput.ReadToEnd();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }

                sXML = sXML.Replace("NOSELLODIG", sello.Replace("\n", ""));

                fs = new System.IO.FileStream(path + folioSerie2 + ".xml", System.IO.FileMode.Truncate, System.IO.FileAccess.Write);
                sw = new System.IO.StreamWriter(fs);
                sw.Write(sXML);
                sw.Flush();
                sw.Close();
                fs.Close();

                RVCFDI33.GeneraCFDI cfdi3 = new RVCFDI33.GeneraCFDI();

                string XML = System.IO.File.ReadAllText(path + folioSerie2 + ".xml");

                bool produccion = true;
                if (emitRFC == "LAN7008173R5" || emitRFC == "AAA010101AAA")
                    produccion = false;
                cfdi3.TimbrarCfdiArchivo(path + folioSerie2 + ".xml", timbradoUser, timbradoPassword, "http://generacfdi.com.mx/rvltimbrado/service1.asmx?WSDL", path, folioSerie2 + ".xml", produccion);

                if ((!string.IsNullOrEmpty(cfdi3.MensajeError)))
                {
                    //MessageBox.Show(cfdi3.MensajeError, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    throw new Exception("No se pudo timbrar\n" + cfdi3.MensajeError + "");
                }

              
                selloSAT = cfdi3.SelloSat;
                cadenaSAT = cfdi3.CadenaTimbre;
                fechaSAT = cfdi3.FechaTimbrado;
                certificadoSAT = cfdi3.NoCertificadoPac;
                folioSAT = cfdi3.UUID;
             
                if (fechaSAT == "")
                    fechaSAT = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                dh.Query("update sale_payments set cadena_original='" + cadenaOriginal + "',sello='" + sello + "',fecha_cfd='" + fechaNow + " " + horaNow + "',SAT_certificate='" + certificadoSAT + "',cadena_original_SAT='" + cadenaSAT + "',sello_SAT='" + selloSAT + "',fecha_cfd_sat='" + fechaSAT.Replace("T", " ") + "',UUID='" + folioSAT + "' where sale_payment_id=" + PayId + "");

                #region Generacion de PDF 
                /*
                fs = new System.IO.FileStream(@"../common/cfd/TemplatePago.html", System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.StreamReader
                sr = new System.IO.StreamReader(fs);
                string html = sr.ReadToEnd();
                html = html.Replace("@certificado@", certSerial);
                html = html.Replace("@emit_rfc@", emitRFC);
                html = html.Replace("@legal_name@", emitName);
                html = html.Replace("@fiscal_regime@", regimeID + " - " + regime);
                if (address != "")
                    html = html.Replace("@address@", address);
                else
                    html = html.Replace("@address@", "");
                if (address != "")
                    html = html.Replace("@address2@", address2);
                else
                    html = html.Replace("@address2@", "");
                if (address != "")
                    html = html.Replace("@address3@", address3);
                else
                    html = html.Replace("@address3@", "");
                html = html.Replace("@cpExp@", emitCP);
                if (phones != "")
                    html = html.Replace("@phones@", phones);
                else
                    html = html.Replace("@phones@", "");
                if (webpage != "")
                    html = html.Replace("@web_page@", webpage);
                else
                    html = html.Replace("@web_page@", "");
                string conceptos = "<td class=\"variable\" valign=\"top\">&nbsp;</td>" +
              "<td class=\"variable\" valign=\"top\">1</td>" +
              "<td class=\"variable\" valign=\"top\">PAGO</td>" +
              "<td class=\"variable\" valign=\"top\">ACT</td>" +
              "<td class=\"variable\" valign=\"top\">84111506</td>" +
              "<td class=\"variable\" valign=\"top\">ACT</td> " +
              "<td class=\"variable\" valign=\"top\">" + Convert.ToDouble(0.0).ToString("c") + "</td>" +
              "<td class=\"variable\" valign=\"top\">" + Convert.ToDouble(0.0).ToString("c") + "</td></tr>";
                html = html.Replace("@conceptos@", conceptos);

                html = html.Replace("@pagos@", pagos);
                html = html.Replace("@pago@", pago);

                html = html.Replace("@comments@", "");
                html = html.Replace("@folio@", serie + " " + PayId);
                html = html.Replace("@fecha@", fechaNow + " " + horaNow);
                html = html.Replace("@autorizacion@", "240634");
                html = html.Replace("@ano_aut@", "2010");
                html = html.Replace("@fecha_aut@", "11/29/10 11:44:43");
                //html = html.Replace("@certificado@", "00001000000301080645");//, "00001000000100777962");
                html = html.Replace("@certificado@", "00001000000406330578");//, "00001000000100777962");
                html = html.Replace("@RFC@", rfc.Replace("-", "").Trim());
                html = html.Replace("@Nombre@", General.CleanString(customerName));

                html = html.Replace("@cliente@", customerId);

                html = html.Replace("@selloSAT@", selloSAT);
                html = html.Replace("@cadenaSAT@", cadenaSAT);
                html = html.Replace("@fechaSAT@", fechaSAT);
                html = html.Replace("@folioSAT@", folioSAT);
                html = html.Replace("@certificadoSAT@", certificadoSAT);

                html = html.Replace("@QRCODE@", "" + folioSerie2 + ".png");

                string cp = postalCode.TrimStart();
                if (cp == "")
                    cp = "NA";
                html = html.Replace("@CP@", cp);

                //html = html.Replace("@cantidad@", convertirNumero(Convert.ToDouble(total), 1, Convert.ToInt32(currencyid)));


                html = html.Replace("@cadena@", cadenaOriginal);
                html = html.Replace("@sello@", sello);
                html = html.Replace("@selloSAT@", selloSAT);
                html = html.Replace("@cadenaSAT@", cadenaSAT);

                fs = new System.IO.FileStream(@"../common/cfd/xml/pagos/" + folioSerie2 + ".html", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                sw = new System.IO.StreamWriter(fs);
                sw.Write(html);
                sw.Flush();
                sw.Close();
                fs.Close();

                PdfConverter pdfConverter = new PdfConverter();
                pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
                pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
                pdfConverter.PdfDocumentOptions.ShowHeader = false;
                pdfConverter.PdfDocumentOptions.ShowFooter = true; ;
                pdfConverter.PdfFooterOptions.FooterText = "";
                pdfConverter.PdfFooterOptions.PageNumberTextFontSize = 6;
                pdfConverter.PdfDocumentOptions.LeftMargin = 5;
                pdfConverter.PdfDocumentOptions.RightMargin = 5;
                pdfConverter.PdfDocumentOptions.TopMargin = 5;
                pdfConverter.PdfDocumentOptions.BottomMargin = 5;
                pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = true;
                //pdfConverter.PdfDocumentOptions.AdjustFontSize = true;
                pdfConverter.PdfFooterOptions.DrawFooterLine = false;
                pdfConverter.PdfFooterOptions.PageNumberText = "Página";
                pdfConverter.PdfFooterOptions.ShowPageNumber = true;
                pdfConverter.LicenseFilePath = AppDomain.CurrentDomain.BaseDirectory;
                pdfConverter.LicenseKey = "Qs/JH60GtPFl2tY2zQQn6IkBRIUJYhvb5QYeFHTLdyysq0+7yoZzTF6nyQo2fWfb";

                string path = Convert.ToString(pdfConverter.LicenseFilePath.ToString().Substring(0, pdfConverter.LicenseFilePath.ToString().Length - 4) + @"common\cfd\xml\pagos\" + folioSerie2 + ".html").ToUpper();
                string fileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"..\common\cfd\xml\pagos\" + folioSerie2 + ".pdf";

                if (loaiza)
                {
                    try
                    {
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.UseShellExecute = false;
                        psi.FileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + "wkhtmltopdf.exe";
                        psi.Arguments = "\"" + path + "\" \"" + fileName + "\"";

                        using (Process proc = Process.Start(psi))
                        {
                            proc.WaitForExit();
                        }
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        byte[] pdf = pdfConverter.GetPdfBytesFromHtmlFile(path);


                        using (System.IO.BinaryWriter binWriter = new System.IO.BinaryWriter(System.IO.File.Open(fileName, System.IO.FileMode.Create)))
                            binWriter.Write(pdf);
                    }
                    catch (Exception ex) { MessageBox.Show("No se pudo generar el PDF\n" + ex.ToString()); }
                }

                //MessageBox.Show("CFDi de Pago Generado, El CFDi será subido al servidor, este proceso puede tomar varios segundos");

                */
                #endregion

                #region Conversion a PDF
                System.Collections.Hashtable htextras = new System.Collections.Hashtable();
                htextras.Add("@logo@", "<img src=\"conf/logoInf.gif\" border=\"0\"  height=\"78\"/>");

                //if (comments != "")
                //    htextras.Add("@comments@", address);
                if (address != "")
                    htextras.Add("@address@", address);
                if (address2 != "")
                    htextras.Add("@address2@", address2);
                if (address3 != "")
                    htextras.Add("@address3@", address3);
                if (phones != "")
                    htextras.Add("@phones@", phones);
                if (webpage != "")
                    htextras.Add("@web_page@", webpage);
                /*
                if (calle != "")
                    htextras.Add("@Direccion@", calle);
                if (col != "")
                    htextras.Add("@Colonia@", col);
                if (loc != "")
                    htextras.Add("@ciudad@", loc);
                if (edo != "")
                    htextras.Add("@estado@", edo);
                if (cp != "")
                    htextras.Add("@CP@", cp);
                */

                ConvertPDFProcess(path, folioSerie2 + ".xml", htextras);

                #endregion Conversion PDF
               

                #endregion Generacion CFDI

                
                path = HttpContext.Current.Request.ApplicationPath;
                if (path.EndsWith("/"))
                    path += "CFD/";
                else
                    path += "/CFD/";

                xmlFile = folioSerie2 + ".xml";
                pdfFile = folioSerie2 + ".pdf";

            }

            #endregion
            
            dh.Commit();
            result = "{\"response\":\"success\",\"message\":\"" + result + "\",\"path\":\"" + path + "\",\"xmlFile\":\"" + xmlFile + "\",\"pdfFile\":\"" + pdfFile + "\",\"serie\":\"" + serie + "\",\"folio\":\"" + PayId + "\"}";
        }
        catch (Exception ex)
        {
            try
            {
                dh.Rollback();
            }
            catch { }
            result = "{\"response\":\"error\",\"message\":\"No se pudo insertar en la base de datos\",\"error\":\"" + General.parseJSON(ex.Message) + "\"}";
            
                
        }
        finally
        {
            dh.Disconnect();
           
        }
        return result;

    }

    public class InvLine
    {
        public string code {get;set;}
        public double quantity {get;set;}
        public string product {get;set;}
        public double price {get;set;}
        public double total {get;set;}
        public string unit {get;set;}
        public string satcode {get;set;}
        public string satunit {get;set;}
        public double tax { get; set; }
        public double discount {get;set;}

    }

    [WebMethod(EnableSession = true)]
    public static string CancelSaleInvoice(string invoiceID)
    {
        Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        string result = "";
        string rs = "";
       
        dh.Connect();
        System.Data.DataTable dtCfg = new System.Data.DataTable();
        string emitRFC = ""; string cerRoute = ""; string keyRoute = ""; string csdPwd = ""; string certSerial = ""; string certData = ""; string timbradoUser = ""; string timbradoPassword = "";
        dh.Query("select * from sale_invoice_config");
        if (dh.exception.Message == "No Exception" & dh.Next())
        {
            dtCfg = dh.DataTable;
            emitRFC = dtCfg.Rows[0]["rfc"].ToString();
            cerRoute = dtCfg.Rows[0]["cer_route"].ToString();
            keyRoute = dtCfg.Rows[0]["key_route"].ToString();
            csdPwd = dtCfg.Rows[0]["csd_password"].ToString();
            certSerial = dtCfg.Rows[0]["cer_serie"].ToString();
            certData = dtCfg.Rows[0]["cer_data"].ToString();
            timbradoUser = dtCfg.Rows[0]["timbrado_user"].ToString();
            timbradoPassword = dtCfg.Rows[0]["timbrado_password"].ToString();

        }

        dh.Query("select uuid,status,serie,code,grand_total,project_id from sale_invoices where sale_invoice_id="+invoiceID+"");
        if (dh.Next() & dh.exception.Message=="No Exception")
        {
            string serie = dh.FieldValue("serie").ToString();
            string folio = dh.FieldValue("code").ToString();
            string uuid = dh.FieldValue("uuid").ToString();
            string total = dh.FieldValue("grand_total").ToString();
            string projectID = dh.FieldValue("project_id").ToString();
            string path = HttpContext.Current.Server.MapPath("~/CFD/");
            try
            {
                dh.Begin();
                dh.Update("UPDATE sale_invoices SET status=5,cancelled_updated=CURRENT_TIMESTAMP WHERE sale_invoice_id=" + invoiceID + "");
                if (dh.exception.Message != "No Exception")
                    throw new Exception(dh.exception.Message);

                RVCFDI33.RVCancelacion.Cancelacion cfdi3 = new RVCFDI33.RVCancelacion.Cancelacion();
                string rutaCanc = path +serie + "-" + folio + "-CANC";

                cfdi3.crearXMLCancelacionArchivo(path+"conf/" + cerRoute, path+"conf/" + keyRoute, csdPwd, uuid, rutaCanc);
                bool produccion = true;
                if (emitRFC == "LAN7008173R5" || emitRFC=="AAA010101AAA")
                    produccion = false;
                cfdi3.enviarCancelacionArchivo(rutaCanc, timbradoUser, timbradoPassword, "http://generacfdi.com.mx/rvltimbrado/service1.asmx?WSDL", produccion);
                if (!string.IsNullOrEmpty(cfdi3.MensajeDeError))
                {
                    throw new Exception(cfdi3.MensajeDeError);
                   
                }
                dh.Commit();
                rs = "success";
                
            }
            catch (Exception ex)
            {
                try { dh.Rollback(); }
                catch { }
                rs=ex.Message.ToString();

            }

        }

        dh.Disconnect();
        //string qts = GetQuotes(ht);
        if (rs == "success")
            result = "{\"response\":\"success\",\"message\":\"success\"}";
        else
            result = "{\"response\":\"error\",\"message\":\"No se pudo cancelar\",\"error\":\"" +  General.parseJSON(rs) + "\"}";

        return result;

    }

    [WebMethod(EnableSession = true)]
    public static string SendInvEmail(string serie, string folio,string email, string customer, string total,string comments)
    {
        string result="";
        string rs = "";
        try
        {
            string subject = "CFDi Folio " + serie + "-" + folio + " generado.";
            string mailBody = "<div style=\"font-family: sans-serif, 'Open Sans'\"> Estimado Cliente, se ha generado una factura electrónica <p>";
            mailBody += "Folio de Factura:      " + serie + "-" + folio + "<br>";
            mailBody += "Cliente:               " + customer + "<br>";
            mailBody += "Monto:                 " + total + "<br>";

            if (comments != "")
            {
                mailBody += "Comentarios:<br>" + comments + "<br>";
            }
            mailBody += "<br>Se han anexado dos archivos, un archivo PDF con la representación del CFDI y un archivo XML que deberá resguardar" +
            " para efectos fiscales.<br>";
            mailBody += "<br>Datos para Transferencia Bancaria.<br>";
            mailBody += "Banco: Scotiabank<br>";
            mailBody += "Empresa: InfnIT Solutions S.A. de C.V.<br>";
            mailBody += "Cuenta: 14501050249<br>";
            mailBody += "CLABE: 044580145010502492<br></div>";
            string dirFullPath = HttpContext.Current.Server.MapPath("~/CFD/");
            string File = dirFullPath + serie + "-" + folio;
            rs= General.sendInvEmail(email, subject, mailBody, File + ".xml", File + ".pdf");
        }
        catch (Exception ex)
        {
            rs = ex.Message;
        }
        if(rs=="success")
            result = "{\"response\":\"success\",\"message\":\"success\"}";
        else
            result = "{\"response\":\"error\",\"message\":\"No se pudo enviar el correo\",\"error\":\"" +  General.parseJSON(rs) + "\"}";
       

        return result;


    }

    [WebMethod(EnableSession = true)]
    public static string SendSPEmail(string serie, string folio, string email, string customer, string total, string comments)
    {
        string result = "";
        string rs = "";
        try
        {
            string subject = "CFDi Complemento de pago Folio " + serie + "-" + folio + " generado.";
            string mailBody = "Estimado Cliente, se ha generado complemento de pagos <p>";
            mailBody += "Folio:      " + serie + "-" + folio + "<br>";
            mailBody += "Cliente:               " + customer + "<br>";
            mailBody += "Monto:                 " + total + "<br>";

            if (comments != "")
            {
                mailBody += "Referencia:<br>" + comments + "<br>";
            }
            
            string dirFullPath = HttpContext.Current.Server.MapPath("~/CFD/");
            string File = dirFullPath + serie + "-" + folio;
            rs = General.sendInvEmail(email, subject, mailBody, File + ".xml", File + ".pdf");
        }
        catch (Exception ex)
        {
            rs = ex.Message;
        }
        if (rs == "success")
            result = "{\"response\":\"success\",\"message\":\"success\"}";
        else
            result = "{\"response\":\"error\",\"message\":\"No se pudo enviar el correo\",\"error\":\"" + General.parseJSON(rs) + "\"}";


        return result;


    }

    public static string ConvertPDFProcess(string path,string xmlFile, System.Collections.Hashtable extras)
    {
        string resp = "";
        Comprobante oComprobante;
        XmlSerializer serialize = new XmlSerializer(typeof(Comprobante));
        using (StreamReader reader = new StreamReader(path+xmlFile))
        {
            oComprobante = (Comprobante)serialize.Deserialize(reader);

            //complementos
            try
            {
                foreach (var oComplemento in oComprobante.Complemento)
                {
                    foreach (var oComplementoInterior in oComplemento.Any)
                    {
                        if (oComplementoInterior.Name.Contains("TimbreFiscalDigital"))
                        {

                            XmlSerializer oSerializerComplemento = new XmlSerializer(typeof(TimbreFiscalDigital));
                            using (var readerComplemento = new StringReader(oComplementoInterior.OuterXml))
                            {
                                oComprobante.TimbreFiscalDigital =
                                    (TimbreFiscalDigital)oSerializerComplemento.Deserialize(readerComplemento);
                            }

                        }
                        if (oComplementoInterior.Name.Contains("Pago"))
                        {
                            XmlSerializer oSerializerComplemento = new XmlSerializer(typeof(Pagos));
                            using (var readerComplemento = new StringReader(oComplementoInterior.OuterXml))
                            {
                                oComprobante.Pagos =
                                    (Pagos)oSerializerComplemento.Deserialize(readerComplemento);
                            }
                        }
                    }
                }

                string conceptos = "";
                string pago = "";
                string pagos = "";
                int cantConcept = 0;
                foreach (var concept in oComprobante.Conceptos)
                {
                    string descImpuesto = "";
                    decimal sbase = 0;
                    decimal tasaOcuota = 0;
                    string pedimentoHtml = "";
                    decimal importe = 0;

                    if (oComprobante.TipoDeComprobante != "P")
                    foreach (var traslado in concept.Impuestos.Traslados)
                    {
                        descImpuesto = traslado.Impuesto;
                        if (traslado.Impuesto == "001")
                            descImpuesto += " ISR";
                        if (traslado.Impuesto == "002")
                            descImpuesto += " IVA";
                        if (traslado.Impuesto == "003")
                            descImpuesto += " IEPS";

                        sbase = traslado.Base;
                        tasaOcuota = traslado.TasaOCuota;
                        importe = traslado.Importe;
                    }
                    if (concept.InformacionAduanera != null)
                        foreach (var info in concept.InformacionAduanera)
                        {
                            pedimentoHtml = " - Pedimento - " +  info.NumeroPedimento;

                        }
                    //if(concept.Descuento>0)

                    if (oComprobante.TipoDeComprobante == "P")
                    {
                        conceptos = "<td class=\"variable\" valign=\"top\">&nbsp;</td>" +
                      "<td class=\"variable\" valign=\"top\">" + concept.Cantidad + "</td>" +
                      "<td class=\"variable\" valign=\"top\">" + concept.Descripcion + "</td>" +
                      "<td class=\"variable\" valign=\"top\">" + concept.ClaveUnidad + "</td> " +
                      "<td class=\"variable\" valign=\"top\">" + concept.ClaveProdServ + "</td>" +
                      "<td class=\"variable\" valign=\"top\">" + concept.ClaveUnidad + "</td> " +
                      "<td class=\"variable\" valign=\"top\">" + concept.ValorUnitario.ToString("c") + "</td>" +
                      "<td class=\"variable\" valign=\"top\">" + concept.Importe.ToString("c") + "</td></tr>";

                    }
                    else
                    {
                        conceptos += "<td class=\"variable\" valign=\"top\">" + concept.NoIdentificacion + "</td>" +
                               "<td class=\"variable\" valign=\"top\">" + concept.Cantidad + "</td>" +
                               "<td class=\"variable\" valign=\"top\">" + concept.Descripcion + "</td>" +
                               "<td class=\"variable\" valign=\"top\">" + concept.Unidad + "</td>" +
                               "<td class=\"variable\" valign=\"top\">" + concept.ClaveProdServ + "</td>" +
                               "<td class=\"variable\" valign=\"top\">" + concept.ClaveUnidad + "</td> " +
                               "<td class=\"variable\" valign=\"top\">" + concept.ValorUnitario.ToString("c") + "</td>" +
                               "<td class=\"variable\" valign=\"top\">" + concept.Importe.ToString("c") + "</td>" +
                               "<td class=\"variable\" valign=\"top\">" + concept.Descuento.ToString("c") + "</td></tr>" +
                               "<tr><td colspan=\"9\"> Clave Prod. Serv. - " + concept.ClaveProdServ + " " + GetProdOServ(concept.ClaveProdServ) + " - Impuestos - Traslados " + descImpuesto + " Base - " + sbase.ToString("c") + " - Tasa - " + tasaOcuota + " - Importe - " + importe + " " + pedimentoHtml + " </td></tr>";
                        cantConcept++;
                    }

                }

                int rowsToAdd = 20;
                if (oComprobante.CfdiRelacionados != null)
                {
                    rowsToAdd -= 2;
                    rowsToAdd -= oComprobante.CfdiRelacionados.CfdiRelacionado.Count();
                }

                if (cantConcept < 15 & oComprobante.TipoDeComprobante!="P")
                {
                    for (int i = (int)(cantConcept * 3); i < rowsToAdd; i++)
                    {
                        conceptos += "<td class=\"variable\" valign=\"top\">&nbsp;</td>" +
                              "<td class=\"variable\" valign=\"top\">&nbsp;</td>" +
                              "<td class=\"variable\" valign=\"top\">&nbsp;</td>" +
                              "<td class=\"variable\" valign=\"top\">&nbsp;</td>" +
                              "<td class=\"variable\" valign=\"top\">&nbsp;</td>" +
                              "<td class=\"variable\" valign=\"top\">&nbsp;</td> " +
                              "<td class=\"variable\" valign=\"top\">&nbsp;</td>" +
                              "<td class=\"variable\" valign=\"top\">&nbsp;</td>" +
                              "<td class=\"variable\" valign=\"top\">&nbsp;</td></tr>";
                    }
                }

                string template="TemplateFactura.html";
                if (oComprobante.TipoDeComprobante == "P")
                    template = "TemplatePago.html";

                FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("~/CFD/")+ "conf/"+template, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                StreamReader sr = new System.IO.StreamReader(fs);
                string html = sr.ReadToEnd();
                sr.Close();
                fs.Close();

                if (extras.ContainsKey("@logo@"))
                    html = html.Replace("@logo@", extras["@logo@"].ToString());
                else
                    html = html.Replace("@logo@", "");
                if (oComprobante.TipoDeComprobante=="E")
                    html = html.Replace("@cfditype@", "EGRESO");
                else
                    html = html.Replace("@cfditype@", "INGRESO");
                html = html.Replace("@certificado@", oComprobante.NoCertificado);
                html = html.Replace("@emit_rfc@", oComprobante.Emisor.Rfc);
                html = html.Replace("@legal_name@", oComprobante.Emisor.Nombre);
                html = html.Replace("@fiscal_regime@", oComprobante.Emisor.RegimenFiscal + " - " + GetFiscalRegime(oComprobante.Emisor.RegimenFiscal));
                html = html.Replace("@cpExp@", oComprobante.LugarExpedicion);

                if (extras.ContainsKey("@address@"))
                    html = html.Replace("@address@", extras["@address@"].ToString());
                else
                    html = html.Replace("@address@", "");
                if (extras.ContainsKey("@address2@"))
                    html = html.Replace("@address2@", extras["@address2@"].ToString());
                else
                    html = html.Replace("@address2@", "");
                if (extras.ContainsKey("@address3@"))
                    html = html.Replace("@address3@", extras["@address3@"].ToString());
                else
                    html = html.Replace("@address3@", "");
                if (extras.ContainsKey("@phones@"))
                    html = html.Replace("@phones@", extras["@phones@"].ToString());
                else
                 html = html.Replace("@phones@", "");
                if (extras.ContainsKey("@web_page@"))
                    html = html.Replace("@web_page@", extras["@web_page@"].ToString());
                else
                    html = html.Replace("@web_page@", "");

                if(extras.ContainsKey("@cliente@"))
                    html = html.Replace("@cliente@", extras["@cliente@"].ToString());
                else
                    html = html.Replace("@cliente@", "");
                if (extras.ContainsKey("@OrdenCompra@"))
                    html = html.Replace("@OrdenCompra@", extras["@OrdenCompra@"].ToString());
                else
                    html = html.Replace("@OrdenCompra@", "");

                if (extras.ContainsKey("@Direccion@"))
                    html = html.Replace("@Direccion@", extras["@Direccion@"].ToString());
                else
                    html = html.Replace("@Direccion@", "");
                if (extras.ContainsKey("@ciudad@"))
                    html = html.Replace("@ciudad@", extras["@ciudad@"].ToString());
                else
                    html = html.Replace("@ciudad@", "");
                if (extras.ContainsKey("@estado@"))
                    html = html.Replace("@estado@", extras["@estado@"].ToString());
                else
                    html = html.Replace("@estado@", "");
                if (extras.ContainsKey("@Colonia@"))
                    html = html.Replace("@Colonia@", extras["@Colonia@"].ToString());
                else
                    html = html.Replace("@Colonia@", "");
                if (extras.ContainsKey("@CP@"))
                    html = html.Replace("@CP@", extras["@CP@"].ToString());
                else
                    html = html.Replace("@CP@", "");


                html = html.Replace("@conceptos@", conceptos);
                if (extras.ContainsKey("@comments@"))
                    html = html.Replace("@comments@", extras["@comments@"].ToString());
                else
                    html = html.Replace("@comments@", "");
                html = html.Replace("@folio@", oComprobante.Serie + "-" + oComprobante.Folio);
                html = html.Replace("@fecha@", oComprobante.TimbreFiscalDigital.FechaTimbrado.ToString("yyyy/MM/dd HH:mm:ss"));
                html = html.Replace("@RFC@", oComprobante.Receptor.Rfc);
                html = html.Replace("@Nombre@", oComprobante.Receptor.Nombre);

                string cadSat = "||" + oComprobante.TimbreFiscalDigital.Version + "|" + oComprobante.TimbreFiscalDigital.UUID + "|" + oComprobante.TimbreFiscalDigital.FechaTimbrado.ToString("yyyy-MM-ddTHH:mm:ss" + "|" + oComprobante.TimbreFiscalDigital.SelloCFD + "|" + oComprobante.TimbreFiscalDigital.NoCertificadoSAT + "||");
                html = html.Replace("@selloSAT@", oComprobante.TimbreFiscalDigital.SelloSAT);
                html = html.Replace("@sello@", oComprobante.TimbreFiscalDigital.SelloCFD);
                html = html.Replace("@cadenaSAT@", cadSat);
                html = html.Replace("@fechaSAT@", oComprobante.TimbreFiscalDigital.FechaTimbrado.ToString("yyyy/MM/dd HH:mm:ss"));
                html = html.Replace("@folioSAT@", oComprobante.TimbreFiscalDigital.UUID);
                html = html.Replace("@certificadoSAT@", oComprobante.TimbreFiscalDigital.NoCertificadoSAT);


                html = html.Replace("@QRCODE@", oComprobante.QR);

                html = html.Replace("@conceptos@", conceptos);
                html = html.Replace("@subtotal@", oComprobante.SubTotal.ToString("c"));
                html = html.Replace("@descuento@", oComprobante.Descuento.ToString("c"));
                if(oComprobante.Impuestos!=null) html = html.Replace("@iva@", oComprobante.Impuestos.TotalImpuestosTrasladados.ToString("c"));
                html = html.Replace("@total@", oComprobante.Total.ToString("c"));
                html = html.Replace("@cantidad@", oComprobante.MonedaConLetra);
                html = html.Replace("@tasa_iva@", "16.0000");
                html = html.Replace("@moneda@", oComprobante.Moneda);

                if (oComprobante.Moneda=="USD")
                {
                    html = html.Replace("@labeltcambio@", " Tipo de cambio: ");
                    html = html.Replace("@tcambio@", oComprobante.TipoCambio.ToString("n2"));
                    if (extras.ContainsKey("@fechatc@"))
                    {
                        html = html.Replace("@labelfechatc@", " Fecha tipo de cambio: ");
                        html = html.Replace("@fechatc@", extras["@fechatc@"].ToString());
                    }
                    else
                    {
                        html = html.Replace("@labelfechatc@", "");
                        html = html.Replace("@fechatc@", "");
                    }
                }
                else
                {
                    html = html.Replace("@labeltcambio@", "");
                    html = html.Replace("@tcambio@", "");
                    html = html.Replace("@labelfechatc@", "");
                    html = html.Replace("@fechatc@", "");
                }

                html = html.Replace("@pay_method@", oComprobante.FormaPago + " " + GetPayForm(oComprobante.FormaPago));
                //html = html.Replace("@acc_number@",oComprobante.FormaPagoSpecified.ToString());

                html = html.Replace("@p_method@", oComprobante.MetodoPago + " " + GetPayMethod(oComprobante.MetodoPago));
                html = html.Replace("@cfdi_usage@", oComprobante.Receptor.UsoCFDI + " " + GetCFDIUsage(oComprobante.Receptor.UsoCFDI));
                html = html.Replace("@condiciones@", oComprobante.CondicionesDePago);

                if (oComprobante.CfdiRelacionados != null)
                {
                    html = html.Replace("@tiporelacion@", oComprobante.CfdiRelacionados.TipoRelacion + " - " + GetTipoRelacion(oComprobante.CfdiRelacionados.TipoRelacion));
                    html = html.Replace("@relstyle@", "");
                    string cfdiRel = "";
                    foreach (var comp in oComprobante.CfdiRelacionados.CfdiRelacionado)
                    {
                        cfdiRel += "<td class=\"variable\" valign=\"top\">" +comp.UUID + "</td>" +
                              "</tr>";

                    }
                    html = html.Replace("@cfdirel@", cfdiRel);
                }
                else
                {
                    html = html.Replace("@relstyle@", "display:none");
                }

                if (oComprobante.Pagos != null)
                {
                    foreach (var Pago in oComprobante.Pagos.Pago)
                    {
                        //Pago.DoctoRelacionado.
                        pago = "<td class=\"variable\" valign=\"top\">" + Pago.FechaPago + "</td>" +
                         "<td class=\"variable\" valign=\"top\">" + GetPayForm(Pago.FormaDePagoP.ToString().Replace("Item","")) + "</td>" +
                         "<td class=\"variable\" valign=\"top\">" + Convert.ToDouble(Pago.Monto).ToString("c") + "</td>" +
                         "<td class=\"variable\" valign=\"top\">" + Pago.TipoCambioP + "</td>" +
                         "</tr>";

                        foreach (var docto in Pago.DoctoRelacionado)
                        {

                            pagos += "<td class=\"variable\" valign=\"top\">" + docto.IdDocumento + "</td>" +
                                 "<td class=\"variable\" valign=\"top\">" + docto.Serie + "-" + docto.Folio + "</td>" +
                                 "<td class=\"variable\" valign=\"top\">" + docto.MetodoDePagoDR + "</td>" +
                                 "<td class=\"variable\" valign=\"top\">" + Convert.ToDouble(docto.ImpPagado).ToString("c") + "</td>" +
                                 "<td class=\"variable\" valign=\"top\">" + docto.ImpSaldoAnt.ToString("c") + "</td>" +
                                 "<td class=\"variable\" valign=\"top\">" + docto.ImpSaldoInsoluto.ToString("c") + "</td> " +
                                 "<td class=\"variable\" valign=\"top\">" + docto.ImpPagado.ToString("c") + "</td>" +
                                 "<td class=\"variable\" valign=\"top\">" + docto.MonedaDR + "</td>" +
                                 "</tr>";
                        }


                    }
                }

                html = html.Replace("@pago@",pago);
                html = html.Replace("@pagos@", pagos);
             
              
                System.IO.File.WriteAllText(path+xmlFile.Replace(".xml", ".html"), html);

                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.UseShellExecute = false;
                    psi.FileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + "wkhtmltopdf.exe";
                    psi.Arguments = path+xmlFile.Replace(".xml", ".html") + " " + path+xmlFile.Replace(".xml", ".pdf");
                    resp = path + xmlFile.Replace(".xml", ".pdf");
                    using (Process proc = Process.Start(psi))
                    {
                        proc.WaitForExit();
                    }
                }
                catch { }

            }
            catch (Exception ex) { resp = "error,El Xml no cuenta con complemento"; }
        }

        return resp;
    }

    [WebMethod(EnableSession = true)]
    public static string ConvertPDF(string uuid)
    {
        
        string resp = "";
        string dirFullPath = HttpContext.Current.Server.MapPath("~/MediaUploader/");
        string xmlFile = dirFullPath + uuid + ".xml";//System.IO.File.ReadAllText(dirFullPath + uuid + ".xml");
        resp = ConvertPDFProcess(dirFullPath, uuid+".xml",new System.Collections.Hashtable());
        resp=resp.Replace(HttpContext.Current.Server.MapPath("~"),"").Replace("\\","/");
        return resp;
    }

    [WebMethod(EnableSession = true)]
    public static string RecOrder(string uuid)
    {
        string resp = "";
        Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        dh.Connect();
        resp=RecOrderProcess(dh,uuid);
        dh.Disconnect();
        return resp;
    }

    public static string RecOrderProcess(Hzone.Api.Database.DataHelper dh,string uuid)
    {
        string resp = "";
        string template = "FormatoRecoleccion.html";
        string path = HttpContext.Current.Server.MapPath("~/MediaUploader/");
        FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("~/CFD/") + "conf/" + template, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        StreamReader sr = new System.IO.StreamReader(fs);
        string html = sr.ReadToEnd();

        
        dh.Query("select * from subcustomer_guides where subcustomer_guide_id=" + uuid + "");
        if (dh.Next())
        {
            
            if (dh.FieldValue("destination_type").ToString() == "DOMICILIO")
            {
                html = html.Replace("@domtype@", "X");
                html = html.Replace("@ocurretype@", "");
            }
            else
            {
                html = html.Replace("@domtype@", "");
                html = html.Replace("@ocurretype@", "X");
            }


            html = html.Replace("@destination@", dh.FieldValue("city")+","+dh.FieldValue("state"));

            html = html.Replace("@recolection_date@", Convert.ToDateTime(dh.FieldValue("recolection_date")).ToString("dd-MMM-yyyy"));
            html = html.Replace("@custref@", HttpContext.Current.Session["customerid"].ToString());
            html = html.Replace("@customer_name@", HttpContext.Current.Session["nomusuario"].ToString().ToUpper());

            html = html.Replace("@guide@", dh.FieldValue("guide").ToString());
            html = html.Replace("@custid@", dh.FieldValue("custid").ToString());

            html = html.Replace("@solicitant@", dh.FieldValue("solicitant").ToString());
            html = html.Replace("@schedule@", dh.FieldValue("schedule").ToString());
            
            html = html.Replace("@caddress@", dh.FieldValue("caddress").ToString());
            html = html.Replace("@cneighborhood@", dh.FieldValue("cneighborhood").ToString());
            html = html.Replace("@cstate@", dh.FieldValue("cstate").ToString());
            html = html.Replace("@ccity@", dh.FieldValue("ccity").ToString());
            html = html.Replace("@czipcode@", dh.FieldValue("czipcode").ToString());
            html = html.Replace("@creference@", dh.FieldValue("creference").ToString());
            html = html.Replace("@ccontact@", dh.FieldValue("ccontact").ToString());
            html = html.Replace("@cphone@", dh.FieldValue("cphone").ToString());

            html = html.Replace("@name@", dh.FieldValue("name").ToString());
            html = html.Replace("@address@", dh.FieldValue("address").ToString());
            html = html.Replace("@neighborhood@", dh.FieldValue("neighborhood").ToString());
            html = html.Replace("@state@", dh.FieldValue("state").ToString());
            html = html.Replace("@city@", dh.FieldValue("city").ToString());
            html = html.Replace("@zipcode@", dh.FieldValue("zipcode").ToString());
            html = html.Replace("@contact@", dh.FieldValue("contact").ToString());
            html = html.Replace("@phone@", dh.FieldValue("phone").ToString());

            html = html.Replace("@packtype@", dh.FieldValue("packtype").ToString());

            html = html.Replace("@content@", dh.FieldValue("content").ToString());
            html = html.Replace("@weight@", dh.FieldValue("weight").ToString());
            html = html.Replace("@length@", dh.FieldValue("length").ToString());
            html = html.Replace("@width@", dh.FieldValue("width").ToString());
            html = html.Replace("@height@", dh.FieldValue("height").ToString());

            html = html.Replace("@insured@", dh.FieldValue("insured").ToString());
            html = html.Replace("@value@", dh.FieldValue("value").ToString());

        }
        sr.Close();
        fs.Close();
        System.IO.File.WriteAllText(path + uuid + ".html", html);

        try
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.UseShellExecute = false;
            psi.FileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + "wkhtmltopdf.exe";
            psi.Arguments = "-O landscape " + path + uuid + ".html" + " " + path + uuid + ".pdf";
            resp = path + uuid + ".pdf";
            using (Process proc = Process.Start(psi))
            {
                proc.WaitForExit();
            }
        }
        catch { }

        resp = resp.Replace(HttpContext.Current.Server.MapPath("~"), "").Replace("\\", "/");
        return resp;
    }

    public static string GetProdOServ(string prod)
    {
        string res = "";
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        DataHelper.Connect();
        DataHelper.Query("select name from sat_catalog where code like '" + prod + "'");
        if (DataHelper.Next())
            res = DataHelper.FieldValue("name").ToString();
        DataHelper.Disconnect();
        return res;
    }

    public static string GetCFDIUsage(string usage)
    {
        string res = "";
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        DataHelper.Connect();
        DataHelper.Query("select name from cfdi_usage where code like '" + usage + "'");
        if (DataHelper.Next())
            res= DataHelper.FieldValue("name").ToString();
        DataHelper.Disconnect();
        return res;
    }
    public static string GetFiscalRegime(string regime)
    {
        string res = "";
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        DataHelper.Connect();
        DataHelper.Query("select name from cfdi_fiscal_regime where code like '" + regime + "'");
        if (DataHelper.Next())
            res = DataHelper.FieldValue("name").ToString();
        DataHelper.Disconnect();
        return res;
    }
    public static string GetPayMethod(string paymethod)
    {
        string res = "";
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        DataHelper.Connect();
        DataHelper.Query("select name from pay_method where code like '" + paymethod + "'");
        if (DataHelper.Next())
            res = DataHelper.FieldValue("name").ToString();
        DataHelper.Disconnect();
        return res;
    }
    public static string GetPayForm(string payform)
    {
        string res = "";
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        DataHelper.Connect();
        DataHelper.Query("select name from payment_method where code like '" + payform + "'");
        if (DataHelper.Next())
            res = DataHelper.FieldValue("name").ToString();
        DataHelper.Disconnect();
        return res;
    }
    public static string GetTipoRelacion(string relation)
    {
        string res = "";
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        DataHelper.Connect();
        DataHelper.Query("select name from cfdi_tipo_relacion where code like '" + relation + "'");
        if (DataHelper.Next())
            res = DataHelper.FieldValue("name").ToString();
        DataHelper.Disconnect();
        return res;
    }
    public static string GetCurrencyCode(int currencyID, Hzone.Api.Database.DataHelper DataHelper)
    {
        string res = "";
        DataHelper.Query("select code from currency where currency_id = " + currencyID + "");
        if (DataHelper.Next())
            res = DataHelper.FieldValue("code").ToString();
        return res;
    }

    [WebMethod(EnableSession = true)]
    public static string GetSaleInvCatalogs()
    {
        string result = "{\"currency\": ";
        result+=  GetCurrency();
        result += ",{\"state\": ";
        //result+= GetStates();
        result += " }";
        
        
        return result; 
    }

    [WebMethod(EnableSession = true)]
    public static string GetCatalog(string control,string defaultValue,string table,string id,string val,string val2,string condition,string order,string extraparam)
    {

        string res = "";
        System.Data.DataTable dt = new System.Data.DataTable();
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        DataHelper.Connect();
        string val2str = "";
        string extraparamstr = "";
        if (condition != "")
            condition = " AND " + condition;
        if (order != "")
            order = " " + order;
        if (val2 != "")
            val2str = "," + val2;
        if (extraparam != "")
            extraparamstr = extraparam;
        DataHelper.Query("select "+id+" ,"+val+" "+val2str+" from "+table+" WHERE 1>0 " + condition + order );
        res = "{";        
        res += "\"defaultValue\":\"" + defaultValue+"\",";
        res += "\"control\":\"" + control + "\",";
        res += "\"error\":\"" + General.parseJSON(DataHelper.exception.Message) + "\",";
        res += "\"extraparam\":\"" + extraparamstr + "\",";
        res += "\"catalog\":[";
        res += "{\"id\":\"-1\",\"name\":\"Seleccione\"},";
        while (DataHelper.Next())
        {
            string val2json = "";
            if (val2 != "")
                val2json = ",\"data\":\"" + DataHelper.FieldValue(val2).ToString() + "\"";
            res += "{\"id\":\"" + DataHelper.FieldValue(id).ToString() + "\",\"name\":\"" + DataHelper.FieldValue(val) + "\""+val2json+"},";
        }
        res = res.Remove(res.LastIndexOf(","), 1);
        DataHelper.Disconnect();
        res += "]}";
        return res;
    }
    
    public static string GetCurrency()
    {
        string res = "";
        System.Data.DataTable dt = new System.Data.DataTable();
        Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
        DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        DataHelper.Connect();
        DataHelper.Query("select -1 as currency_id,'Seleccione' as code UNION ALL select currency_id,code from currency order by currency_id");
        res="{\"catalog\":[";
        while (DataHelper.Next())
            res += "{\"currency_id\":\"" + DataHelper.FieldValue("currency_id").ToString() + "\",\"code\":\"" + DataHelper.FieldValue("code") + "\"},";
        res=res.Remove(res.LastIndexOf(","),1);
        DataHelper.Disconnect();
        res+="]}";
        return res;
    }

    [WebMethod(EnableSession = true)]
    public static string InsertChat(object userid,object message)
    {
        Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        string userID = userid.ToString();
        string result = "";
        string destUserID = "3";
        if (userID == "3")
            destUserID = "4";
            
        dh.Connect();

        dh.Update("INSERT INTO chat (chat_id,origin_user_id,dest_user_id,description,created_by,updated_by,created,updated) VALUES "+
        "( Nextval('sq_chats'), "+userid+","+destUserID+",'"+message+"',"+userid+","+userid+",now(),now())");
        if (dh.exception.Message != "No Exception")
            result = "{\"response\":\"error\",\"message\":\"No se pudo insertar en la base de datos\",\"error\":\"" +  General.parseJSON(dh.exception.Message) + "\"}";
        else
            result = "{\"response\":\"success\",\"message\":\"" +userid + "\"}";
        dh.Disconnect();

        return result;


    }

    [WebMethod(EnableSession = true)]
    public static string GetChat(Form[] form)
    {
        Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        string userid = "" + HttpContext.Current.Session["userid"];
        string username= "" + HttpContext.Current.Session["nomusuario"];
        string userBadge= username.Substring(0,1);
        string result = "";
        dh.Connect();

        dh.Query("SELECT origin_user_id,c.description,c.created,u.name as destname from chat c INNER JOIN users u ON u.user_id=origin_user_id where origin_user_id=" + userid + " OR dest_user_id="+userid+" ORDER BY c.created");
        if (dh.exception.Message != "No Exception")
            result = "No se pudieron cargar los chats: " + dh.exception.Message;
        for (int i = 0; i < dh.DataTable.Rows.Count; i++)
        {
            string origUser=dh.DataTable.Rows[i]["origin_user_id"].ToString();
            string destUser=dh.DataTable.Rows[i]["destname"].ToString();
            string message=dh.DataTable.Rows[i]["description"].ToString();
            string destUserBadge = destUser.Substring(0, 1);
            if (userid == origUser)
            {
                result += "<div class=\"kt-chat__message kt-chat__message--brand kt-chat__message--right\">"+
                    "<div class=\"kt-chat__user\">" +
                    "<div class=\"kt-badge kt-badge--md kt-badge--brand\">"+userBadge+"</div>"+
                    "<a href=\"\" class=\"kt-chat__username\">"+username+"</span></a>" +
                    "<span class=\"kt-chat__datetime\">"+Convert.ToDateTime(dh.DataTable.Rows[i]["created"]).ToString("dd-MMM-yy hh:mm:ss tt")+"</span>" +
				"</div>" +
				"<div class=\"kt-chat__text kt-bg-light-brand\">" +
					message +
                "</div>"+
                "</div>";
            }
            else
            {
                result += "<div class=\"kt-chat__message kt-chat__message--success kt-chat__message\">" +
                   "<div class=\"kt-chat__user\">" +
                   "<div class=\"kt-badge kt-badge--md kt-badge--brand\">" + destUserBadge + "</div>" +
                   "<a href=\"\" class=\"kt-chat__username\">" + destUser + "</span></a>" +
                   "<span class=\"kt-chat__datetime\">" + Convert.ToDateTime(dh.DataTable.Rows[i]["created"]).ToString("dd-MMM-yy hh:mm:ss tt") + "</span>" +
               "</div>" +
               "<div class=\"kt-chat__text kt-bg-light-brand\">" +
                   message +
               "</div>" +
               "</div>";
            }
        }
        dh.Disconnect();

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
        DataHelper.Query("SELECT bp.legal_code,usr.login_name,bp.description FROM business_partners bp inner join users usr on bp.bpartner_id=usr.bpartner_id where usr.usr_id = '" + userid + "';");

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

        string url = "http://166.62.93.54/ProconecttApi/Configuracion/GetParameters?ClienteRFC=" + clienteRFC.ToString() + "&EmpresaRFC=" + empresaRFC.ToString() + "&Cuenta=" + cuenta.ToString() + "";
        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            result = streamReader.ReadToEnd();
        }

        return result;
  }



}