var Lista = [];
var LineaActualDetalle = [];
var GrupoImpo = [];
var GrupoExpo = [];
var GrupoOrden = [];

var ListaSeleccionada = [];

$(document).ready(function () {

    $('#estados').dropdown();
    $('#servicios').dropdown()
    //$('#DolarLiq').dropdown();

    $('#fechaINI').datepicker({
        format: 'dd/mm/yyyy',
        value: Today,
        locate: 'es-es'
    });

    $('#fechaFIN').datepicker({
        format: 'dd/mm/yyyy',
        value: Today,
        locate: 'es-es'
    });
    //LlenarDetalle(Lista);
    ConsultarOrdenes()
    $('#btnBuscar').on('click', function () {
        ConsultarOrdenes();
    })

});


function ConsultarOrdenes() {
    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

    //Prepare parameters
    var params = {
        DESDE: $('#fechaINI').val(),
        HASTA: $('#fechaFIN').val(),
        SERVICIO: $('#servicios').val(),
        DOCUMENTO: $('#documento').val(),
        OT: $('#ot').val(),
        REPORTE: ""
    };

    function exito(rpta) {
        lista = [];

        if (rpta.length > 0) {

            if ($('#estados').val() != '') {
                lista = rpta.filter(x => x.ESTADO == $('#estados').val());
            }
            else {
                lista = rpta;
            }
            
            var totalCuadrillas = lista.filter(x => x.CSRVNV == 831)
            console.log(totalCuadrillas.length);    
        }
        LlenarDetalle(lista)
    }

    function error(rpta) {

    }

    HelperFN.AjaxJson("POST", "../procesosJO/GetReportesOT", params, true, exito, error, antiForgeryToken);
}


function LlenarDetalle(datos) {
    if ($.fn.DataTable.isDataTable('#tblDetalle')) {
        $('#tblDetalle').DataTable().destroy();
        $('#tblDetalle thead').empty();
        $('#tblDetalle tbody').empty();

    }

    var Cabecera = '<tr>' +
        '<th scope="col" >FECHA</th>' +
        '<th scope="col" >REGIMEN</th>' +
        '<th scope="col" >CLIENTE</th>' +
        '<th scope="col" >OT</th>' +
        '<th scope="col" >BL / Booking</th>' +
        '<th scope="col" >CONTENEDOR</th>' +
        '<th scope="col" >TIPO/TAMAÑO</th>' +
        '<th scope="col" >UBICACIÓN</th>' +
        '<th scope="col" >IMO/IQBF</th>' +
        '<th scope="col" >SERVICIO</th>' +
        '<th scope="col" >TIPO SERVICIO</th>' +
        '<th scope="col" >OBSERVACIÓN</th>' +
        '<th scope="col" >CANTIDAD</th>' +
        '<th scope="col" >PESO</th>' +
        '<th scope="col" >ESTADO</th>' +
        '<th scope="col" >Auditoria</th>' +
        '</tr>';
    $('#tblDetalle thead').empty();

    $('#tblDetalle thead').append(Cabecera);

    var table = $('#tblDetalle').DataTable({
        destroy: true,
        //  responsive: true,
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
                "mData": "FPRGOT",
                "bSortable": true,
                "mRender": function (o) {
                    var valor = o.toString();
                    var fechastr = valor.substr(6, 2) + '/' + valor.substr(4, 2) + '/' + valor.substr(0, 4);
                    return fechastr;
                }
            },
            {
                "mData": "CGRONG",
                "bSortable": true,
                "mRender": function (o) {
                    if (o == "51") {
                        return "Importación";
                    } else if (o == "53") {
                        return "Exportación";
                    } else {
                        return "";
                    }

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
                "mData": null,
                "bSortable": true,
                "mRender": function (o) {
                    return o.CPRCN1 + o.NSRCN1;
                }
            },
            {
                "mData": "CLASE",
                "bSortable": true,
                "mRender": function (o) {
                    return o ;
                }
            },
            {
                "mData": "UBICACION",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            {
                "mData": "IMO",
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
                "mData": "MOTIVO",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            {
                "mData": "OBSERVACION",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            {
                "mData": "QSRVC",
                "bSortable": true,
                "mRender": function (o) {
                    return o;
                }
            },
            {
                "mData": "PSRVC",
                "bSortable": true,
                "mRender": function (o) {

                    return darFormatoMoneda(o);
                }
            },
            {
                "mData": "ESTADO",
                "bSortable": true,
                "mRender": function (o) {
                    var estado = "";
                    if (o == "P") {
                        estado = "Pendiente";
                    } else if (o == "L") {
                        estado = "Liquidado";
                    } else if (o == "T") {
                        estado = "Programado";
                    } else if (o == "R") {
                        estado = "Re-programado";
                    } else if (o == "F") {
                        estado = "Facturado";
                    } else if (o == "X") {
                        estado = "Falso Servicio";
                    }
                    return estado;
                }
            },
            {
                "mData": "NORDTR",
                "bSortable": false,
                "mRender": function (o) {
                    
                    var estado = '<a href="#" onclick="LlenarHistorial(' + o + ');" ><i class="fas fa-info-circle"></i></a>';
                   
                    return estado;
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

function LlenarHistorial(id) {
   
    var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

    //Prepare parameters
    var params = {
        OT: id
    };

    function exito(rpta) {

        var tooltipText = "<table style='width: 100%;' ><thead style='background-color: #4CAF50; color: white; '>";
        tooltipText += "<tr><td>Orden Trabajo</td><td>Estado</td><td>Fecha</td><td>Detalle</td><td>Responsable</td></tr></thead>";
        tooltipText += "<tbody>";
        console.log(rpta);
        $.each(rpta, function (k, v) {
            var OBSERV = v.OBSERV.trim();
             var estado = "";
            if (v.SESFAC == "P") {
                    estado = "Pendiente";
            } else if (v.SESFAC == "L") {
                    estado = "Liquidado";
            } else if (v.SESFAC == "T") {
                    estado = "Programado";
            } else if (v.SESFAC == "R") {
                    estado = "Re-programado";
            } else if (v.SESFAC == "F") {
                    estado = "Facturado";
            } else if (v.SESFAC == "X") {
                    estado = "Falso Servicio";
                }

            tooltipText += "<tr><td>" + v.NORDTR + "</td><td>" + estado + "</td><td>" + HelperFN.FormatoFecha( v.FECCRE )+ "</td><td>" + OBSERV + "</td><td>" + v.USUARIO + "</td></tr>";
        })
        tooltipText += "</tbody></table>";
       
        Swal.fire({
            title: 'Historial de Cambios de Orden Trabajo Nº ' + id,
            html: tooltipText,
            width: 800,
            icon: 'info',
            showCancelButton: false,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Cerrar'
        });
    }

    function error(rpta) {

    }

    HelperFN.AjaxJson("POST", "../procesosJO/GetHistorial", params, true, exito, error, antiForgeryToken);

   
}


function darFormatoMoneda(valor) {
    const formato = new Intl.NumberFormat('es-ES', {
        minimumFractionDigits: 2
    }).format(valor);
    return formato;
}

