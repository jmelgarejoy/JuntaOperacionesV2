var ListAutorizaciones = [];


$(document).ready(function () {
    $('#CboPlanificacion').dropdown();
    $('#Clave').passwordify({ maxLength: 20 });
    
    $('#btnTransporte').attr('disabled', true);
    $('#btnMaquinaria').attr('disabled', true);
    $('#btnOperaciones').attr('disabled', true);
    $('#btnServicios').attr('disabled', true);

    if (AutoR == 1) {
        $('#btnTransporte').attr('disabled', false);
    } else if (AutoR == 2) {
        $('#btnMaquinaria').attr('disabled', false);
    } else if (AutoR == 3) {
        $('#btnOperaciones').attr('disabled', false);
    } else if (AutoR == 4) {
        $('#btnServicios').attr('disabled', false);
    }

    $('#CboPlanificacion').on('change', function () {
        var id = $('#CboPlanificacion').dropdown().value();
        $('#btnTransporte').attr('disabled', true);
        $('#btnMaquinaria').attr('disabled', true);
        $('#btnOperaciones').attr('disabled', true);
        $('#btnServicios').attr('disabled', true);

        $('#userTransporte').removeClass('fa-check');
        $('#userTransporte').addClass('fa-user-clock');

        $('#userMaquinaria').removeClass('fa-check');
        $('#userMaquinaria').addClass('fa-user-clock');
;
        $('#userOperaciones').removeClass('fa-check');
        $('#userOperaciones').addClass('fa-user-clock');

        $('#userServicios').removeClass('fa-check');
        $('#userServicios').addClass('fa-user-clock');

        if (AutoR == 1) {
            $('#btnTransporte').attr('disabled', false);
        } else if (AutoR == 2) {
            $('#btnMaquinaria').attr('disabled', false);
        } else if (AutoR == 3) {
            $('#btnOperaciones').attr('disabled', false);
        } else if (AutoR == 4) {
            $('#btnServicios').attr('disabled', false);
        }
        TraerPlanificacion(id);
    });

    $('#Observacion').on('keyup', function () {
        $("#nCaracteres").text("Caracteres restantes: " + (250 - $(this).val().length));
    });

    $('#btnAprobar').on('click', function () {


        var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
        var param = {
            ACCION: $('#ACCION').val(),
            IDJTAOPE: $('#CboPlanificacion').dropdown().value(),
            AUTH1: $('#Clave').data('val'),
            AUTH1OBS: $('#Observacion').val()
        }

        function exito(rpta) {
            if (rpta.mensaje == "OK") {
                $('#Clave').val('');
                $('#Clave').data('val','');
                $('#AutorizacionModal').modal('hide');
                HelperFN.stickyShow('Aprobado con exito.', 'success');
                //window.location.href = './planificacion/index';
            } else {
                $('#Clave').val('');
                $('#Clave').data('val', '');
                HelperFN.stickyShow(rpta.mensaje, 'error');
            }
            
        }

        function error(rpta) {

        }


        HelperFN.AjaxJson('POST', './Planificacion/AccionesPlanificacion', param, true, exito, error, antiForgeryToken);
    });

    $('#AutorizacionModal').on('shown.bs.modal', function (event) {

        if ($('#CboPlanificacion').dropdown().value() != "") {
            var button = $(event.relatedTarget) // Button that triggered the modal
            var recipient = button.data('whatever') // Extract info from data-* attributes
            $('#Clave').val('');
            $('#Observacion').val('');
            $('#Clave').data('val', '');
            if (recipient == "A1") {
                $('#ACCION').val(1);
            } else if (recipient == "A2") {
                $('#ACCION').val(2);
            } else if (recipient == "A3") {
                $('#ACCION').val(3);
            } else if (recipient == "A4") {
                $('#ACCION').val(4);
            }
        } else {
            $('#AutorizacionModal').modal('hide');
            HelperFN.stickyShow('Debe seleccionar una planificación.', 'notice');
        }

    });
    $('#AcuerdosModal').on('show.bs.modal', function (event) {
        console.log(ListAutorizaciones,',')


        if ($('#CboPlanificacion').dropdown().value() != "") {

            $('#Observacion1').val(ListAutorizaciones[0]['AUTH1OBS']);
            $('#Observacion2').text(ListAutorizaciones[0].AUTH2OBS);
            $('#Observacion3').val(ListAutorizaciones[0].AUTH3OBS);
            $('#Observacion4').val(ListAutorizaciones[0].AUTH4OBS);

        } else {
            $('#AutorizacionModal').modal('hide');
            HelperFN.stickyShow('Debe seleccionar una planificación.', 'notice');
        }

    });
});


