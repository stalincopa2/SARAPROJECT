/*AJAX ENVIAR COMPRA CON DETALLE cobrando*/
$(document).ready(function () {
    $(document).on('click', '.btnEnviar', function (e) {
        e.preventDefault();
        var ventum = AsignarValoresToSend(true);

        $.ajax({ // cabecera de mi metodo ajax
            url: urlPedidoRegistro,
            type: "POST",
            data: JSON.stringify(ventum),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
        }).done(function (data) {
            if (data.respuesta > 0) {
                /*Body del modal*/
                document.getElementById("bodyModalCob").innerHTML = "<div class=\"alert alert-success\" role=\"alert\">" +
                    "Venta cobrada con éxito </div>";
                /*Footer del modal*/
                document.getElementById("footerModalCob").innerHTML = "<button class=\"btn btn-primary\" types=\"button\" onclick=\"location.reload()\">" +
                    "Nueva Venta" +
                    "</button>" +
                    "<form method=\"GET\" action=\"" + urlPrintPreticket + "\" >"+
                    "<input type =\"hidden\" name=\"idVenta\" value=\"" + data.respuesta + "\">" +
                    "<button class=\"btn btn - primary\" type=\"submit\" >" +
                    "Imprimir" +
                    "</button> </form>";
            //IMPRIMIR
            }     
        })
    });
});
