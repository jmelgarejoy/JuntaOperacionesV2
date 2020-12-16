var Lista = [];
var ListaSeleccionada = [];

$(document).ready(function () {
    $('#fecha').datepicker({
        format: 'dd/mm/yyyy',
        //minDate: Today,
        value: Today,
        locate: 'es-es'
    });

    $('#nuevaFecha').datepicker({
        format: 'dd/mm/yyyy',
        value: Maniana,
        minDate: Maniana,
        locate: 'es-es'
    });

    $('#nuevaHORA').timepicker({ footer: false, modal: true });

    $('#fecha').prop('readonly', true);
    $('#nuevaFecha').prop('readonly', true);
    $('#nuevaHORA').prop('readonly', true);

    ConsultarOrdenes($('#fecha').val());

    $('#btnBuscar').on('click', function () {
        ConsultarOrdenes($('#fecha').val());
    });

    $('#ProgramarModal').on('shown.bs.modal', function (event) {

        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('whatever') // Extract info from data-* attributes

        if (recipient == "Ver") {
            var id = button.data('id');
            $('#ORDENPro').val(id);
        }

    });

    $('#ReprogramarModal').on('shown.bs.modal', function (event) {

        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('whatever') // Extract info from data-* attributes

        if (recipient == "Ver") {
            var id = button.data('id');
            $('#ORDEN').val(id);
        }

    });

    $('#btnReprogramar').on('click', function () {
        Swal.fire({
            title: '¿Estas seguro de Reprogramar la Orden de Trabajo?',
            text: "responda Si solo de estar seguro de hacerlo.",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si, Reprogramar!'
        }).then((result) => {
            if (result.value) {
                var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

                //Prepare parameters
                var params = {
                    FECHA: $('#nuevaFecha').val(),
                    ORDEN: $('#ORDEN').val(),
                };

                function exito(rpta) {
                    $('#ProgramarModal').modal('hide');
                    ConsultarOrdenes($('#fecha').val());
                }

                function error(rpta) {

                }

                HelperFN.AjaxJson("POST", "../procesosJO/Reprogramar", params, true, exito, error, antiForgeryToken);
            }
        });



    })

    $('#btnProgramar').on('click', function () {
        Swal.fire({
            title: '¿Estas seguro de Programar la Orden de Trabajo de este DIA?',
            text: "responda Si solo de estar seguro de hacerlo.",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si, Programar!'
        }).then((result) => {
            if (result.value) {
                var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

                //Prepare parameters
                var params = {
                    FECHA: $('#fecha').val(),
                    HORA: $('#nuevaHORA').val(),
                    ORDEN: $('#ORDENPro').val()
                };

                function exito(rpta) {
                    $('#ProgramarModal').modal('hide');
                    ConsultarOrdenes($('#fecha').val());
                }

                function error(rpta) {

                }

                HelperFN.AjaxJson("POST", "../procesosJO/Programar", params, true, exito, error, antiForgeryToken);
            }
        });



    })

});

function cambiarImpo() {
    var impo = [];
    if ($('#ServImpo').val() != '') {
        impo = Lista.Grupo.filter(x => x.CGRONG == '51' && x.CSRVNV == $('#ServImpo').val() && (x.ESTADO == 'P' || x.ESTADO == 'R'));
    } else {
        impo = Lista.Grupo.filter(x => x.CGRONG == '51' && (x.ESTADO == 'P' || x.ESTADO == 'R'));
    }

    LlenarDetalleImpo(impo);

}

function cambiarExpo() {
    var expo = [];
    if ($('#ServExpo').val() != '') {
        expo = Lista.Grupo.filter(x => x.CGRONG == '53' && x.CSRVNV == $('#ServExpo').val() && (x.ESTADO == 'P' || x.ESTADO == 'R'));
    } else {
        expo = Lista.Grupo.filter(x => x.CGRONG == '53' && (x.ESTADO == 'P' || x.ESTADO == 'R'));
    }

    LlenarDetalleExpo(expo);

}

