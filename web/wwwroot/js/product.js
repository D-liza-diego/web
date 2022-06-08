
$("#tabla > tbody > tr").each(function (index, tr) {
    var x = parseInt($(this).find("td:eq(2)").html())
    if (x < 5) {
        $(this).closest('tr').addClass('r')
        $(this).find("td:eq(2)").html("Bajo de stock: (" + x + ")")

    }
    console.log(x)
})

$(document).on('click', '#p_actualizar', function () {
    var form = $('#p-form').serialize();
    //var valorneo=$('#llenar').val();
    console.log($('#p-form').serializeArray());
    $.ajax({
        type: "POST",
        url: '/Product/EditProduct',
        data: form,
        success: function (result) {
            $('#editar').modal('hide');
            window.location.reload();
        }
    })
})

$(document).on('click', '#p_editar', function () {
    var idp = $(this).attr('idproduct');
    $('#productID').val(idp);
    console.log(idp);
    $.ajax({
        type: "Get",
        url: '/Product/Index',
        data: { id: idp },
        success: function (result) {
            console.log(result);
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
            console.log(result);
            if (result) {
                $('#borrar').modal('hide');
                window.location.reload();
            }
        }
    })
})