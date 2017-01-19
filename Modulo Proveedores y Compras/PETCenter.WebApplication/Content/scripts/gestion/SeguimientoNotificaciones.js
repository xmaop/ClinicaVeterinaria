var isMiddle;
function ShowDiv_(content, hidden, angle) {
    CleanControl();
    if ($("#" + hidden).val() == "off") {
        $("#" + content).fadeIn("slow");
        $("#" + hidden).val("on");
        $("#" + angle).removeClass("fa fa-plus-square");
        $("#" + angle).addClass("fa fa-minus-square");
    }
    else {
        $("#" + content).fadeOut("slow");
        $("#" + hidden).val("off");
        $("#" + angle).removeClass("fa fa-minus-square");
        $("#" + angle).addClass("fa fa-plus-square");
        $("#inIdEstado").val("1");
        SearchEntidad_Id("ESTADONOTIFICA", "inIdEstado", "inDescripcionEstado", null, null);
    }

}

function CleanControl() {
    $("#inIdAerolinea").val("");
    $("#inNombreAerolinea").val("");
    $("#inGuiaMadre").val("");
    $("#inGuiaHija").val("");
    $("#inAnioManifiesto").val("2016");
    $("#inNroManifiesto").val("");
    $("#inVuelo").val("");
    $("#inFecha").val("");
    $("#inIdAgenteCarga").val("");
    $("#inNombreAgenteCarga").val("");
    $("#inIdConsignatario").val("");
    $("#inNombreConsignatario").val("");

    $("#inIdTipoAviso").val("");
    $("#inDescripcionTipoAviso").val("");
    $("#inIdTipoIndicación").val("");
    $("#inDescripcionTipoIndicación").val("");
    $("#inIdArea").val("");
    $("#inDescripcionArea").val("");
    $("#inIdEstado").val("");
    $("#inDescripcionEstado").val("");
}

function searchEntity(evt, tipo, IdControl, NombreControl, IdPadre, Controlevent) {
    var tecla = charCode = (evt.which) ? evt.which : evt.keyCode;
    var texto = $("#" + IdControl).val();
    if (texto == null || texto.length == 0 || /^\s+$/.test(texto)) {
        $("#" + NombreControl).val("");
        return;
    }
    if (tecla == 13) {
        SearchEntidad_Id(tipo, IdControl, NombreControl, IdPadre, Controlevent);
    } else {
        return;
    }
}

function SearchEntidad_Id(tipo, IdControl, NombreControl, IdPadre, Controlevent) {

    $('#processingModal').modal('show');
    //console.log("inicio1");
    var parametros = null;
    var Consulta = null;
    if (IdPadre == null) {
        parametros = JSON.stringify({ "id": $("#" + IdControl).val(), "nombre": "" });
    }
    else {
        parametros = JSON.stringify({ "idpadre": $("#" + IdPadre).val(), "id": $("#" + IdControl).val(), "nombre": "" });
    }

    if ((Controlevent == null || Controlevent.which == 13) && $("#" + IdControl).val() == "") {
        $("#" + IdControl).val("");
        $("#" + NombreControl).val("");
    }

    if ((Controlevent == null || Controlevent.which == 13) && $("#" + IdControl).val() != "") {
        switch (tipo) {
            case "LIN":
                Consulta = "GetListLineaAerea";
                break;
            case "REC":
                Consulta = "GetListAgenteCarga";
                break;
            case "CON":
                Consulta = "GetListConsignatario";
                break;
            case "TIPOAVISO":
                Consulta = "GetListTipoNotificacion";
                break;
            case "TIPOINDICACION":
                Consulta = "GetListIndicacionxTipo";
                break;
            case "AREA":
                Consulta = "GetListArea";
                break;
            case "ESTADONOTIFICA":
                Consulta = "GetListEstadoNotificacion";
                break;
        }
        //console.log("inicio2");
        $.ajax({
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            url: wsnode + "wsGenerico.svc/" + Consulta,
            dataType: "json",
            data: parametros,
            async: false,
            processData: false,
            cache: false,
            success: function (data) {
                $("#" + IdControl).val("");
                $("#" + NombreControl).val("");
                var Entity = data[0];
                $("#" + IdControl).val(Entity.CO_ENTITY);
                $("#" + NombreControl).val(Entity.DE_ENTITY);
                //console.log("success");
                $('#processingModal').modal('hide');
            },
            error: function (response) {
                $("#" + IdControl).val("");
                $("#" + NombreControl).val("");
                $('#processingModal').modal('hide');
                //console.log("error");
            }
        });
        $('#processingModal').modal('hide');
    }
}