function ConsultarOrdenes(fecha) {
    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

    //Prepare parameters
    var params = {
        FECHA: fecha
    };

    function exito(rpta) {
        Lista = rpta;

        var total = rpta.Totales.filter(x => (x.ESTADO == 'P' || x.ESTADO == 'R'));
        ListaOrden = rpta.GrupoByOrden;
        LlenarDetalle(total);

        var listaComboServicioImpo = [];
        $.each(rpta.Grupo.filter(x => (x.ESTADO == 'P' || x.ESTADO == 'R') && x.CGRONG == '51'), function (k, v) {
            var objTempo = {
                id: 0,
                valor: ""
            }
            objTempo.id = v.CSRVNV;
            objTempo.valor = v.SERVICIO;
            var c = listaComboServicioImpo.filter(x => x.id == v.CSRVNV);
            
            if (c.length == 0) {
                listaComboServicioImpo.push(objTempo)
            }

        });
        HelperFN.CargarComboList("#ServImpo", listaComboServicioImpo, "id", "valor", "", "Todos los Servicios");

        var listaComboServicioExpo = [];
        $.each(rpta.Grupo.filter(x => (x.ESTADO == 'P' || x.ESTADO == 'R') && x.CGRONG == '53'), function (k, v) {
            var objTempo = {
                id: 0,
                valor: ""
            }
            objTempo.id = v.CSRVNV;
            objTempo.valor = v.SERVICIO;
            var c = listaComboServicioExpo.filter(x => x.id == v.CSRVNV);

            if (c.length == 0) {
                listaComboServicioExpo.push(objTempo)
            }

        });


        HelperFN.CargarComboList("#ServExpo", listaComboServicioExpo, "id", "valor", "", "Todos los Servicios");
    }

    function error(rpta) {

    }

    HelperFN.AjaxJson("POST", "../procesosJO/GetOrdenTrabajo", params, true, exito, error, antiForgeryToken);
}

