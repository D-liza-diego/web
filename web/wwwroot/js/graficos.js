let divProductos = document.getElementById('divProductos')
let divVentas = document.getElementById('divVentas')
divProductos.style.display = "none"

$(document).on('click', '#productos', function () {
    divVentas.style.display = "none"
    divProductos.style.display = "block"
})
$(document).on('click', '#ventas', function () {
    divProductos.style.display = "none"
    divVentas.style.display = "block"
})

$.ajax({
    type: "Get",
    url: '/Home/GraficoVentas',
    success: function (result) {
        var arrayMes = []
        var arrayTotal = []
        for (let i = 0; i < result.length; i++) {
            arrayMes.push(result[i].mes)
            arrayTotal.push(result[i].total)
        }

        const data = {
            labels: arrayMes,
            datasets: [
                {
                    data: arrayTotal,
                    borderColor: '#007bff',
                    backgroundColor: 'transparent',
                }
            ]
        };
        const config = {
            type: 'line',
            data: data,
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        display: false,
                    },
                    title: {
                        display: true,
                        text: 'Ventas al mes'
                    }
                }
            },
        };
        const myChart = new Chart(
            document.getElementById('ChartVentas'),
            config
        );
    }
})

$.ajax({
    type: "GET",
    url: '/Home/GraficoProductos',
    success: function (result) {
        var arrayProductos = []
        var arrayColores = []
        var arrayCantidad = []
        for (let i = 0; i < result.length; i++) {
            let arraycolor = colorRandom()
            arrayProductos.push(result[i].producto)
            arrayCantidad.push(result[i].cantidad)
            arrayColores.push('rgb(' + arraycolor[0] + ',' + arraycolor[1] + ',' + arraycolor[2] + ')')
        }

        const data = {
            labels: arrayProductos,
            datasets: [{
                label: arrayProductos,
                backgroundColor: arrayColores,
                data: arrayCantidad,
            }]
        };

        const config = {
            type: 'doughnut',
            data: data,
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        position: 'top',
                    },
                    title: {
                        display: true,
                        text: 'Productos vendidos'
                    }
                }
            },
        };
        const myChart = new Chart(
            document.getElementById('ChartProductos'),
            config
        );
    }
})
function colorRandom() {
    let arrayRandom = []
    for (let i = 0; i < 3; i++) {
        let x = Math.floor(Math.random() * 255) + 1;
        arrayRandom.push(x)
    }
    return arrayRandom
}