function SetValidationFields()
{
    var result = true;
    if (
        ($("#inIdAerolinea").val() != "" && $("#inFecha").val() == "")||
        ($("#inIdAgenteCarga").val() != "" && $("#inFecha").val() == "") ||
        ($("#inIdConsignatario").val() != "" && $("#inFecha").val() == "") || 
        ($("#inVuelo").val() != "" && $("#inFecha").val() == "")
        )
    {
        $("#inFecha").focus();
        showWarningMessage("Ud. debe seleccionar la fecha de llegada de vuelo");
        result = false;
    }
    else if(
            ($("#inIdAerolinea").val() == "" && $("#inFecha").val() != "") &&
            ($("#inIdAgenteCarga").val() == "" && $("#inFecha").val() != "") &&
            ($("#inIdConsignatario").val() == "" && $("#inFecha").val() != "") &&
            ($("#inVuelo").val() == "" && $("#inFecha").val() != "")
        ) {
        showWarningMessage("Ud. debe indicar el codigo de una entidad");
        result = false;
    }
    else if (
    ($("#inVuelo").val() != "" && $("#inIdAerolinea").val() == "")
    ) {
        $("#inIdAerolinea").focus();
        showWarningMessage("Ud. debe ingresar una linea aerea");
        result = false;
    }
    else if (
            $("#inGuiaMadre").val() == "" &&
            $("#inGuiaHija").val() == "" &&
            $("#inNroManifiesto").val() == "" &&
            $("#inIdAerolinea").val() == "" &&
            $("#inVuelo").val() == "" &&
            $("#inFecha").val() == "" &&
            $("#inIdAgenteCarga").val() == "" &&
            $("#inIdConsignatario").val() == ""
        )
    {
        $("#inGuiaMadre").focus();
        showWarningMessage("Ud. no ha ingresado parametros de busqueda");
        result = false;
    } 
    return result;
}

function GetdataNotifications() {

    //$('#processingModal').modal('show');

    var busquedaavanzada = $("#hdBusquedaAvanzada").val();
    var parametros;
    var Consulta;
    if (busquedaavanzada == "on") {
        if (!SetValidationFields())
            return false;
        parametros = JSON.stringify({
            "guiamadre": $("#inGuiaMadre").val(),
            "guiahija": $("#inGuiaHija").val(),
            "aniomanifiesto": $("#inAnioManifiesto").val(),
            "numeromanifiesto": $("#inNroManifiesto").val(),
            "lineaaerea": $("#inIdAerolinea").val(),
            "vuelo": $("#inVuelo").val(),
            "fecha": $("#inFecha").val(),
            "agentecarga": $("#inIdAgenteCarga").val(),
            "importador": $("#inIdConsignatario").val()
        });
        Consulta = "GetListNotificaciones_Avanzada";
    }
    else
    {
        parametros = JSON.stringify({
            "idtiponotificacion": parseInt($("#inIdTipoAviso").val()) || 0,
            "idindicacion": parseInt($("#inIdTipoIndicación").val()) || 0,
            "idarea": parseInt($("#inIdArea").val()) || 0,
            "idestado": parseInt($("#inIdEstado").val()) || 0
        });
        Consulta = "GetListNotificaciones_Simple";
    }

    var dataSource = new kendo.data.DataSource({
        batch: true,
        transport: {
            read: {
                type: "POST",
                url: wsnode + "wsNotificacion.svc/" + Consulta,
                contentType: "application/json; charset=utf-8",
                dataType: 'json'
            },
            parameterMap: function (options, operation) {
                return parametros;
            },
            pageSize: 10,
        }
    });

    return dataSource;
}

