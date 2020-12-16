$(document).ready(function () {

    CargarUsuarios();
    CargarModulos();
    $(document).ready(function () {

        $("#divLoading").hide();
    });

    $('#usuarioModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('whatever') // Extract info from data-* attributes

        if (recipient == "Nuevo") {
            $('#ID').val(0);
            $('#ESTADO').val('A');
            $('#USUARIO').val('');
            $("#AUTORIZ option[value='0']").attr("selected", true);
            $('#ACCION').val('I');
            $('#usuarioModalLabel').text('Nuevo Usuario');

        } else if (recipient == "Editar") {
            var ID = button.data('id')
            var Parametros = {
                id: ID
            }

            $.ajax({
                data: Parametros,
                type: "POST",
                url: './Seguridad/UsuarioGetbyID',
                success: function (respuesta) {

                    $('#ID').val(respuesta[0].IDUSER);
                    $('#ESTADO').val(respuesta[0].SESTRG);
                    $('#USUARIO').val(respuesta[0].USERNM);
                    $('#usuarioModalLabel').text('Editar Usuario');
                    var ValorSel = "ADM";

                    if (respuesta[0].NVLACC == "SUPERVISOR") {
                        ValorSel = "SUP";
                    } else if (respuesta[0].NVLACC == "ADMINISTRADOR") {
                        ValorSel = "ADM";
                    } else if (respuesta[0].NVLACC == "USUARIO 1") {
                        ValorSel = "US1";
                    }

                    $("#NIVEL option[value='" + ValorSel + "']").attr("selected", true);
                    $("#AUTORIZ option[value='" + respuesta[0].AUTORIZ + "']").attr("selected", true);
                    $('#ACCION').val('U');
                },
                error: function () {
                    console.log("No se ha podido obtener la información");
                }
            });
        }
    })
    $("#btnGuardarUsuario").on("click", function () {
        var Parametros = {
            USUARIO: $('#USUARIO').val(),
            NIVEL: $('#NIVEL').val(),
            ID: $('#ID').val(),
            AUTORIZ: $('#AUTORIZ').val(),
            ESTADO: $('#ESTADO').val(),
            ACCION: $('#ACCION').val()
        }
        $.ajax({
            data: Parametros,
            type: "POST",
            url: './Seguridad/AccionesUsuario',
            success: function (respuesta) {
                CargarUsuarios();
                if ($('#ID').val() == 0) {
                    HelperFN.stickyShow('Registro añadido con éxito.', 'success');
                }
                else {
                    HelperFN.stickyShow('Registro Actualizado con éxito.', 'success');
                }
                $('#usuarioModal').modal("hide");
            },
            error: function () {
                console.log("No se ha podido obtener la información");
            }
        });
    });

    //MODULOS

    $('#moduloModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('whatever') // Extract info from data-* attributes

        if (recipient == "NUEVO") {
            $('#ID').val(0);
            $('#ESTADO').val('A');
            $('#MODULO').val('');
            $('#ACCION').val('I');
            $('#moduloModalLabel').text('Nuevo Módulo');

        } else if (recipient == "Editar") {
            var ID = button.data('id')
            var Parametros = {
                id: ID
            }

            $.ajax({
                data: Parametros,
                type: "POST",
                url: './Seguridad/ModuloGetbyID',
                success: function (respuesta) {

                    $('#IDMod').val(respuesta[0].IDMDLO);
                    $('#ESTADOMod').val(respuesta[0].SESTRG);
                    $('#MODULO').val(respuesta[0].NMMDLO);
                    $('#MENU').val(respuesta[0].NMMENU);
                    $('#VISTA').val(respuesta[0].PPVISTA);
                    $('#CONTROLADOR').val(respuesta[0].PPCNTRL);
                    $('#moduloModalLabel').text('Editar Módulo');
                    $('#ACCIONMod').val('U');
                },
                error: function () {
                    console.log("No se ha podido obtener la información");
                }
            });
        }
    })

    $("#btnGuardarModulo").on("click", function () {
        var Parametros = {
            ID: $('#IDMod').val(),
            MODULO: $('#MODULO').val(),
            MENU: $('#MENU').val(),
            VISTA: $('#VISTA').val(),
            CONTROLADOR: $('#CONTROLADOR').val(),
            ESTADO: $('#ESTADOMod').val(),
            ACCION: $('#ACCIONMod').val()
        }
        $.ajax({
            data: Parametros,
            type: "POST",
            url: './Seguridad/AccionesModulo',
            success: function (respuesta) {

                CargarModulos();
                $('#moduloModal').modal("hide");
            },
            error: function () {
                console.log("No se ha podido obtener la información");
            }
        });
    });
    // Accesos

    $('#detalleModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('whatever') // Extract info from data-* attributes

        if (recipient == "Detalle") {
            var ID = button.data('id')
            var Parametros = {
                id: ID
            }

            $.ajax({
                data: Parametros,
                type: "POST",
                url: './Seguridad/AccesosGetbyUser',
                success: function (respuesta) {

                    $('#IDDetalle').val(ID);

                    dataAccesos = respuesta;

                    $('#tblAccesos').DataTable({
                        destroy: true,
                        responsive: true,
                        data: dataAccesos,
                        columns: [
                            { data: 'NMMDLO' },
                            { data: 'PPINSERT' },
                            { data: 'PPUPDATE' },
                            { data: 'PPDELETE' },
                            { data: 'PPEXPORT' },
                            { data: 'PPPRINT' },
                            {
                                "mData": null,
                                "bSortable": false,
                                "mRender": function (o) {
                                    return '<a href=#>' + '<i class="fas fa-edit" data-toggle="modal" data-target="#AccesoModal" data-whatever="Editar" data-id="' + o.IDMDLO + '"></i>' + '</a>' +
                                        '<a href=javascript:deleteAcceso(' + o.IDMDLO + ');>' + '<i class="fas fa-trash-alt" style="color:red;"></i>' + '</a>';
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
                                "first": "Primero",
                                "last": "Ultimo",
                                "next": "Siguiente",
                                "previous": "Anterior"
                            }
                        },
                    });
                },
                error: function () {
                    console.log("No se ha podido obtener la información");
                }
            });
        }
    })

    $('#AccesoModal').on('show.bs.modal', function (event) {

        var button = $(event.relatedTarget) // Button that triggered the modal
        var recipient = button.data('whatever') // Extract info from data-* attributes


        if (recipient == "Nuevo") {
            $('#IDUSER').val($('#IDDetalle').val());
            $('#ACCIONAcce').val('I');
            $('#ESTADOAcce').val('A');

            var ID = $('#IDDetalle').val();

            var Parametros = {
                IDUSER: ID,
                IDMODULO: 0,
                ACCION: 'A'
            }


            $('#IDMODULO').empty();

            $.ajax({
                data: Parametros,
                type: "POST",
                url: './Seguridad/ModulosGetNotUser',
                success: function (respuesta) {


                    respuesta.forEach(function (modulo, index) {
                        $('#IDMODULO').append('<option value = "' + modulo.IDMDLO + '">' + modulo.NMMDLO + '</option >');
                    });
                },
                error: function () {
                    console.log("No se ha podido obtener la información");
                }
            });
        } else if (recipient == "Editar") {
            $('#IDUSER').val($('#IDDetalle').val())
            var ID = $('#IDDetalle').val();

            var IDMDLO = button.data('id');
            var Parametros = {
                IDUSER: ID,
                IDMDLO: IDMDLO
            }


            $('#IDMODULO').empty();

            $.ajax({
                data: Parametros,
                type: "POST",
                url: './Seguridad/AccesosGetbyUserModulo',
                success: function (respuesta) {

                    respuesta.forEach(function (modulo, index) {
                        $('#IDMODULO').append('<option value = "' + modulo.IDMDLO + '">' + modulo.NMMDLO + '</option >');
                    });
                    $('#ACCIONAcce').val('U');
                    $('#ESTADOAcce').val(respuesta[0].SESTRG);

                    $('#IDUSER').val(respuesta[0].IDUSER);
                    var valInsert = respuesta[0].PPINSERT == "NO" ? 0 : 1;
                    $("#INSERT option[value='" + valInsert + "']").attr("selected", true);
                    var valUpdate = respuesta[0].PPUPDATE == "NO" ? 0 : 1;
                    $("#UPDATE option[value='" + valUpdate + "']").attr("selected", true);
                    var valDelete = respuesta[0].PPDELETE == "NO" ? 0 : 1;
                    $("#DELETE option[value='" + valDelete + "']").attr("selected", true);
                    var valExport = respuesta[0].PPEXPORT == "NO" ? 0 : 1;
                    $("#EXPORT option[value='" + valExport + "']").attr("selected", true);
                    var valPrint = respuesta[0].PPPRINT == "NO" ? 0 : 1;
                    $("#PRINT option[value='" + valPrint + "']").attr("selected", true);


                },
                error: function () {
                    console.log("No se ha podido obtener la información");
                }
            });

        }



    })

    $('#GuardarAcceso').on("click", function () {

        var Parametros = {
            IDUSER: $('#IDUSER').val(),
            IDMODULO: $('#IDMODULO').val(),
            INSERT: $('#INSERT').val(),
            UPDATE: $('#UPDATE').val(),
            DELETE: $('#DELETE').val(),
            EXPORT: $('#EXPORT').val(),
            PRINT: $('#PRINT').val(),
            ESTADO: $('#ESTADOAcce').val(),
            ACCION: $('#ACCIONAcce').val()
        }


        $.ajax({
            data: Parametros,
            type: "POST",
            url: './Seguridad/AccionesAccesos',
            success: function (respuesta) {

                if ($.fn.DataTable.isDataTable('#tblAccesos')) {
                    $('#tblAccesos').DataTable().destroy();
                }

                dataAccesos = respuesta;
                console.log(dataAccesos);

                $('#tblAccesos').DataTable({
                    destroy: true,
                    responsive: true,
                    data: dataAccesos,
                    columns: [
                        { data: 'NMMDLO' },
                        { data: 'PPINSERT' },
                        { data: 'PPUPDATE' },
                        { data: 'PPDELETE' },
                        { data: 'PPEXPORT' },
                        { data: 'PPPRINT' },
                        {
                            "mData": null,
                            "bSortable": false,
                            "mRender": function (o) {
                                return '<a href=#>' + '<i class="fas fa-edit" data-toggle="modal" data-target="#AccesoModal" data-whatever="Editar" data-id="' + o.IDMDLO + '"></i>' + '</a>' +
                                    '<a href=javascript:deleteAcceso(' + o.IDMDLO + ');>' + '<i class="fas fa-trash-alt" style="color:red;"></i>' + '</a>';
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
                            "first": "Primero",
                            "last": "Ultimo",
                            "next": "Siguiente",
                            "previous": "Anterior"
                        }
                    },
                });
                HelperFN.stickyShow('Se ha registrado con éxito.', 'success');

                $('#AccesoModal').modal("hide");
            },
            error: function () {

                console.log("No se ha podido obtener la información");
            }
        });
    });

    $('#INSERT').on("change", function () {

        $("#INSERT option").attr("selected", false);

    });

    $('#INSERT').on("change", function () {

        $("#INSERT option").attr("selected", false);

    });

    $('#UPDATE').on("change", function () {

        $("#UPDATE option").attr("selected", false);

    });

    $('#DELETE').on("change", function () {

        $("#DELETE option").attr("selected", false);

    });

    $('#EXPORT').on("change", function () {

        $("#EXPORT option").attr("selected", false);

    });

    $('#PRINT').on("change", function () {

        $("#PRINT option").attr("selected", false);

    });


});



