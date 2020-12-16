var myVar = setInterval(myTimer, 1000);

function myTimer() {
    var d = new Date();
    document.getElementById("Hora").innerHTML = "Hora: " + d.toLocaleTimeString();
}
var tipoSeleccion = "IMPO";

$(document).ready(function () {

    LlenarDetalleImpo(ListaImportacionDetallada);
    var ordenObj = new Object();
    ordenObj = {
        nOrden: 0
    }

    var ObjetoDetalle = new Object();

    ObjetoDetalle = {
        oOrden: 0,
        oCodNave: 0,
        oContenedor: '',
        oClase: '',
        oTamanio: 0,
        oPesoManifestado: 0,
        oTipoContenedor: '',
        oRefrigerado: '',
        oFechaFinDesc: 0,
        oHoraFinDesc: 0,
        oTipoPlan: '',
        oCutOff: '',
        oCutOffReef: ''
    };


    ListaSeleccionadaDet = [];
    ListaSeleccionada = [];

    $('#fecha').datepicker({
        format: 'dd/mm/yyyy',
        value: Today,
        locate: 'es-es'
    });
    $('#fechaIni').datepicker({
        format: 'dd/mm/yyyy',
        value: Today,
        locate: 'es-es'
    });
    $('#fechaFin').datepicker({
        format: 'dd/mm/yyyy',
        value: Today,
        locate: 'es-es'
    });
    $('#horaIni').timepicker({
        // format: 'HH:MM:ss',
        value: '22:00:00'
    });
    $('#horaFin').timepicker({
        // format: 'HH:MM:ss',
        value: '21:59:59'
    });
    $('#Opciones').dropdown();

    $('#btnBuscar').on('click', function () {
        if ($('#Opciones').val() == 1) {
            ListaSeleccionadaDet = [];
            tipoSeleccion = "EXPO"
            CargarExportaciones();
        }
        else {
            ListaSeleccionadaDet = [];
            tipoSeleccion = "IMPO"
            CargarImportaciones();
        }

    });

    $('#PrePlanificacionModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('whatever') // Extract info from data-* attributes

        if (recipient == "Nuevo") {
            console.log(ListaSeleccionadaDet);

            var ID = 'JO' + moment($('#fecha').val(), 'DD/MM/YYYY').format('YYYYMMDD');
            $('#ID').val(ID);
            $('#ESTADO').val('P');
            $('#ACCION').val('I');
            $('#nCont').text(ListaSeleccionadaDet.length);
            $('#Resta').text(ListaSeleccionadaDet.length);
        }
    })

    $('.Resta').on('change', function () {
        var Distribucion = parseInt($('#turno1').val()) + parseInt($('#turno2').val()) + parseInt($('#turno3').val());
        var total = ListaSeleccionadaDet.length - Distribucion;
        $('#Resta').text(total);
    });

    $('#AgrupadoModal').on('hidden.bs.modal', function (e) {

        ListaTemp = [];
        ListaFinal = []

        if (tipoSeleccion == "IMPO") {
            $.each(ListaSeleccionada, function (index, value) {

                ListaTemp = ListaImportacionDetallada.filter(x => x.NORSRN == value['nOrden'] && x.NBLRCR == 0);
                ListaFinal = ListaFinal.concat(ListaTemp);
            });


            ListaSeleccionada = []
            ListaImportacionDetallada = [];
            ListaImportacionDetallada = ListaFinal;
            LlenarDetalleImpo(ListaImportacionDetallada);

            $.each(ListaImportacionDetallada, function (index, value) {

                ObjetoDetalle = {
                    oOrden: value['NORSRN'],
                    oCodNave: value['CVPRCN'],
                    oContenedor: value['CONTENE'],
                    oClase: value['CTPOC2'],
                    oTamanio: value['TTMNCN1'],
                    oPesoManifestado: value['PBRKLM'],
                    oTipoContenedor: value['SPRPRP'],
                    oRefrigerado: value['SCNRFG'],
                    oFechaFinDesc: value['FINDSC'],
                    oHoraFinDesc: value['HFNDSC'],
                    oOpeportu: value['FLGOPP'],
                    oTipoPlan: 'I',
                    oCutOff: '',
                    oCutOffReef: ''
                };
                ListaSeleccionadaDet.push(ObjetoDetalle);

            });
        } else {


            $.each(ListaSeleccionada, function (index, value) {
                console.log(value)
                ListaTemp = ListaExportacionDetallada.filter(x => x.NORSRN == value['nOrden']);
                console.log(ListaTemp)
                ListaFinal = ListaFinal.concat(ListaTemp);
            });
            console.log(ListaFinal);
            ListaSeleccionada = []
            ListaExportacionDetallada = [];
            //ListaExportacionDetallada = ListaFinal.filter(x => x.NGSLCN==0);
            ListaExportacionDetallada = ListaFinal;
            LlenarDetalleExpo(ListaExportacionDetallada);

            $.each(ListaExportacionDetallada, function (index, value) {

                ObjetoDetalle = {
                    oOrden: value['NORSRN'],
                    oCodNave: value['CVPRCN'],
                    oContenedor: value['CONTENEDOR'],
                    oClase: value['CTPOC2'],
                    oTamanio: value['TTMNCN'],
                    oPesoManifestado: 0,
                    oTipoContenedor: value['SPRPRP'],
                    oRefrigerado: value['REFRIGER'],
                    oFechaFinDesc: '',
                    oHoraFinDesc: '',
                    oOpeportu: value['FLGOPP'],
                    oTipoPlan: 'E',
                    oCutOff: value['FECHAHORACUTOFF'],
                    oCutOffReef: ''
                };
                ListaSeleccionadaDet.push(ObjetoDetalle);

            });
        }
        if (ListaSeleccionadaDet.length > 0) { $('#btnProcesar').attr('disabled', false) } else { $('#btnProcesar').attr('disabled', true) }



        $("#tblDetalle tbody tr").each(function (index) {

            $(this).children("td").each(function (index2) {
                var contenedor = "";
                if (index2 == 3) {
                    contenedor = $(this).text().trim();
                    var Marcado = ListaSeleccionadaDet.filter(X => X.oContenedor === contenedor).length;

                    if (Marcado == 1) {
                        $('#CheckCont' + contenedor).prop("checked", true);
                    }
                    else {
                        $('#CheckCont' + contenedor).prop("checked", false);
                    }

                }

            });
        });

    })
   
    $('#tblDetalle').on('draw.dt', function () {
        
        $("#tblDetalle tbody tr").each(function (index) {

            $(this).children("td").each(function (index2) {
                var contenedor = "";
                if (index2 == 3) {
                    contenedor = $(this).text().trim();
                    var Marcado = ListaSeleccionadaDet.filter(X => X.oContenedor === contenedor).length;

                    if (Marcado == 1) {
                        $('#CheckCont' + contenedor).prop("checked", true);
                    }
                    else {
                        $('#CheckCont' + contenedor).prop("checked", false);
                    }

                }

            });
        });

    });

    $("#btnGuardarPlan").on("click", function () {

        var Distribucion = parseInt($('#turno1').val()) + parseInt($('#turno2').val()) + parseInt($('#turno3').val());

        if (ListaSeleccionadaDet.length == Distribucion) {
            HelperFN.PreguntaShow('¿Desea procesar enviar esta planificación?', '').then((result) => {
                if (result.value) {
                    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
                    var objDetalle = new Object();
                    var JsonDetalle = [];


                    $.each(ListaSeleccionadaDet, function (index, value) {
                        if (tipoSeleccion == "IMPO") {
                            tipoPP = 'I'
                        }
                        else {
                            tipoPP = 'E'
                        }
                        objDetalle = {
                            IDJTAOPE: $('#ID').val(),
                            ORDEN: value['oOrden'],
                            CODNAVE: value['oCodNave'],
                            CONTENE: value['oContenedor'],
                            CLASE: value['oClase'],
                            TAMANIO: value['oTamanio'],
                            PESOMAN: value['oPesoManifestado'],
                            TIPOCONT: value['oTipoContenedor'],
                            REFRIGER: value['oRefrigerado'],
                            FCHFNDSC: 0,
                            HORFNDSC: 0,
                            OPEPORTU: value['oOpeportu'],
                            TIPOPLAN: tipoPP,
                            FCHCUTOFF: 0,
                            HORCUTOFF: 0,
                            FCHCTOFFR: 0,
                            HORCTOFFR: 0,
                            ESTADO: 'P'
                        }
                        JsonDetalle.push(objDetalle);
                    });


                    var parameters = {
                        IDJTAOPE: $('#ID').val(),
                        FCINPLN: moment($('#fechaIni').val(), 'DD/MM/YYYY').format('YYYYMMDD'),
                        HORAINI: moment($('#horaIni').val(), 'HH:mm:ss').format('HHmmss'),
                        FCFNPLN: moment($('#fechaFin').val(), 'DD/MM/YYYY').format('YYYYMMDD'),
                        HORAFIN: moment($('#horaFin').val(), 'HH:mm:ss').format('HHmmss'),
                        CNTTUR3: $('#turno3').val(),
                        CNTTUR1: $('#turno1').val(),
                        CNTTUR2: $('#turno2').val(),
                        AUTH1: '',
                        AUTH2: '',
                        AUTH3: '',
                        AUTH4: '',
                        USERCRE: UsuarioActual,
                        FECHCRE: moment().format('YYYYMMDD'),
                        HORCRE: moment().format('HHmmss'),
                        USERUPD: UsuarioActual,
                        FECHUPD: moment().format('YYYYMMDD'),
                        HORUPD: moment().format('HHmmss'),
                        ESTADO: $('#ESTADO').val(),
                        DETALLE: JSON.stringify(JsonDetalle),
                        ACCION: $('#ACCION').val(),
                    };


                    function exito(rpta) {
                        console.log(rpta);
                    }
                    function error(rpta) {

                    }

                    HelperFN.AjaxJson('POST', './planificacion/AccionesPlanificacion', parameters, true, exito, error, antiForgeryToken)
                }

            })

        } else {
            HelperFN.stickyShow('El numero de Contenedores seleccionado debe ser igual a la distribución de los turnos.', 'notice');
        }

        
    });
});


