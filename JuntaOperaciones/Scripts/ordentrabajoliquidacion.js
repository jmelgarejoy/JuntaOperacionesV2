
var Lista = [];
var LineaActualDetalle = [];
var GrupoImpo = [];
var GrupoExpo = [];
var GrupoOrden = [];
var ListaOrden = [];
var ListaSeleccionada = [];

$(document).ready(function () {
    
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
            //$('divLoading').hide();
            ListaSeleccionada = [];
            var id = button.data('id');
            $('#ORDEN').val(id);
            $('#LiquidarModalLabel').text('Liquidar Orden de Trabajo Nº ' + id);
            $('#DetalleGrid').show();
            $('#Liquidar').hide();
            $('#btnVolver').hide();

            $.each(Lista.GrupoByOrden.filter(X => X.NORDTR == id && (X.ESTADO == 'P' || X.ESTADO == 'R' )), function (k, v) {
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
            
            LlenarDetalle1(ListaSeleccionada,1);
            TraerProveedores();
        } else if (recipient == "Falso") {
            $('divLoading').show();
            //            $('divLoading').hide();
            ListaSeleccionada = [];
            var id = button.data('id');
            $('#ORDEN').val(id);
            $('#LiquidarModalLabel').text('Liquidar Orden de Trabajo Nº ' + id);
            $('#DetalleGrid').show();
            $('#Liquidar').hide();
            $('#btnVolver').hide();
            

            $.each(Lista.GrupoByOrden.filter(X => X.NORDTR == id && (X.ESTADO == 'P' || X.ESTADO == 'R')), function (k, v) {
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

            LlenarDetalle1(ListaSeleccionada,2);
            //TraerProveedores();
        }

    });


    $('#RecursosModal').on('shown.bs.modal', function (event) {

        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('whatever') // Extract info from data-* attributes

        if (recipient == "Ver") {
            $('divLoading').show();
            //            $('divLoading').hide();
            ListaSeleccionada = [];
            var id = button.data('id');
            $('#ORDENModalRec').val(id);
            $('#RecursosModalLabel').text('Recursos Orden de Trabajo Nº ' + id);
            $('#DetalleGridModalRec').show();
            $('#RecursosModalRec').hide();
            $('#btnVolverModalRec').hide();


            $.each(Lista.GrupoByOrden.filter(X => X.NORDTR == id && (X.ESTADO == 'P' || X.ESTADO == 'R')), function (k, v) {
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

            LlenarDetalle2(ListaSeleccionada, 1);
            TraerProveedores();
        }

    });

    $('#btnVolver').on('click', function () {
        $('#DetalleGrid').show();
        $('#Liquidar').hide();
        $('#btnVolver').hide();

    });

    $('#btnVolverModalRec').on('click', function () {
        $('#DetalleGridModalRec').show();
        $('#RecursosModalRec').hide();
        $('#btnVolverModalRec').hide();

    }); 

    $('#btnSiguiente').on('click', function () {
        $('divLoading').show();
        var id = parseInt($('#UltimoIndice').val()) + 1;
        if (id > ListaSeleccionada.length) {
            id = 1;
        }
        LiquidarServicio(id)

    });

    $('#btnSiguienteModalRec').on('click', function () {
        $('divLoading').show();
        var id = parseInt($('#UltimoIndiceModalRec').val()) + 1;
        if (id > ListaSeleccionada.length) {
            id = 1;
        }
        AgregarRecursos(id)

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
                    VALORIZADO: $('#valorizadoLiq').val(),
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
                        Lista.GrupoByOrden.map(function (dato) {
                            if (dato.NORDTR == $('#ORDEN').val() && dato.CSRVNV == $('#codServicioliq').val()) {
                                dato.ESTADO = 'L';
                            }
                        });
                        ListaSeleccionada = [];

                        $.each(Lista.GrupoByOrden.filter(X => X.NORDTR == $('#ORDEN').val() && (X.ESTADO == 'P' || X.ESTADO == 'R' )), function (k, v) {
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

                        LlenarDetalle1(ListaSeleccionada,1);

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

    $('#btnGuardarRecursoModalRec').on('click', function () {
        var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

        var params = {
            ORDEN: $('#ORDENModalRec').val(),
            CODSERV: $('#codServicioliqModalRec').val(),
            CANTSER: $('#CANTRECModalRec').val(),
            CODREC: $('#RecursoLiModalRec').val(),
            TIPOORD: $('#tipoOrdenModalRec').val(),
            TIPOREC: $('#tipoRecursoModalRec').val(),
            OBSERVACION: $('#OBSERVRECModalRec').val(),

        };
        function exito(rpta) {
            if (rpta == "OK") {
                TraerRecursosLiq2($('#ORDENModalRec').val(), $('#codServicioliqModalRec').val());
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

function TraerRecursosLiq2(OT, SERVICIO) {
    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
    var param = {
        OT: OT,
        SERVICIO: SERVICIO
    }

    function exito(rpta) {
        LlenarDetalleRecursos2(rpta);
    }
    function error(rpta) {

    }
    HelperFN.AjaxJson("POST", "../procesosJO/GetDetalleRecursosLiq", param, true, exito, error, antiForgeryToken);
}


function LlenarDetalle1(datos,tipo)
{
    if ($.fn.DataTable.isDataTable('#tblDetalle1')) {
        $('#tblDetalle1').DataTable().destroy();
        $('#tblDetalle1 thead').empty();
        $('#tblDetalle1 tbody').empty();

    }


    var Cabecera = '<tr>' +
        '<th scope="col" >CODIGO</th>' +
        //'<th scope="col" >ITEM</th>' +

        '<th scope="col" >SERVICIO</th>' +
        '<th scope="col" >CANTIDAD</th>' +
        //'<th scope="col" >PESO</th>' +
        '<th scope="col" >Acciones</th>' +
        '</tr>';
    $('#tblDetalle1 thead').empty();

    $('#tblDetalle1 thead').append(Cabecera);

    var table = $('#tblDetalle1').DataTable({
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
                    
                    if (tipo == 1) {
                        return '<a href=#>' + '<i onclick="LiquidarServicio(' + o.indice + ');" class="fas fa-thumbs-up" data-tooltip="tooltip" title="Liquidar Servicio : ' + o.SERVICIO + '" ></i>' + '</a>';
                    }
                    else {
                        
                        return '<a href=#>' + '<i onclick="FalsoServicio(' + o.CSRVNV + ',' + o.NORDTR + ');" class="fas fa-minus-square" data-tooltip="tooltip" title="Indicar Falso Servicio a  ' + o.SERVICIO + '" ></i>' + '</a>';
                    }

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

function LlenarDetalle2(datos, tipo) {
    if ($.fn.DataTable.isDataTable('#tblDetalle2')) {
        $('#tblDetalle2').DataTable().destroy();
        $('#tblDetalle2 thead').empty();
        $('#tblDetalle2 tbody').empty();

    }


    var Cabecera = '<tr>' +
        '<th scope="col" >CODIGO</th>' +
        //'<th scope="col" >ITEM</th>' +

        '<th scope="col" >SERVICIO</th>' +
        '<th scope="col" >CANTIDAD</th>' +
        //'<th scope="col" >PESO</th>' +
        '<th scope="col" >Acciones</th>' +
        '</tr>';
    $('#tblDetalle2 thead').empty();

    $('#tblDetalle2 thead').append(Cabecera);

    var table = $('#tblDetalle2').DataTable({
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
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (o) {

                    return '<a href=#>' + '<i onclick="AgregarRecursos(' + o.indice + ');" class="fas fa-cog" data-tooltip="tooltip" title="Agregar Recuersos del Servicio a  ' + o.SERVICIO + '" ></i>' + '</a>';


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

function AgregarRecursos(id) {
    $('divLoading').show();
    $('#DetalleGridModalRec').hide();
    $('#RecursosModalRec').show();
    $('#btnVolverModalRec').show();
    
    if (ListaSeleccionada.length == 1) {
        $('#btnSiguienteModalRec').hide();
    }
    else {
        $('#btnSiguienteModalRec').show();

    }
    
    LineaActualDetalle = ListaSeleccionada.filter(X => X.indice == id && (X.ESTADO == 'P' || X.ESTADO == 'R' ))[0];
    
    $('#codServicioliqModalRec').val(LineaActualDetalle.CSRVNV);
    $('#ServicioLiqModalRec').val(LineaActualDetalle.SERVICIO);
    $('#UltimoIndiceModalRec').val(LineaActualDetalle.indice);
   
    TraerRecursosLiq2($('#ORDENModalRec').val(), $('#codServicioliqModalRec').val());
    
    $('divLoading').hide();
}

function FalsoServicio(servicio, orden) {
    Swal.fire({
        title: '¿Desea marcar como falso servicio este servicio?',
        text: "",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Marcar!'
    }).then((result) => {
        if (result.value) {
            var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

            var params = {
                ORDEN: orden,
                CODSERV: servicio
            };

            function exito(rpta) {
                if (rpta == 'OK') {
                    Lista.GrupoByOrden.map(function (dato) {
                        if (dato.NORDTR == orden && dato.CSRVNV == servicio) {
                            dato.ESTADO = 'X';
                        }
                    });
                    ListaSeleccionada = [];

                    $.each(Lista.GrupoByOrden.filter(X => X.NORDTR == orden && (X.ESTADO == 'P' || X.ESTADO == 'R')), function (k, v) {
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
                    
                    LlenarDetalle1(ListaSeleccionada, 2);
                    Swal.fire({
                        icon: 'success',
                        title: 'se ha marcado como Falso Servicio con Exito!.',
                        showConfirmButton: true,
                        timer: 5000
                    });
                    if (ListaSeleccionada.length == 0) {
                        $('#LiquidarModal').modal('hide');
                    }

                }
            }
            function error(rpta) {

            }
            HelperFN.AjaxJson("POST", "../procesosJO/FalsoServicio", params, true, exito, error, antiForgeryToken);
        }


    })
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

function LlenarDetalleRecursos2(datos) {
    if ($.fn.DataTable.isDataTable('#tblDetalleRecursosModalRec')) {
        $('#tblDetalleRecursosModalRec').DataTable().destroy();
        $('#tblDetalleRecursosModalRec thead').empty();
        $('#tblDetalleRecursosModalRec tbody').empty();

    }


    var Cabecera = '<tr>' +
        '<th scope="col" >Recurso</th>' +
        '<th scope="col" >Cantidad</th>' +
        '<th scope="col" >Unidad de recurso</th>' +
        '<th scope="col" >Unidad de servicio</th>' +
        '<th scope="col" >Tipo de Orden</th>' +

        //'<th scope="col" >Acciones</th>' +
        '</tr>';
    $('#tblDetalleRecursosModalRec thead').empty();

    $('#tblDetalleRecursosModalRec thead').append(Cabecera);

    var table = $('#tblDetalleRecursosModalRec').DataTable({
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