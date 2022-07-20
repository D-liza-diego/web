
function obtenerFecha(q) {
    let mes
    var fecha = new Date(q)
    var year = fecha.getFullYear();
    var month = fecha.getMonth() + 1;
    if (month < 10) {
         mes = "0" + month
    }
    else {
        mes =month
    }
    var day = fecha.getDate();
    var hour = fecha.getHours();
    var minutes = fecha.getMinutes();
    var seconds = fecha.getSeconds();
    let cadena = year + "-" + mes + "-" + day + "T" + hour + ":" + minutes + ":" + seconds + "-05:00"
    return cadena
}
$(document).on('click', '#ver', function () {
    var id = $(this).attr('idsale');
    console.log(id)
    $('#modalDetalle').modal('show')
    let body = document.getElementById('body')
    body.innerHTML = ""
    
    $.ajax({
        type: "Post",
        url: "Sale/Detalle",
        data: { id: id },
        success: function (r) {
            console.log(r)
            for (let i of r) {
               
                $('#detalleCliente').html(i.idcustomerNavigation.namecustomer + i.idcustomerNavigation.lastnamecustomer)
                $('#detalleComprobante').html(i.idComprobanteNavigation.descripcion)
                $('#detalleTotal').html(i.total)
                $('#detalleFecha').html(i.fecha)
                for (let o of i.idComprobanteNavigation.numeracions) {
                    if (i.idComprobante == 1) {
                        $('#detalleSerie').html(i.idComprobanteNavigation.codigo + '-' + o.numeracionBoleta)
                    }
                    else {
                        $('#detalleSerie').html(i.idComprobanteNavigation.codigo + '-' + o.numeracionFactura)
                    }
                    
                }
                for (let u of i.salesdetails) {
                    body.innerHTML += `
                     <tr>
                        <td>${u.idProductNavigation.codigo}</td>
                        <td>${u.idProductNavigation.nameproduct}</td>
                        <td>${u.cantidad}</td>
                        <td>${u.idProductNavigation.precio}</td>
                        <td>${u.cantidad * u.idProductNavigation.precio}</td>
                     </tr>
                    `
                }
            }
           
         
            //for (let i of r) {
            //    body.innerHTML += `
            //    <tr>
            //     <td>${i.idProductNavigation.nameproduct}</td>
            //     <td>${i.cantidad}</td>
            //     <td>s/. ${i.precio}</td>
            //     <td>s/. ${i.precio * i.cantidad}</td>
            //    </tr>`
            //}

           
                //$.ajax({
                //    type: "Get",
                //    url: "Sale/VerProducto",
                //    data: { id: i.idProduct },
                //    success: function (q) {

                //        for (let o of q) {
                //            body.innerHTML += `
                //            <tr>
                //            <td>${o.nameproduct}</td>
                //            <td>${i.cantidad}</td>
                //            <td>${i.precio}</td>
                //            </tr>
                //            `
                //        }

                //    }
                //})
           

            
        }
    })
})
$(document).on('click', '#eliminar', function () {
    var id = $(this).attr('idsale');
    $.ajax({
        type: "Post",
        url: "Sale/Ex",
        data: { id: id },
        success: function () {
            window.location.reload();
        }
    })
})

$(document).on('click', '#visible', function () {
    var id = $(this).attr('idsale');
    console.log(id)

    $.ajax({
        type: "Post",
        url: "Sale/Eliminar",
        data: { id: id },
        success: function () {
            window.location.reload();
        }
    })
})
$(document).on('click', '#pdf', function () {
    var id = $(this).attr('idsale')
    console.log(id)
    $.ajax({
        type: "Get",
        url: "Sale/Reporte",
        data: { id: id },
        success: function (r) {console.log(r)}
    })
})