function SearchNotifications() {      
    $('#processingModal').modal('show');
    //console.log('begin: SearchNotifications');

    var columnas = [
            { field: "NU_MANI", title: "Manifiesto", width: "100px" },
            { field: "NU_GUIA", title: "Guia Hija", width: "95px" },
            { field: "NU_GUIA_MADR", title: "Guia Madre", width: "95px" },
            { field: "NU_VOLA", title: "Volante", width: "110px" },
            { field: "NU_BULT_MANI", title: "Bultos<br>Manif.", width: "60px" }
    ];
    
    $('#grid').empty();
    
    $("#grid").kendoGrid({
        dataSource: GetdataNotifications(),
        
        rowTemplate:
            "<tr>" +
            "<td onclick=\"GetIdSelectionAWB('#: NU_MANI #','#: NU_GUIA #','#: NU_VOLA #','#: EXISTE_NOTI #');return false;\" onmouseover=\"ShowDiv_Detail('#: NU_VOLA #')\" onmouseout=\"HideDiv_Detail('#: NU_VOLA #')\">" +
            "<div class='container-fluid'>" +
            "<div class='row-fluid'>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'><b>MANIFIESTO: #: NU_MANI #</b></div>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'><b>GUÍA: #: NU_GUIA #</b></div>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'><b>VOLANTE: #: NU_VOLA #</b>&nbsp;&nbsp;&nbsp;<i class='#: EXISTE_NOTI #'>&nbsp</i></div>" +
            //"<div class='col-xs-12 col-sm-6 col-md-4 pad-none'><b>VOLANTE: #: NU_VOLA #</b>&nbsp;&nbsp;&nbsp;<i #: EXISTE_NOTI #>&nbsp</i></div>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'><b>#: TIEMPO #</b></div>" +
            "<div style='display:none' id=\"#: NU_VOLA #\">" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'><b>GUÍA MADRE: #: NU_GUIA_MADR #</b></div>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'>BULTOS MANI: #: NU_BULT_MANI #U - #: KG_MANI #kg</div>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'>BULTOS RECI: #: NU_BULT_RECI #U - #: KG_RECI #kg</div>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'>LINEA AEREA: #: NO_ENTI_LINE #</div>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'>AG. CARGA: #: NO_ENTI_AGRE #</div>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'>CONSIGNATARIO: #: NO_ENTI_CONS #</div>" +
            "</div>" +            
            "</div>" +            
            "</div>" +            
            "</td>" +
            "</tr>",
        altRowTemplate:
            "<tr>" +
            "<td onclick=\"GetIdSelectionAWB('#: NU_MANI #','#: NU_GUIA #','#: NU_VOLA #','#: EXISTE_NOTI #');return false;\" onmouseover=\"ShowDiv_Detail('#: NU_VOLA #')\" onmouseout=\"HideDiv_Detail('#: NU_VOLA #')\">" +
            "<div class='container-fluid'>" +
            "<div class='row-fluid'>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'><b>MANIFIESTO: #: NU_MANI #</b></div>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'><b>GUÍA: #: NU_GUIA #</b></div>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'><b>VOLANTE: #: NU_VOLA #</b>&nbsp;&nbsp;&nbsp;<i class='#: EXISTE_NOTI #'>&nbsp;</i></div>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'><b>#: TIEMPO #</b></div>" +
            "<div style='display:none' id=\"#: NU_VOLA #\">" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'><b>GUÍA MADRE: #: NU_GUIA_MADR #</b></div>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'>BULTOS MANI: #: NU_BULT_MANI #U - #: KG_MANI #kg</div>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'>BULTOS RECI: #: NU_BULT_RECI #U - #: KG_RECI #kg</div>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'></div>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'>LINEA AEREA: #: NO_ENTI_LINE #</div>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'>AG. CARGA: #: NO_ENTI_AGRE #</div>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'>CONSIGNATARIO: #: NO_ENTI_CONS #</div>" +
            "<div class='col-xs-12 col-sm-6 col-md-3 pad-none'></div>" +
            "</div>" +
            "</div>" +
            "</div>" +
            "</td>" +
            "</tr>",
        selectable: "row",
        navigatable: false,
        height: 500,
        resizable: true,        
        pageable: {
            refresh: false,
            pageSize: 10,
            pageSizes: [10, 20, 40],
            //buttonCount: 1
        },
        columns: [{ field: "", title: "Listado de Guías" }],
        dataBound: function (e) {
            ////console.log('end: SearchNotifications');
            //$('#processingModal').modal('hide');
        }
    });
    isMiddle = 0;
    onResizeGrid();

    $('#processingModal').modal('hide');
}

