/// <reference path="../../Planificacion/pgGestionPlanCompra.html" />
var _Id;
var _RazonSocial;
var _Consulta;
var _IdPadre;

var _CampoID;
var _CampoNombre;

function showSuccess(message) {
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            positionClass: 'toast-top-center',
            timeOut: 4000
        };
        toastr.success(message, 'PETCenter');

    }, 300);
}

function showError(message) {
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            positionClass: 'toast-top-center',
            timeOut: 4000
        };
        toastr.error(message, 'PETCenter');

    }, 300);
}

function showGeneralModal(modalId) {
    $('#' + modalId).modal('show');
}


function showModalPlanCompras() {
    $('#' + modalId).modal('hide');
}

function SearchEntidad(tipo, IdControl, RazonSocialControl, IdPadre, inload) {
    _Id = IdControl;
    _RazonSocial = RazonSocialControl;
    _IdPadre = IdPadre;
    _CampoID = "ID";
    Showmodal(true);
    $('#inPopupParamFiltro').empty();
    $('#inPopupParamFiltro').append("<span class=\"input-group-addon input-sm\" id=\"inDatosGuia\">Datos de<br />Busqueda</span>");
    $('#inPopup').modal('show');

    switch (tipo) {
        case "LIN":
            $('#inTitlePopup').html("BUSQUEDA DE LINEAS AEREAS");
            _CampoNombre = "Razón Social";
            _Consulta = "GetListLineaAerea";
            break;
        case "REC":
            $('#inTitlePopup').html("BUSQUEDA DE AGENTES DE CARGA");
            _CampoNombre = "Razón Social";
            _Consulta = "GetListAgenteCarga";
            break;
        case "CON":
            $('#inTitlePopup').html("BUSQUEDA DE CONSIGNATARIOS");
            _CampoNombre = "Razón Social";
            _Consulta = "GetListConsignatario";
            break;
        case "TIPOAVISO":
            $('#inTitlePopup').html("BUSQUEDA DE TIPOS DE AVISO");
            _CampoNombre = "Descripción";
            _Consulta = "GetListTipoNotificacion";
            break;
        case "TIPOINDICACION":
            $('#inTitlePopup').html("BUSQUEDA DE TIPOS DE INDICACIÓN");
            _CampoNombre = "Descripción";
            _Consulta = "GetListIndicacionxTipo";
            break;
        case "AREA":
            $('#inTitlePopup').html("BUSQUEDA DE AREAS");
            _CampoNombre = "Descripción";
            _Consulta = "GetListArea";
            break;
        case "ESTADONOTIFICA":
            $('#inTitlePopup').html("BUSQUEDA DE ESTADOS DE NOTIFICACIONES");
            _CampoNombre = "Descripción";
            _Consulta = "GetListEstadoNotificacion";
            break;
    }
    $('#inPopupParamFiltro').append("<input type=\"text\" id=\"inpopupid\" class=\"form-control input-sm pad-control text-uppercase focusedInput\" placeholder=\"" + _CampoID + "\" autofocus onkeypress=\"GetGridListEntity('inpopupid', 'inpopupnombre', event);\" />");
    $('#inPopupParamFiltro').append("<input type=\"text\" id=\"inpopupnombre\" class=\"form-control input-sm pad-control text-uppercase\" placeholder=\"" + _CampoNombre + "\" required onkeypress=\"GetGridListEntity('inpopupid', 'inpopupnombre', event);\" />");
    $('#inpopupid').focus();
    $('#grid-view').empty();
    $('#grid-view').append("<div id=\"grid\" class=\"label-sm grid-setcursor\"></div>");

    if (inload) {
        var parametros = null;
        if (_IdPadre == null) {
            parametros = JSON.stringify({ "id": "", "nombre": "" });
        }
        else {
            parametros = JSON.stringify({ "idpadre": $("#" + _IdPadre).val(), "id": "", "nombre": "" });
        }

        $('#grid').empty();
        setTimeout(
            $("#grid").kendoGrid({
                dataSource: {
                    transport: {
                        read: {
                            type: "POST",
                            url: wsnode + "wsGenerico.svc/" + _Consulta,
                            contentType: "application/json; charset=utf-8",
                            dataType: 'json'
                        },
                        parameterMap: function (options, operation) {
                            return parametros;
                        },
                        pageSize: 10
                    }
                },
                rowTemplate: "<tr><td  onclick=\"GetIdSelection('#: CO_ENTITY #','#: DE_ENTITY #');return false;\">#: CO_ENTITY #</td><td onclick=\"GetIdSelection('#: CO_ENTITY #','#: DE_ENTITY #');return false;\">#: DE_ENTITY #</td></tr>",
                altRowTemplate: "<tr class=\"k-alt\"><td  onclick=\"GetIdSelection('#: CO_ENTITY #','#: DE_ENTITY #');return false;\">#: CO_ENTITY #</td><td onclick=\"GetIdSelection('#: CO_ENTITY #','#: DE_ENTITY #');return false;\">#: DE_ENTITY #</td></tr>",
                pageable: {
                    refresh: true,
                    pageSize: 10,
                    pageSizes: [5, 10, 20]
                },
                selectable: "row",
                navigatable: false,
                height: 350,
                columns: [
                    {
                        field: "CO_ENTITY",
                        title: _CampoID,
                        width: "30px"
                    },
                    {
                        field: "DE_ENTITY",
                        title: _CampoNombre,
                        width: "120px"
                    }
                ]
            }), 2000);
        $("#grid").height = 350;
    }
}

