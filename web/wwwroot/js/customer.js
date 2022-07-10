var flag = 0
const boton = document.getElementById("agregar");
const botonupdate = document.getElementById("update-customer");

$("#filtro").on('keyup', function () {
    var value = $(this).val()
    var data= buscar(value,data)
})

function limpiar() {
    $('#add-buscar-dni').val('');
    $('#add-dni').val('');
    $('#add-nombre').val('');
    $('#add-apellidos').val('');
    $('#add-telf').val('');

    $('#update-buscar-dni').val('');
    $('#update-dni').val('');
    $('#update-nombre').val('');
    $('#update-apellidos').val('');
    $('#update-telf').val('');
    $('#span').html('');
    boton.setAttribute('disabled', true);
}
function mostrarID(id) {
    $('#id-customer').val(id)
    console.log($('#id-customer').val())
}
function addbuscar(id, flag) {
    $.ajax({
        type: "Get",
        url: 'https://dniruc.apisperu.com/api/v1/dni/' + id + '?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6ImRpZWdvMDNsaXphQGdtYWlsLmNvbSJ9.YMccp65gLQTDdT-ZQNg4uLBmX66IkI_854e6uC2Lglg',
        success: function (result) {
            if (result.success == false) {
                Swal.fire({
                    icon: 'error',
                    title: 'Persona no encontrada',
                    showConfirmButton: false,
                    timer: 900
                })
                $('#buscar-dni').val('');
                $('#buscar-dni').val('');
                $('#update-dni').val('');
                $('#update-nombre').val('');
                $('#update-apellidos').val('');
                $('#update-telf').val('');
            }
            else {
                if (flag = 1) {
                    $('#add-dni').val(result.dni);
                    $('#add-nombre').val(result.nombres);
                    $('#add-apellidos').val(result.apellidoPaterno + " " + result.apellidoMaterno);
                    boton.removeAttribute('disabled');
                }
                if (flag = 2) {
                    $('#u-dni').val(result.dni);
                    $('#u-nombre').val(result.nombres);
                    $('#u-apellidos').val(result.apellidoPaterno + " " + result.apellidoMaterno);
                    $('#u-telf').val('')
                    botonupdate.removeAttribute('disabled');
                }
            }

        }
    })
}
function verificar(id, flag) {
    if (id.length == 8) {
        $.ajax({
            type: "GET",
            url: '/Customer/Verificar',
            data: { id: id },
            success: function (result) {
                if (result == true) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Cliente ya registrado',
                        showConfirmButton: false,
                        timer: 1000
                    })
                    limpiar()
                }
                else { addbuscar(id, flag) }
            }
        })
    }
    else {
        Swal.fire({
            icon: 'error',
            title: 'El DNI debe tener 8 digitos',
            showConfirmButton: false,
            timer: 900
        })
    }
}
$(document).on('keyup', '#buscar-dni', function (event) {
    if (event.keyCode === 13) {
        let dni = $('#buscar-dni').val();
        flag = 1
        verificar(dni, flag)
    }
})
$(document).on('click', '#buscar-cliente', function () {
    let dni = $('#buscar-dni').val();
    flag = 1
    verificar(dni, flag)
})
$(document).on('click', '#edit-customer', function () {

    var idcustomer = $(this).attr('idcustomer');
    console.log(idcustomer)

    $.ajax({
        type: "Post",
        url: "/Customer/Index",
        data: { id: idcustomer },
        success: function (a) {
            $('#u-dni').val(a.dnicustomer),
                $('#u-nombre').val(a.namecustomer),
                $('#u-apellidos').val(a.lastnamecustomer),
                $('#u-telf').val(a.phonecustomer)
        }
    })
})
$(document).on('click', '#update-customer', function () {
    var form = $('#update-form').serialize();
    console.log($('#update-form').serializeArray());
    $.ajax({
        type: "POST",
        url: '/Customer/UpdateCustomer',
        data: form,
        success: function (result) {
            $('#editar-c').modal('hide');
            window.location.reload();
        }
    })
})
$(document).on('click', '#update-buscar', function () {
    let dni = $('#update-buscar-dni').val();
    flag = 2
    verificar(dni, flag)
})
$(document).on('click', '#borrar-customer', function () {
    var idcustomer = $(this).attr('idcustomer');

    //Swal.fire({
    //       title: '¿Desea eliminar?',
    //       icon: 'warning',
    //       showCancelButton: true,
    //       confirmButtonColor: '#3085d6',
    //       cancelButtonColor: '#d33',
    //       confirmButtonText: 'Eliminar'
    //         }).then((result) => {
    //   if (result.isConfirmed) {
    //        $.ajax({
    //             type:'post',
    //             url:'/Customer/DeleteCustomer',
    //             data:{id:idcustomer},
    //             success:function(result)
    //             {
    //                 console.log(result)

    //                     Swal.fire({
    //                     icon: 'success',
    //                     title: 'Cliente Eliminado',
    //                     showConfirmButton: false,
    //                     timer: 2000 })
    //                      window.location.reload();
    //             }

    //             })
    //   }})

    $.ajax({
        type: 'post',
        url: '/Customer/Confirmar',
        data: { id: idcustomer },
        success: function (result) {
            if (result == false) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'EL CLIENTE TIENE UNA VENTA',
                    showConfirmButton: false,
                    timer: 2000

                })
            }
            else {
                Swal.fire({
                    icon: 'success',
                    title: 'Cliente Eliminado',
                    showConfirmButton: false,
                })
            }
            setTimeout(() => {
                window.location.reload()
            }, 2500);
        }
    })

})