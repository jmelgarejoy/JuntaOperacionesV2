var Lista = [];
var LineaActualDetalle = [];
var GrupoImpo = [];
var GrupoExpo = [];
var GrupoOrden = [];

var ListaSeleccionada = [];

$(document).ready(function () {
    $('#fecha').datepicker({
        format: 'dd/mm/yyyy',
        //minDate: Today,
        value: Today,
        locate: 'es-es'
    });

    $('#FacturarLiq').dropdown();
    $('#valorizadoLiq').dropdown()
    $('#DolarLiq').dropdown();
    $('#CuadrillaLiq').dropdown();
    $('#Maquinaria').dropdown();


    $('#FechaINILiq').datepicker({
        format: 'dd/mm/yyyy',
        //value: Today,
        locate: 'es-es'
    });
    $('#horaINILiq').timepicker({
        format: 'HH:MM'
    });
    $('#FechaFINLiq').datepicker({
        format: 'dd/mm/yyyy',
        // value: Today,
        locate: 'es-es'
    });
    $('#horaFINLiq').timepicker({
        format: 'HH:MM'
    });

    $('#nuevaFecha').datepicker({
        format: 'dd/mm/yyyy',
        value: Maniana,
        minDate: Maniana,
        locate: 'es-es'
    });

    $('#fecha').prop('readonly', true);
    $('#nuevaFecha').prop('readonly', true);

    ConsultarOrdenes($('#fecha').val());
    $('#btnBuscar').on('click', function () {
        ConsultarOrdenes($('#fecha').val());
    });

    $('#LiquidarModal').on('hidden.bs.modal', function () {
        $('#DetalleGrid').show();
        $('#Liquidar').hide();
        $('#btnVolver').hide();
        ListaSeleccionada = [];

        ConsultarOrdenes($('#fecha').val());

    });

    $('#LiquidarModal').on('shown.bs.modal', function (event) {

        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('whatever') // Extract info from data-* attributes

        if (recipient == "Ver") {
            $('divLoading').show();
            //            $('divLoading').hide();
            ListaSeleccionada = [];
            var id = button.data('id');
            $('#ORDEN').val(id);
            $('#LiquidarModalLabel').text('Liquidar Orden de Trabajo Nº ' + id);
            $('#DetalleGrid').show();
            $('#Liquidar').hide();
            $('#btnVolver').hide();

            $.each(ListaOrden.filter(X => X.NORDTR == id && (X.ESTADO == 'P' || X.ESTADO == 'R' || X.ESTADO == 'T')), function (k, v) {
                var i = k + 1;
                var obj = {
                    indice: i,
                    CGRONG: v.CGRONG,
                    NORDTR: v.NORDTR,
                    CSRVNV: v.CSRVNV,
                    SERVICIO: v.SERVICIO,
                    CANTSERV: v.CANTSERV,
                    PESOSERV: v.PESOSERV,
                    ESTADO: v.ESTADO
                }

                ListaSeleccionada.push(obj);
            })


            LlenarDetalle(ListaSeleccionada);
            TraerProveedores();
        }

    });

    $('#btnVolver').on('click', function () {
        $('#DetalleGrid').show();
        $('#Liquidar').hide();
        $('#btnVolver').hide();

    });

    $('#btnSiguiente').on('click', function () {
        $('divLoading').show();
        var id = parseInt($('#UltimoIndice').val()) + 1;
        if (id > ListaSeleccionada.length) {
            id = 1;
        }
        LiquidarServicio(id)

    });

    $('#btnLiquidar').on('click', function () {

    Swal.fire({
        title: '¿Desea Liquidar este servicio?',
        text: "Recuerde agregar los recursos adicionales pertenecientes a este servicio antes de LIQUIDAR!.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Liquidar!'
    }).then((result) => {
            if (result.value) {
                var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

                var params = {
                    ORDEN: $('#ORDEN').val(),
                    CODSERV: $('#codServicioliq').val(),
                    CANTSOLI: $('#CantSolicitadaLiq').val(),
                    PESOSOLI: $('#PesoSolicitadoLiq').val(),
                    TRF: $('#trfLiq').val(),
                    MONEDA: $('#DolarLiq').val(),
                    CUADRILLA: $('#CuadrillaLiq').val(),
                    OBSERVACION: $('#observacionesLiq').val(),
                    CANTATEN: $('#CantidadLiq').val(),
                    PESOATEN: $('#PesoLiq').val(),
                    FACTURAR: $('#FacturarLiq').val(),
                    VALORIZADO: ' ',
                    FINIREE: $('#FechaINILiq').val(),
                    HINIREE: $('#horaINILiq').val(),
                    FFINREE: $('#FechaFINLiq').val(),
                    HFINREE: $('#horaFINLiq').val(),
                    ZONA: $('#ZonaLiq').val(),
                    FECHA: $('#fecha').val()
                };
                function exito(rpta) {
                    if (rpta == 'OK') {
                        //$('divLoading').show();
                        $('#Liquidar').hide();
                        ListaOrden.map(function (dato) {
                            if (dato.NORDTR == $('#ORDEN').val() && dato.CSRVNV == $('#codServicioliq').val()) {
                                dato.ESTADO = 'L';
                            }
                        });
                        ListaSeleccionada = [];

                        $.each(ListaOrden.filter(X => X.NORDTR == $('#ORDEN').val() && (X.ESTADO == 'P' || X.ESTADO == 'R' || X.ESTADO == 'T')), function (k, v) {
                            var i = k + 1;
                            var obj = {
                                indice: i,
                                CGRONG: v.CGRONG,
                                NORDTR: v.NORDTR,
                                CSRVNV: v.CSRVNV,
                                SERVICIO: v.SERVICIO,
                                CANTSERV: v.CANTSERV,
                                PESOSERV: v.PESOSERV,
                                ESTADO: v.ESTADO
                            }

                            ListaSeleccionada.push(obj);
                        })

                        LlenarDetalle(ListaSeleccionada);

                        if (ListaSeleccionada.length == 0) {
                            $('#LiquidarModal').modal('hide');
                        }
                        else {
                            LiquidarServicio(ListaSeleccionada[0].indice);
                        }

                        Swal.fire({
                            icon: 'success',
                            title: 'se ha liquidado con Exito!.',
                            showConfirmButton: true,
                            timer: 5000
                        });

                        if (ListaSeleccionada.length == 1) {
                            $('#btnSiguiente').hide();
                        }
                        else {

                            $('#btnSiguiente').show();
                        }
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Ups.....',
                            text: rpta,
                            showConfirmButton: true,
                            timer: 5000
                        })
                    }

                }
                function error(rpta) {

                }
                HelperFN.AjaxJson("POST", "../procesosJO/LiquidarServicio", params, true, exito, error, antiForgeryToken);
            }


        })


    });

    $('#btnGuardarRecurso').on('click', function () {
        var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

        var params = {
            ORDEN: $('#ORDEN').val(),
            CODSERV: $('#codServicioliq').val(),
            CANTSER: $('#CANTREC').val(),
            CODREC: $('#Recurso').val(),
            TIPOORD: $('#tipoOrden').val(),
            TIPOREC: $('#tipoRecurso').val(),
            OBSERVACION: $('#OBSERVREC').val(),
          
        };
        function exito(rpta) {
            if (rpta == "OK") {
                TraerRecursosLiq($('#ORDEN').val(), $('#codServicioliq').val());
            }
        }
        function error(rpta) {

        }
        HelperFN.AjaxJson("POST", "../procesosJO/AddRecurso", params, true, exito, error, antiForgeryToken);
    });

});

