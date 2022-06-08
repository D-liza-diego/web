
$(document).on('click', '#ver', function () {
    var id = $(this).attr('idsale');

    $('#modalDetalle').modal('show')
    let body = document.getElementById('body')
    body.innerHTML = ""

    $.ajax({
        type: "Post",
        url: "Sale/Ver",
        data: { id: id },
        success: function (r) {

            for (let i of r) {
                console.log(i)
                $.ajax({
                    type: "Get",
                    url: "Sale/VerProducto",
                    data: { id: i.idProduct },
                    success: function (q) {

                        for (let o of q) {
                            body.innerHTML += `
                            <tr>
                            <td>${o.nameproduct}</td>
                            <td>${i.cantidad}</td>
                            <td>${i.precio}</td>
                            </tr>
                            `
                        }

                    }
                })
            }


        }
    })
})
$(document).on('click', '#eliminar', function () {
    var id = $(this).attr('idsale');
    $.ajax({
        type: "Post",
        url: "Sale/Ex",
        data: { id: id },
        success: function (f) {
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
        success: function (r) {
            window.location.reload();
        }
    })
})