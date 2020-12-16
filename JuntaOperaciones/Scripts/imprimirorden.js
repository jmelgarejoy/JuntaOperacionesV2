
var Lista = [];
//var key = "";

$(document).ready(function () {
    var ot = getParameterByName('ot');
    var key = getParameterByName('k');
    BuscarOrdenTrabajo(ot, key);
});

function BuscarOrdenTrabajo(strDocumento, key) {
    var antiForgeryToken = key;

    //Prepare parameters
    var params = {
        tipo: 3,
        documento: strDocumento
    };


    function exito(rpta) {
        Lista = rpta;

        if (rpta.length > 0) {
            console.log(Lista);
            $('#agente').text('Cliente / Client: ' + Lista[0].AGENTE);
            $('#codigoagente').text('Codigo / Code: ' + Lista[0].CODAGEN);
            $('#otrabajo').text(Lista[0].OTRABAJO);
            $('#recalada').text(Lista[0].RECALADA);
            $('#nave').text(Lista[0].NAVE);
            $('#nvjes').text(Lista[0].NVJES);
            $('#codagentecab').text(Lista[0].CODAGEN);
            $('#agentecab').text(Lista[0].AGENTE);
            $('#coddespachador').text(Lista[0].CODDESP);
            $('#regimen').text(Lista[0].REGIMEN);
            $('#despachador').text(Lista[0].DESPACHADOR);
            $('#documento').text(Lista[0].DOCUMENTO);
            $('#Detalle').empty();
            var lineaCab = '<div class="row" style="border-bottom:1px solid DARKGREEN">' +
                '    <div class="col-2">ITEM</div>' +
                '    <div class="col-3">CONTENEDOR</div>' +
                '    <div class="col-4">SERVICIO</div>' +
                '    <div class="col-3">CANTIDAD SOLICITADA</div>' +
                '</div>';
            $('#Detalle').append(lineaCab);
            $.each(Lista, function(k, v){
                var linea = '<div class="row">' +
                    '    <div class="col-2">' + v.ITEM + '</div>' +
                    '    <div class="col-3">' + v.CONTENEDOR + '</div>' +
                    '    <div class="col-4">' + v.SERVICIO + '</div>' +
                    '    <div class="col-3">' + v.CANTSOLIC + '</div>' +
                    '</div>';
                $('#Detalle').append(linea);
            })

        }

    }
    function error(rpta) {
        console.log(rpta);
    }

    HelperFN.AjaxJson("POST", "Procesos/GetOrdenTrabajo", params, true, exito, error, antiForgeryToken);

}


function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}