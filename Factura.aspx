<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="Factura.aspx.cs" Inherits="Factura" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Facturación Rey del Cabrito</title>


</head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<link rel="stylesheet" href="https://www.w3schools.com/lib/w3.css">
<link rel='stylesheet' href='https://fonts.googleapis.com/css?family=Roboto'>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css?parameter=1">

<style>
html,body,h1,h2,h3,h4,h5,h6 {font-family: "Roboto", sans-serif}
</style>
<body class="w3-light-grey">

<form id="form1" runat="server">


<!-- Page Container -->
<div class="w3-content w3-margin-top" style="max-width:1400px;">
<!-- Menu icon to open sidenav -->
  
  <!-- The Grid -->
  <div class="w3-row-padding">

    <!-- Left Column -->
    <div class="w3">
      <div class="w3-container w3-card-2 w3-white w3-margin-bottom">
      <div class="w3-display-container">
          <img src="CFD/conf/logo.png" width="200" alt="" >
          <!--<div class="w3-display-bottomright w3-container w3-text-black w3-padding-12">
          <a href="Login.aspx?mode=battery"><span class="glyphicon glyphicon-log-out"></span> Sesión</a>
          </div>-->
        </div>
        <h4 class="w3-text-grey w3-padding-6"><i class="fa-fw w3-margin-right w3-xlarge w3-text-yellow"></i>Factura</h4>
        <!--<div class="w3-container">-->
       
        <table>
            <tr>
            <tr>
            <td>ID:</td>
            <td><input class="w3-input w3-border" type="text" value="<%Response.Write(soid); %>" id="soid" name="soid"/></td>
            <td></td>
            </tr>
            <td>Cod Fact:</td>
            <td><input class="w3-input w3-border" type="text" value="<%Response.Write(dtsoid); %>"  id="dtsoid" name="dtsoid"/></td>
            <td></td>
            </tr>
            <tr>
            <td>RFC:</td>
            <td><input class="w3-input w3-border" type="text" value="<%Response.Write(lcode); %>" id="lcode" name="lcode"/></td>
            <td><button id="Button2" runat="server" class="w3-btn w3-yellow w3-large w3-opacity w3-hover-opacity-off">Buscar</button></td>
            </tr>
           
            </table>


        <div class="w3-container">
        <h4>Cliente: <% Response.Write(cname); %></h4>
        <% Response.Write(info); %>
        <div class="w3-half">
        <table>
          <tr>
          <td><i class="fa fa-calendar fa-fw w3-margin-right w3-large w3-text-yellow"></i></td><td>Fecha:</td><td> <b><% Response.Write(date);%></b></td>
          </tr>
          <tr>
          <tr>
          <td><i class="fa fa-asterisk fa-fw w3-margin-right w3-large w3-text-yellow"></i></td><td>Razón:</td><td><b><input type="text" name="trazon" value="<% Response.Write(legalname); %>" /></b></td>
          </tr>
          <tr>
          <td><i class=" fa-fw w3-margin-right w3-large w3-text-yellow"></i></td><td>RFC:</td><td><b><input type="text" name="trfc" value="<% Response.Write(legalcode); %>" /></b></td>
          </tr>
          <td><i class="fa fa-home fa-fw w3-margin-right w3-large w3-text-yellow"></i></td><td>Calle:</td><td> <b><input type="text" name="calle" value="<% Response.Write(calle); %>" /></b></td>
          </tr>
          <tr>
          <td><i class=" fa-fw w3-margin-right w3-large w3-text-yellow"></i></td><td>No ext:</td><td> <b><input type="text" name="noext" value="<% Response.Write(no); %>" /> </b></td>
          </tr>
           <tr>
          <td><i class=" fa-fw w3-margin-right w3-large w3-text-yellow"></i></td><td>No Int:</td><td> <b><input type="text"  name="noint" value="<% Response.Write(noint); %>" /></b></td>
          </tr>
          <tr>
          <td><i class=" fa-fw w3-margin-right w3-large w3-text-yellow"></i></td><td>Colonia:</td><td> <b><input type="text" name="col" value="<% Response.Write(col); %>" /></b></td>
          </tr>
         </table>
         </div>
         <div id="rcolumn" class="w3-half">
         <table>
          <tr>
          <td><i class="fa-fw w3-margin-right w3-large w3-text-yellow"></i></td><td>Estado:</td><td> <b>
              <asp:DropDownList name="cmbEdo" ID="cmbEdo" runat="server" 
                AutoPostBack="true"  onselectedindexchanged="cmbEdo_SelectedIndexChanged">
              </asp:DropDownList></b></td>
          </tr>
          <tr>
             <tr>
          <td><i class=" fa-fw w3-margin-right w3-large w3-text-yellow"></i></td><td>Ciudad:</td><td> <b>
              <asp:DropDownList name="cmbLoc" ID="cmbLoc" runat="server" >
              </asp:DropDownList></b></td>
          </tr>
          <tr>
          <td><i class=" fa-fw w3-margin-right w3-large w3-text-yellow"></i></td><td>CP:</td><td> <b><input type="text" name="cp" value="<% Response.Write(cp); %>" /></b></td>
          </tr>
          <tr>
          <td><i class="fa fa-money fa-fw w3-margin-right w3-large w3-text-yellow"></i></td><td>Met Pago:</td><td> <b><% Response.Write(paymenttype);%></b></td>
          </tr>
           <tr>
          <td><i class=" fa-fw w3-margin-right w3-large w3-text-yellow"></i></td><td>Cta Pago:</td><td> <b><input type="text" name="ctapago" value="<% Response.Write(payacc); %>" /></b></td>
          </tr>
          <tr>
          <td><i class="fa fa-envelope fa-fw w3-margin-right w3-large w3-text-yellow"></i></td><td>Email:</td><td> <b><input type="text" name="email" value="<% Response.Write(email); %>"/></b></td>
          </tr>
          <tr>
          <td><i class="fa fa-phone fa-fw w3-margin-right w3-large w3-text-yellow"></i></td><td>Teléfono:</td><td><b><% Response.Write(tel); %></b></td>
          </tr>
           <td><i class=" fa-fw w3-margin-right w3-large w3-text-yellow"></i></td><td>Uso CFDI:</td><td> <b>
              <asp:DropDownList name="cmbUso" style="width: 150px;" ID="cmbUso" runat="server" >
              </asp:DropDownList></b></td>
          </tr>

          
         </table>
         </div>
         <hr>
         </div>

          <% Response.Write(addprotable);%>
          <% Response.Write(btnFinishSO);%>
          <hr>
          <% Response.Write(downloadCfdi); %>
        <!--</div>-->
      
      </div>

    <!-- End Left Column -->
    </div>
    
  <!-- End Grid -->
  </div>
  
  <!-- End Page Container -->
</div>


<footer class="w3-container w3-yellow w3-center w3-margin-top">
 
</footer>
</form>
<script>
// Open and close sidenav
function openNav() {
    document.getElementById("mySidenav").style.width = "200px";
    document.getElementById("mySidenav").style.display = "block";
}

function closeNav() {
    document.getElementById("mySidenav").style.display = "none";
}

function getAlert()
{
    document.getElementById("btnSubmit").click(); 
}
</script>

</body>
</html>