function ShowDiv_Detail(name) {
    $("#" + name).css("display", "block");
}

function HideDiv_Detail(name) {
    $("#" + name).css("display", "none");
}

function onResizeGrid() {
    
    if (isMiddle == 1) {
        if (document.body.clientWidth > 989) {
            grid.style.width = '49.5%';
            detalle.style.width = '49.5%';
            $("#detalle").fadeIn("slow");
        }
        else {
            grid.style.width = '100%';
            detalle.style.width = '100%';
            $("#detalle").fadeIn("slow");
        }
    }
    else {
        detalle.style.display = 'none';
        grid.style.width = '100%';
        detalle.style.width = '100%';
        $("#detalle").fadeOut("slow");
    }

    console.log('grid', grid.style.width);
    console.log('document', document.body.clientWidth);    
}

$(document).ready(function () {
    $("#contentBusquedaAvanzada").hide();
    $("#hdBusquedaAvanzada").val("off");
    $("#faangleBusqueda").removeClass("fa fa-minus-square");
    $("#faangleBusqueda").addClass("fa fa-plus-square");

    $("#inFecha").kendoDatePicker(
        {
            culture: "es-PE"
        }
    );
    $("#inIdTipoAviso").val("0");
    $("#inIdTipoIndicación").val("0");
    $("#inIdArea").val("0");
    $("#inIdEstado").val("1");
    SearchEntidad_Id("ESTADONOTIFICA", "inIdEstado", "inDescripcionEstado", null, null);


});

$(window).resize(function () {
    onResizeGrid();
});

//Edgar

var nu_mani_;
var nu_guia_;
var nu_vola_;

function GetIdSelectionAWB(nu_mani, nu_guia, nu_vola, c_msg) {
    $('#processingModal').modal('show');
    nu_mani_ = nu_mani;
    nu_guia_ = nu_guia;
    nu_vola_ = nu_vola;
    isMiddle = 1;
    onResizeGrid();
    parametros = JSON.stringify({ "nu_mani": nu_mani, "nu_guia": nu_guia });
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsNotificacion.svc/GetArbol_Notificaciones",
        dataType: "json",
        data: parametros,
        async: false,
        processData: false,
        cache: false,
        success: function (data) {
            if (c_msg == "") {                
                $("#pages").resize();
            }
            console.log('cerrar success GetIdSelectionAWB');
            $('#processingModal').modal('hide');
            $("#ArbolNotificaciones").html(data);            
        },
        error: function (response) {
            $('#processingModal').modal('hide');
        }
    });

    if (c_msg == "fa fa-exclamation-triangle noti-pend") {
        $.ajax({
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            url: wsnode + "wsNotificacion.svc/GetValidateNotification",
            dataType: "json",
            data: parametros,
            async: false,
            processData: false,
            cache: false,
            success: function (data) {
                if (data.length != 0) {
                    var listDatos = data.split("|");
                    var co_Notificacion = listDatos[0];
                    var no_Notificacion = listDatos[1];
                    var co_indicacion = listDatos[2];
                    switch (listDatos[5]) {
                        case "TLM":
                            var entidad = "TALMA";
                            break;
                        case "CON":
                            var entidad = "IMPORTADOR";
                            break;
                        case "REC":
                            var entidad = "AGENTE DE CARGA";
                            break;
                        case "LIN":
                            var entidad = "LINEA AEREA";
                            break;
                        default:
                            var entidad = "";
                            break;
                    }
                    var idArea = listDatos[4];
                    GetHistory_save(co_Notificacion, no_Notificacion, entidad, co_indicacion, nu_mani, nu_guia, "", idArea);
                    
                } else {
                    $('#processingModal').modal('hide');
                }
            },
            error: function (response) {
                $('#processingModal').modal('hide');
            }
        });
    } else {
        $('#processingModal').modal('hide');        
    }
}

function newNotificacion() {
    $('#processingModal').modal('show');
    $("#inTitlePopupNotification").html("REGISTRAR NUEVA NOTIFICACIÓN");
    $('#inPopupNotification').modal('show');
    parametros = JSON.stringify({ "nu_mani": nu_mani_, "nu_guia": nu_guia_, "nu_vola": nu_vola_ });
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsNotificacion.svc/modalNotificacion",
        dataType: "json",
        data: parametros,
        async: false,
        processData: false,
        cache: false,
        success: function (data) {
            $("#content-Notification").html(data);
            $('#processingModal').modal('hide');
        },
        error: function (response) {
            $('#processingModal').modal('hide');
        }
    });
}

