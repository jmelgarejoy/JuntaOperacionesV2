function myTimer() {
    var d = new Date();
    document.getElementById("Hora").innerHTML = "Hora: " + d.toLocaleTimeString();
}
$(document).ready(function () {
    $('#fechaIni').datepicker({
        format: 'dd/mm/yyyy',
        value: Today,
        locate: 'es-es'
    });
    $('#fechaFin').datepicker({

        format: 'dd/mm/yyyy',
        value: Maniana,
        locate: 'es-es'
    });
    CargarAlertas('', "L");
    $('#btnBuscar').on('click', function () {
        CargarAlertas('', "L");
    });
    $('#txtOrdenServ').on('input', function () {
            this.value = this.value.replace(/[^0-9]/g, '');
    });
    $('#txtNroCita').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
});
function CargarAlertas(id, accion) {
    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
    var desde;
    var hasta;
    if ($('#fechaIni').datepicker().value() != '') {
        desde = moment($('#fechaIni').datepicker().value(), 'DD/MM/YYYY').format('YYYYMMDD');
    }
    else { desde = 0; }
    if ($('#fechaFin').datepicker().value() != '') { hasta = moment($('#fechaFin').datepicker().value(), 'DD/MM/YYYY').format('YYYYMMDD'); }
    else { hasta = 0; }
    //Prepare parameters
    var params = {
        ID: id,
        ORDSERV: $('#txtOrdenServ').val(),
        DESDE: desde,
        HASTA: hasta,
        CITA: $('#txtNroCita').val(), 
        BOOKING: $('#txtBooking').val(), 
        CONTENEDOR: $('#txtContenedor').val(), 
        ACCION: accion
    };
    function exito(rpta) {
        if ($.fn.DataTable.isDataTable('#tblDetalleAlertas')) {
            $('#tblDetalleAlertas').DataTable().destroy();
            $('#tblDetalleAlertas thead').empty();
            $('#tblDetalleAlertas tbody').empty();
        }

        var Cabecera = '<tr>' +
            '<th scope="col" class="">Tipo de alerta</th>' +
            '<th scope="col" class="">Descripción</th>' +
            '<th scope="col">Mensaje de alerta</th>' +
            '<th scope="col">Nave.Viaje</th>' +
            '<th scope="col">Booking</th>' +
            '<th scope="col">Contenedor</th>' +
            '<th scope="col">Placa</th>' +
            '<th scope="col">Roleado</th>' +
            '<th scope="col">Nro. de Cita</th>' +
            '<th scope="col">Fecha</th>' +
            '<th scope="col">Estado</th>' +
            '</tr>';
        $('#tblDetalleAlertas thead').empty();
        $('#tblDetalleAlertas thead').append(Cabecera);


        var table = $('#tblDetalleAlertas').DataTable({
            destroy: true,
            responsive: true,
            scrollX: false,
            data: rpta,
            columnDefs: [
                { responsivePriority: 1, targets: 0 },
                { responsivePriority: 2, targets: -1 }
            ],
            columns: [

                { data: 'TIPALERT' },
                { data: 'DESCALERT' },
                { data: 'MNSJALERT' },
                { data: 'NAVVIAJE' },
                { data: 'NUMBKG' },
                { data: 'NROCON' },
                { data: 'NROPLACA' },
                { data: 'ROLEADO' },
                { data: 'NUMCITA' },
                { data: 'FECCITA' },
                { data: 'ESTCITA' }

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
                    "first": "|<<",
                    "last": ">>|",
                    "next": ">>",
                    "previous": "<<"
                }
            },
        });
    }
    function error(rpta) {
        console.log(rpta);
    }

    HelperFN.AjaxJson("POST", "./MonitorAlertaJO/GetAlertasCitas", params, true, exito, error, antiForgeryToken);
}