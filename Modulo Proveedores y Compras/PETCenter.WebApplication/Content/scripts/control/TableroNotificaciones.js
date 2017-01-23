$(document).ready(function () {    

    //$("#inFecha").kendoDatePicker(
    //    {
    //        value: new Date(),
    //        culture: "es-PE"
    //    }
    //);
    //$("#pagTableroControlNotificaciones").css("height", (window.innerHeight - 60) + "px");

    if (window.innerWidth <= 940) {
        $("#content-responsables").addClass("pad-both-30-10");
    }
    else {
        $("#content-responsables").removeClass("pad-both-30-10");
    }
    getResponsableMensaje();
    getChartTablero();
    window.setInterval(function () {
        var time = parseInt($("#inTiempo").html());
        if (time == 1)
            getChartTablero();
        if (time > 0) {
            time = time - 1;
        }
        $("#inTiempo").html(time);
    }, 1000);

    $('#inTableroPopup').on('shown.bs.modal', function () {
        $(document).off('focusin.modal');
    });

});



function getChartTablero()
{
    $('#processingModal').modal('show');
    //$("#rowchange").toggleClass('flip');
    parametros = JSON.stringify({ "oini": $("#oini").val(), "filas": $("#filas").val() });
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsNotificacion.svc/GetTablero_TipoNotificacion ",
        dataType: "json",
        data: parametros,
        success: function (data) {
            if (data != "") {                
                $("#rowchange").empty();
                $("#rowchange").append(data);
                var indice = parseInt($("#oini").val());
                var filas = parseInt($("#filas").val());
                indice += filas;
                $("#oini").val(indice);

                console.log('devuelve',indice);
                //window.setInterval(function () { $("#rowchange").toggleClass('flip'); }, 3000);
            }
            //Showmodal(false);
            $('#processingModal').modal('hide');
        },
        error: function (response) {
            //showWarningMessage(response);
            //Showmodal(false);
            $('#processingModal').modal('hide');
        }
    });
}

function getResponsableMensaje() {
    //$('#processingModal').modal('show');
   
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsNotificacion.svc/GetResponsableMensaje ",
        dataType: "json",
        data: {},
        success: function (data) {
            //console.log(data);
            if (data.length > 0) {
                console.log(data);
                var entity = data[0];

                $('#hidSupervisorTurno').val(entity.DNI_JEFE);
                $('#inSupervisorTurno').val(entity.NOM_JEFE);
                $('#hidLiderRecepcion').val(entity.DNI_SUP);
                $('#inLiderRecepcion').val(entity.NOM_SUP);
                $('#hidLiderManifisto').val(entity.DNI_LIDER);
                $('#inLiderManifisto').val(entity.NOM_LIDER);
            }
            
            //Showmodal(false);
            //$('#processingModal').modal('hide');
        },
        error: function (response) {
            //showWarningMessage(response);
            //Showmodal(false);
            //$('#processingModal').modal('hide');
        }
    });
}

function insertResponsableMensaje() {
    //$('#processingModal').modal('show');
    var jefe = $("#inTableroJefeTurno").data("kendoDropDownList");
    var supervisor = $("#inTableroSupervisorTurno").data("kendoDropDownList");
    var lider = $("#inTableroLiderTurno").data("kendoDropDownList");

    //var value = data.value();
    //var text = data.text();


    var datos = JSON.stringify(
        {
            'dniJefe': jefe.value(),
            'nombreJefe': jefe.text(),
            'dniSupervisor': supervisor.value(),
            'nombreSupervisor': supervisor.text(),
            'dniLider': lider.value(),
            'nombreLider': lider.text(),
            'coUsuarioCreacion': ''
        });
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsNotificacion.svc/InsertResponsableMensaje ",
        dataType: "json",
        data: datos,
        success: function (data) {
            console.log(data);
            if (data == 'OK') {
                getResponsableMensaje()
            }
            //Showmodal(false);
            //$('#processingModal').modal('hide');
        },
        error: function (response) {
            //showWarningMessage(response);
            //Showmodal(false);
            //$('#processingModal').modal('hide');
        }
    });
}