function deleteUser(ID) {
    var Parametros = {
        id: ID,
        accion: 'D',
        usuario: '',
        nivel: '',
        estado: ''
    }
    Swal.fire({
        title: '¿Estas seguro de Eliminar el Registro?',
        text: "Luego no podrá recuperar esta información.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Borralo!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                data: Parametros,
                type: "POST",
                url: './Seguridad/DeleteUsuario',
                success: function (respuesta) {
                    HelperFN.stickyShow('Registro eliminado.', 'success');
                    CargarUsuarios();
                },
                error: function () {
                    HelperFN.stickyShow('No se ha podido eliminar el registro.', 'error');
                    console.error("error al tratar de el registro.");
                }
            });
        } else{
            HelperFN.stickyShow('Acción cancelada por el usuario.', 'notice');
        }
    })


    
}

function deleteModulo(ID) {
    var Parametros = {
        id: ID,
        accion: 'D',
        modulo: '',
        estado: ''
    }

    $.ajax({
        data: Parametros,
        type: "POST",
        url: './Seguridad/DeleteModulo',
        success: function (respuesta) {
            CargarModulos();
        },
        error: function () {
            console.log("No se ha podido obtener la información");
        }
    });
}

function deleteAcceso(_IDMODU) {

    var _IDUSUARIO = $('#IDDetalle').val();

    var Parametros = {
        IDUSER: _IDUSUARIO,
        IDMODULO: _IDMODU,
        INSERT: 0,
        UPDATE: 0,
        DELETE: 0,
        EXPORT: 0,
        PRINT: 0,
        ESTADO: "",
        ACCION: "D"
    }

    $.ajax({
        data: Parametros,
        type: "POST",
        url: './Seguridad/DeleteAcceso',
        success: function (respuesta) {

            dataAccesos = respuesta;
            $('#tblAccesos').DataTable({
                destroy: true,
                responsive: true,
                data: dataAccesos,
                columns: [
                    { data: 'NMMDLO' },
                    { data: 'PPINSERT' },
                    { data: 'PPUPDATE' },
                    { data: 'PPDELETE' },
                    { data: 'PPEXPORT' },
                    { data: 'PPPRINT' },
                    {
                        "mData": null,
                        "bSortable": false,
                        "mRender": function (o) {
                            return '<a href=#>' + '<i class="fas fa-edit" data-toggle="modal" data-target="#AccesoModal" data-whatever="Editar" data-id="' + o.IDMDLO + '"></i>' + '</a>' +
                                '<a href=javascript:deleteAcceso(' + o.IDMDLO + ');>' + '<i class="fas fa-trash-alt" style="color:red;"></i>' + '</a>';
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
                        "first": "Primero",
                        "last": "Ultimo",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    }
                },
            });
        },
        error: function () {
            console.log("No se ha podido obtener la información");
        }
    });


}

function CargarUsuarios() {

    var Url = "./Seguridad/GetUsuarios";
    var table = $('#tblUsuarios').DataTable({
        destroy: true,
        responsive: true,
        ajax:
        {
            method: "POST",
            url: Url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            dataSrc: ""
        },
        columns: [
            { data: 'USERNM' },
            { data: 'NVLACC' },
            { data: 'SESTRG' },
            {
                "mData": 'AUTORIZ',
                "bSortable": false,
                "mRender": function (o) {
                    console.log(o);
                    var aut = "Ninguno";
                    if (o == 1) {
                        aut = "Transporte";
                    } else if (o == 2) {
                        aut = "Maquinaria";
                    } else if (o == 3) {
                        aut = "Operaciones";
                    } else if (o == 4) {
                        aut = "Servicios";
                    }


                    return aut;
                }
            },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (o) {
                    return '<a href=#>' + '<i class="fas fa-edit" data-toggle="modal" data-target="#usuarioModal" data-whatever="Editar" data-id="' + o.IDUSER + '"></i>' + '</a>' +
                        '<a href=javascript:deleteUser(' + o.IDUSER + ');>' + '<i class="fas fa-trash-alt" style="color:red;"></i>' + '</a>' +
                        '<a href=#>' + '<i class="fas fa-key" style="color:#fcbe03;" data-toggle="modal" data-target="#detalleModal" data-whatever="Detalle" data-id="' + o.IDUSER + '"></i>' + '</a>';
                }
            },
            //},
            //{
            //    "mData": null,
            //    "bSortable": false,
            //    "mRender": function (o) { return '<a href=javascript:deleteUser(' + o.IDUSER + ');>' + '<i class="fas fa-trash-alt" style="color:red;"></i>' + '</a>'; }
            //},
            //{
            //    "mData": null,
            //    "bSortable": false,
            //    "mRender": function (o) { return '<a href=#>' + '<i class="fas fa-key" style="color:#fcbe03;" data-toggle="modal" data-target="#detalleModal" data-whatever="Detalle" data-id="' + o.IDUSER + '"></i>' + '</a>'; }
            //}
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

function CargarModulos() {

    var Url = "./Seguridad/GetModulos";
    var table = $('#tblModulos').DataTable({
        destroy: true,
        responsive: true,
        ajax:
        {
            method: "POST",
            url: Url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            dataSrc: ""
        },
        columns: [
            { data: 'NMMDLO' },
            { data: 'NMMENU' },
            { data: 'PPVISTA' },
            { data: 'PPCNTRL' },
            {
                "mData": null,
                "bSortable": false,
                "mRender": function (o) {
                    return '<a href=#>' + '<i class="fas fa-edit" data-toggle="modal" data-target="#moduloModal" data-whatever="Editar" data-id="' + o.IDMDLO + '"></i>' + '</a>' +
                        '<a href=javascript:deleteModulo(' + o.IDMDLO + ');>' + '<i class="fas fa-trash-alt" style="color:red;"></i>' + '</a>';
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
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
    });

}