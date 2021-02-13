<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Authorize.aspx.cs" Inherits="Authorize" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Autorizar</title>
    
    <META HTTP-EQUIV="refresh" CONTENT="10;URL=https://onecarrier.inf.com.mx">
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <a href="https://onecarrier.inf.com.mx"><img border=0 src="https://onecarrier.inf.com.mx/assets\media\logos\logo_onecarrier.png"></a><p>
      <% 

          Hzone.Api.Database.DataHelper dh = new Hzone.Api.Database.DataHelper();
          dh.ConnectionType = Hzone.Api.Database.ConnectionType.Odbc;
          dh.ConnectionUrl = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;
         try
          {
            dh.Connect();
            dh.Update("update customer_guide_range set authorized=CURRENT_TIMESTAMP, authorized_by='" + Request.Params["userid"] + "' where customer_guide_range_id  = "+Request.Params["id"]+" AND token='"+Request.Params["token"]+"'");
            if(dh.exception.Message=="No Exception")
            {
                if(dh.recordsAffected>0)
                    Response.Write("Se autorizó el rango correctamente<p>");
                else
                   Response.Write("La cadena de autorización es incorrecta, no se pudo autorizar<p>");
            }
            else
                Response.Write("No se pudo autorizar, error:"+ dh.exception.Message);
           
            dh.Disconnect();
         }
        catch { }
         %>
    </div>
    </form>
</body>
</html>