function setValuesToDropDownList() {
    console.log('setValuesToDropDownList');
    var jefe = $("#inTableroJefeTurno").data("kendoDropDownList");
    var supervisor = $("#inTableroSupervisorTurno").data("kendoDropDownList");
    var lider = $("#inTableroLiderTurno").data("kendoDropDownList");

    jefe.select(function (dataItem) {
        console.log($('#hidSupervisorTurno').val());
        return dataItem.symbol === $('#hidSupervisorTurno').val();
    });

    supervisor.select(function (dataItem) {
        console.log($('#hidLiderRecepcion').val());
        return dataItem.symbol === $('#hidLiderRecepcion').val();
    });

    lider.select(function (dataItem) {
        console.log($('#hidLiderManifisto').val());
        return dataItem.symbol === $('#hidLiderManifisto').val();
    });

}
function getDataLeft() {
    // 0: 2
    // 2: 4
    // 4 : 2
    var inicio = parseInt($("#oini").val());
    inicio = inicio - 4;

    if (inicio == -2)
        inicio = 0;

    console.log('envio', inicio);

    $("#oini").val(inicio);

    getChartTablero();
}
function getDataRight() {
    console.log('envio', $("#oini").val());
    getChartTablero();
}

$(window).resize(function () {
    if (window.innerWidth <= 940) {
        $("#content-responsables").addClass("pad-both-30-2");
    }
    else {
        $("#content-responsables").removeClass("pad-both-30-2");
    }
});