function CargarExportaciones() {

    var strDesde = moment($('#fecha').val(), 'DD/MM/YYYY').add(-1, 'days').format('DD/MM/YYYY');
    var strHasta = moment($('#fecha').val(), 'DD/MM/YYYY').add(6, 'days').format('DD/MM/YYYY');

    strDesde = strDesde.substr(6, 4) + strDesde.substr(3, 2) + strDesde.substr(0, 2);
    strHasta = strHasta.substr(6, 4) + strHasta.substr(3, 2) + strHasta.substr(0, 2);

    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

    //Prepare parameters
    var params = {
        desde: strDesde,
        hasta: strHasta
    };


    function exito(rpta) {
        if ($.fn.DataTable.isDataTable('#tblGroup')) {
            $('#tblGroup').DataTable().destroy();
            $('#tblGroup thead').empty();
            $('#tblGroup tbody').empty();
        }
        var Cabecera = '<tr>' +
            '<th scope="col" class=""></th>' +
            '<th scope="col" class="">Nave</th>' +
            '<th scope="col">CutOff</th>' +
            '<th scope="col">Man. 20</th>' +
            '<th scope="col">Man. 40</th>' +
            '<th scope="col">Man. 20 Reef</th>' +
            '<th scope="col">Man. 40 Reef</th>' +
            '<th scope="col">Rec. 20</th>' +
            '<th scope="col">Rec. 20 Reef</th>' +
            '<th scope="col">Rec. 20 CD</th>' +
            '<th scope="col">Rec. 20 CD Reef</th>' +
            '<th scope="col">Rec. 20 SD</th>' +
            '<th scope="col">Rec. 20 SD Reef</th>' +
            '<th scope="col">Rec. 40</th>' +
            '<th scope="col">Rec. 40 CD</th>' +
            '<th scope="col">Rec. 40 SD</th>' +
            '<th scope="col">Sin Info 20</th>' +
            '<th scope="col">Sin Info 40</th>' +
            '<th scope="col">Sin Info 20 Reef</th>' +
            '<th scope="col">Sin Info 40 Reef</th>' +
            '<th scope="col">Alerta</th>' +
            '</tr>';
        $('#tblGroup thead').empty();
        $('#tblGroup thead').append(Cabecera);

        ListaExportacionDetallada = rpta[0].Detallado;
        ListaExportacionAgrupada = rpta[0].Agrupado;
        var IndRow = 1;
        $('#AgrupadoModal').modal();


         
        var table = $('#tblGroup').DataTable({
            destroy: true,
            responsive: true,
            scrollX: false,
            data: ListaExportacionAgrupada,
            columnDefs: [
                { responsivePriority: 1, targets: 0 },
                { responsivePriority: 2, targets: -1 }
            ],
            columns: [
                {
                    "mData": null,
                    "bSortable": false,
                    "mRender": function (o) {
                        IndRow++;
                        return '<label class="checkbox">' +
                            '   <input type="checkbox" class="OrdenChk" id="CheckExpo' + o.orden + '" data-NORSRN="' + o.orden + '" onClick="SeleccionCH(event)" > ' +
                            '   <span class="check"></span>' +
                            '</label>';
                    }
                },
                { data: 'nave' },
                { data: 'CutOff' },
                { data: 'TotalManifestado20' },
                { data: 'TotalManifestado40' },
                { data: 'TotalManifestado20Ree' },
                { data: 'TotalManifestado40Ree' },
                { data: 'Total20Recibido' },
                { data: 'Total20RecibidoReef' },
                { data: 'Total20RecibidoCD' },
                { data: 'Total20RecibidoCDReef' },
                { data: 'Total20RecibidoSD' },
                { data: 'Total20RecibidoSDReef' },
                { data: 'Total40Recibido' },
                { data: 'Total40RecibidoCD' },
                { data: 'Total40RecibidoSD' },
                { data: 'Faltan20' },
                { data: 'Faltan20Ree' },
                { data: 'Faltan40' },
                { data: 'Faltan40Ree' },
                {
                    "mData": null,
                    "bSortable": false,
                    "mRender": function (o) {
                        var Fecha = '';
                        var Hora = '';
                        var Valor = HelperFN.Semaforo(o.CutOff);

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
    }
    function error(rpta) {
        console.log(rpta);
    }

    HelperFN.AjaxJson("POST", "./ImpoExpo/GetExportaciones", params, true, exito, error, antiForgeryToken);

}

function CargarImportaciones() {

    var strDesde = moment($('#fecha').val(), 'DD/MM/YYYY').add(-3, 'days').format('DD/MM/YYYY');
    var strHasta = moment($('#fecha').val(), 'DD/MM/YYYY').add(3, 'days').format('DD/MM/YYYY');


    strDesde = strDesde.substr(6, 4) + strDesde.substr(3, 2) + strDesde.substr(0, 2);
    strHasta = strHasta.substr(6, 4) + strHasta.substr(3, 2) + strHasta.substr(0, 2);

    var intDesde = parseInt(strDesde);
    var intHasta = parseInt(strHasta);

    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

    //Prepare parameters
    var params = {
        desde: intDesde,
        hasta: intHasta
    };

    function exito(rpta) {
        if ($.fn.DataTable.isDataTable('#tblGroup')) {
            $('#tblGroup').DataTable().destroy();
            $('#tblGroup thead').empty();
            $('#tblGroup tbody').empty();
        }
        var Cabecera = '<tr>' +
            '<th scope="col" class=""></th>' +
            '<th scope="col" class="">Nave</th>' +
            '<th scope="col">Fecha ETA</th>' +
            '<th scope="col">Man. 20</th>' +
            '<th scope="col">Man. 40</th>' +
            '<th scope="col">Man. 20 Reef</th>' +
            '<th scope="col">Man. 40 Reef</th>' +
            '<th scope="col">Man. CS</th>' +
            '<th scope="col">Rec. 20</th>' +
            '<th scope="col">Rec. 40</th>' +
            '<th scope="col">Rec. 20 Reef</th>' +
            '<th scope="col">Rec. 40 Reef</th>' +
            '<th scope="col">Rec. CS</th>' +
            '<th scope="col">Faltan 20</th>' +
            '<th scope="col">Falta 40</th>' +
            '<th scope="col">Faltan 20 Reef</th>' +
            '<th scope="col">Faltan 40 Reef</th>' +
            '<th scope="col">Falta CS</th>' +
            '</tr>';
        $('#tblGroup thead').empty();
        $('#tblGroup thead').append(Cabecera);
        ListaImportacionDetallada = [];
        ListaImportacionAgrupada = [];
        ListaImportacionDetallada = rpta[0].Detallado;
        ListaImportacionAgrupada = rpta[0].Agrupado;
        var IndRow = 1;
        $('#AgrupadoModal').modal();
        var table = $('#tblGroup').DataTable({
            destroy: true,
            responsive: true,
            data: ListaImportacionAgrupada,
            //ajax: Datos,
            columns: [
                {
                    "mData": null,
                    "bSortable": false,
                    "mRender": function (o) {
                        IndRow++;
                        return '<label class="checkbox">' +
                            '   <input type="checkbox" class="OrdenChk" id="CheckExpo' + o.orden + '" data-NORSRN="' + o.orden + '" onClick="SeleccionCH(event)" > ' +
                            '   <span class="check"></span>' +
                            '</label>';

                    }
                },
                { data: 'nave' },
                { data: 'fechaEta' },
                { data: 'Manifestado20' },
                { data: 'Manifestado40' },
                { data: 'Manifestado20Ree' },
                { data: 'Manifestado40Ree' },
                { data: 'ManifestadoCargaSuelta' },
                { data: 'Recibido20' },
                { data: 'Recibido40' },
                { data: 'Recibido20Ree' },
                { data: 'Recibido40Ree' },
                { data: 'RecibidoCargaSuelta' },
                { data: 'Faltan20' },
                { data: 'Faltan40' },
                { data: 'Faltan20Ree' },
                { data: 'Faltan40Ree' },
                { data: 'FaltanCargaSuelta' },

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

    }
    function error(rpta) {
        console.log(rpta);
    }

    HelperFN.AjaxJson("POST", "./ImpoExpo/GetImportaciones", params, true, exito, error, antiForgeryToken);


}

function SeleccionCH(e) {
    var ObejtoCHK = e.target;
    var Orden = $(ObejtoCHK).data('norsrn');
    if ($(ObejtoCHK).is(':checked')) {
        ordenObj = { nOrden: Orden };
        ListaSeleccionada.push(ordenObj)
    } else {
        ListaSeleccionada = ListaSeleccionada.filter(X => X.nOrden != Orden);
    }

}

function SeleccionCHDet(e) {
    var ObejtoCHK = e.target;
    var vContenedor = $(ObejtoCHK).data('contenedor');
  
    if ($(ObejtoCHK).is(':checked')) {
        if (vContenedor != 'todos') {
            var ListaImportaciontemp = ListaImportacionDetallada.filter(X => X.CONTENE == vContenedor);
            ObjetoDetalle = {
                oOrden: ListaImportaciontemp[0]['NORSRN'],
                oCodNave: ListaImportaciontemp[0]['CVPRCN'],
                oContenedor: ListaImportaciontemp[0]['CONTENE'],
                oClase: ListaImportaciontemp[0]['CTPOC2'],
                oTamanio: ListaImportaciontemp[0]['TTMNCN1'],
                oPesoManifestado: ListaImportaciontemp[0]['PBRKLM'],
                oTipoContenedor: ListaImportaciontemp[0]['SPRPRP'],
                oRefrigerado: ListaImportaciontemp[0]['SCNRFG'],
                oFechaFinDesc: ListaImportaciontemp[0]['FINDSC'],
                oHoraFinDesc: ListaImportaciontemp[0]['HFNDSC'],
                oTipoPlan: 'I',
                oCutOff: '',
                oCutOffReef: ''
            };
            ListaSeleccionadaDet.push(ObjetoDetalle);

            if (ListaSeleccionadaDet.length == ListaImportacionDetallada.length) {
                $('#CheckContTodos').prop("checked", true);
            } else if (ListaSeleccionadaDet.length == 0) {
                //$('#CheckContTodos').prop("checked", false);
            } else {
                $('#CheckContTodos').prop("checked", false);
            }

        } else {
           
            ListaSeleccionadaDet = [];
            $.each(ListaImportacionDetallada, function (index, value) {
                ObjetoDetalle = {
                    oOrden: value['NORSRN'],
                    oCodNave: value['CVPRCN'],
                    oContenedor: value['CONTENE'],
                    oClase: value['CTPOC2'],
                    oTamanio: value['TTMNCN1'],
                    oPesoManifestado: value['PBRKLM'],
                    oTipoContenedor: value['SPRPRP'],
                    oRefrigerado: value['SCNRFG'],
                    oFechaFinDesc: value['FINDSC'],
                    oHoraFinDesc: value['HFNDSC'],
                    oTipoPlan: 'I',
                    oCutOff: '',
                    oCutOffReef: ''
                };
                ListaSeleccionadaDet.push(ObjetoDetalle);

            });
            $('.contenedorChk').prop("checked", true);
            console.log(ListaSeleccionadaDet);
        }

    } else {
        if (vContenedor != 'todos') {
            ListaSeleccionadaDet = ListaSeleccionadaDet.filter(X => X.oContenedor != vContenedor);
            if (ListaSeleccionadaDet.length == ListaImportacionDetallada.length) {
                $('#CheckContTodos').prop("checked", true);
            } else if (ListaSeleccionadaDet.length == 0) {
                //$('#CheckContTodos').prop("checked", false);
            } else {
                $('#CheckContTodos').prop("checked", false);
            }
        } else {
            ListaSeleccionadaDet = [];
            $('.contenedorChk').prop("checked", false);
        }
    }
    if (ListaSeleccionadaDet.length > 0) { $('#btnProcesar').attr('disabled', false) } else { $('#btnProcesar').attr('disabled', true) }

    $("#tblDetalle tbody tr").each(function (index) {

        $(this).children("td").each(function (index2) {
            var contenedor = "";
            if (index2 == 3) {
                contenedor = $(this).text().trim();
                var Marcado = ListaSeleccionadaDet.filter(X => X.oContenedor === contenedor).length;

                if (Marcado == 1) {
                    $('#CheckCont' + contenedor).prop("checked", true);
                }
                else {
                    $('#CheckCont' + contenedor).prop("checked", false);
                }

            }

        });
    });

}

function LlenarDetalleImpo(datos) {
    if ($.fn.DataTable.isDataTable('#tblDetalleExpo')) {
        $('#tblDetalleExpo').DataTable().destroy();
        $('#tblDetalleExpo thead').empty();
        $('#tblDetalleExpo tbody').empty();

    }


    var Cabecera = '<tr>' +
        '<th scope="col" class="CheckboxDT">' +
        '<label class="checkbox">' +
        '   <input type="checkbox" class="contenedorChk" checked id="CheckContTodos" data-contenedor="todos" onClick="SeleccionCHDet(event)" > ' +
        '<span class="check"></span> </label> </th>' +
        '<th scope="col" class="">Orden Serv.</th>' +
        '<th scope="col" class="">Cod Nave</th>' +
        '<th scope="col" class="">Nave</th>' +
        '<th scope="col" class="">Contenedor</th>' +
        '<th scope="col" class="">Clase</th>' +
        '<th scope="col" class="">Tamaño</th>' +
        '<th scope="col" class="">Peso Br. Manifestado</th>' +
        '<th scope="col" class="">Cant. Conocimientos</th>' +
        '<th scope="col" class="">Ope. Portuario</th>' +
        '<th scope="col" class="">Exclusivo</th>' +
        '<th scope="col" class="">TIPO</th>' +
        '<th scope="col" class="">Refrigerado</th>' +
        '<th scope="col" class="">Fecha Fin Descarga</th>' +
        '<th scope="col" class="">Hora Fin Descarga</th>' +
        '<th scope="col" class="">Alerta</th>' +
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
        columnDefs: [
            {
                "targets": [2],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [8],
                "visible": false,
                "searchable": false
            },
            //{
            //    "targets": [9],
            //    "visible": false,
            //    "searchable": false
            //},
        ],
        createdRow: function (row, data, dataIndex) {

            if (data['CANTCONO'] > 1 && data['EXCLUSIVO'] == 'SI') {
                $(row).addClass('LineaDTVerde');
            } else if (data['CANTCONO'] > 1 && data['EXCLUSIVO'] == 'NO') {
                $(row).addClass('LineaDTAmarilla');
            }

        },
        columns: [
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (o) {
                    return '<label class="checkbox">' +
                        '   <input type="checkbox" class="contenedorChk" checked id="CheckCont' + o.CONTENE + '" data-contenedor="' + o.CONTENE + '" onClick="SeleccionCHDet(event)" > ' +
                        '   <span class="check"></span>' +
                        '</label>';

                }
            },
            { data: 'NORSRN' },
            { data: 'CVPRCN' },
            { data: 'TCMPVP' },
            { data: 'CONTENE' },
            { data: 'CTPOC2' },
            { data: 'TTMNCN1' },
            { data: 'PBRKLM' },
            { data: 'CANTCONO' },
            { data: 'FLGOPP' },
            { data: 'EXCLUSIVO' },
            {
                "mData": 'SPRPRP',
                "bSortable": true,
                "mRender": function (o) {
                    var Valor = '';
                    if (o == 'G') {
                        Valor = 'General'
                    }
                    else if (o == 'C') {
                        Valor = 'Carga Peligrosa'
                    }
                    else if (o == 'R') {
                        Valor = 'Refrigerado'
                    }
                    else if (o == 'Q') {
                        Valor = 'IQBF'
                    }
                    else if (o == '1') {
                        Valor = 'IQBF/Carga Peligrosa'
                    }
                    else if (o == '2') {
                        Valor = 'Refrigerado/Carga Peligrosa'
                    }
                    return Valor;

                }
            },
            {
                "mData": 'SCNRFG',
                "bSortable": true,
                "mRender": function (o) {
                    var Valor = '';
                    if (o == 'S') {
                        Valor = 'SI'
                    }
                    else {
                        Valor = 'NO'
                    }

                    return Valor;
                }
            },
            {
                "mData": 'FFNDSC',
                "bSortable": true,
                "mRender": function (o) {
          
                    var Valor = "";
                    if (o.length == 8) {

                        Valor = o.substring(6, 2) + '/' + o.substring(4, 2) + '/' + o.substring(0, 4)
                    }

                    return Valor;
                }
            },
            {
                "mData": 'HFNDSC',
                "bSortable": true,
                "mRender": function (o) {
                    var Valor = '';
                    // console.log(0);
                    return Valor;
                }
            },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (o) {
                    var Fecha = '';
                    var Hora = '';

                    if (o.FFNDSC.length == 8) {

                        Fecha = o.FFNDSC.substring(6, 2) + '/' + o.FFNDSC.substring(4, 2) + '/' + o.FFNDSC.substring(0, 4)
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
}

function LlenarDetalleExpo(datosExpo) {
    if ($.fn.DataTable.isDataTable('#tblDetalle')) {
        $('#tblDetalle').DataTable().destroy();
        $('#tblDetalle thead').empty();
        $('#tblDetalle tbody').empty();
    }

    var Cabecera = '<tr>' +
        '<th scope="col" class="CheckboxDT">' +
        '<label class="checkbox">' +
        '   <input type="checkbox" class="contenedorChk" checked id="CheckContTodos" data-contenedor="todos" onClick="SeleccionCHDet(event)" > ' +
        '<span class="check"></span> </label> </th>' +
        '<th scope="col" class="">Orden Serv.</th>' +
        '<th scope="col" class="">Cod Nave</th>' +
        '<th scope="col" class="">Nave</th>' +
        '<th scope="col" class="">Contenedor</th>' +
        '<th scope="col" class="">Clase</th>' +
        '<th scope="col" class="">Tamaño</th>' +
        '<th scope="col" class="">Peso Br. Manifestado</th>' +
        '<th scope="col" class="">Ope. Portuario</th>' +
        '<th scope="col" class="">Exclusivo</th>' +
        '<th scope="col" class="">TIPO</th>' +
        '<th scope="col" class="">Refrigerado</th>' +
        '<th scope="col" class="">Cut Off</th>' +
        '<th scope="col" class="">Alerta</th>' +
        '</tr>';
    $('#tblDetalleExpo thead').empty();
    $('#tblDetalleExpo thead').append(Cabecera);

    var table = $('#tblDetalleExpo').DataTable({
        destroy: true,
        responsive: true,
        data: datosExpo,
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
        columnDefs: [
            {
                "targets": [2],
                "visible": false,
                "searchable": false
            },

        ],
        columns: [
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (o) {
                    return '<label class="checkbox">' +
                        '   <input type="checkbox" class="contenedorChk" checked id="CheckCont' + o.CONTENEDOR + '" data-contenedor="' + o.CONTENEDOR + '" onClick="SeleccionCHDet(event)" > ' +
                        '   <span class="check"></span>' +
                        '</label>';

                }
            },
            { data: 'NORSRN' },
            { data: 'CVPRCN' },
            { data: 'TCMPVP' },
            { data: 'CONTENEDOR' },
            { data: 'CTPOC2' },
            { data: 'TTMNCN' },
            {
                "mData": null,
                "bSortable": true,
                "mRender": function (o) {
                    return 'No Disp.';
                }
            },
            { data: 'FLGOPP' },
            { data: 'EXCLUSIVO' },
            {
                "mData": 'SPRPRP',
                "bSortable": true,
                "mRender": function (o) {
                    var Valor = '';
                    if (o == 'G') {
                        Valor = 'General'
                    }
                    else if (o == 'C') {
                        Valor = 'Carga Peligrosa'
                    }
                    else if (o == 'R') {
                        Valor = 'Refrigerado'
                    }
                    else if (o == 'Q') {
                        Valor = 'IQBF'
                    }
                    else if (o == '1') {
                        Valor = 'IQBF/Carga Peligrosa'
                    }
                    else if (o == '2') {
                        Valor = 'Refrigerado/Carga Peligrosa'
                    }
                    return Valor;

                }
            },
            {
                "mData": 'REFRIGER',
                "bSortable": true,
                "mRender": function (o) {
                    var Valor = '';
                    if (o == 'SI') {
                        Valor = 'SI'
                    }
                    else {
                        Valor = 'NO'
                    }

                    return Valor;
                }
            },
            { data: 'FECHAHORACUTOFF' },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (o) {
                    var Fecha = '';
                    var Hora = '';
                    var Valor = HelperFN.Semaforo(o.FECHAHORACUTOFF);

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
        }
    });

}
