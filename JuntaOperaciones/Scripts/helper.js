

HelperFN = {

    AjaxJson: function (type, url, parameters, async, exito, error, antiForgeryToken, loading = true, closeloading = true) {
        loading = typeof loading !== 'undefined' ? loading : true;
        closeloading = typeof closeloading !== 'undefined' ? closeloading : true;

        $.ajax({
            type: type,
            url: url,
            headers: { "__RequestVerificationToken": antiForgeryToken },
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: async,
            data: JSON.stringify(parameters),
            success: exito,
            error: error,
            beforeSend: function () {
                if (loading) {

                    $('#divLoading').show();
                }
            }

        }).always(function () {
            if (closeloading) {
                $('#divLoading').css('display', 'none');
            }
        })

    },
    AjaxJsonFile: function (type, url, formData, exito, error, antiForgeryToken, loading = true, closeloading = true) {
        loading = typeof loading !== 'undefined' ? loading : true;
        closeloading = typeof closeloading !== 'undefined' ? closeloading : true;

        $.ajax({
            type: type,
            url: url,
            headers: { "__RequestVerificationToken": antiForgeryToken },
            cache: false,
            dataType: 'json',
            contentType: false,
            processData: false,
            data: formData,
            success: exito,
            error: error,
            beforeSend: function () {
                if (loading) {

                    $('#divLoading').show();
                }
            }


        }).always(function () {
            if (closeloading) {
                $('#divLoading').css('display', 'none');
            }
        })

    },
    Semaforo: function (fechaHora, tipo) {

        var IcoVerde = '<div style="margin:auto;width: 20px;"><img src="./Content/images/verde.png" alt="" height="20" width="20"></div>';//'\ud83d\udfe2';
        var IcoAmarillo = '<div style="margin:auto;width: 20px;"><img src="./Content/images/amarillo.png" alt="" height="20" width="20"></div>';
        var IcoRojo = '<div style="margin:auto;width: 20px;"> <img src="./Content/images/rojo.png" alt="" height="20" width="20"></div>';

        var Ico = IcoVerde;

        if (fechaHora != "") {
            var FieldDatetime = moment(fechaHora, 'DD/MM/YYYY HH:mm:ss', true).format();
            var horas = 0;
            if (tipo == 'I') {
                horas = moment().diff(moment(FieldDatetime), 'hours');
            } else {
                horas = moment(FieldDatetime).diff(moment(), 'hours')
            }

            if (horas >= 24) {
                Ico = IcoVerde;
            } else if (horas >= 12) {
                Ico = IcoAmarillo;
            } else {
                Ico = IcoRojo;
            }
        }

        return Ico;
    },
    stickyShow: function (mensaje, tipo, sticky = false) {
        sticky = typeof sticky !== 'undefined' ? sticky : false;

        $().toastmessage('showToast', {
            text: mensaje,
            sticky: sticky,
            type: tipo,
            nEffectDuration: 600,   // in effect duration in miliseconds
            stayTime: 3000,   // time in miliseconds before the item has to disappear
        });
    },
    PreguntaBorrarShow: function () {
        var result = false;
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

                result = true;
            }
        })

        return result;
    },
    PreguntaShow: function (pregunta, informacion) {
        var result = false;
        return Swal.fire({
            title: pregunta,
            text: informacion,
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si'
        })
    },
    FormatoFecha: function (valor) {
        var result = '';
        var dia, mes, anio;
        dia = valor.toString().substr(6, 2);
        mes = valor.toString().substr(4, 2);
        anio = valor.toString().substr(0, 4);


        result = dia + '/' + mes + '/' + anio;
        return result;
    },
    FormatoHora: function (valor) {
        var result = '';
        var ValorCeros = valor.toString();
        var HORA, MIN, SEG;
        if (ValorCeros.length < 6) {
            for (var i = ValorCeros.length; i < 6; i++) {
                ValorCeros = '0' + ValorCeros.toString();
            }
        }

        HORA = ValorCeros.toString().substr(0, 2);
        MIN = ValorCeros.toString().substr(2, 2);
        SEG = ValorCeros.toString().substr(4, 4);
        result = HORA + ':' + MIN + ':' + SEG;
        return result;
    },
    CargarCombo: function (comboName, params, url, campoValue, campoText, CampoSelect = "", textoInicial = "", valorInical = "") {

        var antiForgeryToken = $("input[name='__RequestVerificationToken']").val();

        function exito(rpta) {

            var datos = rpta;
            $(comboName).dropdown().destroy();
            $(comboName).empty();
            if (textoInicial != "") $(comboName).append('<option value="' + valorInical + '">' + textoInicial + '</option>')
            if (rpta.length == 0) $(comboName).append('<option value="0">Sin Asignar</option>')

            $.each(datos, function (key, value) {

                var valor = value[campoValue];
                var texto = value[campoText];
                if (valor == CampoSelect) {
                    $(comboName).append('<option value="' + valor + '" Selected >' + texto + '</option>')
                } else {
                    $(comboName).append('<option value="' + valor + '">' + texto + '</option>')
                }

            });
            //CampoSelect
            if (CampoSelect != "") {
                $(comboName).val(CampoSelect).trigger('change');
            }
            $(comboName).dropdown();


        }
        function error(rpta) {
            console.error(rpta)
        }
        HelperFN.AjaxJson("POST", url, params, true, exito, error, antiForgeryToken);
    },
    CargarComboList: function (comboName, datos, campoValue, campoText, CampoSelect = "",textoInicial = "") {

        //var datos = rpta;
        $(comboName).dropdown().destroy();
        $(comboName).empty();
        if (textoInicial == "") {
            $(comboName).append('<option value="" >Seleccione un Servicio</option>')
        } else {
            $(comboName).append('<option value="" >'+textoInicial+'</option>')
        }
        
        $.each(datos, function (key, value) {
            var valor = value[campoValue];
            var texto = value[campoText];
            if (valor == CampoSelect) {
                $(comboName).append('<option value="' + valor + '" Selected >' + texto + '</option>')
            } else {
                $(comboName).append('<option value="' + valor + '">' + texto + '</option>')
            }

        });
        //CampoSelect
        if (CampoSelect != "") {
            $(comboName).val(CampoSelect).trigger('change');
        }
        $(comboName).dropdown();

    }

}