function GetIdSelection(Id, RazónSocial) {
    $("#" + _Id).val(Id);
    $("#" + _RazonSocial).val(RazónSocial);
    $('#grid-view').empty();
    $('#inPopup').modal('hide');
    _Id = "";
    _RazonSocial = null;
    _CampoID = null;
    _CampoNombre = null;
    _Consulta = null;
    _IdPadre = null;
}

$(document).on('shown.bs.modal', function (e) {
    $('[autofocus]', e.target).focus();
});

function GetGridListEntity(id, nombre, e) {
    if (e.which == 13) {

        var parametros = null;
        if (_IdPadre == "") {
            parametros = JSON.stringify({ "id": $("#" + id).val(), "nombre": $("#" + nombre).val() });
        }
        else {
            parametros = JSON.stringify({ "idpadre": $("#" + _IdPadre).val(), "id": $("#" + id).val(), "nombre": $("#" + nombre).val() });
        }

        $('#grid').empty();
        $("#grid").kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        type: "POST",
                        url: wsnode + "wsGenerico.svc/" + _Consulta,
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json'
                    },
                    parameterMap: function (options, operation) {
                        return parametros;
                    },
                    pageSize: 10
                }
            },
            rowTemplate: "<tr><td  onclick=\"GetIdSelection('#: CO_ENTITY #','#: DE_ENTITY #');return false;\">#: CO_ENTITY #</td><td onclick=\"GetIdSelection('#: CO_ENTITY #','#: DE_ENTITY #');return false;\">#: DE_ENTITY #</td></tr>",
            altRowTemplate: "<tr class=\"k-alt\"><td  onclick=\"GetIdSelection('#: CO_ENTITY #','#: DE_ENTITY #');return false;\">#: CO_ENTITY #</td><td onclick=\"GetIdSelection('#: CO_ENTITY #','#: DE_ENTITY #');return false;\">#: DE_ENTITY #</td></tr>",
            pageable: {
                refresh: true,
                pageSize: 10,
                pageSizes: [5, 10, 20]
            },
            selectable: "row",
            navigatable: false,
            height: 350,
            columns: [
                {
                    field: "CO_ENTITY",
                    title: _CampoID,
                    width: "30px"
                },
                {
                    field: "DE_ENTITY",
                    title: _CampoNombre,
                    width: "120px"
                }
            ]
        });
    }
}


// PARA PRUEBAS
$(document).ready(function () {
    //$('#pages').load('Planificacion/pgGenerarOrdenCompra.html');

});

