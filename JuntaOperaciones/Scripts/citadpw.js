var myVar = setInterval(myTimer, 1000);
var ListaSeleccionada = [];
function myTimer() {
    var d = new Date();
    document.getElementById("Hora").innerHTML = "Hora: " + d.toLocaleTimeString();
}
$(document).ready(function () {
    $('#fechaIni').datepicker({
        format: 'dd/mm/yyyy',
        value: Ayer,
        locate: 'es-es'
    });
    $('#fechaFin').datepicker({
        format: 'dd/mm/yyyy',
        value: Today,
        locate: 'es-es'
    });
    $('#btnBuscar').on('click', function () {
        CargarCitas('', "L");
    });
    $('#Contenedor').on('blur', function (e) {

        CargarDatosCont($('#Contenedor').val(), "U");



    });
    CargarCitas('', "L");
    $('#nuevoregistroModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('whatever') // Extract info from data-* attributes

        if (recipient == "Nuevo") {
            $('#ID').val('');
            $('#ACCION').val('I');
            $('#NroCita').val('');
            $('#NroCita').prop('readonly', false);
            $('#IDERCE').val('');
            $('#Contenedor').val('');

            $("#IsoType").val(0);
            $('#Tara').val('');
            $('#Peso').val('');
            $('#Placa').val('');
            $('#DNIChofer').val('');
            $('#RucTransporte').val('');
            $('#Precinto1').val('');
            $('#Precinto2').val('');
            $('#Precinto3').val('');
            $('#Precinto4').val('');

        }
        else
        {
            var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
            var ID = button.data('id');
            //Prepare parameters
            var params = {
                NUMID03: ID,
                DESDE: 0,
                HASTA: 0,
                ACCION: 'U'
            };

            function exito(rpta) {
                $('#ID').val(rpta[0].NUMID03);
                $('#NroCita').val(rpta[0].NUMCITA);
                $('#NroCita').prop('readonly', true);
                $('#IDERCE').val('');
                $('#Contenedor').val(rpta[0].NROCON);

                if (rpta[0].ISOTYPE == '22GP') {
                    $("#IsoType > option[value=1]").attr("selected", true);
                }
                else if (rpta[0].ISOTYPE == '42GP') { $("#IsoType").val(2); }
                else if (rpta[0].ISOTYPE == '25GP') { $("#IsoType").val(3); }
                else if (rpta[0].ISOTYPE == '45GP') { $("#IsoType").val(4); }
                else if (rpta[0].ISOTYPE == '22RT') { $("#IsoType").val(5); }
                else if (rpta[0].ISOTYPE == '42RT') { $("#IsoType").val(6); }
                else if (rpta[0].ISOTYPE == '45RT') { $("#IsoType").val(7); }
                else if (rpta[0].ISOTYPE == '22PC') { $("#IsoType").val(8); }
                else if (rpta[0].ISOTYPE == '42PC') { $("#IsoType").val(9); }
                else if (rpta[0].ISOTYPE == '22UT') { $("#IsoType").val(10); }
                else if (rpta[0].ISOTYPE == '42UT') { $("#IsoType").val(11); }
                else if (rpta[0].ISOTYPE == '22TD') { $("#IsoType").val(12); }
                else if (rpta[0].ISOTYPE == '42TD') { $("#IsoType").val(13); }
                else { $("#IsoType").val(0); }

                $('#Tara').val(rpta[0].TARA);
                $('#Peso').val(rpta[0].PESO);
                $('#Placa').val(rpta[0].NROPLACA);
                $('#DNIChofer').val(rpta[0].DOCCHFR);
                $('#RucTransporte').val(rpta[0].RUCEMP);
                $('#Precinto1').val(rpta[0].NROPREC1);
                $('#Precinto2').val(rpta[0].NROPREC2);
                $('#Precinto3').val(rpta[0].NROPREC3);
                $('#Precinto4').val(rpta[0].NROPREC4);
                $('#ACCION').val('U')

                



            }
            function error(rpta) {
                console.log(rpta);
            }

            HelperFN.AjaxJson("POST", "./CitaDPWJO/GetInfoCitasDPW", params, true, exito, error, antiForgeryToken);

        }


    });
    var form = document.getElementById("frmNuevoRegistro");
    var pristine = new Pristine(form);
    form.addEventListener('submit', function (e) {
        e.preventDefault();
        var valid = pristine.validate();
        var combo = document.getElementById("IsoType");
        var selected = combo.options[combo.selectedIndex].innerText;


        if (selected == 'SELECCIONE') {
            valid = false;
        }
        if (valid) {
            HelperFN.PreguntaShow('¿Desea guardar esta cita?', '').then((result) => {
                if (result.value) {
                    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

                    var parameters = {
                        NUMID03: 0,
                        NUMCITA: $('#NroCita').val(),
                        IDRCE: $('#IDERCE').val(),
                        NROCON: $('#Contenedor').val(),
                        ISOTYPE: selected,
                        NROPLACA: $('#Placa').val(),
                        DOCCHFR: $('#DNIChofer').val(),
                        RUCEMP: $('#RucTransporte').val(),
                        NROPREC1: $('#Precinto1').val(),
                        NROPREC2: $('#Precinto2').val(),
                        NROPREC3: $('#Precinto3').val(),
                        NROPREC4: $('#Precinto4').val(),
                        TARA: $('#Tara').val(),
                        PESO: $('#Peso').val(),
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

                    HelperFN.AjaxJson('POST', './CitaDPWJO/AccionesCita', parameters, true, exito, error, antiForgeryToken)
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
                    if (value.input['id'] == 'Contenedor') {
                        HelperFN.stickyShow('Debe ingresar un Contenedor.', 'error');
                    }
                    if (value.input['id'] == 'Placa') {
                        HelperFN.stickyShow('Debe ingresar una Placa.', 'error');
                    }
                    if (value.input['id'] == 'DNIChofer') {
                        HelperFN.stickyShow('Debe ingresar DNI del chofer.', 'error');
                    }
                    if (value.input['id'] == 'RucTransporte') {
                        HelperFN.stickyShow('Debe ingresar RUC de la empresa transportista.', 'error');
                    }
                    if (value.input['id'] == 'Precinto1') {
                        HelperFN.stickyShow('Debe ingresar un Precinto.', 'error');
                    }

                }


            });


        }

    });
});

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
        NUMID03: '',
        DESDE: desde,
        HASTA: hasta, 
        ACCION: accion
    };


    function exito(rpta) {
        if ($.fn.DataTable.isDataTable('#tblDetalle')) {
            $('#tblDetalle').DataTable().destroy();
            $('#tblDetalle thead').empty();
            $('#tblDetalle tbody').empty();
        }

        var Cabecera = '<tr>' +

            '<th scope="col">Nro. de Cita</th>' +
            '<th scope="col">Contenedor</th>' +
            '<th scope="col">Booking</th>' +
            '<th scope="col">Placa</th>' +
            '<th scope="col">Doc Chofer</th>' +
            '<th scope="col">Precinto</th>' +
            '<th scope="col">Fecha envío</th>' +
            '<th scope="col">Acciones</th>' +
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

                { data: 'NUMCITA' },
                { data: 'NROCON' },
                { data: 'NUMBKG' },
                { data: 'NROPLACA' },
                { data: 'DOCCHFR' },
                { data: 'NROPREC1' },
                { data: 'FECREG' },
                {
                    "mData": null,
                    "bSortable": false,
                    "mRender": function (o) {
                        return "<a href=#>" +
                            '<i class="fas fa-edit" data-toggle="modal" data-target="#nuevoregistroModal" data-whatever="Editar" data-id="' + o.NUMID03 + '"></i>' +
                            "</a>"
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

    HelperFN.AjaxJson("POST", "./CitaDPWJO/GetInfoCitasDPW", params, true, exito, error, antiForgeryToken);

}
function CargarDatosCont(id, accion) {

    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

    //Prepare parameters
    var params = {
        ID: id,
        ACCION: accion
    };


    function exito(rpta) {

        if (rpta.length > 0) {
            if (rpta[0].NPRECINTO !='') {
                
                $('#IDERCE').val(rpta[0].IDRCE.trim())
                $('#Placa').val(rpta[0].PLACAVEH.trim())
                $('#DNIChofer').val(rpta[0].NDOCCHOFER.trim()),      
                $('#Precinto1').val(rpta[0].NPRECINTO.trim()),    
                $('#Tara').val(rpta[0].TARACONTE.trim()),
                $('#Peso').val(rpta[0].PESONETO.trim())
                $('#RucTransporte').val(rpta[0].NRUCTRANPO.trim())
                if (rpta[0].TIPCONT == 'ST20') {
                    $("#IsoType > option[value=1]").attr("selected", true);
                }
                else if (rpta[0].TIPCONT == 'ST40') { $("#IsoType").val(2); }
                else if (rpta[0].TIPCONT == 'HC20') { $("#IsoType").val(3); }
                else if (rpta[0].TIPCONT == 'HC40') { $("#IsoType").val(4);}
                else if (rpta[0].TIPCONT == 'RE20') { $("#IsoType").val(5); }
                else if (rpta[0].TIPCONT == 'RE40') { $("#IsoType").val(6); }
                else if (rpta[0].TIPCONT == 'RH40') { $("#IsoType").val(7); }
                else if (rpta[0].TIPCONT == 'FR20') { $("#IsoType").val(8); }
                else if (rpta[0].TIPCONT == 'FR40') { $("#IsoType").val(9); }
                else if (rpta[0].TIPCONT == 'OT20') { $("#IsoType").val(10); }
                else if (rpta[0].TIPCONT == 'OT40') { $("#IsoType").val(11); }
                else if (rpta[0].TIPCONT == 'TA20') { $("#IsoType").val(12); }
                else if (rpta[0].TIPCONT == 'TA40') { $("#IsoType").val(13); }
                else { $("#IsoType").val(0); }
                    
                    
                
                
            } 
        }
        else {

            
            HelperFN.stickyShow('No se encontró datos para el contenedor', 'error');
        }

    }
    function error(rpta) {
        console.log(rpta);
    }

    HelperFN.AjaxJson("POST", "./CitaDPWJO/GetContenedorInfo", params, true, exito, error, antiForgeryToken);

}