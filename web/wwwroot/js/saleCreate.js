var coltotal = 0;
var registroCliente = $('#tablaCliente tbody tr');
var registroProducto = $('#tablaProduct tbody tr');
var botonguardar = document.getElementById('guardarVenta');
$('#filtroCliente').keyup(function () {
    var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

    registroCliente.show().filter(function () {
        var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
        return !~text.indexOf(val);
    }).hide();
});
$('#filtroProducto').keyup(function () {
    var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

    registroProducto.show().filter(function () {
        var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();
        return !~text.indexOf(val);
    }).hide();
});
$('#tablaCliente tbody tr').click(function () {
    var nombreCliente = ($(this).find("td:eq(2)").text())
    var idCliente = ($(this).find("td:eq(0)").text())
    $('#inputClienteName').val(nombreCliente)
    $('#inputClienteId').val(idCliente)
    $('#ModalClientes').modal('hide');
})
$('#tablaProducto tbody tr').click(function () {
    var nombreProducto = ($(this).find("td:eq(1)").text())
    var idProducto = ($(this).find("td:eq(0)").text())
    $('#inputProductoName').val(nombreProducto)
    $('#inputProductoId').val(idProducto)
    $('#ModalProductos').modal('hide');
})
$('#carrito').on('click', function () {
    
    var productoRepetido = false
    var idproducto = $('#inputProductoId').val() 
    var cantidad = $('#cantidad').val()
    var productName = $('#inputProductoName').val()
    var clienteName = $('#inputClienteName').val() 
    if (productName.length == 0 || cantidad.length == 0 || clienteName.length ==0) {
        $('#cantidad').addClass('error');
        $("#inputProductoName").addClass('error');
        $("#inputClienteName").addClass('error');
    }
    else {
        $('#cantidad').removeClass('error');
        $("#inputProductoName").removeClass('error');
        $("#inputClienteName").removeClass('error');
        $("#tablaCarrito > tbody > tr").each(function () {
            var idProductTable = parseInt($(this).find("td:eq(0)").html())
            if (idProductTable == idproducto) {
                productoRepetido = true
                $(this).closest('tr').addClass('repetido')
            }
        })
        if (!productoRepetido) {
            $.ajax({
                type: "Get",
                url: '/Sale/Crear',
                data: { id: idproducto },
                success: function (result) {
                    var total = parseInt($('#cantidad').val()) * parseFloat(result.precio)
                    $('#tablaCarrito tbody').append(
                        $("<tr>").append(
                            $("<td hidden>").text(result.idproduct),
                            $("<td>").text(result.nameproduct),
                            $("<td>").html('<div id="aumentar" contenteditable="true">' + cantidad + '</div>'),
                            $("<td>").text(result.precio),
                            $("<td>").text(total),
                            $("<td>").html(" <button type='button' id='borrar' class='btn btn-danger'> Eliminar </button> <button type='button' id='edit' class='btn btn-warning'> Editar </button>")))
                    coltotal += parseFloat(total);
                    $('#total').html('TOTAL: S/.' + coltotal)
                    cantidad.length == 0;
                    botonguardar.removeAttribute('disabled')
                }
            })
        }
    }
   
})
$('#guardarVenta').on('click', function () {
    var detalle_venta = [];
    var total = 0;
    $("#tablaCarrito > tbody > tr").each(function () {
        detalle_venta.push(
            {
                IdProduct: parseInt($(this).find("td:eq(0)").text()),
                Cantidad: parseInt($(this).find("td:eq(2)").text()),
                Precio: parseFloat($(this).find("td:eq(3)").text()),
            })
        total = total + parseFloat($(this).find("td:eq(4)").text())
    })
    var tabla = document.getElementById("tablaCarrito");
    var totalRowCount = (tabla.rows.length) - 1;

    var venta =
    {
        Total: total,
        Items: totalRowCount,
        Comprobante: "Boleta",
        Estado: "Pagado",
        Visibilidad: true,
        Idcustomer: parseInt($('#inputClienteId').val()),
        Salesdetails: detalle_venta

    }
    $.ajax({
        type: "POST",
        url: '/Sale/Save',
        data: JSON.stringify(venta),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            window.location.href = "/Sale/Index"
        }
    })
})
$(document).on('keypress', '#aumentar', function (event) {
    var newValor = event.key
    if (!$.isNumeric(newValor)) {
        if (event.keyCode === 13) {
            var x = $(this).html()
            var f = $(this).closest('tr').find("td:eq(3)").html();
            var t = x * f
            $(this).closest('tr').find("td:eq(4)").html(t);
            obtener();
            $(this).find("td:eq(2)").addClass('repetido')
            botonguardar.removeAttribute('disabled')
        }event.preventDefault();
    }

})

$('#tablaCarrito tbody').on('click', '#borrar', function () {
    var valorRestado = $(this).closest('tr').find("td:eq(4)").html()
    $(this).closest('tr').remove();
    coltotal -= valorRestado
    $('#total').html('TOTAL: S/.' + coltotal)
    verificar()
})
$('#tablaCarrito tbody').on('click', '#edit', function () {
    $(this).closest('tr').removeClass('repetido')
    $('#aumentar').addClass('repetido')
})
function verificar() {
    var filas = $('#tablaCarrito tbody').children().length
    if (filas == 0) {
        botonguardar.setAttribute("disabled", true)
        cliente.removeAttribute('disabled');
    }
}
function obtener() {
    coltotal = 0
    $("#tablaCarrito > tbody > tr").each(function () {
        coltotal += parseFloat($(this).find("td:eq(4)").text())
    })
    $('#total').html('TOTAL: S/.' + coltotal)
}
$(document).on('keypress', '#cantidad', function (event) {
    if (event.keyCode === 13) { event.preventDefault(); }
})