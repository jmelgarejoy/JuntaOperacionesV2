var myVar = setInterval(myTimer, 1000);
var ListaSeleccionada = [];
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
    $('#FechaCita').datepicker({
        format: 'dd/mm/yyyy',
        value: Today,
        locate: 'es-es'
    });
    $('#btnEliminar').on('click', function () {
        if (ListaSeleccionada.length > 0) {
            deleteCita(ListaSeleccionada);
        }
        else {
            HelperFN.stickyShow('Debe seleccionar al menos un registro', 'error');
        }
    });
    $('#btnBuscar').on('click', function () {
        CargarCitas('', "L");
    });
    $('#btnCancelar').on('click', function () {
        CargarCitas('', "L");
    });

    
    CargarCitas('', "L");

    $('#HoraCita').timepicker({
        // format: 'HH:MM:ss',
        value: '22:00'
    });
    $("#Motivo").on("keyup", function () {

        var este = $(this),
            textoActual = este.val(),
            currentCharacters = este.val().length;
        if (currentCharacters > 250) {
            este.val(textoActual.substring(0, 250));
        }


    });
    $("#reprogramaCheck").click(function () {
        check = document.getElementById("reprogramaCheck");
        if (check.checked) {


            document.getElementById('CitaAnt').disabled = false;
            document.getElementById('Motivo').disabled = false;
        }
        else {
            document.getElementById('CitaAnt').disabled = true;
            $('#CitaAnt').val('');
            document.getElementById('Motivo').disabled = true;
            $('#Motivo').val('');
        }

    });
    $("#LarCheck").click(function () {
        check = document.getElementById("LarCheck");
        if (check.checked) {


            document.getElementById('Contenedor').disabled = false;
            
        }
        else {
            document.getElementById('Contenedor').disabled = true;
            $('#Contenedor').val('');
           
        }

    });
    $('#Booking').on('blur', function (e) {

        CargarOrdenServ($('#Booking').val(), "U");



    });
    $('#CitaAnt').on('blur', function (e) {
        ValidarCita($('#CitaAnt').val(), "U");
    });
    $('#NroCita').on('blur', function (e) {
        ValidarCitaNew($('#NroCita').val(), "N");
    });
    $('#NroCita').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
    $('#CitaAnt').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
    $('#txtOrdenServ').on('input', function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
    
    $('#masivoModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('whatever') // Extract info from data-* attributes

        if (recipient == "Nuevo") {
            $("#inputFileArchivo #FileArchivo").remove();
            $('#inputFileArchivo').append('<input type="file" class="form-control-file" id="FileArchivo" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet">');
        }
    })
    
    $('#visualizaralertasModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('whatever') // Extract info from data-* attributes
        var id = button.data('id')
        if (recipient == "ver") {
            CargarAlertas(id, 'U');
        }
    })
    $('#nuevoregistroModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('whatever') // Extract info from data-* attributes

        if (recipient == "Nuevo") {
            $('#ACCION').val('I');
            $('#NroCita').val('');
            $('#FechaCita').val(Today);
            $('#HoraCita').val('22:00');
            $('#Booking').val('');

            document.getElementById("reprogramaCheck").checked = false;
            $('#CitaAnt').val('');
            $("#CitaAnt").prop("disabled", true);
            $('#Motivo').val('');
            $("#Motivo").prop("disabled", true);
            $('#OrdenServ').val('');
            document.getElementById("LarCheck").checked = false;
            $("#Contenedor").prop("disabled", true);
            $('#Contenedor').val('');
        }
    })
    $('#nuevoalertasModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('whatever') // Extract info from data-* attributes
        $('#CfDryPre').val('');
            $('#CfReeferPre').val('');
            $('#StackingPre').val('');
            $('#CfDryPos').val('');
        $('#CfReeferPos').val('');
        $('#HorasLimit').val('');
        
        if (recipient == "Nuevo") {
            var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
            var ID = button.data('id')
            //Prepare parameters
            var params = {
                ACCION: 'S'
            };

            function exito(rpta) {
                for (var x = 0; x < rpta.length; x++)
                {
                    if (rpta[x].TIPALERT == "PRE")
                    {
                        if (rpta[x].DESCALERT == "DRY")
                        {
                            $('#CfDryPre').val(rpta[x].TEMPALERT);
                        }
                        if (rpta[x].DESCALERT == "REF") {
                            $('#CfReeferPre').val(rpta[x].TEMPALERT);
                        }
                        if (rpta[x].DESCALERT == "STK") {
                            $('#StackingPre').val(rpta[x].TEMPALERT);
                        }
                        if (rpta[x].DESCALERT == "LIM") {
                            $('#HorasLimit').val(rpta[x].TEMPALERT);
                        }
                        
                    }
                    else
                    {
                        if (rpta[x].DESCALERT == "DRY") {
                            $('#CfDryPos').val(rpta[x].TEMPALERT);
                        }
                        if (rpta[x].DESCALERT == "REF") {
                            $('#CfReeferPos').val(rpta[x].TEMPALERT);
                        }
                    }

                }
              
                    $('#ACCION').val('I');
               
               
               


            }
            function error(rpta) {
                //console.log(rpta);
            }

            HelperFN.AjaxJson("POST", "./GestionCitaJO/GetConfigAlert", params, true, exito, error, antiForgeryToken);
        }
    })
    var form3 = document.getElementById("frmNuevoAlerta");
    form3.addEventListener('submit', function (e) {
        e.preventDefault();
       
        HelperFN.PreguntaShow('¿Seguro que desea guardar los datos?', '').then((result) => {
            if (result.value) {
                var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
                var parameters = {
                    PRECODRY: $('#CfDryPre').val(),
                    PRECOREF: $('#CfReeferPre').val(),
                    PRECOSTACK: $('#StackingPre').val(),
                    POSCODRY: $('#CfDryPos').val(),
                    POSCOREF: $('#CfReeferPos').val(),
                    HORASLIMIT: $('#HorasLimit').val(),
                    ACCION: $('#ACCION').val()
                };
            }
            function exito(rpta) {
                if (rpta == "OK") {
                    $('#nuevoalertasModal').modal('hide');
                    HelperFN.stickyShow('Configuración de alertas registrada con exito!.', 'success');
                    CargarCitas('', "L");

                } else {
                    HelperFN.stickyShow(rpta, 'error');
                }


            }
            function error(rpta) {

            }

            HelperFN.AjaxJson('POST', './GestionCitaJO/AlerConfig', parameters, true, exito, error, antiForgeryToken)
        

        });

    });
    var form2 = document.getElementById("frmMasiva");
    form2.addEventListener('submit', function (e) {
        e.preventDefault();

        HelperFN.PreguntaShow('¿Seguro que desea guardar los datos?', '').then((result) => {
            if (result.value) {
                InsertarCargaMasiva();
            }

        });

    });
    var form = document.getElementById("frmNuevoRegistro");
    var pristine = new Pristine(form);
    form.addEventListener('submit', function (e) {
        e.preventDefault();
        var FLG,contenedor,checklar;
        check = document.getElementById("reprogramaCheck");
        if (check.checked) { FLG = 'S' }
        else { FLG = 'N' }
        var valid = pristine.validate();
        if (check.checked) {
            if ($('#CitaAnt').val() == '') {
                HelperFN.stickyShow('Debe ingresar un Número Cita Anterior.', 'error');
                valid = false;
            }
        }
        checklar = document.getElementById("LarCheck");
        if (checklar.checked) { contenedor = $('#Contenedor').val() }
        else { contenedor = '' }
        if (valid) {
            HelperFN.PreguntaShow('¿Desea guardar esta nueva cita?', '').then((result) => {
                if (result.value) {
                    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

                    var parameters = {
                        NUMID: 0,
                        NUMCITA: $('#NroCita').val(),
                        NORSRN: $('#OrdenServ').val(),
                        NUMBKG: $('#Booking').val(),
                        FECCITA: moment($('#FechaCita').datepicker().value(), 'DD/MM/YYYY').format('YYYYMMDD'),
                        HORCITA: $('#HoraCita').val(),
                        CITAANT: $('#CitaAnt').val(),
                        ESTCITA: 'P',
                        FLGREP: FLG,
                        OBSCITA: $('#Motivo').val(),
                        NROCON: contenedor,
                        ACCION: $('#ACCION').val()
                    };


                    function exito(rpta) {
                        if (rpta == "OK") {
                            $('#nuevoregistroModal').modal('hide');
                            HelperFN.stickyShow('Cita registrada con exito!.', 'success');
                            CargarCitas('', "L");
                            
                        } else {
                            HelperFN.stickyShow(rpta, 'error');
                        }


                    }
                    function error(rpta) {

                    }

                    HelperFN.AjaxJson('POST', './GestionCitaJO/AccionesCita', parameters, true, exito, error, antiForgeryToken)
                }

            })
        } else {
            var errores = pristine.getErrors();
            var campos = ""
            $.each(errores, function (key, value) {

                if (value.input['type'] == 'select-one') {
                    //if (value.input['name'] == 'cboSistemas') {
                    //    campos += "* Sistema." + '</br>'
                    //}

                }
                else {
                    if (value.input['id'] == 'NroCita') {
                        HelperFN.stickyShow('Debe ingresar un Número Cita.', 'error');
                    }
                    if (value.input['id'] == 'Booking') {
                        HelperFN.stickyShow('Debe ingresar un Booking.', 'error');
                    }
                    if (value.input['id'] == 'OrdenServ') {
                        HelperFN.stickyShow('Debe ingresar una Orden Servicio.', 'error');
                    }

                }


            });


        }

    });
}
);
function SeleccionCH(e) {
    var ObejtoCHK = e.target;
    var numid = $(ObejtoCHK).data('id');
    if ($(ObejtoCHK).is(':checked')) {
        ordenObj = { nNumid: numid };
        ListaSeleccionada.push(ordenObj)
    } else {
        ListaSeleccionada = ListaSeleccionada.filter(X => X.nNumid != numid);
    }


}
function CargarAlertas(id, accion) {
    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
   
    //Prepare parameters
    var params = {
        ID: id,
        ORDSERV: '0',
        DESDE: 0,
        HASTA: 0,
        CITA: '',
        BOOKING: '',
        CONTENEDOR:'',
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
            '<th scope="col">Mensaje de alerta</th>' +
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
                { data: 'MNSJALERT' }
                
                
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
function CargarOrdenServ(id, accion) {

    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

    //Prepare parameters
    var params = {
        ID: id,
        ACCION: accion
    };


    function exito(rpta) {

        if (rpta.length > 0) {
            if (rpta[0].NORSRN == 0) {
                $('#OrdenServ').val('');
            } else {
                $('#OrdenServ').val(rpta[0].NORSRN);
                $('#OrdenServ').focus();

            }

        }
        else {

            $('#OrdenServ').val('');
            HelperFN.stickyShow('No se encontró una OS para el Booking', 'error');
        }

    }
    function error(rpta) {
        console.log(rpta);
    }

    HelperFN.AjaxJson("POST", "./GestionCitaJO/GetOrdenServ", params, true, exito, error, antiForgeryToken);

}

function ValidarCita(id, accion) {

    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

    //Prepare parameters
    var params = {
        ID: id,
        ACCION: accion
    };


    function exito(rpta) {

        if (rpta.length > 0) {


        }
        else {

            $('#CitaAnt').val('');
            HelperFN.stickyShow('La cita ingresada no existe.', 'error');
        }

    }
    function error(rpta) {
        console.log(rpta);
    }

    HelperFN.AjaxJson("POST", "./GestionCitaJO/GetCita", params, true, exito, error, antiForgeryToken);

}
function ValidarCitaNew(id, accion) {

    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

    //Prepare parameters
    var params = {
        ID: id,
        ACCION: accion
    };


    function exito(rpta) {

        if (rpta.length > 0) {
            $('#NroCita').val('');
            HelperFN.stickyShow('Nro. De cita ya se encuentra registrado', 'error');

        }


    }
    function error(rpta) {
        console.log(rpta);
    }

    HelperFN.AjaxJson("POST", "./GestionCitaJO/GetCita", params, true, exito, error, antiForgeryToken);

}

function CargarCitas(id, accion) {

    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
    var desde;
    var hasta;
    if ($('#fechaIni').datepicker().value() != '') {
        desde = moment($('#fechaIni').datepicker().value(), 'DD/MM/YYYY').format('YYYYMMDD');
    }
    else { desde = 0; }
    if ($('#fechaFin').datepicker().value() != '') { hasta = moment($('#fechaFin').datepicker().value(), 'DD/MM/YYYY').format('YYYYMMDD'); }
    else { hasta = 0; }

    var estadodatos = $('input:radio[name=datosestado]:checked').val();

    //Prepare parameters
    var params = {
        ID: id,
        ORDSERV: $('#txtOrdenServ').val(),
        NUMBKG: $('#txtBooking').val(),
        DESDE: desde,
        HASTA: hasta,
        CONTENEDOR: $('#txtContenedor').val(),
        ESTDAT: estadodatos,
        ACCION: accion
    };


    function exito(rpta) {
        if ($.fn.DataTable.isDataTable('#tblDetalle')) {
            $('#tblDetalle').DataTable().destroy();
            $('#tblDetalle thead').empty();
            $('#tblDetalle tbody').empty();
        }

        var Cabecera = '<tr>' +
            '<th scope="col" class="">Selec.</th>' +
            '<th scope="col" class="">Nave-Viaje</th>' +
            '<th scope="col">Nro. de Cita</th>' +
            '<th scope="col">Fecha y hora</th>' +
            '<th scope="col">OS</th>' +
            '<th scope="col">Booking</th>' +
            '<th scope="col">Contenedor Real</th>' +
            '<th scope="col">Placa</th>' +
            '<th scope="col">DNI Chofer</th>' +
            '<th scope="col">Reprog</th>' +
            '<th scope="col">Cita anterior</th>' +
            '<th scope="col">IQBF</th>' +
            '<th scope="col">LAR</th>' +
            '<th scope="col">Roleado</th>' +
            '<th scope="col">Contenedor Plan</th>' +
            '<th scope="col">Estado</th>' +
            '<th scope="col">Alertas</th>' +
          
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
                        if (o.ESTCITA == 'PENDIENTE') {
                            return '<label class="checkbox">' +
                                '   <input type="checkbox" class="OrdenChk" id="NUMID' + o.NUMID + '" data-id="' + o.NUMID + '" onClick="SeleccionCH(event)" > ' +
                                '   <span class="check"></span>' +
                                '</label>';
                        }
                        else {
                            return '<label class="checkbox">' +

                                '</label>';
                        }
                        //return '<label class="checkbox">' +
                        //    '   <input type="checkbox" class="OrdenChk" id="NUMID' + o.NUMID + '" data-id="' + o.NUMID + '" onClick="SeleccionCH(event)" > ' +
                        //    '   <span class="check"></span>' +
                        //    '</label>';

                    }
                },
                { data: 'NAVVIAJE' },
                { data: 'NUMCITA' },
                { data: 'FECCITA' },
                { data: 'NORSRN' },
                { data: 'NUMBKG' },
                { data: 'NROCON' },
                { data: 'NROPLACA' },
                { data: 'DOCCHFR' },
                { data: 'FLGREP' },
                { data: 'CITAANT' },
                { data: 'IQBF' },
                { data: 'LAR' },
                { data: 'ROL' },
                { data: 'NROCONPLAN' },
                
                { data: 'ESTCITA' },
                {
                    "mData": null,
                    "bSortable": false,
                    "mRender": function (o) {

                        if (o.ALERT == 'V') {
                            return "<a href=#>" +
                                '<i class="fas fa-circulo-verde" data-toggle="" data-target="" data-whatever="" data-id=""></i>' +
                                "</a>"
                        }
                        if (o.ALERT == 'A') {
                            return "<a href=#>" +
                                '<i class="fas fa-circulo-amarillo" data-toggle="modal" data-target="#visualizaralertasModal" data-whatever="ver" data-id="' + o.NUMID + '"></i>' +
                                "</a>"
                        }
                        if (o.ALERT == 'R') {
                            return "<a href=#>" +
                                '<i class="fas fa-circulo-rojo" data-toggle="modal" data-target="#visualizaralertasModal" data-whatever="ver" data-id="' + o.NUMID + '"></i>' +
                                "</a>"
                        }
                    }
                }
               
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

    HelperFN.AjaxJson("POST", "./GestionCitaJO/GetInfoCitas", params, true, exito, error, antiForgeryToken);

}
function deleteCita(data) {
    HelperFN.PreguntaShow('¿Desea eliminar esta(s) Cita(s)?', '').then((result) => {
        if (result.value) {
            var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

            var CitaGroup = "";
            if (!Array.isArray(data)) {
                CitaGroup = data;
            } else {
                for (var i = 0; i < data.length; i++) {

                    if ((i + 1) < data.length) {
                        CitaGroup = CitaGroup + data[i].nNumid + ',';
                    } else {
                        CitaGroup = CitaGroup + data[i].nNumid;
                    }
                }

            }


            var parameters = {
                NUMID: CitaGroup,
                ACCION: 'D'
            };


            function exito(rpta) {
                if (rpta == "OK") {
                    ListaSeleccionada = [];
                    CargarCitas('', "L");
                } else {
                    HelperFN.stickyShow(rpta, 'error');
                }


            }
            function error(rpta) {

            }

            HelperFN.AjaxJson('POST', './GestionCitaJO/AccionesCita', parameters, true, exito, error, antiForgeryToken)
        }

    })


}
function InsertarCargaMasiva() {

    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
    var nroCuadrilla;

    var formdata = new FormData();
    //var file = document.getElementById("FILE").files[0];
    var FileArchivo = document.getElementById("FileArchivo").files[0];


    formdata.append("FILEARCHIVO", FileArchivo);




    function exito(rpta) {
        if (rpta == "OK") {
            CargarCitas('', "L");
            $('#masivoModal').modal('hide');
            
            HelperFN.stickyShow('Se realizo la carga masiva con exito!.', 'success');
        }
        else { HelperFN.stickyShow('Error al cargar las sgts citas: ' + rpta, 'Error'); }
    }
    function error(rpta) {

    }
    HelperFN.AjaxJsonFile('POST', './GestionCitaJO/CargaMasiva', formdata, exito, error, antiForgeryToken)




}