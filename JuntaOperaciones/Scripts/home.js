var myVar = setInterval(myTimer, 60000);

function myTimer() {
    var strDesde = $('#fecha').datepicker().value();
    strDesde = strDesde.substr(6, 4) + strDesde.substr(3, 2) + strDesde.substr(0, 2);
    CargarDashBoard(strDesde, 'T');
}

$(document).ready(function () {

    $('#fecha').datepicker({
        format: 'dd/mm/yyyy',
        value: Today,
        locate: 'es-es'
    });

    var strDesde = $('#fecha').datepicker().value();
    strDesde = strDesde.substr(6, 4) + strDesde.substr(3, 2) + strDesde.substr(0, 2);

    CargarDashBoard(strDesde, 'T');

    $('#fecha').on('change', function () {
        var strDesde = $('#fecha').datepicker().value();
        strDesde = strDesde.substr(6, 4) + strDesde.substr(3, 2) + strDesde.substr(0, 2);
        CargarDashBoard(strDesde, 'T');
    });
});


function MostrarProgreso(Objeto, txtValor, maximo, valor, color1 = '#1ABC9C', color2 = '#0e7662') {


    var opts = {
        angle: 0.15, // The span of the gauge arc
        lineWidth: 0.39, // The line thickness
        radiusScale: 1, // Relative radius
        pointer: {
            length: 0.64, // // Relative to gauge radius
            strokeWidth: 0.035, // The thickness
            color: '#000000' // Fill color
        },
        limitMax: false,     // If false, max value increases automatically if value > maxValue
        limitMin: false,     // If true, the min value of the gauge will be fixed
        colorStart: color1,   // Colors
        colorStop: color2,    // just experiment with them
        strokeColor: '#E0E0E0',  // to see which ones work best for you
        generateGradient: true,
        highDpiSupport: true,     // High resolution support
        // renderTicks is Optional
        renderTicks: {
            divisions: 10,
            divWidth: 1.1,
            divLength: 0.7,
            divColor: '#333333',
            subDivisions: 3,
            subLength: 0.5,
            subWidth: 0.6,
            subColor: '#666666'
        }

    };
 
    var target = document.getElementById(Objeto); // Elemento onde o gauge deve ser criado
    var gauge = new Gauge(target).setOptions(opts); // Criar gauge
    gauge.maxValue = maximo; // Valor maximo
    gauge.setMinValue(0);  // Valor minimo
    gauge.animationSpeed = 32; // Velocidade da animacao
    gauge.set(valor); // Valor a ser exibido
    var porcentaje = 0

    if (maximo != 0) {
        porcentaje = (valor / maximo) * 100;
    }


    $('#' + txtValor).text(Math.round(porcentaje));

}