function TraerPlanificacion(ID) {

    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();
    var param = {
        ACCION: 'U',
        ESTADO: '',
        FECHA: 0,
        IDJTAOPE: ID
    }

    function exito(rpta) {
        ListAutorizaciones = rpta['Simple'];
        ListDet = rpta['Detalle']


        if (rpta['Simple'].length != 0) {
            LlenarDetalle(ListDet)
            if (rpta['Simple'].AUTH1 != null) {
                $('#userTransporte').removeClass('fa-user-clock');
                $('#userTransporte').addClass('fa-check');
                $('#btnTransporte').attr('disabled', true);
            }
            if (rpta['Simple'].AUTH2 != null) {
                $('#userMaquinaria').removeClass('fa-user-clock');
                $('#userMaquinaria').addClass('fa-check');
                $('#btnMaquinaria').attr('disabled', true);
            }
            if (rpta['Simple'].AUTH3 != null) {
                $('#userOperaciones').removeClass('fa-user-clock');
                $('#userOperaciones').addClass('fa-check');
                $('#btnOperaciones').attr('disabled', true);
            }
            if (rpta['Simple'].AUTH4 != null) {
                $('#userServicios').removeClass('fa-user-clock');
                $('#userServicios').addClass('fa-check');
                $('#btnServicios').attr('disabled', true);
            }

            $("#fechaInicio").text(HelperFN.FormatoFecha(rpta['Simple'][0].FCINPLN));
            $("#horaInicio").text(HelperFN.FormatoHora(rpta['Simple'][0].HORAINI));
            $("#fechaFin").text(HelperFN.FormatoFecha(rpta['Simple'][0].FCFNPLN));
            $("#horaFin").text(HelperFN.FormatoHora(rpta['Simple'][0].HORAFIN));
            $("#turno3").text(rpta['Simple'][0].CNTTUR3);
            $("#turno1").text(rpta['Simple'][0].CNTTUR1);
            $("#turno2").text(rpta['Simple'][0].CNTTUR2);
            var Total = rpta['Simple'][0].CNTTUR3 + rpta.Simple[0].CNTTUR1 + rpta['Simple'][0].CNTTUR2
            $("#contenedores").text(Total);
        } else {
         
            $('#btnTransporte').attr('disabled', true);
            $('#btnMaquinaria').attr('disabled', true);
            $('#btnOperaciones').attr('disabled', true);
            $('#btnServicios').attr('disabled', true);

            $('#userTransporte').removeClass('fa-check');
            $('#userTransporte').addClass('fa-user-clock');

            $('#userMaquinaria').removeClass('fa-check');
            $('#userMaquinaria').addClass('fa-user-clock');
            ;
            $('#userOperaciones').removeClass('fa-check');
            $('#userOperaciones').addClass('fa-user-clock');

            $('#userServicios').removeClass('fa-check');
            $('#userServicios').addClass('fa-user-clock');

            if (AutoR == 1) {
                $('#btnTransporte').attr('disabled', false);
            } else if (AutoR == 2) {
                $('#btnMaquinaria').attr('disabled', false);
            } else if (AutoR == 3) {
                $('#btnOperaciones').attr('disabled', false);
            } else if (AutoR == 4) {
                $('#btnServicios').attr('disabled', false);
            }
            $("#fechaInicio").text(0);
            $("#horaInicio").text(0);
            $("#fechaFin").text(0);
            $("#horaFin").text(0);
            $("#turno3").text(0);
            $("#turno1").text(0);
            $("#turno2").text(0);
            $("#contenedores").text(0);
        }
        

    }

    function error(rpta) {

    }


    HelperFN.AjaxJson('POST', './Planificacion/GetPlanificaciones', param, true, exito, error, antiForgeryToken);

}


function LlenarDetalle(datos) {
    if ($.fn.DataTable.isDataTable('#tblDetalle')) {
        $('#tblDetalle').DataTable().destroy();
        $('#tblDetalle thead').empty();
        $('#tblDetalle tbody').empty();
    }

    var Cabecera = '<tr>' +
        '<th scope="col" class="">Orden Serv.</th>' +
        '<th scope="col" class="">Nave</th>' +
        '<th scope="col" class="">Contenedor</th>' +
        '<th scope="col" class="">Ope. Portuario</th>' +
        '<th scope="col" class="">Clase</th>' +
        '<th scope="col" class="">Tamaño</th>' +
        '<th scope="col" class="">Peso Br. Manifestado</th>' +
        '<th scope="col" class="">TIPO</th>' +
        '<th scope="col" class="">Refrigerado</th>' +
        '<th scope="col" class="">Impo / Expo</th>' +
        '</tr>';
    $('#tblDetalle thead').empty();
    $('#tblDetalle thead').append(Cabecera);

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
            { data: 'ORDEN' },
            { data: 'NOMNAVE' },
            { data: 'CONTENE' },
            { data: 'OPEPORTU' },
            { data: 'CLASE' },
            { data: 'TAMANIO' },
            {
                "mData": null,
                "bSortable": true,
                "mRender": function (o) {
                    if (o.PESOMAN == 0) {
                        return 'No Disp.';
                    }
                    else {
                        return o.PESOMAN;
                    }
                }
            },            
            {
                "mData": 'TIPOCONT',
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
                "mData": 'TIPOPLAN',
                "bSortable": true,
                "mRender": function (o) {
                    var Valor = '';
                    if (o == 'I') {
                        Valor = 'Importación'
                    }
                    else {
                        Valor = 'Exportación'
                    }

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
                "first": "|<<",
                "last": ">>|",
                "next": ">",
                "previous": "<"
            }
        }
    });

}