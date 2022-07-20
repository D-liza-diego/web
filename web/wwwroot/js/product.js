
var span = document.getElementById('span')

$("#tabla > tbody > tr").each(function (index, tr) {
    var x = parseInt($(this).find("td:eq(2)").html())
    if (x < 5) {
        $(this).closest('tr').addClass('r')
        $(this).find("td:eq(2)").html("Bajo de stock: (" + x + ")")

    }
  /*  console.log(x)*/
})

$(document).on('click', '#p_actualizar', function () {
    var idp = $('#p_editar').attr('idproduct');
    var inputCantidad = document.getElementById('llenar-p-cantidad')
    var inputCodigo = document.getElementById('llenar-p-codigo')
    $.ajax({
        type: 'POST',
        url: '/Product/CantidadVerificar',
        data: { id: idp },
        success: function (result) {

            if ($('#llenar-p-cantidad').val() >= result)
            {
                inputCantidad.removeAttribute('disabled');
                inputCodigo.removeAttribute('disabled');
                var form = $('#p-form').serialize();
                /*console.log($('#p-form').serializeArray());*/
                $.ajax({
                    type: "POST",
                    url: '/Product/EditProduct',
                    data: form,
                    success: function () {
                        inputCantidad.setAttribute('disabled', true);
                        inputCodigo.setAttribute('disabled', true);
                        $('#editar').modal('hide');
                        window.location.reload();
                    }
                })
            }
            else
            {
               $('#span').html("el nuevo valor debe ser mayor a :"+result)
               span.removeAttribute('hidden')
            }
        }
    })
    
})
function cerrar() {
    span.setAttribute('hidden', true)
}
$(document).on('click', '#p_editar', function () {
    var idp = $(this).attr('idproduct');
    $('#productID').val(idp);
 /*   console.log(idp);*/
    $.ajax({
        type: "Get",
        url: '/Product/Index',
        data: { id: idp },
        success: function (result) {
            /* console.log(result);*/
            $('#llenar-p-codigo').val(result.codigo);
            $('#llenar-p-name').val(result.nameproduct);
            $('#llenar-p-precio').val(result.precio);
            $('#llenar-p-cantidad').val(result.cantidad);
            $('#llenar-p-cat').val(result.idcategoria);
        }
    })
})

function mostrar(id) {
    $('#productID').val(id);
}
$(document).on('click', '#borrarpro', function () {
    var valordel = $('#productID').val();
    $.ajax({
        type: 'POST',
        url: '/Product/DeleteProduct',
        data: { id: valordel },
        success: function (result) {
   /*         console.log(result);*/
            if (result) {
                $('#borrar').modal('hide');
                window.location.reload();
            }
        }
    })
})

function cambiar() {
    var input = document.getElementById('llenar-p-cantidad')
    var toggle = $('#flexSwitchCheckDefault').is(':checked')
    if (toggle == false) {
        input.setAttribute('disabled',true);
    }
    else {
        input.removeAttribute('disabled');
    }
}