function getIndication() {
    $('#processingModal').modal('show');

    var co_noti = $("#cboNoti").val();
    $("#divIndication").html("");
    parametros = JSON.stringify({ "co_noti": co_noti });
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsNotificacion.svc/getIndication",
        dataType: "json",
        data: parametros,
        async: false,
        processData: false,
        cache: false,
        success: function (data) {
            $("#divIndication").html(data);
            $('#processingModal').modal('hide');
        },
        error: function (response) {
            showErrorMessage(response);
            $('#processingModal').modal('hide');
        }
    });
}

function GetHistory_save(co_Notificacion, no_Notificacion, entidad, co_indicacion, nu_mani, nu_guia, nu_vola, idArea) {

    //$('#processingModal').modal('show');

    ////console.log("inicio popup historia");
    $("#inTitlePopupHistory").html("Historial de Mensajes - " + no_Notificacion + " - " + entidad);
    $('#inPopupHistory').modal('show');
    parametros = JSON.stringify({
        "co_noti": co_Notificacion,
        "co_indi": co_indicacion,
        "tipo": "RPT",
        "nu_mani": nu_mani,
        "nu_guia": nu_guia,
        "nu_vola": nu_vola,
        "idArea": idArea
    });
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsNotificacion.svc/GetHistorial_newNotifi",
        dataType: "json",
        data: parametros,
        async: false,
        processData: false,
        cache: false,
        success: function (data) {
            $("#content-history").html(data);
            $('#processingModal').modal('hide');
            //console.log("fin popup historia");
        },
        error: function (response) {
            $('#processingModal').modal('hide');
            ////console.log("fin popup historia");
        }
    });
    //$('#processingModal').modal('hide');
}

function saveNotification() {
    $('#processingModal').modal('show');
    var from = $("#cboEntity").val();
    var to = $("#cboTo").val();
    var noti = $("#cboNoti").val();
    var ind = $("#cboIndi").val().split("|")[0];
    var no_Noti = $("#cboNoti option:selected").text();
    var send = true;

    var entidad;

    switch (from) {
        case "TLM":
            entidad = "Talma";
            break;
        case "LIN":
            entidad = "Línea Áerea";
            break;
        case "REC":
            entidad = "Agente de Carga";
            break;
        case "CON":
            entidad = "Importador";
            break;
    }

    if (from == 0) {
        send = false;
        $("#cboEntity").css("border-color", "red");
    }
    if (to == 0) {
        send = false;
        $("#cboTo").css("border-color", "red");
    }
    if (noti == 0) {
        send = false;
        $("#cboNoti").css("border-color", "red");
    }
    if (ind == 0) {
        send = false;
        $("#cboIndi").css("border-color", "red");
    }

    if (!send) {
        $('#processingModal').modal('hide');
        showWarningMessage("Por favor seleccionar las opciones obligatorias");
    } else {
        parametros = JSON.stringify({
            "from": from,
            "to": to,
            "tAviso": noti,
            "indication": ind,
            "nu_mani": nu_mani_,
            "nu_guia": nu_guia_
        });
        $.ajax({
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            url: wsnode + "wsNotificacion.svc/saveNotification",
            dataType: "json",
            data: parametros,
            async: false,
            processData: false,
            cache: false,
            success: function (data) {
                $('#processingModal').modal('hide');
                if (data.substring(0, 5) != "Error") {
                    GetHistory_save(data, no_Noti, entidad, ind, nu_mani_, nu_guia_, nu_vola_, to);
                } else {
                    showErrorMessage(data);
                }
            },
            error: function (response) {
                $('#processingModal').modal('hide');
                showErrorMessage(response);
            }
        });
    }
    
    
}

//@Angel 
function numero(num) {
    numtmp = '"' + num + '"';
    largo = numtmp.length - 2;
    numtmp = numtmp.split('"').join('');
    if (largo == 11) return numtmp;
    ceros = '';
    pendientes = 11 - largo;
    for (i = 0; i < pendientes; i++) ceros += '0';
    return ceros + numtmp;

}
