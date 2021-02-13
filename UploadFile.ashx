<%@ WebHandler Language="C#" Class="UploadFile" %>

using System;
using System.IO;
using System.Web;
using SAT.Services.ConsultaCFDIService;
using SW.Services.Status;
using System.Xml.Serialization;
using System.Configuration;

public class UploadFile : IHttpHandler, System.Web.SessionState.IRequiresSessionState{
    
    public void ProcessRequest (HttpContext context) 
    {
        context.Response.ContentType = "text/plain";

        DateTime start = DateTime.Now;

        string dirFullPath = HttpContext.Current.Server.MapPath("~/MediaUploader/");
        string[] files;
        int numFiles;
        files = System.IO.Directory.GetFiles(dirFullPath);

        numFiles = files.Length;
        numFiles = numFiles + 1;
        string str_image = "";
        string resp = "";
        string po = context.Request.Form["po"];
        string supplierid = HttpContext.Current.Session["supplierid"].ToString();
        //if (po == "")
        //{
        //    context.Response.Write("error,No ha seleccionado orden de compra");
        //    return;
        //}
        //else
        foreach (string s in context.Request.Files)
        {
            HttpPostedFile file = context.Request.Files[s];
            //  int fileSizeInBytes = file.ContentLength;
            string fileName = file.FileName;
            string fileExtension = file.ContentType;
            
            System.IO.Stream fileStream= file.InputStream;

            if (!string.IsNullOrEmpty(fileName))
            {
                fileExtension = Path.GetExtension(fileName);
                Hzone.Api.Database.DataHelper DataHelper = new Hzone.Api.Database.DataHelper();
                DataHelper.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
                DataHelper.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
                
                if (fileExtension != ".xml")
                {
                    if (fileExtension == ".pdf")
                    {
                        if (supplierid == "2")
                        {

                            string type = "I";
                            string emisor = "COCA COLA COMPANY";
                            string fecha = "2020-05-23";
                            //"2";//
                            //string folio = DateTime.Now.Ticks;


                            DataHelper.Connect();
                            DataHelper.Update("UPDATE pch_orders set processed='1' where pch_order_id=" + (Convert.ToInt32(po)+20) + "");
                            DataHelper.Update("INSERT INTO notifications (notif_id,user_id,name,created_by,updated_by,created,updated) VALUES (nextval('sq_notifications'),3,'Se agregó la factura INT - 295554 del proveedor " + emisor + " ',4,4,now(),now()) ");
                            DataHelper.Update("INSERT INTO pch_invoices (pch_invoice_id,pch_order_id,serie,folio,uuid,type,legal_name,status,subtotal,tax,total,date_invoiced,created,updated,created_by,updated_by,supplier_id) VALUES (nextval('sq_pch_invoices')," + po + ",'INT','295554','32AE5392-2323AB2323-235BB3','" + type + "','" + emisor + "',1,1400," + 0 + ",1400,'" + fecha + "',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP,4,4," + supplierid + ")");
                            if (DataHelper.exception.Message == "No Exception")
                            { }
                            General.sendEmail(General.sendEmailTo, "Alta de Factura", "Se ha dado de alta una factura del proveedor " + emisor + "<p>");
                            DataHelper.Disconnect();
                        }
                        string pathToSave_100 = HttpContext.Current.Server.MapPath("~/MediaUploader/") + file.FileName;
                        file.SaveAs(pathToSave_100);
                        TimeSpan elapsed = DateTime.Now - start;
                        int sleep = 1000;
                        if (elapsed.TotalSeconds < 1)
                            sleep = Convert.ToInt32(1500 - elapsed.TotalSeconds);
                        System.Threading.Thread.Sleep(sleep);
                        context.Response.Write("success,Archivo Cargado"); 
                    }                        
                    else if (fileExtension == ".txt")
                    {
                        DataHelper.Connect();
                        DataHelper.Update("INSERT INTO notifications (notif_id,user_id,name,created_by,updated_by,created,updated) VALUES (nextval('sq_notifications'),4,'Se crearon 5 Órdenes de Compra',4,4,now(),now()) ");
                        DataHelper.Disconnect();
                        TimeSpan elapsed = DateTime.Now - start;
                        int sleep = 1500;
                        if (elapsed.TotalSeconds < 1)
                            sleep = Convert.ToInt32(1500 - elapsed.TotalSeconds);
                        System.Threading.Thread.Sleep(sleep);
                        context.Response.Write("success,Archivo Cargado");
                        General.sendEmail(General.sendEmailTo, "Se han cargado 5 órdenes de compra", "Estimado Proveedor<p>Le anunciamos que han sido cargadas 5 órdenes de compra, por lo cual, puede proceder al portal para dar de alta las facturas correspondientes");
                    }
                    else
                        context.Response.Write("No es un XML válido");
                }
                else
                {
                    string pathToSave_100 ="";
                    Comprobante oComprobante;
                    XmlSerializer serialize = new XmlSerializer(typeof(Comprobante));

                    using (StreamReader reader = new StreamReader(file.InputStream))
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
                                }
                            }

                            str_image = oComprobante.TimbreFiscalDigital.UUID + fileExtension;
                            pathToSave_100 = HttpContext.Current.Server.MapPath("~/MediaUploader/") + str_image;
                            file.SaveAs(pathToSave_100);
                        }
                        catch (Exception ex) { resp = "error,El Xml no cuenta con complemento"; }
                    }
                    if (resp == "")
                    {
                        
                        try
                        {
                            Status status = new Status("https://consultaqr.facturaelectronica.sat.gob.mx/ConsultaCFDIService.svc");
                            Acuse response = status.GetStatusCFDI(oComprobante.Emisor.Rfc, oComprobante.Receptor.Rfc, oComprobante.Total.ToString(), oComprobante.TimbreFiscalDigital.UUID);
                            if (response.CodigoEstatus.StartsWith("S"))
                            {
                                if (response.Estado.ToLower() == "cancelado")
                                {
                                    resp = "error,";
                                    System.IO.File.Delete(pathToSave_100);
                                }
                                else
                                {



                                    string type = oComprobante.TipoDeComprobante;
                                    string emisor = oComprobante.Emisor.Nombre;
                                    string fecha = oComprobante.Fecha.Replace("T", " ");
                                    string pstatus = "1";
                                    string impuestos = "0";
                                    //"2";//

                                    DataHelper.Connect();

                                    if (type == "P")
                                    {
                                        po = "-1";
                                        pstatus = "4";
                                    }
                                    try
                                    {
                                        impuestos = oComprobante.Impuestos.TotalImpuestosTrasladados.ToString();
                                    }
                                    catch { }

                                    DataHelper.Query("INSERT INTO pch_invoices (pch_invoice_id,pch_order_id,serie,folio,uuid,type,legal_name,status,subtotal,tax,total,date_invoiced,created,updated,created_by,updated_by,supplier_id) VALUES (nextval('sq_pch_invoices')," + po + ",'" + oComprobante.Serie + "','" + oComprobante.Folio + "','" + oComprobante.TimbreFiscalDigital.UUID + "','" + type + "','" + emisor + "'," + pstatus + "," + oComprobante.SubTotal + "," + impuestos + "," + oComprobante.Total + ",'" + fecha + "',CURRENT_TIMESTAMP,CURRENT_TIMESTAMP,4,4," + supplierid + ")");
                                    if (DataHelper.exception.Message == "No Exception")
                                    {
                                        resp = "success,";
                                        DataHelper.Update("UPDATE pch_orders set processed='1' where pch_order_id=" + po + "");
                                        DataHelper.Update("INSERT INTO notifications (notif_id,user_id,name,created_by,updated_by,created,updated) VALUES (nextval('sq_notifications'),3,'Se agregó la factura "+oComprobante.Serie+"-"+oComprobante.Folio+" del proveedor "+emisor+" ',4,4,now(),now()) ");
                                        General.sendEmail(General.sendEmailTo, "Alta de Factura", "Se ha dado de alta una factura del proveedor " + emisor + "<p>");
                                        //str_image = oComprobante.TimbreFiscalDigital.UUID + fileExtension;
                                        //string pathToSave_100 = HttpContext.Current.Server.MapPath("~/MediaUploader/") + str_image;
                                        //using (FileStream output = File.OpenWrite(pathToSave_100))
                                        //{
                                        //    fileStream.CopyTo(output);
                                        //}
                                        //file.SaveAs(pathToSave_100);
                                    }
                                    else
                                    {
                                        resp = "error,No cuenta con Orden de Compra<p>";
                                        System.IO.File.Delete(pathToSave_100);
                                    }

                                    DataHelper.Disconnect();
                                }

                            }
                            else
                            {
                                resp = "error,";
                                System.IO.File.Delete(pathToSave_100);
                            }
                            resp += response.CodigoEstatus;
                            resp += "<p>Estatus: " + response.Estado;
                        }
                        catch (Exception ex) { resp = "error,No cumple con los parámetros para 3 way Match<p>El total no coincide"; System.IO.File.Delete(pathToSave_100); }
                        
                    }
                    TimeSpan elapsed = DateTime.Now - start;
                    int sleep = 1000;
                    if (elapsed.TotalSeconds < 1)
                        sleep = Convert.ToInt32(1500 - elapsed.TotalSeconds);
                    System.Threading.Thread.Sleep(sleep);
                    context.Response.Write(resp);
                }
            }
        } 
        
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}