function CargarDashBoard(fecha, accion) {


    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

    //Prepare parameters
    var params = {
        Fecha: fecha,
        Accion: accion
    };


    function exito(rpta) {

        var GlobalList = rpta.Global;
        var DiarioList = rpta.Diario;
        if (GlobalList.length > 0) {
            $('#NavesActivas').text(GlobalList[0].CANTNAVE);
            $('#ManImpo').text(GlobalList[0].MANIMPO);
            $('#ReciImpo').text(GlobalList[0].RECIIMPO);
            $('#ManExpo').text(GlobalList[0].MANEXPO);
            $('#EnviExpo').text(GlobalList[0].ENVIEXPO);
        }

        //if (DiarioList.length > 0) {
            var ImpoTotal = 0;
            var ImpoRecibido = 0;
            var ExpoTotal = 0;
            var ExpoRecibido = 0;

            $.each(DiarioList, function (ind, elem) {

                ImpoTotal += parseInt(elem['MANIMPO']);
                ImpoRecibido += parseInt(elem['RECIIMPO']);
                ExpoTotal += parseInt(elem['MANEXPO']);
                ExpoRecibido += parseInt(elem['NVIEXPO']);
            });

            MostrarProgreso('chart_gauge_IMPO', 'ValorActualI', ImpoTotal, ImpoRecibido);
            MostrarProgreso('chart_gauge_EXPO', 'ValorActualE', ExpoTotal, ExpoRecibido, '#E74C3C', '#882217');
            var Cabecera = '<tr>' +
                '<th scope="col" class="">Nave</th>' +
                //'<th scope="col" class="">Viaje</th>' +
                '<th scope="col" class="">Man. Im</th>' +
                '<th scope="col" class="">Rec. Im</th>' +
                '<th scope="col" class="">Fin Desc.</th>' +
                '<th scope="col" class="">Alerta Im.</th>' +
                '<th scope="col" class="">Man. Ex</th>' +
                '<th scope="col" class="">Env. Ex</th>' +
                '<th scope="col" class="">CutOff</th>' +
                '<th scope="col" class="">Alerta Ex.</th>' +
                '</tr>';
            $('#tblDetalle thead').empty();

            $('#tblDetalle thead').append(Cabecera);
            var IndRow = 1;
            var table = $('#tblDetalle').DataTable({
                destroy: true,
                responsive: true,
                data: DiarioList,
                columnDefs: [

                    { responsivePriority: 1, targets: 0 },
                    //{ responsivePriority: 2, targets: -1 },
                ],
                columns: [
                    { data: 'NOMNAVE' },
                    //{ data: 'NVJES' },
                    { data: 'MANIMPO' },
                    { data: 'RECIIMPO' },
                    {
                        "mData": null,
                        "bSortable": false,
                        "mRender": function (o) {
                            var Fecha = Today;
                            var Hora = o.HORFINOPERA.toString();

                            Hora = pad(Hora, 6);
                            Hora = Hora.substr(0, 2) + ':' + Hora.substr(2, 2) + ':' + Hora.substr(4, 2)

                            if (o.FECFINDESCA.length == 8) {

                                Fecha = o.FECFINDESCA.substring(6, 2) + '/' + o.FECFINDESCA.substring(4, 2) + '/' + o.FECFINDESCA.substring(0, 4)
                            }
                            return Fecha + ' ' + Hora;
                        }
                    },
                    {
                        "mData": null,
                        "bSortable": false,
                        "mRender": function (o) {
                            var Fecha = '';
                            var Hora = '';

                            if (o.FECFINDESCA.length == 8) {

                                Fecha = o.FECFINDESCA.substring(6, 2) + '/' + o.FECFINDESCA.substring(4, 2) + '/' + o.FECFINDESCA.substring(0, 4)
                            }
                            var Valor = '';
                            if (fecha != '') {
                                Valor = HelperFN.Semaforo(Fecha + Hora);
                            }

                            return Valor;
                        }
                    },
                    { data: 'MANEXPO' },
                    { data: 'ENVIEXPO' },
                    {
                        "mData": null,
                        "bSortable": false,
                        "mRender": function (o) {
                            var Fecha = Today;
                            var Hora = o.HORCUTOFF.toString();

                            Hora = pad(Hora, 6);
                            Hora = Hora.substr(0, 2) + ':' + Hora.substr(2, 2) + ':' + Hora.substr(4, 2)

                            if (o.FECCUTOFF.length == 8) {

                                Fecha = o.FECCUTOFF.substring(6, 2) + '/' + o.FECCUTOFF.substring(4, 2) + '/' + o.FECCUTOFF.substring(0, 4)
                            }
                            return Fecha + ' ' + Hora;
                        }
                    },
                    {
                        "mData": null,
                        "bSortable": false,
                        "mRender": function (o) {
                            var Fecha = '';
                            var Hora = o.HORCUTOFF.toString();

                            Hora = pad(Hora, 6);
                            Hora = Hora.substr(0, 2) + ':' + Hora.substr(2, 2) + ':' + Hora.substr(4, 2)

                            if (o.FECCUTOFF.length == 8) {

                                Fecha = o.FECCUTOFF.substring(6, 2) + '/' + o.FECCUTOFF.substring(4, 2) + '/' + o.FECCUTOFF.substring(0, 4)
                            }

                            var Valor = HelperFN.Semaforo(Fecha + Hora);

                            return Valor;
                        }
                    },

                ],

                language: {
                    "decimal": "",
                    "emptyTable": "No hay información",
                    "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
                    "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
                    "infoFiltered": "(Filtrado de _MAX_ total entradas)",
                    "infoPostFix": "",
                    "thousands": ",",
                    "lengthMenu": "Mostrar _MENU_ Entradas",
                    "loadingRecords": "Cargando...",
                    "processing": "Procesando...",
                    "search": "Buscar:",
                    "zeroRecords": "Sin resultados encontrados",
                    "paginate": {
                        "first": "Primero",
                        "last": "Ultimo",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    }
                },
            });
        //}

    }
    function error(rpta) {
        console.error(rpta);
    }

    HelperFN.AjaxJson("POST", "./Home/GetDashBoard", params, true, exito, error, antiForgeryToken, true, true);

}
function pad(str, max) {
    str = str.toString();
    return str.length < max ? pad("0" + str, max) : str;
}