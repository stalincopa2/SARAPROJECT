/**AJAX REGISTRAR VENTA SIN COBRAR*/
$(document).ready(function () {
    $(document).on('click', '.btnRegistrar', function (e) {
        e.preventDefault();
        var ventum = AsignarValoresToSend(false);

        $.ajax({ // cabecera de mi metodo ajax
            url: urlPedidoRegistro,
            type: "POST",
            data: JSON.stringify(ventum),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
        }).done(function (data) {
            if (data.respuesta > 0) {
                document.getElementById("bodyModalNoCob").innerHTML = "<div class=\"alert alert-success\" role=\"alert\">" +
                    "Pedido registrado con éxito </div>";
            }
        })
    });
});
