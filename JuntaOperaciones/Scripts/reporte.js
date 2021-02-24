var myVar = setInterval(myTimer, 1000);
function myTimer() {
    var d = new Date();
    document.getElementById("Hora").innerHTML = "Hora: " + d.toLocaleTimeString();
}
$(document).ready(function () {
    $('#FechaPlanTrans').datepicker({
        format: 'dd/mm/yyyy',
        value: Today,
        locate: 'es-es'
    });
    
    $('#FechaPlanTransDet').datepicker({
        format: 'dd/mm/yyyy',
        value: Today,
        locate: 'es-es'
    });
    $('input[name=Filtro]').on('change', function (event) {

        var tipo = $(event.currentTarget)
        if (tipo.data('filtro') == 'Fecha') {
           
            $('#FiltroFecha').show();
            $('#FiltroOS').css('display', 'none');
        } else {
           
            $('#FiltroOS').show();
            $('#FiltroFecha').css('display', 'none');
        }
    });
    CargarReportes1();
    $('#cboProcesos').dropdown();
    $('#btnBuscar').on('click', function () {
        CargarReportes();
    });
    $('#btnDescargarPlanTransp').on('click', function () {
        DescargarReportesPlanTransp();
    });
    $('#btnDescargarPlanTranspDet').on('click', function () {
         DescargarReportesPlanTranspDetallado();
    });
    
    $('#filtroModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('id') // Extract info from data-* attributes
        $('#frmFiltrosPlanTrans').hide();
        $('#frmFiltrosPlanTransDetalle').hide();
        frmFiltrosPlanTransDetalle
        if (recipient == "1") {
            $('#frmFiltrosPlanTrans').show();
            $('#OrdenServ').val('');

        } 
        if (recipient == "2") {
            $('#frmFiltrosPlanTransDetalle').show();
            $('#OrdenServDet').val('');
            $('#TipDocDet').val('');
            $('#ContenedorDet').val('');
            $('#PlacaDet').val('');
            $('#BreveteDet').val('');
            $('#RucDet').val('');
            $('#FechaPlanTransDet').val('');
        }
    });
    var form = document.getElementById("filtroModal");
    var pristine = new Pristine(form);
    form.addEventListener('submit', function (e) {
        e.preventDefault();
        var valid = pristine.validate();
        
        if (valid) {
           

                   


               
        } else {
            var errores = pristine.getErrors();
            var campos = ""
            $.each(errores, function (key, value) {

                if (value.input['type'] == 'select-one') {
                    //if (value.input['name'] == 'cboSistemas') {
                    //    campos += "* Sistema." + '</br>'
                    //}

                } else {


                }


            });


        }

    });
});

