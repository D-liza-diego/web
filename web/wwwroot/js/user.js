var div = document.getElementById("divCreate")
div.style.display = "none"
let estadoCrear = 0
function FillId(id) {
    $('#valorIdUser').val(id)
}
function posiciones() {
    $("#tabla").removeClass('col-12').addClass('col-8');
    div.style.display = "block"
    $('#divCreate').addClass('col-4');
}
$(document).on('click', '#nuevoCliente', function () {
    estadoCrear = 1
    let titulo = document.getElementById('title')
    titulo.innerHTML = "Registrar usuario"
    $('#InputName').val("")
    $('#InputDni').val("")
    $('#InputPhone').val("")
    $('#InputEmail').val("")
    posiciones()

})
$(document).on('click', '#cerrar', function () {
    $('#divCreate').removeClass('col-4');
    $("#tabla").removeClass('col-8').addClass('col-12');
    div.style.display = "none"
})
$(document).on('click', '#obtener', function () {
    estadoCrear = 2
    let titulo = document.getElementById('title')
    titulo.innerHTML = "Modificar usuario"
    posiciones()
    var iduser = $(this).attr('iduser');
    $.ajax({
        type: "Get",
        url: '/Usuario/ObtenerDatos',
        data: { id: iduser },
        success: function (result) {
            $('#InputName').val(result.name)
            $('#InputDni').val(result.dni)
            $('#InputPhone').val(result.phone)
            $('#InputEmail').val(result.correo)
        }
    })
})

$(document).on('click', '#save', function () {
    var form = $('#formulario').serialize();
    if (estadoCrear == 1) {
        console.log($('#formulario').serializeArray());
        $.ajax({
            type: "Post",
            url: '/Usuario/GuardarCliente',
            data: form,
            success: function (result) {
                if (result.estado = true) {
                    window.location.reload();
                }
            }
        })
    }
    else if (estadoCrear == 2) {
        console.log($('#formulario').serializeArray());
        $.ajax({
            type: "Post",
            url: '/Usuario/ModificarCliente',
            data: form,
            success: function (result) {
                if (result.estado = true) {
                    window.location.reload();
                }
            }
        })
    }
})