function LlenarDetalle(datos) {
    if ($.fn.DataTable.isDataTable('#tblDetalle')) {
        $('#tblDetalle').DataTable().destroy();
        $('#tblDetalle thead').empty();
        $('#tblDetalle tbody').empty();

    }

    var Cabecera = '<tr>' +
        '<th scope="col" class="">Regimen</th>' +
        '<th scope="col" class="">Cantidad de Servicios</th>' +
        '<th scope="col" class="">Servicios</th>' +
        '</tr>';
    $('#tblDetalle thead').empty();

    $('#tblDetalle thead').append(Cabecera);
    var IndRow = 1;
    var table = $('#tblDetalle').DataTable({
        destroy: true,
        responsive: true,
        data: datos,
        columnDefs: [

            { responsivePriority: 1, targets: 0 },
            { responsivePriority: 2, targets: -1 },
            {

                "targets": -1,
                "visible": true,
                "searchable": false,
                "orderable": false,
            },
        ],

        columns: [
            {
                "mData": "CGRONG",
                "bSortable": true,
                "mRender": function (o) {
                    if (o == '51') {
                        return 'Importación'
                    } else {
                        return 'Exportacón'
                    }

                }
            },
            { data: 'CANTSERVICIOS' },
            { data: 'SERVICIO' },
        ],
        //order: [[1, 'asc']],
        rowGroup: {
            dataSrc: "REGIMEN"

        },
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
}

function LlenarDetalleImpo(datos) {
    if ($.fn.DataTable.isDataTable('#tblDetalleImpo')) {
        $('#tblDetalleImpo').DataTable().destroy();
        $('#tblDetalleImpo thead').empty();
        $('#tblDetalleImpo tbody').empty();
    }

    var Cabecera = '<tr>' +
        '<th scope="col" class="">Cliente</th>' +
        '<th scope="col" class="">Agente</th>' +
        '<th scope="col" class="">BL</th>' +
        '<th scope="col" class="">Orden de Serv.</th>' +
        '<th scope="col" class="">Orden Trabajo</th>' +
        '<th scope="col" class="">Cantidad</th>' +
        '<th scope="col" class="">Clase/Dim.</th>' +
        '<th scope="col" class="">Servicios</th>' +
        '<th scope="col" class="">Tipo de Servicio</th>' +
        '<th scope="col" class="">Observación</th>' +
        '<th scope="col" class="">Nro. Req. Ope USUAL</th>' +
        '<th scope="col" class=""></th>' +
        '</tr>';
    $('#tblDetalleImpo thead').empty();

    $('#tblDetalleImpo thead').append(Cabecera);
    //var Acumulado = 0;
    ListaSeleccionada = [];
    var table = $('#tblDetalleImpo').DataTable({
        destroy: true,
        responsive: true,
        data: datos,

        columnDefs: [
            { responsivePriority: 1, targets: 0, Sortable: false, visible: false, searchable: true },
            { responsivePriority: 1, targets: 1, Sortable: false, visible: false, searchable: true },
            { responsivePriority: 2, targets: -1 },
            {

                "targets": -1,
                "visible": true,
                "searchable": false,
                "orderable": false,
            },
        ],

        rowGroup: {
            dataSrc: ['CLIENTE', 'AGENTE', 'NORDTR'],
            startRender: function (rows, group, level) {

                if (level == 0) {
                    return '' + group;
                } else if (level == 2) {                    

                    return 'Orden de Trabajo Nº ' + group + ' <button type="button" class="btn btn-primary btn-sm"  data-tooltip="tooltip" title="Programar Orden de Trabajo Nº: ' + group + '"  data-toggle="modal" data-target="#ProgramarModal" data-whatever="Ver" data-id="' + group + '"><i class="fas fa-business-time"></i> Programar </button > ' +
                        ' <button type="button" class="btn btn-secondary btn-sm" data-tooltip="tooltip" title="Reprogramar Orden de Trabajo Nº: ' + group + '"  data-toggle="modal" data-target="#ReprogramarModal" data-whatever="Ver" data-id="' + group + '"><i class="fas fa-business-time" ></i> Reprogramar </button>' +
                        ' <button type="button" class="btn btn-success btn-sm" data-tooltip="tooltip" title="Liquidación Orden de Trabajo Nº: ' + group + '"  data-toggle="modal" data-target="#LiquidarModal" data-whatever="Ver" data-id="' + group + '"><i class="fas fa-business-time" ></i> Liquidación Valorizada </button>' +
                        ' <button type="button" class="btn btn-warning btn-sm" data-tooltip="tooltip" title="Recursos Orden de Trabajo Nº: ' + group + '"  data-toggle="modal" data-target="#RecursosModal" data-whatever="Ver" data-id="' + group + '"><i class="fas fa-cogs" ></i> Recursos </button>' +
                        ' <button type="button" class="btn btn-danger btn-sm" data-tooltip="tooltip" title="Falso Orden de Trabajo Nº: ' + group + '"  data-toggle="modal" data-target="#LiquidarModal" data-whatever="Falso" data-id="' + group + '"><i class="fas fa-minus-square"></i> Falso Servicio </button>';;
                }
                else {
                    return group;
                }

            }

        },
        columns: [

            {
                "mData": "CLIENTE",
                "bSortable": true,
                "mRender": function (o) {
                    var result = o.replace('CLIENTE: ', '');
                    return result;
                }
            },
            {
                "mData": "AGENTE",
                "bSortable": true,
                "mRender": function (o) {
                    var result = o.replace('AGENTE DE ADUANA: ', '');
                    return result;
                }
            },
            {
                "mData": "DOCUMENTO",
                "bSortable": true,
                "mRender": function (o) {
                    // IndRow++;
                    return o;
                }
            },
            {
                "mData": "NORDN1",
                "bSortable": true,
                "mRender": function (o) {
                    // IndRow++;
                    return o;
                }
            },
            {
                "mData": "NORDTR",
                "bSortable": true,
                "mRender": function (o) {
                    // IndRow++;
                    return o;
                }
            },
            { data: 'CANTSERVICIOS' },

            { data: 'CLASE' },
            { data: 'SERVICIO' },
            { data: 'MOTIVO' },
            { data: 'OBSERVACION' },
            //{ data: 'NROEXPED' },
            {
                "mData": null,
                "bSortable": true,
                "mRender": function (o) {
                    console.log(o.ESTTRANS)
                    if (o.ESTTRANS == "0") {
                        return '<span data-tooltip="tooltip" data-container="body" title=""><i class="fas fa-info-circle"></i><a href="#"> Pendiente </a></span>';
                    } else if (o.ESTTRANS == "1") {
                        return '<span data-tooltip="tooltip" data-container="body" title="' + o.RESP + '"><i class="fas fa-info-circle" style="color:blue"></i><a href="#">' + o.NROEXPED + '</a></span>';
                    } else if (o.ESTTRANS == "8") {
                        return '<span data-tooltip="tooltip" data-container="body" title="' + o.RESP + '"><i class="fas fa-exclamation-triangle" style="color:red"></i><a href="#"> Error de Transmisión </a></span>';
                    }
                    else {
                        return '';
                    }

                    
                   
                }
            },
            {
                "mData": null,
                "bSortable": true,
                "mRender": function (o) {

                    var listContenedor = Lista.Detallado.filter(x => x.SERVICIO == o.SERVICIO && x.NORDTR == o.NORDTR);
                    listContenedor = listContenedor.filter(x => x.ESTADO == 'P' || x.ESTADO == 'R')
                    
                    var menu = '<div class="d-flex"><div class="dropdown mr-1"><a href="#" id="dropdownMenuOffset" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" >' +
                        'Ver detalle de Contenedor' +
                        '</a> <div id="' + o.ORDEN + o.SERVICIO + '" class="dropdown-menu" aria-labelledby="dropdownMenuOffset">';
                    menu += ' <h6 class="dropdown-header"> CONTENEDORES EN ESTE SERVICIO </h6>';
                    menu += '<spam class="dropdown-item">  CONTENEDOR      CLASE</spam>';

                    $.each(listContenedor, function (k, v) {
                        var contenedor = v.CPRCN1 + v.NSRCN1;
                        menu += '<spam class="dropdown-item">       ' + v.CPRCN1 + v.NSRCN1 + '            '+v.CLASE+ '</spam>';
                    })
                    menu += '</div>  </div>';

                    return menu;
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
    $('[data-tooltip="tooltip"]').tooltip({ html: true });

    $('div.dt-buttons .btn').removeClass('btn-secondary');
    $('div.dt-buttons .btn').addClass('btnExportar');
    $('div.dt-buttons .btn').attr('data-tooltip', 'tooltip');
    $('div.dt-buttons .btn').tooltip();


    // $('[data-toggle="tooltip"]').tooltip({ html: true });
}

function LlenarDetalleExpo(datos) {

    if ($.fn.DataTable.isDataTable('#tblDetalleExpo')) {
        $('#tblDetalleExpo').DataTable().destroy();
        $('#tblDetalleExpo thead').empty();
        $('#tblDetalleExpo tbody').empty();

    }

    var Cabecera = '<tr>' +
        '<th scope="col" class="">Cliente</th>' +
        '<th scope="col" class="">Agente</th>' +
        '<th scope="col" class="">BL</th>' +
        '<th scope="col" class="">Orden de Serv.</th>' +
        '<th scope="col" class="">Orden Trabajo</th>' +
        '<th scope="col" class="">Cantidad</th>' +
        '<th scope="col" class="">Clase/Dim.</th>' +
        '<th scope="col" class="">Servicios</th>' +
        '<th scope="col" class="">Tìpo de Servicio</th>' +
        '<th scope="col" class="">Observación</th>' +
        '<th scope="col" class="">Nro. Req. Ope USUAL</th>' +
        '<th scope="col" class=""> </th>' +
        '</tr>';
    $('#tblDetalleExpo thead').empty();

    $('#tblDetalleExpo thead').append(Cabecera);
    var Acumulado = 0;
    var table = $('#tblDetalleExpo').DataTable({
        destroy: true,
        responsive: true,
        data: datos,
        columnDefs: [
            { responsivePriority: 1, targets: 0, Sortable: false, visible: false, searchable: true },
            { responsivePriority: 1, targets: 1, Sortable: false, visible: false, searchable: true },
            { responsivePriority: 2, targets: -1 },
            {
                "targets": -1,
                "visible": true,
                "searchable": false,
                "orderable": false,
            },
        ],

        rowGroup: {
            dataSrc: ['CLIENTE', 'AGENTE', 'NORDTR'],
            startRender: function (rows, group, level) {

                if (level == 0) {
                    return '' + group;
                } else if (level == 2) {

                    return 'Orden de Trabajo Nº ' + group + ' <button type="button" class="btn btn-primary btn-sm"  data-tooltip="tooltip" title="Programar Orden de Trabajo Nº: ' + group + '"  data-toggle="modal" data-target="#ProgramarModal" data-whatever="Ver" data-id="' + group + '"><i class="fas fa-business-time"></i> Programar </button > ' +
                        ' <button type="button" class="btn btn-secondary btn-sm" data-tooltip="tooltip" title="Reprogramar Orden de Trabajo Nº: ' + group + '"  data-toggle="modal" data-target="#ReprogramarModal" data-whatever="Ver" data-id="' + group + '"><i class="fas fa-business-time" ></i> Reprogramar </button>' +
                        ' <button type="button" class="btn btn-success btn-sm" data-tooltip="tooltip" title="Liquidación Orden de Trabajo Nº: ' + group + '"  data-toggle="modal" data-target="#LiquidarModal" data-whatever="Ver" data-id="' + group + '"><i class="fas fa-business-time" ></i> Liquidación Valorizada </button>' +
                        ' <button type="button" class="btn btn-warning btn-sm" data-tooltip="tooltip" title="Recursos Orden de Trabajo Nº: ' + group + '"  data-toggle="modal" data-target="#RecursosModal" data-whatever="Ver" data-id="' + group + '"><i class="fas fa-cogs" ></i> Recursos </button>' +
                        ' <button type="button" class="btn btn-danger btn-sm" data-tooltip="tooltip" title="Falso Orden de Trabajo Nº: ' + group + '"  data-toggle="modal" data-target="#LiquidarModal" data-whatever="Falso" data-id="' + group + '"><i class="fas fa-minus-square"></i> Falso Servicio </button>';;
                }
                else {
                    return group;
                }


            }

        },
        columns: [

            {
                "mData": "CLIENTE",
                "bSortable": true,
                "mRender": function (o) {
                    var result = o.replace('CLIENTE: ', '');
                    return result;
                }
            },
            {
                "mData": "AGENTE",
                "bSortable": true,
                "mRender": function (o) {
                    var result = o.replace('AGENTE DE ADUANA: ', '');
                    return result;
                }
            },
            {
                "mData": "DOCUMENTO",
                "bSortable": true,
                "mRender": function (o) {
                    // IndRow++;
                    return o;
                }
            },
            {
                "mData": "NORDN1",
                "bSortable": true,
                "mRender": function (o) {
                    // IndRow++;
                    return o;
                }
            },
            {
                "mData": "NORDTR",
                "bSortable": true,
                "mRender": function (o) {
                    // IndRow++;
                    return o;
                }
            },
            { data: 'CANTSERVICIOS' },
            { data: 'CLASE' },
            { data: 'SERVICIO' },
            { data: 'MOTIVO' },
            { data: 'OBSERVACION' },
            {
                "mData": null,
                "bSortable": true,
                "mRender": function (o) {
                    console.log(o.ESTTRANS)
                    if (o.ESTTRANS == "0") {
                        return '<span data-tooltip="tooltip" data-container="body" title=""><i class="fas fa-info-circle"></i><a href="#"> Pendiente </a></span>';
                    } else if (o.ESTTRANS == "1") {
                        return '<span data-tooltip="tooltip" data-container="body" title="' + o.RESP + '"><i class="fas fa-info-circle" style="color:blue"></i><a href="#">' + o.NROEXPED + '</a></span>';
                    } else if (o.ESTTRANS == "8") {
                        return '<span data-tooltip="tooltip" data-container="body" title="' + o.RESP + '"><i class="fas fa-exclamation-triangle" style="color:red"></i><a href="#"> Error de Transmisión </a></span>';
                    }
                    else {
                        return '';
                    }



                }
            },
            {
                "mData": null,
                "bSortable": true,
                "mRender": function (o) {

                    var listContenedor = Lista.Detallado.filter(x => x.SERVICIO == o.SERVICIO && x.NORDTR == o.NORDTR);
                    listContenedor = listContenedor.filter(x => x.ESTADO == 'P' || x.ESTADO == 'R')

                    var menu = '<div class="d-flex"><div class="dropdown mr-1"><a href="#" id="dropdownMenuOffset" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" >' +
                        'Ver detalle de Contenedor' +
                        '</a> <div id="' + o.ORDEN + o.SERVICIO + '" class="dropdown-menu" aria-labelledby="dropdownMenuOffset">';
                    menu += ' <h6 class="dropdown-header"> CONTENEDORES EN ESTE SERVICIO </h6>';
                    menu += '<spam class="dropdown-item">  CONTENEDOR      CLASE</spam>';

                    $.each(listContenedor, function (k, v) {
                        var contenedor = v.CPRCN1 + v.NSRCN1;
                        menu += '<spam class="dropdown-item">       ' + v.CPRCN1 + v.NSRCN1 + '            ' + v.CLASE + '</spam>';
                    })
                    menu += '</div>  </div>';

                    return menu;
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

    $('[data-tooltip="tooltip"]').tooltip();
}