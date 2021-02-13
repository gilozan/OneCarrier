<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QR.aspx.cs" Inherits="QR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Validador de CFDI con QR</title>
    <script src="js/jsQR.js"></script>
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

<!-- Page Container -->
<div class="w3-content w3-margin-top" style="max-width:1400px;">
 <!-- Left Column -->
    <div class="w3">
        <div class="w3-container w3-card-2 w3-white w3-margin-bottom">
            <div class="w3-display-container">
            <p></p>
                <form id="form1" runat="server">
                <div>
                <% if (resp == "")
                   {
                       hasData = "Si";
                       %>
                <div id="loadingMessage">🎥 No hay acceso a la cámara (revisar que esté habilitada)</div>
                    
                <% }%>
                  <canvas id="canvas" hidden></canvas>
                  <div id="output" hidden>
                    <div id="outputMessage">No se detecto código QR.</div>
                    <div hidden><b>Info:</b> <span id="outputData"></span></div>
                    <asp:TextBox ID="TextBox" size=16 placeholder="Contenido" class="w3-input w3-border" runat="server"></asp:TextBox>
                    <asp:Button ID="button" runat="server" class="w3-btn w3-large w3-opacity w3-hover-opacity-off" style="background-color:#5d78ff" Text="Ok" onclick="button_Click" />
                    <a href="Default.aspx" class="w3-btn w3-large w3-opacity w3-hover-opacity-off" style="background-color:#5d78ff"> Regresar a Invoice</a>
                  </div>
                    <%Response.Write(resp); %>
                </div>
                </form>
            </div>
        </div>
    </div>
</div>
<script>
    var video = document.createElement("video");
    var canvasElement = document.getElementById("canvas");
    var canvas = canvasElement.getContext("2d");
    var loadingMessage = document.getElementById("loadingMessage");
    var outputContainer = document.getElementById("output");
    var outputMessage = document.getElementById("outputMessage");
    var outputData = document.getElementById("outputData");

    function drawLine(begin, end, color) {
      canvas.beginPath();
      canvas.moveTo(begin.x, begin.y);
      canvas.lineTo(end.x, end.y);
      canvas.lineWidth = 4;
      canvas.strokeStyle = color;
      canvas.stroke();
    }

    if(document.getElementById("TextBox").value==="")
    {
        // Use facingMode: environment to attemt to get the front camera on phones
        navigator.mediaDevices.getUserMedia({ video: { facingMode: "environment" } }).then(function(stream) {
          video.srcObject = stream;
          video.setAttribute("playsinline", true); // required to tell iOS safari we don't want fullscreen
          video.play();
          requestAnimationFrame(tick);
        });
    }

    function refresh(){
        
       document.getElementById("TextBox").value="";
       location.reload();
    }

    function tick() {
      loadingMessage.innerText = "⌛ Cargando video..."
      if (video.readyState === video.HAVE_ENOUGH_DATA) {
        loadingMessage.hidden = true;
        canvasElement.hidden = false;
        outputContainer.hidden = false;

        canvasElement.height = video.videoHeight;
        canvasElement.width = video.videoWidth;
        canvas.drawImage(video, 0, 0, canvasElement.width, canvasElement.height);
        var imageData = canvas.getImageData(0, 0, canvasElement.width, canvasElement.height);
        var code = jsQR(imageData.data, imageData.width, imageData.height, {
          inversionAttempts: "dontInvert",
        });
        if (code) {
          drawLine(code.location.topLeftCorner, code.location.topRightCorner, "#FF3B58");
          drawLine(code.location.topRightCorner, code.location.bottomRightCorner, "#FF3B58");
          drawLine(code.location.bottomRightCorner, code.location.bottomLeftCorner, "#FF3B58");
          drawLine(code.location.bottomLeftCorner, code.location.topLeftCorner, "#FF3B58");
          outputMessage.hidden = true;
          outputData.parentElement.hidden = false;
          outputData.innerText = code.data;
          document.getElementById("TextBox").value= code.data;
          document.getElementById("button").click();

        } else {
          outputMessage.hidden = false;
          outputData.parentElement.hidden = true;
        }
      }
      requestAnimationFrame(tick);
    }
  </script>
</body>
</html>
