let divProductos = document.getElementById('divProductos')
let divVentas = document.getElementById('divVentas')
let flag = 1

divProductos.style.display = "none"

$(document).on('click', '#ventas', function () {
    divProductos.style.display = "none"
    divVentas.style.display = "block"
    flag = 1
    indicacion(flag)
    inicialCharts()
})
$(document).on('click', '#productos', function () {
    divVentas.style.display = "none"
    divProductos.style.display = "block"
    flag = 2
    indicacion(flag)
    inicialCharts()
})
inicialCharts()
function colorRandom() {
    let arrayRandom = []
    for (let i = 0; i < 3; i++) {
        let x = Math.floor(Math.random() * 255) + 1;
        arrayRandom.push(x)
    }
    return arrayRandom
}
let mychart
let arregloMes =
    [
        { "mes": 1, "texto":"enero"},
        { "mes": 2, "texto": "febrero"},
        { "mes": 3, "texto": "marzo"},
        { "mes": 4, "texto": "abril"},
        { "mes": 5, "texto": "mayo"},
        { "mes": 6, "texto": "junio" },
        { "mes": 7, "texto": "julio" },
        { "mes": 8, "texto": "agosto"},
        { "mes": 9, "texto": "septiembre"},
        { "mes": 10, "texto": "octubre"},
        { "mes": 11, "texto": "noviembre" },
        { "mes": 12, "texto": "diciembre"}
    ]
let titulo
function indicacion() {
    if(flag==1){
        $("#drop li a").click(function ()
        {
            var item = $(this).attr('value')
            {
                $.ajax({
                    type: "Get",
                    url: "/Home/salesMade",
                    data: { mes: item },
                    success: function (r) {
                        (r.fechaCheck == false) ? titulo = "Ventas mensuales" : titulo = "Ventas del mes de " + arregloMes.find(t => { return t.mes == item }).texto
                        let arregloFechaInt = []
                        let arregloTotal = []
                        let arregloFechaString = []
                        let arregloColores=[]

                        r.data.forEach(function (r) {
                            let Color = colorRandom();
                            arregloFechaInt.push(r.fecha)
                            arregloTotal.push(r.total)
                            arregloColores.push('rgb(' + Color[0] + ',' + Color[1] + ',' + Color[2] + ',0.5)')
                        })
                        for (let i of arregloFechaInt.values()) {
                            arregloFechaString.push(arregloMes.find(t => { return t.mes == i }).texto)
                        }

                        const data = {
                            labels: arregloFechaString,
                            datasets: [{
                                label: arregloFechaString,
                                backgroundColor: arregloColores,
                                barThickness: 100,
                                data: arregloTotal,
                            }]
                        };

                        const config = {
                            type: 'bar',
                            data: data,
                            options: {
                                responsive: true,
                                plugins: {
                                    legend: { display: false, position: 'top', },
                                    title: { display: true, text: (titulo) }
                                }
                            },
                        };

                        $('#ChartSales').remove();
                        $('#divVentas').html(` <canvas class="my-4 w-100" id="ChartSales" width="900" height="380"></canvas>`)
                        myChart = new Chart(document.getElementById('ChartSales'), config)
                    }
                })
            }
        })
    }
    else if (flag == 2) {
        $('#drop li a').click(function () {
            var item = $(this).attr('value')
            {
                $.ajax({
                    type: "Get",
                    url: "/Home/productSold",
                    data: { mes: item },
                    success: function (r) {
                        (r.fechaCheck == false) ? titulo = "Productos vendidos del año" : titulo = ("Productos vendidos del mes: " + arregloMes.find(t => { return t.mes == item }).texto)
                        let arregloProductos = []
                        let arregloTotales = []
                        let arrayColores = []


                        r.data.forEach(function (r) {
                            let arraycolor = colorRandom()
                            arregloProductos.push(r.producto)
                            arregloTotales.push(r.total)
                            arrayColores.push('rgb(' + arraycolor[0] + ',' + arraycolor[1] + ',' + arraycolor[2] + ',0.5)')
                        })

                        const data = {
                            labels: arregloProductos,
                            datasets: [{
                                label: arregloProductos[r],
                                backgroundColor: arrayColores,
                                barThickness: 100,
                                data: arregloTotales,
                            }]
                        };

                        const config = {
                            type: 'bar',
                            data: data,
                            options: {
                                responsive: true,
                                plugins: {
                                    legend: { display: false, position: 'top', },
                                    title: {display: true,text: (titulo)}
                                }
                            },
                        };

                        $('#ChartProducts').remove();
                        $('#divProductos').html(` <canvas class="my-4 w-100" id="ChartProducts" width="900" height="380"></canvas>`)
                        myChart = new Chart(document.getElementById('ChartProducts'), config)
                    }

                })
            }
        });
    }
   
}

function inicialCharts() {
    $.ajax({
        type: "Get",
        url: "/Home/productSold",
        data: { mes: 0 },
        success: function (r) {
            let arregloProductos = []
            let arregloTotales = []
            let arrayColores = []


            r.data.forEach(function (r) {
                let arraycolor = colorRandom()
                arregloProductos.push(r.producto)
                arregloTotales.push(r.total)
                arrayColores.push('rgb(' + arraycolor[0] + ',' + arraycolor[1] + ',' + arraycolor[2] + ',0.5)')
            })
            const data = {
                labels: arregloProductos,
                datasets: [{
                    label: arregloProductos[r],
                    backgroundColor: arrayColores,
                    barThickness: 100,
                    data: arregloTotales,
                }]
            };

            const config = {
                type: 'bar',
                data: data,
                options: {
                    responsive: true,
                    plugins: {
                        legend: { display: false, position: 'top', },
                        title: { display: true, text: "Productos vendidos del año" }
                    }
                },
            };

            $('#ChartProducts').remove();
            $('#divProductos').html(` <canvas class="my-4 w-100" id="ChartProducts" width="900" height="380"></canvas>`)
            myChart = new Chart(document.getElementById('ChartProducts'), config)
        }
    })

    $.ajax({
        type: "Get",
        url: "/Home/salesMade",
        data: { mes: 0 },
        success: function (r) {
            let arregloFecha = []
            let arregloTotal = []
            let arregloFechaMes = []
            let arregloColores=[]
            r.data.forEach(function (r) {
                let arraycolor = colorRandom()
                arregloFecha.push(r.fecha)
                arregloTotal.push(r.total)
                arregloColores.push('rgb(' + arraycolor[0] + ',' + arraycolor[1] + ',' + arraycolor[2] + ',0.5)')
            })
            for (let i of arregloFecha.values()) {
              arregloFechaMes.push(arregloMes.find(t => { return t.mes == i }).texto)
            }
            const data = {
                labels: arregloFechaMes,
                datasets: [
                    {
                        data: arregloTotal,
                        backgroundColor: (arregloColores),
                        barThickness: 100,
                        order:0
                    },
                    {
                        data: arregloTotal,
                        borderColor: '#007bff',
                        backgroundColor: 'transparent',
                        type: 'line',
                        order:1
                    }
                ]
            };

            const config = {
                type: 'bar',
                data: data,
                options: {
                    responsive: true,
                    plugins: {
                        legend: {display: false,},
                        title: {display: true,text: 'Ventas mensuales'}
                    }
                },
            };

            $('#ChartSales').remove();
            $('#divVentas').html(` <canvas class="my-4 w-100" id="ChartSales" width="900" height="380"></canvas>`)
            myChart = new Chart(document.getElementById('ChartSales'), config)

        }
    })
}