function TraerProveedores() {
    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
    function exito(rpta) {
        HelperFN.CargarComboList('#CuadrillaLiq', rpta, "IDPROV", "RAZCOMER", "", "Seleccione uno")
    }
    function error(rpta) {

    }
    HelperFN.AjaxJson("POST", "../procesosJO/GetProveedores", null, true, exito, error, antiForgeryToken);
}
function TraerRecursosLiq(OT,SERVICIO) {
    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
    var param = {
        OT: OT,
        SERVICIO: SERVICIO
    }

    function exito(rpta) {
        LlenarDetalleRecursos(rpta);
    }
    function error(rpta) {

    }
    HelperFN.AjaxJson("POST", "../procesosJO/GetDetalleRecursosLiq", param, true, exito, error, antiForgeryToken);
}


function ConsultarOrdenes(fecha) {
    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

    //Prepare parameters
    var params = {
        FECHA: fecha
    };

    function exito(rpta) {
        lista = [];
        ListaImpo = [];
        ListaExpo = [];

        if (rpta.Detalle.length > 0) {
            Lista = rpta.Detalle;
            //ListaImpo = rpta.Grupo.filter(X => X.CGRONG == '51' && ( X.ESTADO == 'P' || X.ESTADO == 'R' || X.ESTADO == 'T'));
            //ListaExpo = rpta.Grupo.filter(X => X.CGRONG == '53' && (X.ESTADO == 'P' || X.ESTADO == 'R' || X.ESTADO == 'T'));
            ListaImpo = rpta.Grupo.filter(X => X.CGRONG == '51' && (X.ESTADO == 'T'));
            ListaExpo = rpta.Grupo.filter(X => X.CGRONG == '53' && (X.ESTADO == 'T'));
            ListaOrden = rpta.GrupoByOrden;
        }
        LlenarDetalleImpo(ListaImpo);
        LlenarDetalleExpo(ListaExpo)

    }

    function error(rpta) {

    }

    HelperFN.AjaxJson("POST", "../procesosJO/GetOrdenTrabajoLiquidacion", params, true, exito, error, antiForgeryToken);
}

