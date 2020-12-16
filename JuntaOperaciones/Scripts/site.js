var ListaImportacionDetallada = [];
var ListaImportacionAgrupada = [];
var ListaExportacionDetallada = [];
var ListaExportacionAgrupada = [];

var d = new Date();

var month = d.getMonth() + 1;
var day = d.getDate();

var Today = (('' + day).length < 2 ? '0' : '') + day + '/' + (('' + month).length < 2 ? '0' : '') + month + '/' + d.getFullYear();
var Maniana = moment().add('days', 1).format('DD/MM/YYYY');
var Ayer = moment().add('days', -1).format('DD/MM/YYYY');

$(document).on('shown.bs.modal', function (e) {
    $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
});

////$(document).ready(function () {
////    $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
////});
////$(document).on('shown.bs.modal', function (e) {
////    $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
////});

$(document).ready(function () {
    $.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
    $.extend(true, $.fn.dataTable.defaults, {
        pageLength: 25,
        lengthMenu: [
            [5, 10, 25, 50, -1],
            ['5 Lineas', '10 Lineas', '25 Lineas', '50 Lineas', 'Todas']
        ],
        dom: 'lBfrtip',
        buttons: [
            //{
            //    extend: 'pdfHtml5',
            //    text: '<i class="fas fa-file-pdf fa-lg"></i>',
            //    className: 'btn btnExportar',
            //    titleAttr: 'Exportar a Pdf'
            //}, ,
            {
                extend: 'excelHtml5',
                className: 'btn btnExportar',
                text: '<i class="fas fa-file-excel fa-lg"></i>',
                titleAttr: 'Exportar a Excel'
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
        }
    });
});