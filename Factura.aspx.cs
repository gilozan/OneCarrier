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


public partial class Factura : System.Web.UI.Page
{

    public double tax = 1.16;

    string userSession;
    string passwordSession;
    public string result = "";
    public Bnet.Api.Database.DataHelper dh = new Bnet.Api.Database.DataHelper();


    public string origdate = DateTime.Today.ToString("yyyy-MM-dd");
    
    public string osid = "";
    public string btnFinishSO = "";
    public string cname = "";
    public string custid = "";
    public string lcode = "";
    public string bpartnerid = "";
    public string soid = "-1";
    public string soiddisp = "";
    public string addressc = "";
    public string date = "";
    public string tel = "";
    public string email = "";
    public string comment = "";
    public string details = "";
    public string vehicle = "";
    public string showdet = "";
    public string totalregsch = "0";
    public string schname = "";
    public string schpid = "";
    public string schcode = "";
    public string schbrand = "";
    public string cmbpaymenttype = "";
    public string payamount = "";
    public string payamount2 = "";
    public string cmbpaymenttype2 = "";
    public string ptable = "";
    public string addprotable = "";
    public string downloadCfdi = "";
    public string storeid = "";
    public string techname = "";
    public int statusId = 0;
    public string paymenttype = "";
    public string status = "";

    public string paymethod = "";
    public string payacc = "NO IDENTIFICADA";
    public string legalname = "";
    public string legalcode = "";

    public string calle = "";
    public string no = "";
    public string col = "";
    public string edo = "";
    public string loc = "";
    public string cp = "";
    public string noint = "";
    public string intstr = "INT";

    public string edoid = "";
    public string locid = "";

    public string prods = "";
    public string totalsale = "";
    public string totalord = "";
    public double ivatotal = 0;
    public double grandtotal = 0;

    public string ostable = ""; 

    public string totalreg = "0";
    public string qryext = "-1";

    public string serie = "A";
    public string expedicion = "";
    public string sCadena="";

    public string amountText = "";
    public string onlysoid = "0";
    public string info = "";
    public string dtsoid = "";

    public string uscfd = "";
    public string pmcnd = "PUE";

    public string tipAmt = "0";

    string cerRoute = "";
    string keyRoute = "";
    string origSerie = "";
    int initFolio = -1;
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

    public ArrayList arrProds= new ArrayList();
    public string[] prod;


    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;


        soid = Request.QueryString["soid"];
        custid = Request.QueryString["custid"];
        dtsoid = Request.QueryString["dtsoid"];
        lcode = Request.QueryString["lcode"];

        if(string.IsNullOrEmpty(custid))
            custid = Request.Form["custid"];
        if (string.IsNullOrEmpty(dtsoid))
            dtsoid = Request.Form["dtsoid"];
        if (string.IsNullOrEmpty(soid))
            soid = Request.Form["soid"];
        if (string.IsNullOrEmpty(lcode))
            lcode = Request.Form["lcode"];

        onlysoid = Request.QueryString["onlysoid"];

        legalcode = Request.Form["trfc"];
        legalname = Request.Form["trazon"];
        email = Request.Form["email"];
        calle= Request.Form["calle"];
        no = Request.Form["noext"];
        noint = Request.Form["noint"];
        col = Request.Form["col"];
        locid = Request.Form["cmbLoc"];
        edoid = Request.Form["CmbEdo"];

        uscfd = Request.Form["cmbUso"];

        cp = Request.Form["cp"];
        payacc  = Request.Form["ctapago"];
        if(string.IsNullOrEmpty(payacc))
            payacc = "NO IDENTIFICADA";


        dh.ConnectionType = Bnet.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Maver"].ConnectionString;

