using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAT.Services.ConsultaCFDIService;
using SW.Services.Status;
using System.Configuration;

public partial class QR : System.Web.UI.Page
{
    public string resp = "";
    public string hasData = "";
    public Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
    public string codStatus = "";
    public string estatus = "";
    public string cancelable = "";
    public string statusCanc = "";
    public string uuid = "";
    public string emisor = "";
    public string receptor = "";
    public string total = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void button_Click(object sender, EventArgs e)
    {
        dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
        dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
        dh.Connect();
        if (this.TextBox.Text != "")
        {
            try
            {
                Status status = new Status("https://consultaqr.facturaelectronica.sat.gob.mx/ConsultaCFDIService.svc");
                string qrcont = TextBox.Text;
                //this.TextBox.Text = "";
                string[] data = qrcont.Split('&');
         
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i].Contains("id"))
                    {
                        uuid = data[i].Replace("https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id=", "").Replace("id=", "");
                        continue;
                    }
                    if (data[i].Contains("re"))
                    {
                        emisor = data[i].Replace("re=", "");
                        continue;

                    }
                    if (data[i].Contains("rr"))
                    {
                        receptor = data[i].Replace("rr=", "");
                        continue;

                    }
                    if (data[i].Contains("tt"))
                    {
                        total = data[i].Replace("tt=", "");
                        continue;

                    }
                }

                //Acuse response = status.GetStatusCFDI("CVA9904266T9", "ISO090626DD1", "323.23", "438032cd-dd81-4a24-b42b-5e48287cbc36");
                Acuse response = status.GetStatusCFDI(emisor, receptor, total, uuid);
                codStatus = response.CodigoEstatus;
                estatus = response.Estado.ToString();
                cancelable = response.EsCancelable;
                statusCanc = response.EstatusCancelacion;

                string totalst = "";
                try { totalst = Convert.ToDouble(total).ToString("c"); }
                catch { totalst = total; }
                resp += "<p>UUID: " + uuid;
                resp += "<p>Emisor: " + emisor;
                resp += "<p>Receptor: " + receptor;
                resp += "<p>Monto: " + totalst;
                resp += "<p>" + response.CodigoEstatus;
                resp += "<p>Estatus: " + response.Estado;
                resp += "<p>Cancelable: " + response.EsCancelable;
                resp += "<p>Estatus Cancelación: " + response.EstatusCancelacion;
                resp += "<p><button class=\"w3-btn w3-large w3-opacity w3-hover-opacity-off\" style=\"background-color:#5d78ff\" onclick=\"refresh();\" >Continuar</button>";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Input string"))
                    resp = "<i class=\"w3-red\">El Formato de QR no corresponde con un código del SAT</i>";
                else
                    resp = "<i class=\"w3-red\">" + ex.ToString() + "</i>";
                    resp += "<button class=\"w3-btn w3-large w3-opacity w3-hover-opacity-off\" style=\"background-color:#5d78ff\" onclick=\"location=URL\" >Continuar</button>";
            }
        }
        else
        {
            resp += "<i class=\"w3-red\">Favor de seleccionar archivo</i>";
            resp += "<button class=\"w3-btn w3-large w3-opacity w3-hover-opacity-off\" style=\"background-color:#5d78ff\" onclick=\"location=URL\" >Continuar</button>";
        }
        dh.Update("INSERT INTO qr_log VALUES (nextval('sq_qr'),'" + uuid + "','" + emisor + "','" + receptor + "','" + total + "','" + codStatus + "','" + estatus + "','" + cancelable + "','" + statusCanc + "')");
        // resp += dh.exception;
        dh.Disconnect();
    }
}