function DescargarReportesPlanTranspDetallado()
{
    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
    var Fecha;
    if ($('#FechaPlanTransDet').datepicker().value() != '') {
        Fecha = moment($('#FechaPlanTrans').datepicker().value(), 'DD/MM/YYYY').format('YYYYMMDD');
    }
    else { Fecha=0}
    var params = {
        FECHA: Fecha,
        NORSRN: $('#OrdenServDet').val(),
        DOCREF: $('#TipDocDet').val(),
        CONTENEDOR: $('#ContenedorDet').val(),
        PLACA: $('#PlacaDet').val(),
        BREVETE: $('#BreveteDet').val(),
        RUCTRANSP: $('#RucDet').val()
    };
    function exito(rpta) {
        window.location = './Reportes/Download?fileGuid=' + rpta.FileGuid
            + '&filename=' + rpta.FileName;
    }
    function error(rpta) {
        console.error(rpta);
    }

    HelperFN.AjaxJson("POST", "./Reportes/ExportToExcelPlanificacionTransportesDetalle", params, true, exito, error, antiForgeryToken);

}
function DescargarReportesPlanTransp()
{
    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
    var Fecha;
    if ($('#FechaPlanTrans').datepicker().value() != '') {
        Fecha = moment($('#FechaPlanTrans').datepicker().value(), 'DD/MM/YYYY').format('YYYYMMDD');
    }
    var params = {
        FECHA: Fecha,
        NORSRN: $('#OrdenServ').val()
    };
    function exito(rpta) {
        window.location = './Reportes/Download?fileGuid=' + rpta.FileGuid
            + '&filename=' + rpta.FileName;
    }
    function error(rpta) {
        console.error(rpta);
    }

    HelperFN.AjaxJson("POST", "./Reportes/ExportToExcelPlanificacionTransportes", params, true, exito, error, antiForgeryToken);
}
function CargarReportes1() {

    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

    var Proceso = $('#cboProcesos').dropdown().value();

  
        var params = {
            PROCESOS: ''

        };


        function exito(rpta) {
            if ($.fn.DataTable.isDataTable('#tblDetalle')) {
                $('#tblDetalle').DataTable().destroy();
                $('#tblDetalle thead').empty();
                $('#tblDetalle tbody').empty();
            }


            var Cabecera = '<tr>' +
                '<th scope="col" class=""></th>' +
                '<th scope="col" class="">Procesos</th>' +
                '<th scope="col" class="">Nombre de reporte</th>' +
                '<th scope="col" class="">Descipción</th>' +
                '</tr>';
            $('#tblDetalle thead').empty();
            $('#tblDetalle thead').append(Cabecera);


            var table = $('#tblDetalle').DataTable({
                destroy: true,
                responsive: true,
                scrollX: false,
                data: rpta,
                columnDefs: [
                    { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }
                ],
                columns: [
                    {
                        "mData": null,
                        "bSortable": false,
                        "mRender": function (o) {

                            return "<a href=#>" +
                                '<i class="fas fa-search" data-toggle="modal" data-target="#filtroModal" data-whatever="Reporte" data-id="' + o.IDPROC + '"></i>' +
                                "</a>";
                        }
                    },
                    { data: 'DESCPROC' },
                    { data: 'NOMREPORT' },
                    { data: 'DESCREPORT' }

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

        HelperFN.AjaxJson("POST", "./Reportes/GetReportes", params, true, exito, error, antiForgeryToken);
    
    //Prepare parameters


}
function CargarReportes() {

    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
    
    var Proceso = $('#cboProcesos').dropdown().value();

    if (Proceso == '') {
        HelperFN.stickyShow('Debe seleccionar un proceso.', 'error');
    }
    else
    {
        var params = {
            PROCESOS: $('#cboProcesos').dropdown().value()

        };


        function exito(rpta) {
            if ($.fn.DataTable.isDataTable('#tblDetalle')) {
                $('#tblDetalle').DataTable().destroy();
                $('#tblDetalle thead').empty();
                $('#tblDetalle tbody').empty();
            }


            var Cabecera = '<tr>' +
                '<th scope="col" class=""></th>' +
                '<th scope="col" class="">Procesos</th>' +
                '<th scope="col" class="">Nombre de reporte</th>' +
                '<th scope="col" class="">Descipción</th>' +
                '</tr>';
            $('#tblDetalle thead').empty();
            $('#tblDetalle thead').append(Cabecera);


            var table = $('#tblDetalle').DataTable({
                destroy: true,
                responsive: true,
                scrollX: false,
                data: rpta,
                columnDefs: [
                    { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -1 }
                ],
                columns: [
                    {
                        "mData": null,
                        "bSortable": false,
                        "mRender": function (o) {

                            return "<a href=#>" +
                                '<i class="fas fa-search" data-toggle="modal" data-target="#filtroModal" data-whatever="Reporte" data-id="' + o.IDPROC + '"></i>' +
                                "</a>";
                        }
                    },
                    { data: 'DESCPROC' },
                    { data: 'NOMREPORT' },
                    { data: 'DESCREPORT' }

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

        HelperFN.AjaxJson("POST", "./Reportes/GetReportes", params, true, exito, error, antiForgeryToken);
    }
    //Prepare parameters
    

}