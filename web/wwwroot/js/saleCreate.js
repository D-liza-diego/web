var descontar
var coltotal = 0;
var cliente = document.getElementById('itemcustomer');
var botonguardar = document.getElementById('prueba');



$(document).on('click', '#carrito', function () {
    var encontrado = false
    var producto = $('#itemproduct').val()
    var cantidad = $('#cantidad').val();
    if (cantidad.length == 0) { $('#cantidad').addClass('error'); }
    else {
        $('#cantidad').removeClass('error');
        clearInterval(descontar)
        $("#tabla > tbody > tr").each(function (index, tr) {
            var x = parseInt($(this).find("td:eq(0)").html())
            if (x == producto) {
                encontrado = true

                $(this).closest('tr').addClass('repetido')
            }
        })

        if (!encontrado) {
            cliente.setAttribute('disabled', '');
            $.ajax({
                type: "Get",
                url: '/Sale/Crear',
                data: { id: producto },
                success: function (result) {
                    var total = parseInt($('#cantidad').val()) * parseFloat(result.precio)
                    $('.table tbody').append(
                        $("<tr>").append(
                            $("<td>").text(result.idproduct),
                            $("<td>").text(result.nameproduct),
                            $("<td>").html('<div id="aumentar" contenteditable="true">' + cantidad + '</div>'),
                            $("<td>").text(result.precio),
                            $("<td>").text(total),
                            $("<td>").html(" <button type='button' id='borrar' class='btn btn-danger'> Eliminar </button> <button type='button' id='edit' class='btn btn-warning'> Editar </button>")))
                    coltotal += parseFloat(total);
                    $('#total').html('TOTAL: S/.' + coltotal)
                    $('#cantidad').val('');
                    botonguardar.removeAttribute('disabled')
                }
            })
        }
    }
})
$(document).on('click', '#prueba', function () {

    var detalle_venta = [];
    var total = 0;

    $("#tabla > tbody > tr").each(function (index, tr) {
        detalle_venta.push(
            {
                IdProduct: parseInt($(tr).find("td:eq(0)").text()),
                Cantidad: parseInt($(tr).find("td:eq(2)").text()),
                Precio: parseFloat($(tr).find("td:eq(3)").text()),
            })
        total = total + parseFloat($(tr).find("td:eq(4)").text())
    })
    var tabla = document.getElementById("tabla");
    var totalRowCount = (tabla.rows.length) - 1;

    var venta =
    {
        Total: total,
        Items: totalRowCount,
        Comprobante: "Boleta",
        Estado: "Pagado",
        Visibilidad: true,
        Idcustomer: parseInt($('#itemcustomer').val()),
        Salesdetails: detalle_venta

    }
    console.log(venta)

    $.ajax({
        type: "POST",
        url: '/Sale/Save',
        data: JSON.stringify(venta),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data)
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
        }

        event.preventDefault();
    }

})
$('.table tbody').on('click', '#borrar', function () {
    var restar = $(this).closest('tr').find("td:eq(4)").html()
    //console.log(restar)
    $(this).closest('tr').remove();
    coltotal -= restar
    $('#total').html('TOTAL: S/.' + coltotal)
    verificar()
})

$('.table tbody').on('click', '#edit', function () {
    $(this).closest('tr').removeClass('repetido')
    $('#aumentar').addClass('repetido')
})
function obtener() {
    coltotal = 0
    $("#tabla > tbody > tr").each(function (index, tr) {
        coltotal += parseFloat($(tr).find("td:eq(4)").text())

    })
    $('#total').html('TOTAL: S/.' + coltotal)
}
function contador() {
    var time = 2;
    descontar = setInterval(function () {
        --time;
        if (time == 0) {
            $('#cantidad').addClass('error');
            time = 2
        }
        if (time == 1) {
            $('#cantidad').removeClass('error');
        }

    }, 100);
}
function verificar() {
    var filas = $('tbody').children().length
    if (filas == 0) {
        botonguardar.setAttribute("disabled", "")
        cliente.removeAttribute('disabled');

    }
}
$(document).on('keypress', '#cantidad', function (event) {
    if (event.keyCode === 13) { event.preventDefault(); }
})