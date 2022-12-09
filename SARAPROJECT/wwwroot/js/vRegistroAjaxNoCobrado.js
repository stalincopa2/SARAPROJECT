/*Funcion para asignar valores definitivos a enviar */
function AsignarValoresToSend(cobrado) {

    var Total = new Decimal(document.getElementById("Total").value);

    var oVandD = {

        NroPedido: document.getElementById("NroPedido").value,
        IdMesa: document.getElementById("IdMesa").value,
        IdUsuario: document.getElementById("IdUsuario").value,
        IdEstventa: document.getElementById("IdEstventa").value,
        CodVenta: document.getElementById("CodVenta").value,
        FechaVenta: document.getElementById("FechaVenta").value,
        Total: Total,
        NroFactura: "SN",
        ClaveAcceso: "SN",
        DetalleVenta: dVentaListTosend
    }
    return oVandD;
}

/*AGREGAR PRODUCTOS A LA LISTA */
function productsToList(x) {

    var i = Math.abs(x.rowIndex - 1);
    var productsToPayList = " ";
    var precioUnitario = null;
    var positionInList = findProductoById(dVentaList[i].idProducto); // busca el producto 
    var totalValueOriginal = new Decimal(totalInput.value);

    if (positionInList >= 0) {
        var idPr = dVentaListTosend[positionInList].idProducto;// busca el producto 
        var cantidadInput = document.getElementById("itemList" + idPr);
        var totalDiv = document.getElementById("totaldv" + idPr);


        precioUnitario = new Decimal(dVentaListTosend[positionInList].precio);
        // aqui se deberia restar el precio antes de hacer cualquier cosa 
        var totalValueAnterior = new Decimal(totalValueOriginal.sub(precioUnitario.mul(dVentaListTosend[positionInList].cantidad)));

        dVentaListTosend[positionInList].cantidad++;

        cantidadInput.setAttribute("value", dVentaListTosend[positionInList].cantidad);

        totalDiv.innerHTML = precioUnitario.mul(dVentaListTosend[positionInList].cantidad);
        totalInput.setAttribute("value", totalValueAnterior.add(precioUnitario.mul(dVentaListTosend[positionInList].cantidad)));

    } else {

        dVentaListTosend.push(dVentaList[i]);
        var posicionActual = dVentaListTosend.length - 1;
        dVentaListTosend[posicionActual].cantidad++;

        precioUnitario = new Decimal(dVentaListTosend[posicionActual].precio);

        productsToPayList += " <tr style=\"font-size: 12px;\" ><td> <input onkeyup=\"modifyCant(this)\" onchange=\"modifyCant(this)\" type=\"number\" id=\"itemList" + dVentaListTosend[posicionActual].idProducto + "\" value = \"" + dVentaListTosend[posicionActual].cantidad + "\" style =\"width:30px;\" /></td>" +
            "<td>" + dVentaListTosend[posicionActual].nombreProducto + "</td>" +
            "<td>" + dVentaListTosend[posicionActual].precio + "</td>" +
            "<td id=\"totaldv" + dVentaListTosend[posicionActual].idProducto + "\">" + precioUnitario.mul(dVentaListTosend[posicionActual].cantidad) + "</td>" +
            "<td> " + "<button class=\"item delete\" data-toggle=\"modal\" title=\"Eliminar\" value=\"" + dVentaListTosend[posicionActual].idProducto + "\" id=\"btnDelete\"> " +
            "<i class=\"zmdi zmdi-delete\"></i> </button>" +
            "  </td> </tr>";
        document.getElementById("productsToPayList").innerHTML += productsToPayList;
        totalInput.setAttribute("value", totalValueOriginal.add(precioUnitario.mul(dVentaListTosend[posicionActual].cantidad)));
    }

    if (totalInput.value == 0) {
        document.getElementById("registrarButton").removeAttribute("enabled", "");
        document.getElementById("registrarButton").setAttribute("disabled", "");
    } else {
        document.getElementById("registrarButton").setAttribute("enabled", "");
        document.getElementById("registrarButton").removeAttribute("disabled", "");
    }
}

$(document).on('click', '.delete', function (event) {
    event.preventDefault();
    $(this).closest('tr').remove();
    var id = $(this).val();
    var posicion = findProductoById(id);
    var precioUnitario = new Decimal(dVentaListTosend[posicion].precio);
    var totalValueOriginal = new Decimal(totalInput.value);

    totalInput.setAttribute("value", totalValueOriginal.sub(precioUnitario.mul(dVentaListTosend[posicion].cantidad)));

    dVentaListTosend[posicion].cantidad = 0;
    dVentaListTosend.splice(posicion, 1);

    if (totalInput.value == 0) {
        document.getElementById("registrarButton").removeAttribute("enabled", "");
        document.getElementById("registrarButton").setAttribute("disabled", "");
    } else {
        document.getElementById("registrarButton").setAttribute("enabled", "");
        document.getElementById("registrarButton").removeAttribute("disabled", "");
    }

});


function findProductoById(idProducto) {

    if (dVentaListTosend.length == 0) {
        return -1;
    }

    for (let i = 0; i < dVentaListTosend.length; i++) {

        if (dVentaListTosend[i].idProducto == idProducto) {
            return i;
        }

    }
    return -1;
}

function modifyCant(elemento) {
    if (elemento.value == "" || elemento.value < 0) {
        elemento.value = 1;
    }
    var idInput = elemento.id;
    var idPr = idInput.substring(8, (idInput.length)); /*itemList- */
    var positionInList = findProductoById(idPr);
    var precioUnitario = new Decimal(dVentaListTosend[positionInList].precio);
    var totalDiv = document.getElementById("totaldv" + idPr);


    var totalValueOriginal = new Decimal(totalInput.value);
    var totalValueAnterior = new Decimal(totalValueOriginal.sub(precioUnitario.mul(dVentaListTosend[positionInList].cantidad)));



    dVentaListTosend[positionInList].cantidad = elemento.value;
    totalDiv.innerHTML = precioUnitario.mul(dVentaListTosend[positionInList].cantidad);
    totalInput.setAttribute("value", totalValueAnterior.add(precioUnitario.mul(dVentaListTosend[positionInList].cantidad)));
}


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
                document.getElementById("footerModalNoCob").innerHTML = "<button class=\"btn btn-primary\" types=\"button\" onclick=\"location.reload()\">" +
                    "Nueva registro" +
                    "</button>" +
                    "<form method=\"GET\" action=\"" + urlPrintPreticket + "\" >" +
                    "<input type =\"hidden\" name=\"idVenta\" value=\"" + data.respuesta + "\">" +
                    "<button class=\"btn btn - primary\" type=\"submit\" >" +
                    "Imprimir" +
                    "</button> </form>";
            }
        })
    });
});