function ShowResponsables() {
    //$('#inPopupParamFiltro').empty();
    //$('#inPopupParamFiltro').append("<span class=\"input-group-addon input-sm\" id=\"inDatosGuia\">Datos de<br />Busqueda</span>");

    var Cuerpo =
        "<div class=\"row k-content\">" +
        "    <div class=\"col-xs-12 input-sm text-uppercase pad-both-20 color-text-black\" >" +
        "        Jefe de Turno" +
        "    </div>" +
        "    <div class=\"col-xs-12\">" +
        "        <input id=\"inTableroJefeTurno\" class=\"form-control input-sm pad-control text-uppercase\" style=\"width: 100%;\"/>" +
        "    </div>" +
        "    <div class=\"col-xs-12 input-sm text-uppercase pad-both-20 color-text-black\">" +
        "        Supervisor de Turno" +
        "    </div>" +
        "    <div class=\"col-xs-12\">" +
        "        <input id=\"inTableroSupervisorTurno\" class=\"form-control input-sm pad-control text-uppercase\" style=\"width: 100%;\"/>" +
        "    </div>" +
        "    <div class=\"col-xs-12 input-sm text-uppercase pad-both-20 color-text-black\">" +
        "        Líder de Turno" +
        "    </div>" +
        "    <div class=\"col-xs-12\"> " +
        "        <input id=\"inTableroLiderTurno\" class=\"form-control input-sm pad-control text-uppercase\" style=\"width: 100%;\"/>" +
        "    </div>" +
        "    <div class=\"col-xs-12\">" +
        "        <div id=\"chart_resumen\" class=\"label-sm\">" +
        "        </div>" +
        "    </div>" +
        "</div>";
    var Script =
        "<script language=\"javascript\" type=\"text/javascript\">" +

        "        function inTableroJefeTurno_filtering(e) {" +
        "            var filter = e.filter;" +
        "        }" +

        "        function inTableroSupervisorTurno_filtering(e) {" +
        "            var filter = e.filter;" +
        "        }" +

        "        function inTableroLiderTurno_filtering(e) {" +
        "            var filter = e.filter;" +
        "        }" +

        "        $(\"#inTableroJefeTurno\").kendoDropDownList({" +
        "            filter: \"contains\"," +
        "            dataTextField: \"DE_ENTITY\"," +
        "            dataValueField: \"CO_ENTITY\"," +
        "value: \"07878195\"," +
        "            dataSource: {" +
        "                type: \"json\"," +
        "                transport: {" +
        "                    read: {" +
        "                        type: \"POST\", " +
        "                        url: wsnode + \"wsNotificacion.svc/GetListPersonal\"," +
        "                        contentType: \"application/json; charset=utf-8\","+
        "                        dataType: 'json'"+
        "                    }," +
        "                    parameterMap: function (options, operation) {" +
        "                                        return JSON.stringify({ \"tipo_personal\": \"JEFE\" });" +
        "                    }" +
        "                }" +
        "            }" +
        "        });" +        

        "        $(\"#inTableroSupervisorTurno\").kendoDropDownList({" +
        "            filter: \"contains\"," +
        "            dataTextField: \"DE_ENTITY\"," +
        "            dataValueField: \"CO_ENTITY\"," +
        "            dataSource: {" +
        "                type: \"json\"," +
        "                transport: {" +
        "                    read: {" +
        "                        type: \"POST\", " +
        "                        url: wsnode + \"wsNotificacion.svc/GetListPersonal\"," +
        "                        contentType: \"application/json; charset=utf-8\"," +
        "                        dataType: 'json'" +
        "                    }," +
        "                    parameterMap: function (options, operation) {" +
        "                                        return JSON.stringify({ \"tipo_personal\": \"SUPERVISOR\" });" +
        "                    }" +
        "                }" +
        "            }" +
        "        });" +

        "        $(\"#inTableroLiderTurno\").kendoDropDownList({" +
        "            filter: \"contains\"," +
        "            dataTextField: \"DE_ENTITY\"," +
        "            dataValueField: \"CO_ENTITY\"," +
        "            dataSource: {" +
        "                type: \"json\"," +
        "                transport: {" +
        "                    read: {" +
        "                        type: \"POST\", " +
        "                        url: wsnode + \"wsNotificacion.svc/GetListPersonal\"," +
        "                        contentType: \"application/json; charset=utf-8\"," +
        "                        dataType: 'json'" +
        "                    }," +
        "                    parameterMap: function (options, operation) {" +
        "                                        return JSON.stringify({ \"tipo_personal\": \"LIDER\" });" +
        "                    }" +
        "                }" +
        "            }" +
        "        });" +

        "        var dropdownlist = $(\"#inTableroJefeTurno\").data(\"kendoDropDownList\");" +
        "        dropdownlist.bind(\"filtering\", inTableroJefeTurno_filtering);" +
        "           dropdownlist.value($(\"#hidSupervisorTurno\").val());" +

        "        var dropdownlist1 = $(\"#inTableroSupervisorTurno\").data(\"kendoDropDownList\");" +
        "        dropdownlist1.bind(\"filtering\", inTableroSupervisorTurno_filtering);" +
        "           dropdownlist1.value($(\"#hidLiderRecepcion\").val());" +

        "        var dropdownlist2 = $(\"#inTableroLiderTurno\").data(\"kendoDropDownList\");" +
        "        dropdownlist2.bind(\"filtering\", inTableroLiderTurno_filtering);" +
        "           dropdownlist2.value($(\"#hidLiderManifisto\").val());" +

      
        //"           setValuesToDropDownList();" +
        //"        $(\"#inTableroJefeTurno\").data(\"kendoDropDownList\").one(\"bind\", function (e) { setValuesToDropDownList(); });" +
        //"        $(\"#inTableroSupervisorTurno\").data(\"kendoDropDownList\").one(\"bind\", function (e) { setValuesToDropDownList(); });" +
        //"        $(\"#inTableroLiderTurno\").data(\"kendoDropDownList\").one(\"bind\", function (e) { setValuesToDropDownList(); });" +


        
        "</script>";

    $('#inTableroTitlePopup').html("CONFIGURACIÓN DE RESPONSABLES");
    $('#inTableroBody').empty();
    $('#inTableroBody').append(Cuerpo);
    $('#inTableroBody').append(Script);
    $('#inTableroPopup').modal('show');


    
}

var nu_mani_;
var nu_guia_;
var nu_vola_;

function GetIdSelectionAWB(nu_mani, nu_guia, nu_vola, c_msg, tipo, coIndicacion) {
    console.log(tipo);
    console.log(coIndicacion);
    $('#processingModal').modal('show');

    nu_mani_ = nu_mani;
    nu_guia_ = nu_guia;
    nu_vola_ = nu_vola;
    isMiddle = 1;
    //onResizeGrid();
    parametros = JSON.stringify({ "nu_mani": nu_mani, "nu_guia": nu_guia, 'tipo': tipo, 'coIndicacion': coIndicacion });
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
            //console.log(data);
            $("#ArbolNotificaciones").html(data);
            $('#processingModal').modal('hide');
            $('#modalNotificaciones').modal('show');

        },
        error: function (response) {
            $('#processingModal').modal('hide');
        }
    });
   
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
}