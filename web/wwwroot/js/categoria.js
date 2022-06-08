$(document).on('click', '#actualizado', function () {
    var form = $('#formulario').serialize();
    console.log($('#formulario').serializeArray());
    $.ajax({
        type: "POST",
        url: '/Categoria/EditCategoria',
        data: form,
        success: function (result) {
            $('#catedit').modal('hide');
            window.location.reload();
        }
    })
})

$(document).on('click', '#b_editar', function () {
    var idc = $(this).attr('idcategoria');
    $('#valor').val(idc);
    console.log(idc);
    $.ajax({
        type: "GET",
        url: '/Categoria/Index',
        data: { id: idc },
        success: function (result) {
            console.log(result);
            $('#catname').val(result.catname);
        }
    })
})
function mostrarID(id) {
    $('#valor').val(id);
}
$(document).on('click', '#borrar', function () {
    var valordel = $('#valor').val();
    $.ajax({
        type: 'POST',
        url: '/Categoria/Delete',
        data: { id: valordel },
        success: function (result) {
            console.log(result);
            if (result) {

                $('#catdel').modal('hide');
                window.location.reload();
            }
        }
    })
})