        try
        {



            dh.Connect();

            //Obtener valores desde la configuración de factura
            #region Valores Config Factura
            System.Data.DataTable dtCfg = new System.Data.DataTable();
            dh.Query("select * from sale_invoice_config");
            if (this.dh.exception.Message == "No Exception" & this.dh.Next())
            {
                dtCfg = this.dh.DataTable;
                cerRoute = dtCfg.Rows[0]["cer_route"].ToString();
                keyRoute = dtCfg.Rows[0]["key_route"].ToString();
                origSerie = dtCfg.Rows[0]["serie"].ToString();
                serie = origSerie;
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

            #endregion Valores Config Fact

            if (!Page.IsPostBack)
            {
                dh.Query("select * from  states where country_id=1");
                cmbEdo.DataValueField = "state_id";
                cmbEdo.DataTextField = "state_code";
                cmbEdo.DataSource = dh.DataTable;
                cmbEdo.DataBind();

                dh.Query("select * from cfdi_usage");
                cmbUso.DataValueField = "code";
                cmbUso.DataTextField = "name";
                cmbUso.DataSource = dh.DataTable;
                cmbUso.DataBind();

                if (string.IsNullOrEmpty(uscfd))
                {
                    uscfd = "G03";
                    cmbUso.SelectedValue = uscfd;
                }

                

            }
            else
            {
                if (!string.IsNullOrEmpty(dtsoid))
                {
                    if (dtsoid.Contains("-") & dtsoid.Length>2) { }
                    else
                    {
                        cname = "<font color=\"salmon\">Favor de ingresar Código de Facturación válido</font>";
                        return;
                    }
                }
                else
                {
                    cname = "<font color=\"salmon\">Favor de ingresar Código de Facturación válido</font>";
                    return;
                }

                if (string.IsNullOrEmpty(lcode))
                {
                    cname = "<font color=\"salmon\">Favor de ingresar RFC</font>";
                    return;
                }
            }

               

            if (!string.IsNullOrEmpty(soid))
            {
                string xtraQry="";
                string custqry = "";
                if (string.IsNullOrEmpty(custid))
                    custqry = "-1";
                else
                    custqry = custid;
                if(onlysoid!="1")
                    xtraQry="AND so.customer_id=" +custqry + " AND TO_CHAR(so.created,'YYYY-MM-DD')='"+dtsoid+"'";

                dh.Query("select sale_invoice_id,code from sale_invoices where project_id=" + soid + " AND status<>'5' and sello is not null ");
                bool validateOKS = true;
                if (dh.exception.Message == "No Exception")
                {
                    if (dh.DataTable.Rows.Count > 0)
                    {
                        //string sale_invoice_id = dh.DataTable.Rows[0]["sale_invoice_id"].ToString();
                        //this.dh.Query("select sello from sale_invoices where sale_invoice_id=" + sale_invoice_id + "");
                        //if (dh.exception.Message == "No Exception")
                        //{
                        //    if (this.dh.DataTable.Rows[0]["sello"].ToString() != "")
                        //    {
                                validateOKS = false;
                        //    }
                        //}
                    }//End count invoices
                }

                if (validateOKS)
                {
                    dh.Query("select order_id, customer_id, invoice_code,pch_order from Orders where order_id=" + soid + "");
                    if (dh.Next())
                    {
                        if (dh.FieldValue("invoice_code").ToString() != "")
                        {
                            downloadCfdi = "<font color=\"salmon\">El ticket ya fue integrado en la factura global y no puede ser facturado</font>";
                            return;
                        }
                    }
                }

                System.Data.DataTable dtOS = new System.Data.DataTable();   
                #region detalle de la OS
                dh.Query("select c.bpartner_id,c.customer_id,c.name as cname,b.code, b.phones, b.cellphone, b.address,b.no_ext,b.no_int, b.neighborhood,b.postal_code,b.state_id, b.emails" +
                ",ci.city_name as city, st.state_code as state,b.city_id,b.state_id " +
                ",b.legal_name,b.legal_code" +
                " from customers c " +
                "INNER JOIN business_partners b ON (c.bpartner_id = b.bpartner_id) " +
                "LEFT JOIN cities ci ON (ci.city_id = b.city_id) " +
                "INNER JOIN states st ON (st.state_id = b.state_id) " +
                " where lower(b.legal_code)= '" + lcode.ToLower() + "' ");
                if (dh.Next())
                {
                    dtOS = dh.DataTable;
                    
                    cname = dtOS.Rows[0]["cname"].ToString();
                    tel = dtOS.Rows[0]["phones"] + " Cel: " + dtOS.Rows[0]["cellphone"];
                    if(string.IsNullOrEmpty(email))
                        email = dtOS.Rows[0]["emails"].ToString();
                    
                    addressc = dtOS.Rows[0]["address"].ToString();
                    if (string.IsNullOrEmpty(calle))
                    calle = dtOS.Rows[0]["address"].ToString();
                    if (string.IsNullOrEmpty(no))
                    no = dtOS.Rows[0]["no_ext"].ToString();
                    if (string.IsNullOrEmpty(noint))
                    noint = dtOS.Rows[0]["no_int"].ToString();
                    if (string.IsNullOrEmpty(col))
                    col =  dtOS.Rows[0]["neighborhood"].ToString();
                    if (string.IsNullOrEmpty(locid))
                    {
                        loc = dtOS.Rows[0]["city"].ToString();
                        edo = dtOS.Rows[0]["state"].ToString();
                        cmbEdo.SelectedValue = dtOS.Rows[0]["state_id"].ToString();
                        if (cmbLoc.DataSource == null)
                        {
                            dh.Query("select * from  cities where state_id=" + cmbEdo.SelectedValue + "");
                            cmbLoc.DataValueField = "city_id";
                            cmbLoc.DataTextField = "city_name";
                            cmbLoc.DataSource = dh.DataTable;
                            cmbLoc.DataBind();
                        }
                        cmbLoc.SelectedValue = dtOS.Rows[0]["city_id"].ToString();
                    }
                    else
                    {
                        cmbEdo.SelectedValue = edoid;
                        dh.Query("select * from  cities where state_id=" + cmbEdo.SelectedValue + "");
                        cmbLoc.DataValueField = "city_id";
                        cmbLoc.DataTextField = "city_name";
                        cmbLoc.DataSource = dh.DataTable;
                        cmbLoc.DataBind();
                        try
                        {
                            cmbLoc.SelectedValue = locid;
                        }
                        catch { }
                    }
                    
                    if (string.IsNullOrEmpty(cp))
                    cp =dtOS.Rows[0]["postal_code"].ToString();
                    
                    soiddisp = soid;
                    storeid = "1";
                    //statusId = Convert.ToInt32(dtOS.Rows[0]["status_id"]);
                    //status = dtOS.Rows[0]["status"].ToString();
                    //paymenttype = dtOS.Rows[0]["payname"].ToString();

                    if (string.IsNullOrEmpty(custid))
                        custid = dtOS.Rows[0]["customer_id"].ToString();
                    bpartnerid = dtOS.Rows[0]["bpartner_id"].ToString();
                    if(string.IsNullOrEmpty(legalname))
                        legalname = dtOS.Rows[0]["legal_name"].ToString();
                    if (string.IsNullOrEmpty(legalcode))
                        legalcode = dtOS.Rows[0]["legal_code"].ToString();


                    //paymethod  = dtOS.Rows[0]["paymethod"].ToString();

                    //Gilo, 2-Ene-2018 Para traer el usoCFDI desde la factura
                    dh.Query("select sale_invoice_id,code,COALESCE(cfdi_usage_id,'P01') as cfdi_usage_id, COALESCE(pay_method_cond,'PUE') as pay_method_cond from sale_invoices where project_id=" + soid + " AND status<>'5' ");
                    if (dh.exception.Message == "No Exception")
                    {
                        if (dh.DataTable.Rows.Count > 0)
                        {
                            //string flio = "";
                            if (string.IsNullOrEmpty(uscfd))
                            {
                                uscfd = dh.DataTable.Rows[0]["cfdi_usage_id"].ToString();
                                cmbUso.SelectedValue = uscfd;
                            }
                            pmcnd = dh.DataTable.Rows[0]["pay_method_cond"].ToString();
                        }
                    }

                }
                #endregion
                DateTime ocreat= DateTime.Now;
                dh.Query("select pt.name,pt.code,pt.payment_type_id,pl.created from payment_lines pl INNER JOIN payment_type pt ON pt.payment_type_id=pl.payment_type_id where order_id=" + soid + "");
                if (dh.exception.Message == "No Exception")
                {
                    if (dh.DataTable.Rows.Count > 0)
                    {
                        paymenttype = dh.DataTable.Rows[0]["name"].ToString();
                        paymethod = dh.DataTable.Rows[0]["code"].ToString();
                        if(lcode!="" & legalcode=="")
                            legalcode = lcode;
                        ocreat = Convert.ToDateTime(dh.DataTable.Rows[0]["created"]);
                    }

                    for (int i = 0; i < dh.DataTable.Rows.Count; i++)
                    {
                        if (this.dh.DataTable.Rows[i]["payment_type_id"].ToString() == "10")
                        {
                            cname = "<font color=\"salmon\">No se puede facturar con ese tipo de pago</font>";
                            return;
                        }
                    }

                }


                #region desplegado productos de la OS
                bool prodsadded = false;
                double totalorder = 0;
                double subtotal=0;
                double total=0;
                //dh.Query("select code,qty_delivered,product_name,unitary_cost,extended_cost,sat_code,unit,cve_unit,tax_percent,product_id FROM (  "+
                //   "SELECT ol.product_code as code, qty_ordered as qty_delivered, ol.name as product_name,(list_price- round( (discount_amount)/1.16,2)) as unitary_cost, total_line as extended_cost,p.sat_code,u.name as unit,u.code as cve_unit,tx.tax_percent*100 as tax_percent,p.product_id,1 as ordr from order_lines ol  INNER JOIN products p ON  p.product_id=ol.product_id  INNER JOIN units u ON p.unit_id=u.unit_id INNER JOIN taxes tx ON tx.tax_id=p.tax_id where ol.mode=1 AND hidden is null AND ol.order_id= "+soid+" " +
                //   " UNION ALL select UPPER('PRP') as code,1 as qty_delivered,upper('PROPINA') as product_name, COALESCE(sum(tip_amount),0) as unitary_cost,COALESCE(sum(tip_amount),0) as extended_cost,'90101501' as sat_code,'SERV' as unit, upper('E48') as cve_unit,0 as tax_percent,-1 as product_id,2 as ordr from payment_lines where order_id="+soid+" ) as temp ORDER by ordr ");

                char[] charArray = dtsoid.ToCharArray();
                Array.Reverse(charArray);
                string codFact = new string(charArray);
                string[] parts = codFact.Split('-');

                if (ocreat > Convert.ToDateTime("2018-07-09"))
                {
                    if (soid.Substring(soid.Length - 1) != dtsoid[0].ToString())
                    {
                        cname = "<font color=\"salmon\">NO SE ENCONTRO INFORMACIÓN CON LOS DATOS PROPORCIONADOS</font>";
                        return;
                    }
                    else
                    {
                        dh.Query("select code,sum(qty_delivered) as qty_delivered,product_name,unitary_cost,sum(extended_cost) as extended_cost,sat_code,unit,cve_unit,tax_percent,product_id FROM (  " +
                      "SELECT ol.product_code as code, qty_ordered as qty_delivered, ol.name as product_name,round(total_line/qty_ordered/(1+tx.tax_percent),4) as unitary_cost,total_line as extended_cost,p.sat_code,u.name as unit,u.code as cve_unit,tx.tax_percent*100 as tax_percent,p.product_id,1 as ordr from order_lines ol  " +
                      "INNER JOIN orders o ON o.order_id=ol.order_id INNER JOIN products p ON  p.product_id=ol.product_id  INNER JOIN units u ON p.unit_id=u.unit_id INNER JOIN taxes tx ON tx.tax_id=p.tax_id where (ol.mode!=9 AND ol.mode!=10) AND ol.total_line>0 AND ol.hidden is null AND ol.order_id= " + soid + " AND TO_CHAR(o.created,'ddHHMI')='" + parts[1].Remove(parts[1].Length - 1, 1) + "' and o.store_id=" + parts[0] + " " +
                      " UNION ALL select UPPER('PRP') as code,1 as qty_delivered,upper('PROPINA') as product_name, COALESCE(sum(tip_amount),0) as unitary_cost,COALESCE(sum(tip_amount),0) as extended_cost,'90101501' as sat_code,'SERV' as unit, upper('E48') as cve_unit,0 as tax_percent,-1 as product_id,2 as ordr from payment_lines where order_id=" + soid + " ) as temp " +
                      "  GROUP BY code,product_name,sat_code,unitary_cost,unit,cve_unit,tax_percent,ordr,product_id ORDER by ordr ");
                    }
                }
                else
                {
                    dh.Query("select code,sum(qty_delivered) as qty_delivered,product_name,unitary_cost,sum(extended_cost) as extended_cost,sat_code,unit,cve_unit,tax_percent,product_id FROM (  " +
                       "SELECT ol.product_code as code, qty_ordered as qty_delivered, ol.name as product_name,round(total_line/qty_ordered/(1+tx.tax_percent),4)  as unitary_cost,total_line as extended_cost,p.sat_code,u.name as unit,u.code as cve_unit,tx.tax_percent*100 as tax_percent,p.product_id,1 as ordr from order_lines ol  " +
                       "INNER JOIN orders o ON o.order_id=ol.order_id INNER JOIN products p ON  p.product_id=ol.product_id  INNER JOIN units u ON p.unit_id=u.unit_id INNER JOIN taxes tx ON tx.tax_id=p.tax_id where (ol.mode!=9 AND ol.mode!=10) AND ol.total_line>0 AND ol.hidden is null AND ol.order_id= " + soid + " AND TO_CHAR(o.created,'mmddHH')='" + parts[1] + "' and o.store_id=" + parts[0] + " " +
                       " UNION ALL select UPPER('PRP') as code,1 as qty_delivered,upper('PROPINA') as product_name, COALESCE(sum(tip_amount),0) as unitary_cost,COALESCE(sum(tip_amount),0) as extended_cost,'90101501' as sat_code,'SERV' as unit, upper('E48') as cve_unit,0 as tax_percent,-1 as product_id,2 as ordr from payment_lines where order_id=" + soid + " ) as temp " +
                       "  GROUP BY code,product_name,sat_code,unitary_cost,unit,cve_unit,tax_percent,ordr,product_id ORDER by ordr ");
                }
                if (dh.DataTable.Rows.Count == 1)
                {
                    cname = "<font color=\"salmon\">NO SE ENCONTRO INFORMACIÓN CON LOS DATOS PROPORCIONADOS</font>";
                }
                else
                {
                    //Comentado por Gilo, para que no aparezca el desglose de productos en la factura 21-Jun-2018
                    //addprotable += "<table class=\"w3-table w3-striped w3-bordered w3-border w3-hoverable w3-white\" ><thead> " +
                    //"<tr class=\"w3-light-grey\" style=\"line-height:10px;\"> <th>Nombre</th><th>Precio</th><th></th></tr></thead>";
                }
                bool shadow = false;
                string conceptos = "";
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = dh.DataTable;
                for (int i = 0; i < dt.Rows.Count;i++ )
                {


                    double txPct = Convert.ToDouble(dt.Rows[i]["tax_percent"]);
                    string tasaOcuota = Math.Round(txPct / 100, 6).ToString("n6");

                    double cantidad = Convert.ToDouble(dt.Rows[i]["qty_delivered"]);
                    string unidad = dt.Rows[i]["unit"].ToString();
                    string code = dt.Rows[i]["code"].ToString();
                    string descripcion = dt.Rows[i]["product_name"].ToString();
                    double punitario = Convert.ToDouble(dt.Rows[i]["unitary_cost"].ToString());

                    double ptotal = cantidad * punitario;//Convert.ToDouble(dt.Rows[i]["extended_cost"].ToString());

                    if (descripcion == "PROPINA") 
                    {
                        if (punitario > 0)
                        {
                            conceptos += "<td class=\"variable\" valign=\"top\">&nbsp;</td>" +
                              "<td class=\"variable\" valign=\"top\">&nbsp;</td>" +
                              "<td class=\"variable\" valign=\"top\">PROPINA</td>" +
                              "<td class=\"variable\" valign=\"top\">&nbsp;</td>" +
                              "<td class=\"variable\" valign=\"top\">&nbsp;</td>" +
                              "<td class=\"variable\" valign=\"top\">&nbsp;</td> " +
                              "<td class=\"variable\" valign=\"top\">" + punitario.ToString("c") + "</td>" +
                              "<td class=\"variable\" valign=\"top\">&nbsp;</td></tr>";

                            tipAmt = punitario.ToString();
                        }

                        continue;
                    }

                    string sat_code = dt.Rows[i]["sat_code"].ToString();
                    string cve_unit = dt.Rows[i]["cve_unit"].ToString();
                    prods += descripcion + " ";

                    double totciva = Convert.ToDouble(punitario) * cantidad * (1 + (Convert.ToDouble(txPct) / 100));
                    double totivared = Convert.ToDouble(ptotal) + Math.Round(Convert.ToDouble(ptotal) * (Convert.ToDouble(txPct) / 100), 2);
                    double ivarow = 0;
                    if (Math.Round(totciva, 2) > Math.Round(totivared, 2))
                        ivarow += Math.Round(Convert.ToDouble(ptotal) * (Convert.ToDouble(txPct) / 100), 2) + .01;
                    else if (Math.Round(totciva, 2) < Math.Round(totivared, 2))
                        ivarow += Math.Round(Convert.ToDouble(ptotal) * (Convert.ToDouble(txPct) / 100), 2) - .01;
                    else
                        ivarow += Math.Round(Convert.ToDouble(ptotal) * (Convert.ToDouble(txPct) / 100), 2);

                    totalsale = "$" + Math.Round(ptotal + ivarow, 2).ToString("#.##");
                    totalorder += Math.Round(ptotal + ivarow, 2);
                    subtotal += ptotal;
                    ivatotal += ivarow; 
                    totalord = "$" + totalorder;
                    //Comentado por Gilo, para que no aparezca el desglose de productos en la factura 21-Jun-2018
                    //addprotable +=
                    //      "<tr><td>" + cantidad + " " + dt.Rows[i]["product_name"] + "</td>" +
                    //      "<td>" + totalsale + "</td>";
                    //addprotable += "<td></td>";
                    //addprotable += "</tr>";
                    prodsadded = true;


                    if (!shadow)
                        shadow = true;
                    else
                        shadow = false;

                    //sCadena += "<cfdi:Concepto cantidad=\"" + cantidad + "\" unidad=\"" + unidad + "\" descripcion=\"" + descripcion + "\" valorUnitario=\"" + punitario + "\" importe=\"" + ptotal + "\"></cfdi:Concepto>";
                    if (ptotal > 0)
                        sCadena += "<cfdi:Concepto NoIdentificacion=\"" + code + "\" ClaveProdServ=\"" + sat_code + "\" ClaveUnidad=\"" + cve_unit + "\"  Cantidad=\"" + cantidad + "\" Unidad=\"" + unidad + "\" Descripcion=\"" + descripcion + "\" ValorUnitario=\"" + punitario + "\" Importe=\"" + ptotal + "\">" +
                            "<cfdi:Impuestos><cfdi:Traslados><cfdi:Traslado Base=\"" + ptotal + "\" Impuesto=\"002\" TipoFactor=\"Tasa\" TasaOCuota=\"" + tasaOcuota + "\" Importe=\"" + ivarow + "\"/></cfdi:Traslados></cfdi:Impuestos></cfdi:Concepto>";

                    string prodServ = "";

                    dh.Query("select name from sat_catalog where code like '" + sat_code + "'");
                    if (dh.Next())
                    {
                        prodServ = this.dh.DataTable.Rows[0]["name"].ToString();
                    }

                    if (shadow)
                        conceptos += "<tr class=\"data-b\">";
                    else
                        conceptos += "<tr class=\"data-a\">";
                    conceptos += "<td class=\"variable\" valign=\"top\">" + code + "</td>" +
                       "<td class=\"variable\" valign=\"top\">" + cantidad + "</td>" +
                       "<td class=\"variable\" valign=\"top\">" + descripcion + "</td>" +
                       "<td class=\"variable\" valign=\"top\">" + unidad + "</td>" +
                       "<td class=\"variable\" valign=\"top\">" + sat_code + "</td>" +
                       "<td class=\"variable\" valign=\"top\">" + cve_unit + "</td> " +
                       "<td class=\"variable\" valign=\"top\">" + Convert.ToDouble(punitario).ToString("c") + "</td>" +
                       "<td class=\"variable\" valign=\"top\">" + Convert.ToDouble(ptotal).ToString("c") + "</td></tr>" +
                       "<tr><td colspan=\"8\"> Clave Prod. Serv. - " + sat_code + " " + prodServ + " - Impuestos - Traslados 002 IVA Base - " + ptotal.ToString("c") + " - Tasa - " + tasaOcuota + " - Importe - " + Math.Round(Convert.ToDouble(ptotal) * Convert.ToDouble(tasaOcuota), 6).ToString("c") + "  </td></tr>";

                    prod = new string[11];

                    prod[0] = dt.Rows[i]["product_id"].ToString();
                    prod[1] = cantidad.ToString();
                    prod[2] = punitario.ToString();
                    prod[3] = dt.Rows[i]["code"].ToString();
                    prod[4] = ptotal.ToString();
                    prod[5] = ptotal.ToString();
                    prod[6] = descripcion;
                    prod[7] = sat_code;
                    prod[8] = cve_unit;
                    prod[9] = txPct.ToString();
                    prod[10] = unidad;

                    arrProds.Add(prod);

                }
                if (prodsadded)
                {
                    //Comentado por Gilo, para que no aparezca el desglose de productos en la factura 21-Jun-2018
                    //addprotable += "<tr><td>Total</td><td>" + totalord + "</td><td></td></tr>";
                    //addprotable += "</table> ";
                    btnFinishSO += "<button id=\"btnfinish\" name=\"btnfinish\" value=\"true\" class=\"w3-btn w3-yellow w3-large w3-opacity w3-hover-opacity-off\" onclick=\"return confirm('Favor de confirmar que sus datos son correctos?')\" >Facturar</button>";
                }

                
                #endregion

                #region Factura
                if (!string.IsNullOrEmpty(Request.Form["btnfinish"]))
                {
                    string sale_invoice_id="-1";
                    bool validateOK = true;
                    bool editMode = false;

                    if (string.IsNullOrEmpty(email))
                    { info = "<i class=\"w3-red\">Favor de agregar Correo</i>"; validateOK = false; }
                    if (string.IsNullOrEmpty(col))
                    { info = "<i class=\"w3-red\">Favor de agregar Colonia</i>"; validateOK = false; }
                    if (string.IsNullOrEmpty(legalcode))
                    { info = "<i class=\"w3-red\">Favor de agregar RFC</i>"; validateOK = false; }
                    if (string.IsNullOrEmpty(legalname))
                    { info = "<i class=\"w3-red\">Favor de agregar la razón social</i>"; validateOK = false; }

                    if (legalcode.ToUpper().StartsWith("RCA8609"))
                    {
                        info = "<i class=\"w3-red\">Favor de revisar el RFC</i>"; validateOK = false;
                    }

                    expedicion = "MONTERREY, N.L.";
                    string expedicion3 = emitCP;

                   
                    int folio = 0;
                    if (validateOK)
                    {
                        dh.Query("select sale_invoice_id,code from sale_invoices where project_id=" + soid + " AND status<>'5' ");
                        if (dh.exception.Message == "No Exception")
                        {
                            if (dh.DataTable.Rows.Count > 0)
                            {
                                //string flio = "";
                                sale_invoice_id = dh.DataTable.Rows[0]["sale_invoice_id"].ToString();
                                folio = Convert.ToInt32(dh.DataTable.Rows[0]["code"]);
                                this.dh.Query("select sello from sale_invoices where sale_invoice_id=" + sale_invoice_id + "");
                                if (dh.exception.Message == "No Exception")
                                {
                                    if (this.dh.DataTable.Rows[0]["sello"].ToString() != "")
                                    {
                                        string folioSerie2 = serie + "-" + folio;
                                        string fileName = @"C:\inetpub\wwwroot\reycabrito\cfd\" + folioSerie2 + ".pdf";
                                        downloadCfdi = "El CFDI ya fue generado, puede descargar los archivos<br/><a href=\"/cfd/" + folioSerie2 + ".pdf\" download=\"" + folioSerie2 + ".pdf\">Descargar archivo PDF</a> <a href=\"/cfd/" + folioSerie2 + ".xml\" download=\"" + folioSerie2 + ".xml\">&nbsp;&nbsp;&nbsp;Descargar archivo XML</a> ";
                                        sendEmail(fileName, fileName.Replace("pdf", "xml"));
                                        validateOK = false;
                                    }
                                    else
                                    {
                                        editMode = true;
                                        //validateOK = false;
                                    }
                                }
                                //info = "<i class=\"w3-red\">Favor de agregar la razón social</i>"; validateOK = false; 
                            }//End count invoices
                        }

                        if (validateOK)
                        {
                            dh.Query("select order_id, customer_id, invoice_code,pch_order from Orders where order_id=" + soid + "");
                            if (dh.Next())
                            {
                                if (dh.FieldValue("invoice_code").ToString() != "")
                                {
                                    downloadCfdi = "El ticket ya fue integrado en la factura global y no puede ser facturado";
                                    validateOK = false;
                                }
                            }
                        }


                    }//End validateok

                    if (validateOK)
                    {

                        //no = this.mskNoExt.Text.TrimEnd().TrimStart();
                        //noint = this.mskNoInt.Text.TrimEnd().TrimStart();
                        //calle = this.CleanString(this.mskTxtAddress.Text.TrimEnd().TrimStart());
                        //col = this.CleanString(this.mskNeighborhood.Text.TrimEnd().TrimStart());
                        //loc = this.CleanString(this.mskCity.Text.TrimEnd().TrimStart());
                        //edo = this.CleanString(this.mskState.Text.TrimEnd().TrimStart());
                        //cp = this.mskPostalCode.Text.TrimStart();

                        //no = "";
                        //noint = "";
                        calle = this.CleanString(calle.TrimEnd().TrimStart());
                        col = this.CleanString(col.TrimEnd().TrimStart());
                        loc = this.CleanString(cmbLoc.SelectedItem.Text.TrimEnd().TrimStart());
                        edo = this.CleanString(cmbEdo.SelectedItem.Text.TrimEnd().TrimStart());
                        cp = cp.TrimStart();

                        if (calle == "" | calle == " ")
                            calle = "NA";
                        if (no == "" | no == " ")
                            no = "SN";
                        if (noint == "" | noint == " ")
                            noint = "SN";
                        if (col == "" | col == " ")
                            col = "NA";
                        if (edo == "")
                            edo = "NA";
                        if (loc == "")
                            loc = "NA";
                        if (cp == "")
                            cp = "00000";

                        string fechaNow = DateTime.Today.ToString("yyyy-MM-dd");
                        string horaNow = DateTime.Now.ToString("HH:mm:ss");
                        string sello = "NOSELLODIG";
                        string formaPago = "PAGO EN UNA SOLA EXHIBICION";
                        string condiciones = "PAGO EN UNA SOLA EXHIBICION";
                        string metodoPago = paymethod;
                        string paymethodid = paymethod;
                        string cuentaPago = payacc;
                        string paymethodcond = pmcnd;
                        string cfdi_usage_id = uscfd;
                        string mpago = "Pago en una sola exhibición";
                        if(pmcnd=="PPD")
                            mpago = "Pago en parcialidades o diferido";
                        string usocfdi = cmbUso.SelectedItem.Text;
                        string rfc = legalcode.ToUpper();
                        string nombre = legalname.ToUpper() ;
                        double iva = 0;
                        string retiva = "0";

                        iva = ivatotal;//subtotal * (tax - 1);
                        total = Math.Round((subtotal + iva), 2);

                        iva = Math.Round(iva, 2);
                        //Gilo 2-Ene-2017 para que tenga 2 decimales
                        subtotal = Math.Round(subtotal,2);

                        if (!editMode)
                        {
                            
                             dh.Query("SELECT max(code::int) as invoice_id FROM sale_invoices WHERE type=1 ");
                           
                            if (dh.DataTable.Rows[0]["invoice_id"].ToString() == "")
                                folio = 15000;
                            else
                                folio = (Convert.ToInt32(dh.DataTable.Rows[0]["invoice_id"].ToString()) + 1);
                        }

                        string folioSerie2 = serie + "-" + folio;

                        bool gencfdi = false;

                        try
                        {
                            this.dh.Begin();

                            string today = DateTime.Today.ToString("yyyy/MM/dd");

                            if (!editMode)
                            {

                                #region Insert Business Partner and Customer

                                if (bpartnerid == "")
                                {
                                    int bpartid = Convert.ToInt32(dh.QueryScalar("select Nextval('sq_business_partners')")); 
                                    bpartnerid = bpartid.ToString();

                                    dh.Update("INSERT INTO business_partners(bpartner_id,name,activated,updated_by,created_by,updated,created,deleted,legal_code,legal_name,city_name,state_name,country_name,phones,emails,address,postal_code,city_id,state_id,country_id,neighborhood,no_int,no_ext)  " +
                                        "      VALUES   (" + bpartid + ",'" + legalname + "','1',3,3,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP,'0','" + legalcode + "','" + legalname + "','" + cmbLoc.Text + "','" + cmbEdo.Text + "','MEXICO',null,'" + email + "','" + calle + "','" + cp + "'," + cmbLoc.SelectedValue + "," + cmbEdo.SelectedValue + ",1,'" + col + "','" + noint + "','" + no + "')");
                                    if (this.dh.exception.Message != "No Exception")
                                        throw new Exception("Hubo un problema al guardar al Socio de negocio\n" + this.dh.exception.ToString());

                                    int bpartAddid = Convert.ToInt32(dh.QueryScalar("select Nextval('sq_bpartner_addresses')")); 

                                    dh.Update("INSERT INTO bpartner_addresses(bpartner_address_id,bpartner_id,activated,updated_by,created_by,updated,created,deleted,city_id,state_id,country_id,address1,neighborhood,phones,no_int,no_ext) " +
                                        "      VALUES   (" + bpartAddid + "," + bpartid + ",'1',3,3,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP,'0'," + cmbLoc.SelectedValue + "," + cmbEdo.SelectedValue + ",1,'" + calle + "','" + col + "',null,'" + noint + "','" + no + "')");
                                    if (this.dh.exception.Message != "No Exception")
                                        throw new Exception("Hubo un problema al guardar la dirección de socio de negocio\n" + this.dh.exception.ToString());

                                    int cusid = Convert.ToInt32(dh.QueryScalar("select Nextval('sq_customers')"));
                                    custid = cusid.ToString();

                                    int cusaccid = Convert.ToInt32(dh.QueryScalar("select Nextval('sq_customer_accounts')"));

                                    dh.Update("INSERT INTO customers(customer_id,bpartner_id,name,activated,updated_by,created_by,updated,created,deleted,customer_account_id,credit_days,pay_method,acc_number) " +
                                        "      VALUES(" + cusid + "," + bpartid + ",'" + legalname + "','1',3,3,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP,'0'," + cusaccid + ",0,'" + paymethodid + "','" + cuentaPago + "')");
                                    if (this.dh.exception.Message != "No Exception")
                                        throw new Exception("Hubo un problema al guardar al cliente\n" + this.dh.exception.ToString());

                                    dh.Update("INSERT INTO customer_accounts(customer_account_id,customer_id,name,credit_days,activated,updated_by,created_by,updated,created) " +
                                        "       VALUES (" + cusaccid + "," + cusid + ",'" + legalname + "',0,'1',3,3,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP)");
                                    if (this.dh.exception.Message != "No Exception")
                                        throw new Exception("Hubo un problema al guardar la cuenta del cliente\n" + this.dh.exception.ToString());

                                }
                                #endregion

                                sale_invoice_id = dh.QueryScalar("select Nextval('sq_sale_invoices')").ToString();


                                dh.Query("INSERT INTO sale_invoices (sale_invoice_id,customer_id,subtotal,grand_total,created,updated,created_by,updated_by,customer_name,customer_address,street,no_ext,no_int,neighborhood,city,state,postal_code,credit_days,date_invoiced,expiration_date,expiration_date_original,promise_date,project_id,type,currency_id,status,tax_amt,store_id,code,invoice_doc_code,discount_amt,pay_method,acc_number,pay_method_id,pay_method_cond,cfdi_usage_id,tip_amt) " +
                                "VALUES			                    (" + sale_invoice_id + "," + custid + "," + subtotal + "," + total + ",now(),now(),1,1,'" + nombre.ToUpper() + "','" + calle + "','"+calle+"','" + no + "','" + noint + "','" + col + "','" + loc + "','" + edo + "','" + cp + "',0,'" + today + "','" + today + "','" + today + "','" + today + "'," + soid + ",1,1,0," + iva + ",1," + folio + ",'" + rfc.ToUpper() + "',0,'" + metodoPago + "','" + cuentaPago + "','" + paymethodid + "','PUE','"+cfdi_usage_id+"',"+tipAmt+")");

                                if (this.dh.exception.Message != "No Exception")
                                    throw new Exception("Hubo un problema al guardar la factura\n" + this.dh.exception.ToString());
                                foreach (string[] prd in arrProds)
                                {

                                    string sale_inv_ln = dh.QueryScalar("select nextval('sq_sale_invoice_lines')").ToString(); 
                                    if (prod[7] == "") 
                                        prod[7] = "01010101";
                                    dh.Query("INSERT INTO sale_invoice_lines (sale_invoice_id,sale_invoiceln_id,product_id,qty_saled,unitary_price,discount_amt,updated,invoiced_product_code,subtotal,line_grand_total,description,unit,sat_code,cve_unit,tax_percent) " +
                                    "VALUES                                  (" + sale_invoice_id + "," + sale_inv_ln + "," + prd[0] + "," + prd[1] + "," + prd[2] + ",0,now(),'" + prd[3] + "'," + prd[4] + "," + prd[5] + ",'" + prd[6] + "','"+prd[10]+"','" + prd[7] + "','" + prd[8] + "',"+prd[9]+")");

                                    if (this.dh.exception.Message != "No Exception")
                                        throw new Exception("Hubo un problema al guardar las lineas de la factura\n" + this.dh.exception.ToString());
                                }


                            }
                            else
                            {
                                dh.Update("UPDATE sale_invoices set subtotal=" + subtotal + ", grand_total=" + total + ",customer_name='" + nombre + "',customer_address='" + calle + "', no_ext='" + no + "',no_int='" + noint + "',neighborhood='" + col + "',city='" + loc + "',"+
                                    "state='" + edo + "',postal_code='" + cp + "',invoice_doc_code='" + rfc.ToUpper() + "',tax_amt=" + iva + ",pay_method='" + paymethod + "',pay_method_id='" + paymethodid + "',pay_method_cond='"+paymethodcond+"',cfdi_usage_id='"+cfdi_usage_id+"', updated=now(),updated_by=1 where sale_invoice_id=" + sale_invoice_id + "");
                                if (this.dh.exception.Message != "No Exception") { throw new Exception("Hubo un problema al actualizar los datos de la factura\n" + this.dh.exception.ToString()); }
                            }
                            
                            dh.Update("update business_partners set legal_code='" + rfc.ToUpper() + "', legal_name='" + nombre + "', address='" + calle + "',no_ext='" + no + "',no_int='" + noint + "',neighborhood='" + col + "',postal_code='" + cp + "'  WHERE bpartner_id=" + bpartnerid + "");
                            if (this.dh.exception.Message != "No Exception")
                                throw new Exception("Hubo un problema al actualizar los datos del cliente\n" + this.dh.exception.ToString());
                            
                            dh.Update("	UPDATE orders SET invoice_amt = " + total + ", invoice_code='"+folio+"' WHERE order_id=" + soid + "");
                            if (this.dh.exception.Message != "No Exception")
                                throw new Exception("Hubo un problema al actualizar el monto facturado\n" + this.dh.exception.ToString());

                            gencfdi = true;

                            downloadCfdi = "La factura fue generada con éxito";
                            #region Genera CFDI
                            if (gencfdi)
                            {

                                string sXML = "";
                                string strret = "";
                                string strdetret = "";
                                //sXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><cfdi:Comprobante xmlns:cfdi=\"http://www.sat.gob.mx/cfd/3\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"  xsi:schemaLocation=\"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd\" version=\"3.2\" serie=\"" + serie + "\" folio=\"" + folio + "\" fecha=\"" + fechaNow + "T" + horaNow + "\" sello=\"" + sello + "\"  tipoDeComprobante=\"ingreso\" formaDePago=\"" + formaPago + "\" condicionesDePago=\"" + condiciones + "\" subTotal=\"" + subtotal + "\" total=\"" + total + "\"  TipoCambio=\"1.0\" Moneda=\"MXN\"  metodoDePago=\"" + metodoPago + "\" LugarExpedicion=\"" + expedicion + "\" NumCtaPago=\"" + cuentaPago + "\" noCertificado=\"00001000000305624262\" certificado=\"MIIEczCCA1ugAwIBAgIUMDAwMDEwMDAwMDAzMDU2MjQyNjIwDQYJKoZIhvcNAQEFBQAwggGKMTgwNgYDVQQDDC9BLkMuIGRlbCBTZXJ2aWNpbyBkZSBBZG1pbmlzdHJhY2nDs24gVHJpYnV0YXJpYTEvMC0GA1UECgwmU2VydmljaW8gZGUgQWRtaW5pc3RyYWNpw7NuIFRyaWJ1dGFyaWExODA2BgNVBAsML0FkbWluaXN0cmFjacOzbiBkZSBTZWd1cmlkYWQgZGUgbGEgSW5mb3JtYWNpw7NuMR8wHQYJKoZIhvcNAQkBFhBhY29kc0BzYXQuZ29iLm14MSYwJAYDVQQJDB1Bdi4gSGlkYWxnbyA3NywgQ29sLiBHdWVycmVybzEOMAwGA1UEEQwFMDYzMDAxCzAJBgNVBAYTAk1YMRkwFwYDVQQIDBBEaXN0cml0byBGZWRlcmFsMRQwEgYDVQQHDAtDdWF1aHTDqW1vYzEVMBMGA1UELRMMU0FUOTcwNzAxTk4zMTUwMwYJKoZIhvcNAQkCDCZSZXNwb25zYWJsZTogQ2xhdWRpYSBDb3ZhcnJ1YmlhcyBPY2hvYTAeFw0xNDEyMDQwMDQ1MDlaFw0xODEyMDQwMDQ1MDlaMIG/MRwwGgYDVQQDExNHUlVQTyBSRURPIFNBIERFIENWMRwwGgYDVQQpExNHUlVQTyBSRURPIFNBIERFIENWMRwwGgYDVQQKExNHUlVQTyBSRURPIFNBIERFIENWMSUwIwYDVQQtExxHUkUwNTEwMjFSWjQgLyBNQU1SNzgwMjExMTI2MR4wHAYDVQQFExUgLyBNQU1SNzgxMTAySE5MTFJEMDAxHDAaBgNVBAsTE0dSVVBPIFJFRE8gU0EgREUgQ1YwgZ8wDQYJKoZIhvcNAQEBBQADgY0AMIGJAoGBAK/Xd5MKbGD1b/576FbpKyDp56wZzymYgQlVpt+5qJNc/SSURnmQUPfVJZECqAEyK6kEPhmANnyVeOTTNSphX3U5tFWHUSpT5W6PRoVrnRfTVpwEFL/ak527mJ1ui+qCjPDne+y+oD4mHl5VuRUVa3gufzRIsaRWPbs6e27/L9pnAgMBAAGjHTAbMAwGA1UdEwEB/wQCMAAwCwYDVR0PBAQDAgbAMA0GCSqGSIb3DQEBBQUAA4IBAQC7ZZ+PyQQrqHS1YbCIBrDdkq77ur0UZSqBbn/UZvMRaOX5bZnyK6UQGE1s1nPThhoqY+pu6CpG+bRY9X1zRV0DVEgnD1U3w/vMXS/IP9sogTM+6z7WElmYBgIZbS880H4Ojt3eg5jSbfvJ8kNc85jst96rkTN22x3I53UjxyhH50kqmuwoyzBR63DeBWr2KlrX1pIjToOPLsph1RWIm/II9PDIWZc3rEFqzHDXwcru3ghwGYA58N0d+QqVlqC2c4yqtyXdvMkLdLAVJY6cE/Fbb63rjqChoVpKAPJFGyL386wTMKA8aiMZXSJQ4YJXm4LzDCAqxAJQyjiDCIE617TC\"><cfdi:Emisor rfc=\"GRE051021RZ4\" nombre=\"GRUPO REDO SA DE CV\"><cfdi:DomicilioFiscal calle=\"LAZARO CARDENAS\" noExterior=\"4344\" colonia=\"LAS TORRES\" localidad=\"MONTERREY\" municipio=\"MONTERREY\" estado=\"NUEVO LEON\" pais=\"MEXICO\" codigoPostal=\"64930\"/><cfdi:RegimenFiscal Regimen=\"REGIMEN GENERAL DE LEY\"/></cfdi:Emisor><cfdi:Receptor rfc=\"" + rfc.Replace("-", "").Trim() + "\" nombre=\"" + this.CleanString(nombre) + "\"><cfdi:Domicilio calle=\"" + calle + "\" noExterior=\"" + no + "\" noInterior=\"" + noint + "\" colonia=\"" + col + "\" localidad=\"" + loc + "\" municipio=\"" + loc + "\" estado=\"" + edo + "\" pais=\"MEXICO\" codigoPostal=\"" + cp + "\"/></cfdi:Receptor><cfdi:Conceptos>" + sCadena + "</cfdi:Conceptos><cfdi:Impuestos totalImpuestosRetenidos=\"" + retiva + "\" totalImpuestosTrasladados=\"" + iva + "\"><cfdi:Retenciones><cfdi:Retencion impuesto=\"IVA\" importe=\"" + retiva + "\" /></cfdi:Retenciones><cfdi:Traslados><cfdi:Traslado impuesto=\"IVA\" tasa=\"16.00\" importe=\"" + iva + "\"></cfdi:Traslado></cfdi:Traslados></cfdi:Impuestos></cfdi:Comprobante>";
                                //GDM
                                //sXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><cfdi:Comprobante xmlns:cfdi=\"http://www.sat.gob.mx/cfd/3\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"  xsi:schemaLocation=\"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd\" version=\"3.2\" serie=\"" + serie + "\" folio=\"" + folio + "\" fecha=\"" + fechaNow + "T" + horaNow + "\" sello=\"" + sello + "\"  tipoDeComprobante=\"ingreso\" formaDePago=\"" + formaPago + "\" condicionesDePago=\"" + condiciones + "\" subTotal=\"" + subtotal + "\" total=\"" + total + "\"  TipoCambio=\"1.0\" Moneda=\"MXN\"  metodoDePago=\"" + metodoPago + "\" LugarExpedicion=\"" + expedicion + "\" NumCtaPago=\"" + cuentaPago + "\" noCertificado=\"00001000000404767770\" certificado=\"MIIGHzCCBAegAwIBAgIUMDAwMDEwMDAwMDA0MDQ3Njc3NzAwDQYJKoZIhvcNAQELBQAwggGyMTgwNgYDVQQDDC9BLkMuIGRlbCBTZXJ2aWNpbyBkZSBBZG1pbmlzdHJhY2nDs24gVHJpYnV0YXJpYTEvMC0GA1UECgwmU2VydmljaW8gZGUgQWRtaW5pc3RyYWNpw7NuIFRyaWJ1dGFyaWExODA2BgNVBAsML0FkbWluaXN0cmFjacOzbiBkZSBTZWd1cmlkYWQgZGUgbGEgSW5mb3JtYWNpw7NuMR8wHQYJKoZIhvcNAQkBFhBhY29kc0BzYXQuZ29iLm14MSYwJAYDVQQJDB1Bdi4gSGlkYWxnbyA3NywgQ29sLiBHdWVycmVybzEOMAwGA1UEEQwFMDYzMDAxCzAJBgNVBAYTAk1YMRkwFwYDVQQIDBBEaXN0cml0byBGZWRlcmFsMRQwEgYDVQQHDAtDdWF1aHTDqW1vYzEVMBMGA1UELRMMU0FUOTcwNzAxTk4zMV0wWwYJKoZIhvcNAQkCDE5SZXNwb25zYWJsZTogQWRtaW5pc3RyYWNpw7NuIENlbnRyYWwgZGUgU2VydmljaW9zIFRyaWJ1dGFyaW9zIGFsIENvbnRyaWJ1eWVudGUwHhcNMTcwMTA5MjMwODUyWhcNMjEwMTA5MjMwODUyWjCBvzEcMBoGA1UEAxMTR1JVUE8gMjcxMSBTQSBERSBDVjEcMBoGA1UEKRMTR1JVUE8gMjcxMSBTQSBERSBDVjEcMBoGA1UEChMTR1JVUE8gMjcxMSBTQSBERSBDVjElMCMGA1UELRMcR0RNMTYxMTA4SFEwIC8gTUFNUjc4MDIxMTEyNjEeMBwGA1UEBRMVIC8gTUFNUjc4MTEwMkhOTExSRDAwMRwwGgYDVQQLExNHUlVQTyAyNzExIFNBIERFIENWMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAoV0ndfMeZUD4fxxsDzIHtDSA8XuvuHg5gKnHCP/lwci65707nEp8VpihzhUqLbsR7e7MEj8Xhu9JmBMGaew6V109r0w16//+cC9t9i+5Oh69znZYCBHo0YQFPmSKR4N/cXeFUtlLiL5XRJALj7fjcpMq+YoxhCVYYX4t/JoUgoIzlF/ueC8KXrPvMcIjmmtUmcFhHq6AqV4vtJvWPN2uD4+P+i9gVpQYzMQLx90D7qUzDp4GIGF0hPsEQQwu5o6gykRJ8mi1Jq3TUfU8+dSyvLr/T7Pjzudg4VsKC5eszw4ccGVTWhMwaQMZf1gR4CX9TMytOjdaUolQqjn9hmRf9QIDAQABox0wGzAMBgNVHRMBAf8EAjAAMAsGA1UdDwQEAwIGwDANBgkqhkiG9w0BAQsFAAOCAgEACIFy2vwoa8yWTfGvF6QZqeqTDpsOiot1ShG4COVsR94WMj05G7GGLQayG/n6Bl/UFN2bSWp9xsl4zB/eIkLXC3/AfUqaBqdYQZEfHYxGzdtWXHOc2zYtlpyJqT/WXq/0KVUQjyuApeKDSDzbR0HUKhZpubJSQssl5yvqirprVNvkpByYqN2B7A9qS9ZRjkjrktOjgdt/TUnOTnLgUwcu5FzwVRwfUsEPbbMunanDEdFsrB8gsjbXZTtF/pWFEkG93F2wHF/z54YeMW60QB7yfAaht9E2P7toYJAg5T8SGulmAGc9kNiOWKTljyivGiQWbPVm+JzPpHR03qOgRQixrQKX2aRfaof2999fi2Rrub2a9+JkjTFFyV2/1QSpOYGOy+J+U3M4ouh8rf/cqncoXzamf2sxO4MfRMbXWK1tSeq7CRSf8P0lisDzdRUxGMj1+oRyX3EVswwyESbJJylzYzrwpvx2XSGHYj8/TlxRU2i26BF3hZuLccPS0fw1wXXAgKQ90RtK+/rn2ngYmJK7f67SbB3FNZcKffe0TJ6XLMdJ5fxfmX/wKLuxJliP7asCd4UAcnj9miiIoSyCdbtG5ndRYYVaPy9iiAV1YIr/ShpgatM9cKKebTLScEQm04QbjlHzai23CbIcR+ilUz77iz94WeSwl8ybbYOVFscpxWE=\"><cfdi:Emisor rfc=\"GDM161108HQ0\" nombre=\"GRUPO 2711 SA DE CV\"><cfdi:DomicilioFiscal calle=\"LAZARO CARDENAS\" noExterior=\"4344\" colonia=\"LAS TORRES\" localidad=\"MONTERREY\" municipio=\"MONTERREY\" estado=\"NUEVO LEON\" pais=\"MEXICO\" codigoPostal=\"64930\"/><cfdi:RegimenFiscal Regimen=\"REGIMEN GENERAL DE LEY\"/></cfdi:Emisor><cfdi:Receptor rfc=\"" + rfc.Replace("-", "").Trim() + "\" nombre=\"" + this.CleanString(nombre) + "\"><cfdi:Domicilio calle=\"" + calle + "\" noExterior=\"" + no + "\" noInterior=\"" + noint + "\" colonia=\"" + col + "\" localidad=\"" + loc + "\" municipio=\"" + loc + "\" estado=\"" + edo + "\" pais=\"MEXICO\" codigoPostal=\"" + cp + "\"/></cfdi:Receptor><cfdi:Conceptos>" + sCadena + "</cfdi:Conceptos><cfdi:Impuestos totalImpuestosRetenidos=\"" + retiva + "\" totalImpuestosTrasladados=\"" + iva + "\"><cfdi:Retenciones><cfdi:Retencion impuesto=\"IVA\" importe=\"" + retiva + "\" /></cfdi:Retenciones><cfdi:Traslados><cfdi:Traslado impuesto=\"IVA\" tasa=\"16.00\" importe=\"" + iva + "\"></cfdi:Traslado></cfdi:Traslados></cfdi:Impuestos></cfdi:Comprobante>";
                                //sXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><cfdi:Comprobante xmlns:cfdi=\"http://www.sat.gob.mx/cfd/3\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"  xsi:schemaLocation=\"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd\" Version=\"3.3\" Serie=\"" + serie + "\" Folio=\"" + folio + "\" Fecha=\"" + fechaNow + "T" + horaNow + "\" Sello=\"" + sello + "\"  TipoDeComprobante=\"I\" FormaPago=\"" + metodoPago + "\" CondicionesDePago=\"" + condiciones + "\" SubTotal=\"" + subtotal + "\" Total=\"" + total + "\"  TipoCambio=\"1\" Moneda=\"MXN\"  MetodoPago=\"" + paymethodcond + "\" LugarExpedicion=\"" + expedicion3 + "\"  NoCertificado=\"00001000000404767770\" Certificado=\"MI=\"><cfdi:Emisor Rfc=\"GDM161108HQ0\" Nombre=\"GRUPO 2711 SA DE CV\" RegimenFiscal=\"601\"/><cfdi:Receptor Rfc=\"" + rfc.Replace("-", "").Trim() + "\" Nombre=\"" + this.CleanString(nombre) + "\" UsoCFDI=\"" + cfdi_usage_id + "\"/><cfdi:Conceptos>" + sCadena + "</cfdi:Conceptos><cfdi:Impuestos " + strret + " TotalImpuestosTrasladados=\"" + iva + "\">" + strdetret + "<cfdi:Traslados><cfdi:Traslado Impuesto=\"002\" TipoFactor=\"Tasa\" TasaOCuota=\"0.160000\" Importe=\"" + iva + "\"/></cfdi:Traslados></cfdi:Impuestos></cfdi:Comprobante>";

                                sXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><cfdi:Comprobante xmlns:cfdi=\"http://www.sat.gob.mx/cfd/3\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"  xsi:schemaLocation=\"http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd\" Version=\"3.3\" Serie=\"" + serie + "\" Folio=\"" + folio + "\" Fecha=\"" + fechaNow + "T" + horaNow + "\" Sello=\"" + sello + "\"  TipoDeComprobante=\"I\" FormaPago=\"" + metodoPago + "\" CondicionesDePago=\"" + condiciones + "\" SubTotal=\"" + subtotal + "\" Total=\"" + total + "\"  TipoCambio=\"1\" Moneda=\"MXN\"  MetodoPago=\"" + paymethodcond + "\" LugarExpedicion=\"" + emitCP.Trim() + "\"  NoCertificado=\"" + certSerial + "\" Certificado=\"" + certData + "\"><cfdi:Emisor Rfc=\"" + emitRFC.ToUpper().Replace("-", "").Trim() + "\" Nombre=\"" + CleanString(emitName.ToUpper().Replace(",", "").Replace(".", "").Trim()) + "\" RegimenFiscal=\"" + regimeID + "\"/><cfdi:Receptor Rfc=\"" + rfc.Replace("-", "").Trim() + "\" Nombre=\"" + this.CleanString(nombre) + "\" UsoCFDI=\"" + cfdi_usage_id + "\"/><cfdi:Conceptos>" + sCadena + "</cfdi:Conceptos><cfdi:Impuestos TotalImpuestosTrasladados=\"" + iva + "\"><cfdi:Traslados><cfdi:Traslado Impuesto=\"002\" TipoFactor=\"Tasa\" TasaOCuota=\"0.160000\" Importe=\"" + iva + "\"/></cfdi:Traslados></cfdi:Impuestos></cfdi:Comprobante>";
                                //Reemplazar & para su representación en XML
                                sXML = sXML.Replace("&", "&amp;");

                                System.IO.FileStream fs = new System.IO.FileStream(@"C:/inetpub/wwwroot/reycabrito/cfd/" + folioSerie2 + ".xml", System.IO.FileMode.Create, System.IO.FileAccess.Write);
                                System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
                                sw.Write(sXML);
                                sw.Flush();
                                sw.Close();
                                fs.Close();

                                string cadenaOriginal = "";

                                StreamReader reader = new StreamReader(@"C:/inetpub/wwwroot/reycabrito/cfd/" + folioSerie2 + ".xml");
                                XPathDocument doc = new XPathDocument(reader);
                                XslCompiledTransform trans = new XslCompiledTransform();
                                //trans.Load(@"C:/inetpub/wwwroot/Maver/cfd/conf/cadena.xslt");
                                trans.Load(@"C:/inetpub/wwwroot/reycabrito/cfd/conf/cadenaoriginal_3_3.xslt");
                                StringWriter writer = new StringWriter();
                                XmlTextWriter myWriter = new XmlTextWriter(writer);
                                trans.Transform(doc, null, myWriter);
                                cadenaOriginal = writer.ToString();
                                //Y poner el caracter correcto en la cadena original
                                cadenaOriginal = cadenaOriginal.Replace("&amp;", "&");


                                fs = new System.IO.FileStream(@"C:/inetpub/wwwroot/reycabrito/cfd/co.txt", System.IO.FileMode.Create, System.IO.FileAccess.Write);
                                sw = new System.IO.StreamWriter(fs);
                                sw.Write(cadenaOriginal);
                                sw.Flush();
                                sw.Close();
                                fs.Close();

                                try
                                {
                                    System.Diagnostics.ProcessStartInfo si;
                                    
                                    si = new System.Diagnostics.ProcessStartInfo(@"C:/inetpub/wwwroot/reycabrito/cfd/conf/openssl.exe", "dgst -sha256 -sign C:/inetpub/wwwroot/reycabrito/cfd/conf/key.pem -out C:/inetpub/wwwroot/reycabrito/cfd/co.signed.txt C:/inetpub/wwwroot/reycabrito/cfd/co.txt");
                                   
                                    si.RedirectStandardOutput = true;
                                    si.UseShellExecute = false;
                                    si.CreateNoWindow = true;
                                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                                    proc.StartInfo = si;
                                    proc.Start();
                                    sello = proc.StandardOutput.ReadToEnd();

                                }
                                catch (Exception ex)
                                {
                                    ex.ToString();
                                }

                                try
                                {
                                    System.Diagnostics.ProcessStartInfo si = new System.Diagnostics.ProcessStartInfo(@"C:/inetpub/wwwroot/reycabrito/cfd/conf/openssl.exe", "base64 -in  C:/inetpub/wwwroot/reycabrito/cfd/co.signed.txt");
                                    si.RedirectStandardOutput = true;
                                    si.UseShellExecute = false;
                                    si.CreateNoWindow = true;
                                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                                    proc.StartInfo = si;
                                    proc.Start();
                                    sello = proc.StandardOutput.ReadToEnd();

                                }
                                catch (Exception ex)
                                {
                                    ex.ToString();
                                }

                                sXML = sXML.Replace("NOSELLODIG", sello.Replace("\n", ""));


                                fs = new System.IO.FileStream(@"C:/inetpub/wwwroot/reycabrito/cfd/" + folioSerie2 + ".xml", System.IO.FileMode.Truncate, System.IO.FileAccess.Write);
                                sw = new System.IO.StreamWriter(fs);
                                sw.Write(sXML);
                                sw.Flush();
                                sw.Close();
                                fs.Close();

                                #region Timbrado
                                
                                string selloSAT = "";
                                string cadenaSAT = "";
                                string fechaSAT = "2016/02/08 00:00:00";
                                string certificadoSAT = "";
                                string folioSAT = "";
                                
                                //GeneraCFD.GeneraCFD cfdi = new GeneraCFD.GeneraCFD();
                                RVCFDI33.GeneraCFDI cfdi3 = new RVCFDI33.GeneraCFDI();

                                
                                cfdi3.TimbrarCfdiArchivo(@"C:/inetpub/wwwroot/reycabrito/cfd/" + folioSerie2 + ".xml", timbradoUser, timbradoPassword, "http://generacfdi.com.mx/rvltimbrado/service1.asmx?WSDL", @"C:/inetpub/wwwroot/reycabrito/cfd/", folioSerie2 + ".xml", true);
                                if ((!string.IsNullOrEmpty(cfdi3.MensajeError)))
                                {
                                    throw new Exception("Hubo un problema al timbrar el CFDI\n" + cfdi3.MensajeError);

                                }
                                //cfdi.AgregarXML(@"C:/inetpub/wwwroot/Maver/cfd/" + folioSerie2 + ".xml");
                                //cfdi.AgregarXML(@"../common/cfd/xml/PruebaTim.xml");

                                MemoryStream ms;

                                selloSAT = cfdi3.SelloSat;
                                cadenaSAT = cfdi3.CadenaTimbre;
                                fechaSAT = cfdi3.FechaTimbrado;
                                certificadoSAT = cfdi3.NoCertificadoPac;
                                folioSAT = cfdi3.UUID;
                                ms = new MemoryStream(cfdi3.GenerarQrCode());

                                System.Drawing.Image imgSave = System.Drawing.Image.FromStream(ms);
                                Bitmap bmSave = new Bitmap(imgSave);
                                Bitmap bmTemp = new Bitmap(bmSave);

                                Graphics grSave = Graphics.FromImage(bmTemp);
                                grSave.DrawImage(imgSave, 0, 0, imgSave.Width, imgSave.Height);

                                bmTemp.Save(@"C:/inetpub/wwwroot/reycabrito/cfd/" + folioSerie2 + ".png");

                                imgSave.Dispose();
                                bmSave.Dispose();
                                bmTemp.Dispose();
                                grSave.Dispose();
                                 

                                #endregion

                                dh.Query("update sale_invoices set sat_authorization=1,cadena_original='" + cadenaOriginal + "',sello='" + sello + "',fecha_cfd='" + fechaNow + " " + horaNow + "' ,SAT_certificate='" + certificadoSAT + "',cadena_original_SAT='" + cadenaSAT + "',sello_SAT='" + selloSAT + "',fecha_cfd_sat='" + fechaSAT.Replace("T", " ") + "',UUID='" + folioSAT + "' where sale_invoice_id=" + sale_invoice_id + "");
                                if (this.dh.exception.Message != "No Exception")
                                {
                                    throw new Exception("Hubo un problema al actualizar los datos de timbrado\n" + this.dh.exception.ToString());
                                }

                                string templFact = @"C:/inetpub/wwwroot/reycabrito/cfd/conf/TemplateFactura.html";
                              
                                fs = new System.IO.FileStream(templFact, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                System.IO.StreamReader sr = new System.IO.StreamReader(fs);
                                string html = sr.ReadToEnd();
                                sr.Close();
                                fs.Close();


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
                                if (phones != "")
                                    html = html.Replace("@phones@", phones);
                                else
                                    html = html.Replace("@phones@", "");
                                if (webpage != "")
                                    html = html.Replace("@web_page@", webpage);
                                else
                                    html = html.Replace("@web_page@", "");

                                html = html.Replace("@conceptos@", conceptos);
                                html = html.Replace("@comments@", "");
                                html = html.Replace("@vehicle_info@", "");
                                html = html.Replace("@folio@", serie + " " + folio);
                                html = html.Replace("@fecha@", fechaNow + " " + horaNow);
                                if (storeid == "1")
                                {
                                    html = html.Replace("@autorizacion@", "1045892");
                                    html = html.Replace("@fecha_aut@", "10/02/2013 16:50:00");      //Certificado anterior a 2013
                                }
                                else
                                {
                                    html = html.Replace("@autorizacion@", "1045820");
                                    html = html.Replace("@fecha_aut@", "10/02/2013 16:35:00");      //Certificado anterior a 2013
                                }

                                html = html.Replace("@ano_aut@", "2013");

                                html = html.Replace("@QRCODE@", "" + folioSerie2 + ".png");


                                html = html.Replace("@fechaSAT@", fechaSAT);
                                html = html.Replace("@folioSAT@", folioSAT);
                                html = html.Replace("@certificadoSAT@", certificadoSAT);


                                html = html.Replace("@RFC@", rfc.Replace("-", "").Trim());
                                html = html.Replace("@Nombre@", this.CleanString(nombre));
                                string nint = "";
                                if (noint != "SN")
                                    nint = noint;
                                html = html.Replace("@Direccion@", calle + " " + no + " " + nint);
                                html = html.Replace("@Colonia@", col);
                                html = html.Replace("@ciudad@", loc);
                                html = html.Replace("@estado@", edo);
                                html = html.Replace("@CP@", cp);

                                //if (this.mskDiscount.Text != "" & this.mskDiscount.Text != "0.00")
                                //    html = html.Replace("@descuento@", "Descuento: $" + this.mskDiscount.Text);
                                //else
                                html = html.Replace("@descuento@", "");

                                html = html.Replace("@subtotal@", subtotal.ToString("c"));
                                html = html.Replace("@iva@", iva.ToString("c"));
                                html = html.Replace("@retiva@", Convert.ToDouble(retiva).ToString("c"));
                                html = html.Replace("@total@", total.ToString("c"));
                                html = html.Replace("@cantidad@", convertirNumero(total, 1));

                                html = html.Replace("@pay_method@", metodoPago);
                                html = html.Replace("@acc_number@", cuentaPago);

                                html = html.Replace("@p_method@", mpago.Replace("ó", "&oacute;"));
                                html = html.Replace("@cfdi_usage@", usocfdi.Replace("ó", "&oacute;").Replace("é", "&eacute;"));

                                html = html.Replace("@cadena@", cadenaOriginal);

                                html = html.Replace("@expedicion@", expedicion3);

                                html = html.Replace("@condiciones@", "2002");

                                html = html.Replace("@sello@", sello);
                                html = html.Replace("@selloSAT@", selloSAT);
                                html = html.Replace("@cadenaSAT@", cadenaSAT);
                                fs = new System.IO.FileStream(@"C:/inetpub/wwwroot/reycabrito/cfd/" + folioSerie2 + ".html", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
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

                                string path = Convert.ToString(pdfConverter.LicenseFilePath + @"cfd\" + folioSerie2 + ".html").ToUpper();
                                byte[] pdf = pdfConverter.GetPdfBytesFromHtmlFile(path);

                                string fileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"cfd\" + folioSerie2 + ".pdf";
                                using (System.IO.BinaryWriter binWriter = new System.IO.BinaryWriter(System.IO.File.Open(fileName, System.IO.FileMode.Create)))
                                    binWriter.Write(pdf);

                                fileName = @"C:\inetpub\wwwroot\reycabrito\cfd\" + folioSerie2 + ".pdf";
                                
                                downloadCfdi = "CFDI generado, puede descargar los archivos<br/><a href=\"/cfd/" + folioSerie2 + ".pdf\" download=\"" + folioSerie2 + ".pdf\">Descargar archivo PDF</a> <a href=\"/cfd/" + folioSerie2 + ".xml\" download=\"" + folioSerie2 + ".xml\">&nbsp;&nbsp;&nbsp;Descargar archivo XML</a> ";
                                sendEmail(fileName, fileName.Replace("pdf", "xml"));
                            }
                            #endregion Genera CFDI
                            this.dh.Commit();

                        }
                        catch (Exception ex)
                        {
                            try { this.dh.Rollback(); }
                            catch { }
                            downloadCfdi = "Error: " + ex.Message;
                        }

                    }

                }
                #endregion

                dh.Disconnect();
            }
           

        }
        catch (Exception ex)
        { Response.Write(ex.ToString());
            ostable = ex.ToString();
        }

       
    }

    private void sendEmail(string pdf, string xml)
    {
        try
        {
            System.Net.Mail.MailMessage aMessage = new System.Net.Mail.MailMessage();
            aMessage.From = new System.Net.Mail.MailAddress("elreydelcabrito@hotmail.com", "Rey del Cabrito");

            aMessage.To.Add(email);
            aMessage.ReplyTo = new System.Net.Mail.MailAddress("elreydelcabrito@hotmail.com");
            aMessage.Subject = "Facturación Rey del Cabrito";
            string msg = "Gracias por su preferencia, se anexan los documentos del CFDI.<p>";
            System.Net.Mail.AlternateView htmlView = System.Net.Mail.AlternateView.CreateAlternateViewFromString(msg, System.Text.Encoding.UTF8, "text/html");
            aMessage.IsBodyHtml = true;
            aMessage.BodyEncoding = System.Text.Encoding.UTF8;
            System.Net.Mail.Attachment attach;

            attach = new System.Net.Mail.Attachment(pdf);
            aMessage.Attachments.Add(attach);
            attach = new System.Net.Mail.Attachment(xml);
            aMessage.Attachments.Add(attach);

            aMessage.AlternateViews.Add(htmlView);
            System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient();
            mailClient.Host = "smtp.gmail.com";
            mailClient.Port = 587;
            mailClient.EnableSsl = true;
            mailClient.Credentials = new System.Net.NetworkCredential("mailing@inf.com.mx", "m4iling2");

            mailClient.Send(aMessage);

            downloadCfdi += "<p>Correo enviado";

        }
        catch (Exception ex)
        {
            downloadCfdi += "<p><i class=\"w3-red\">No se pudo enviar el correo: +" + ex.Message + "</i>";
        }
    }

    private string CleanString(string str)
    {
        return str.Replace("é", "e").Replace("á", "a").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").Replace("ñ", "n").Replace("É", "E").Replace("Á", "A").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U").Replace("Ñ", "N").Replace("ü", "u").Replace("Ü", "U").ToUpper();
    }


    #region ConvertirNumero
    public string convertirNumero(double Numero, int Mayusculas)
    {
        string str = "";
        int cen = 0;
        int dec = 0;
        int uni = 0;
        string str5 = "";
        string str6 = "";
        string str7 = "";
        if (Numero < 0)
        {
            Math.Abs(Numero);
        }
        str = Numero.ToString("000000000000000.00");
        int num = 1;
        int startIndex = 0;
        str7 = "";
        while (num <= 5)
        {
            int num2 = 1;
            while (num2 <= 3)
            {
                int num4 = 0;
                try
                {
                    num4 = Convert.ToInt32(str.Substring(startIndex, 1));
                }
                catch { }
                switch (num2)
                {
                    case 1:
                        cen = num4;
                        break;

                    case 2:
                        dec = num4;
                        break;

                    case 3:
                        uni = num4;
                        break;
                }
                num2++;
                startIndex++;
            }
            string str4 = this.Centena(uni, dec, cen);
            this.amountText = "";
            string str3 = this.Decena(uni, dec);
            this.amountText = "";
            string str2 = this.Unidad(uni, dec);
            this.amountText = "";
            switch (num)
            {
                case 1:
                    if (((cen + dec) + uni) != 1)
                    {
                        break;
                    }
                    str5 = "Billon ";
                    goto Label_01D3;

                case 2:
                    if (!((((cen + dec) + uni) >= 1) & (str.Substring(7, 3) == "0")))
                    {
                        goto Label_0162;
                    }
                    str5 = "Mil Millones ";
                    goto Label_01D3;

                case 3:
                    if (!(((cen + dec) == 0) & (uni == 1)))
                    {
                        goto Label_018F;
                    }
                    str5 = "Millon ";
                    goto Label_01D3;

                case 4:
                    if (((cen + dec) + uni) >= 1)
                    {
                        str5 = "Mil ";
                    }
                    goto Label_01D3;

                case 5:
                    if (((cen + dec) + uni) >= 1)
                    {
                        str5 = "";
                    }
                    goto Label_01D3;

                default:
                    goto Label_01D3;
            }
            if (((cen + dec) + uni) > 1)
            {
                str5 = "Billones ";
            }
            goto Label_01D3;
        Label_0162:
            if (((cen + dec) + uni) >= 1)
            {
                str5 = "Mil ";
            }
            goto Label_01D3;
        Label_018F:
            if (((cen > 0) | (dec > 0)) | (uni > 1))
            {
                str5 = "Millones ";
            }
        Label_01D3:
            num++;
            str7 = str7 + str4 + str3 + str2 + str5;
            str5 = "";
            str2 = "";
            str3 = "";
            str4 = "";
        }
        if ((str == "0") || (Convert.ToDouble(str) < 1))
        {
            str6 = "Cero Pesos ";
        }
        else if ((str == "1") | (Convert.ToDouble(str) < 2))
        {
            str6 = "Peso ";
        }
        else if ((str.Substring(4, 12) == "0") | (str.Substring(10, 6) == "0"))
        {
            str6 = "de Pesos ";
        }
        else
        {
            str6 = "Pesos ";
        }
        str7 = "(" + str7 + str6 + str.Substring(0x10, 2) + "/100 M.N.)";
        if (Mayusculas == 1)
        {
            str7 = str7.ToUpper();
        }
        return str7;
    }

    public string Unidad(int uni, int dec)
    {
        if (dec != 1)
        {
            switch (uni)
            {
                case 1:
                    this.amountText = "un ";
                    break;

                case 2:
                    this.amountText = "dos ";
                    break;

                case 3:
                    this.amountText = "tres ";
                    break;

                case 4:
                    this.amountText = "cuatro ";
                    break;

                case 5:
                    this.amountText = "cinco ";
                    break;
            }
        }
        switch (uni)
        {
            case 6:
                this.amountText = "seis ";
                break;

            case 7:
                this.amountText = "siete ";
                break;

            case 8:
                this.amountText = "ocho ";
                break;

            case 9:
                this.amountText = "nueve ";
                break;
        }
        return this.amountText;
    }

    public string Decena(int uni, int dec)
    {
        switch (dec)
        {
            case 1:
                switch (uni)
                {
                    case 0:
                        this.amountText = "diez ";
                        break;

                    case 1:
                        this.amountText = "once ";
                        break;

                    case 2:
                        this.amountText = "doce ";
                        break;

                    case 3:
                        this.amountText = "trece ";
                        break;

                    case 4:
                        this.amountText = "catorce ";
                        break;

                    case 5:
                        this.amountText = "quince ";
                        break;

                    case 6:
                        this.amountText = "dieci";
                        break;

                    case 7:
                        this.amountText = "dieci";
                        break;

                    case 8:
                        this.amountText = "dieci";
                        break;

                    case 9:
                        this.amountText = "dieci";
                        break;
                }
                break;

            case 2:
                if (uni != 0)
                {
                    if (uni > 0)
                    {
                        this.amountText = "veinti";
                    }
                    break;
                }
                this.amountText = "veinte ";
                break;

            case 3:
                this.amountText = "treinta ";
                break;

            case 4:
                this.amountText = "cuarenta ";
                break;

            case 5:
                this.amountText = "cincuenta ";
                break;

            case 6:
                this.amountText = "sesenta ";
                break;

            case 7:
                this.amountText = "setenta ";
                break;

            case 8:
                this.amountText = "ochenta ";
                break;

            case 9:
                this.amountText = "noventa ";
                break;

            default:
                this.amountText = "";
                break;
        }
        if ((uni > 0) && (dec > 2))
        {
            this.amountText = this.amountText + "y ";
        }
        return this.amountText;
    }

    public string Centena(int uni, int dec, int cen)
    {
        switch (cen)
        {
            case 1:
                if ((dec + uni) != 0)
                {
                    this.amountText = "ciento ";
                    break;
                }
                this.amountText = "cien ";
                break;

            case 2:
                this.amountText = "doscientos ";
                break;

            case 3:
                this.amountText = "trescientos ";
                break;

            case 4:
                this.amountText = "cuatrocientos ";
                break;

            case 5:
                this.amountText = "quinientos ";
                break;

            case 6:
                this.amountText = "seiscientos ";
                break;

            case 7:
                this.amountText = "setecientos ";
                break;

            case 8:
                this.amountText = "ochocientos ";
                break;

            case 9:
                this.amountText = "novecientos ";
                break;

            default:
                this.amountText = "";
                break;
        }
        return this.amountText;
    }
    #endregion Convertirnumero

    protected void cmbEdo_SelectedIndexChanged(object sender, EventArgs e)
    {
        dh.Query("select * from  cities where state_id=" + cmbEdo.SelectedValue + "");
        cmbLoc.DataValueField = "city_id";
        cmbLoc.DataTextField = "city_name";
        cmbLoc.DataSource = dh.DataTable;
        cmbLoc.DataBind();
    }
}