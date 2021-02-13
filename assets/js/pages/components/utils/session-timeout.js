"use strict";

var KTSessionTimeoutDemo = function () {

    var initDemo = function () {
        $.sessionTimeout({
            title: 'Notificación de fin de sesión',
            message: 'La sesión esta por terminar.',
            keepAliveUrl: 'KeepAlive.aspx',
            redirUrl: '?p=page_login',
            logoutUrl: '?p=page_login',
            warnAfter: 900000, //warn after 5 seconds
            redirAfter: 930500, //redirect after 10 secons,
            ignoreUserActivity: true,
            countdownMessage: 'Redireccionando en  {timer} segundos.',
            countdownBar: true,
            onStart: function (object) {
                
            }
        });

        $("#session-timeout-dialog-logout").html("Salir");
        $("#session-timeout-dialog-keepalive").html("Mantener Conexión");
        

    }

    return {
        //main function to initiate the module
        init: function () {
            initDemo();
        }
    };

}();

jQuery(document).ready(function() {    
    KTSessionTimeoutDemo.init();
});