function LlenarDetalleImpo(datos) {
    if ($('#ValorMarcaImput').val() == "") $('#ValorMarcaImput').val(0);

    if ($.fn.DataTable.isDataTable('#tblDetalleImpo')) {
        $('#tblDetalleImpo').DataTable().destroy();
        $('#tblDetalleImpo thead').empty();
        $('#tblDetalleImpo tbody').empty();

    }

    var Cabecera = '<tr>' +
        '<th scope="col" >Orden Trabajo</th>' +
        '<th scope="col" >BL</th>' +
        '<th scope="col" >Orden de Servicio</th>' +
        '<th scope="col" >Cliente</th>' +
        '<th scope="col" >Agente</th>' +
        '<th scope="col" >Cantidad de Servicios</th>' +
        '<th scope="col" >Acciones</th>' +
        '</tr>';
    $('#tblDetalleImpo thead').empty();

    $('#tblDetalleImpo thead').append(Cabecera);
    var Acumulado = 0;
    ListaSeleccionada = [];
    var table = $('#tblDetalleImpo').DataTable({
        destroy: true,
        responsive: true,
        data: datos,
        columnDefs: [

            { responsivePriority: 1, targets: 0, Sortable: false },
            { responsivePriority: 2, targets: -1 },
            {

                "targets": -1,
                "visible": true,
                "searchable": false,
                "orderable": false,
            },
        ],
        //DOCUMENTO = g.Key.DOCUMENTO,
        //    NORDN1 = g.Key.NORDN1,
        //    CLIENTE = g.Key.CLIENTE,
        columns: [

            {
                "mData": "NORDTR",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            {
                "mData": "DOCUMENTO",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            {
                "mData": "NORDN1",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            {
                "mData": "CLIENTE",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            {
                "mData": "AGENTE",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            { data: 'CANTSERV' },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (o) {
                    return '<a href=#>' + '<i class="fas fa-hands-helping" data-tooltip="tooltip" title="Liquidacion Orden de Trabajo Nº: ' + o.NORDTR + '"  data-toggle="modal" data-target="#LiquidarModal" data-whatever="Ver" data-id="' + o.NORDTR + '"></i>' + '</a>';

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
function LlenarDetalleExpo(datos) {
    if ($.fn.DataTable.isDataTable('#tblDetalleExpo')) {
        $('#tblDetalleExpo').DataTable().destroy();
        $('#tblDetalleExpo thead').empty();
        $('#tblDetalleExpo tbody').empty();

    }


    var Cabecera = '<tr>' +
        '<th scope="col" >Orden Trabajo</th>' +
        '<th scope="col" >BL</th>' +
        '<th scope="col" >Orden de Servicio</th>' +
        '<th scope="col" >Cliente</th>' +
        '<th scope="col" >Agente</th>' +
        '<th scope="col" >Cantidad de Servicios</th>' +
        '<th scope="col" >Acciones</th>' +
        '</tr>';
    $('#tblDetalleExpo thead').empty();

    $('#tblDetalleExpo thead').append(Cabecera);
    var IndRow = 1;
    var table = $('#tblDetalleExpo').DataTable({
        destroy: true,
        responsive: true,
        data: datos,
        columnDefs: [

            { responsivePriority: 1, targets: 0, Sortable: false },
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
                "mData": "NORDTR",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            {
                "mData": "DOCUMENTO",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            {
                "mData": "NORDN1",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            {
                "mData": "CLIENTE",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            {
                "mData": "AGENTE",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            { data: 'CANTSERV' },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (o) {
                    return '<a href=#>' + '<i class="fas fa-hands-helping" data-tooltip="tooltip" title="Liquidacion Orden de Trabajo Nº: ' + o.NORDTR + '"  data-toggle="modal" data-target="#LiquidarModal" data-whatever="Ver" data-id="' + o.NORDTR + '"></i>' + '</a>';

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

function LlenarDetalle(datos) {
    if ($.fn.DataTable.isDataTable('#tblDetalle')) {
        $('#tblDetalle').DataTable().destroy();
        $('#tblDetalle thead').empty();
        $('#tblDetalle tbody').empty();

    }


    var Cabecera = '<tr>' +
        '<th scope="col" >CODIGO</th>' +
        //'<th scope="col" >ITEM</th>' +

        '<th scope="col" >SERVICIO</th>' +
        '<th scope="col" >CANTIDAD</th>' +
        //'<th scope="col" >PESO</th>' +
        '<th scope="col" >Acciones</th>' +
        '</tr>';
    $('#tblDetalle thead').empty();

    $('#tblDetalle thead').append(Cabecera);

    var table = $('#tblDetalle').DataTable({
        destroy: true,
        responsive: true,
        data: datos,
        columnDefs: [

            { responsivePriority: 1, targets: 0, Sortable: false },
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
                "mData": "CSRVNV",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            // {
            //     "mData": "NCRRLT",
            //    "bSortable": true,
            //    "mRender": function (o) {
            //        return o;
            //    }
            //},
            {
                "mData": "SERVICIO",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            {
                "mData": "CANTSERV",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            //{
            //    "mData": "PESOSERV",
            //    "bSortable": true,
            //    "mRender": function (o) {
            //        return o;
            //    }
            //},
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (o) {

                    return '<a href=#>' + '<i onclick="LiquidarServicio(' + o.indice + ');" class="fas fa-thumbs-up" data-tooltip="tooltip" title="Liquidar Servicio : ' + o.SERVICIO + '" ></i>' + '</a>';

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
    $('divLoading').hide();
}

function LiquidarServicio(id) {
    $('divLoading').show();
    $('#DetalleGrid').hide();
    $('#Liquidar').show();
    $('#btnVolver').show();
    $('#Liquidar').show();
    if (ListaSeleccionada.length == 1) {
        $('#btnSiguiente').hide();
    }
    else {
        $('#btnSiguiente').show();
    }
    LineaActualDetalle = ListaSeleccionada.filter(X => X.indice == id && (X.ESTADO == 'P' || X.ESTADO == 'R' || X.ESTADO == 'T'))[0];

    $('#codServicioliq').val(LineaActualDetalle.CSRVNV);
    $('#ServicioLiq').val(LineaActualDetalle.SERVICIO);
    $('#UltimoIndice').val(LineaActualDetalle.indice);
    $('#CantSolicitadaLiq').val(LineaActualDetalle.CANTSERV);
    $('#PesoSolicitadoLiq').val(LineaActualDetalle.PESOSERV);
    $('#CantidadLiq').val(LineaActualDetalle.CANTSERV);
    $('#PesoLiq').val(LineaActualDetalle.PESOSERV);
    $('#FacturarLiq').dropdown().value("2");
    TraerRecursosLiq($('#ORDEN').val(), $('#codServicioliq').val());
    $('#CuadrillaLiq').val(0);
    $('#valorizadoLiq').dropdown().value("X");
    $('#trfLiq').val(0);
    $('#DolarLiq').dropdown().value("100");

    $('#FechaINILiq').val("");
    $('#horaINILiq').val("");
    $('#FechaFINLiq').val("");
    $('#horaFINLiq').val("");
    $('#observacionesLiq').val("");
    $('divLoading').hide();
}

function darFormatoMoneda(valor) {
    const formato = new Intl.NumberFormat('es-ES', {
        minimumFractionDigits: 2
    }).format(valor);
    return formato;
}

function LlenarDetalleRecursos(datos) {
    if ($.fn.DataTable.isDataTable('#tblDetalleRecursos')) {
        $('#tblDetalleRecursos').DataTable().destroy();
        $('#tblDetalleRecursos thead').empty();
        $('#tblDetalleRecursos tbody').empty();

    }


    var Cabecera = '<tr>' +
        '<th scope="col" >Recurso</th>' +
        '<th scope="col" >Cantidad</th>' +
        '<th scope="col" >Unidad de recurso</th>' +
        '<th scope="col" >Unidad de servicio</th>' +
        '<th scope="col" >Tipo de Orden</th>' +
        
        //'<th scope="col" >Acciones</th>' +
        '</tr>';
    $('#tblDetalleRecursos thead').empty();

    $('#tblDetalleRecursos thead').append(Cabecera);

    var table = $('#tblDetalleRecursos').DataTable({
        destroy: true,
        responsive: true,
        data: datos,
        columnDefs: [

            { responsivePriority: 1, targets: 0 },
            { responsivePriority: 2, targets: -1 },
        ],

        columns: [
            
            {
                "mData": "RECURSO",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            {
                "mData": "CANTUSA",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            }, 
            {
                "mData": "UNIREC",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            }, 
            {
                "mData": "UNISERV",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            {
                "mData": "TIPORDDESC",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            }, 
            //{
            //    "mData": null,
            //    "bSortable": false,
            //    "mRender": function (o) {

            //        return '<a href=#>' + '<i onclick="LiquidarServicio(' + o.indice + ');" class="fas fa-thumbs-up" data-tooltip="tooltip" title="Liquidar Servicio : ' + o.SERVICIO + '" ></i>' + '</a>';

            //    }
            //},
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
    $('divLoading').hide();
}