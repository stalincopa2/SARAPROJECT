/*AJAX PRODUCTO BY CATEGORIA */
$(document).ready(function () {
    $(document).on('click', '.cargeList', function (e) {
        e.preventDefault();
        var id = $(this).val();
        var data = { id: id };
        $.ajax({ // cabecera de mi metodo ajax
            url: urlProuctosByCategoria,
            type: "get",
            dataType: 'json',
            data: data,
            befonreSend: function () {
                console.log("Accion terminada");
            }
        }).done(function (data) {
            dVentaList = Object.create(data);

            let productList = "";
            for (let i = 0; i < data.length; i++) {
                dVentaList[i].cantidad = 0;
                productList += "<tr  onclick=\"productsToList(this)\" ><td>" + data[i].codProducto + "</td>" +
                    "<td>" + data[i].nombreProducto + "</td>" +
                    "<td>" + data[i].precio + "</td>" +
                    "<td> <img  width=\"50\" heigth=\"50\"  src=\"/Content/imgProducts/" + data[i].fotoProducto + "\" > </td> </tr>";
            }


            document.getElementById("productByCat").innerHTML = productList;
        })
    });
});