$(document).ready(function () {
    $("#txtFechaInicio").mask("99/99/9999", { placeholder: 'DD/MM/YYYY' });
    $("#txtFechaFin").mask("99/99/9999", { placeholder: 'DD/MM/YYYY' });

    cargarAreas();
    cargarEmpleados();
    getSolicitudRecursos(0);

    $('#RecursoModal').on('shown.bs.modal', function () {
        $(document).off('focusin.modal');
    });

});

function SetVerSolicitudId(primarykey) {
    $('#SolicitudModal').modal("show");
    //Clean
    $('#SolicitudModal_header').empty();
    $('#SolicitudModal_footer').empty();


    $("#txtResponsableModal").prop('disabled', true);
    $("#txtAreaModal").prop('disabled', true);
    $("#txtTotalModal").prop('disabled', true);
    $("#txtFechaRegistroModal").mask('00/00/0000');
    $("#txtFechaRegistroModal").prop('disabled', true);
    $("#lblMotivo").show();
    $("#taMotivo").show();
    $('#divSeccionCodigo').empty();
    $('#divSeccionCodigo').append(
        "<div class=\"col-xs-6 col-sm-6 col-md-3\">" +
        "    <div class=\"input-group input-group-sm\">" +
        "        <span class=\"input-group-addon\" style=\"width:130px\">Nro Solicitud</span>" +
        "        <input type=\"text\" class=\"form-control input-lg text-uppercase\" id=\"txtNroSolicitudModal\" />" +
        "    </div>" +
        "</div>" +
        "<div class=\"col-xs-6 col-sm-6 col-md-3\">" +
        "    <div class=\"input-group input-group-sm\">" +
        "        <span class=\"input-group-addon\" style=\"width:130px\">Estado</span>" +
        "        <input id=\"txtEstadoModal\" class=\"form-control input-lg text-uppercase text-uppercase\" style=\"width: 200px\" />" +
        "    </div>" +
        "</div>"
    );
    $("#txtNroSolicitudModal").prop('disabled', true);
    $("#txtEstadoModal").prop('disabled', true);
    //Add Controls
    $('#SolicitudModal_header').append(
        "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">×</button>" +
        "&nbsp;<i class=\"fa fa-pencil\">&nbsp;&nbsp;</i><label class=\"label-md modal-title custom_align\"> Ver Solicitud de Recursos</label>"
    );

    //Add Controls  
    $('#SolicitudModal_footer').append(
        "<div class=\"col-xs-12 \">" +
        "<div class=\"col-xs-8\"></div>" +
        "<div class=\"col-xs-2 home-buttom\" style=\"display:none;\" onclick=\"agregarItemRecurso(); return false;\">" +
        "    <center><i class=\"fa fa-plus\">&nbsp;&nbsp;</i> Agregar</center>" +
        "</div>" +
        "<div class=\"col-xs-2 home-buttom\" style=\"display:none;\" onclick=\"modificarSolicitud(); return false;\">" +
        "    <center><i class=\"fa fa-save\">&nbsp;&nbsp;</i> Guardar</center>" +
        "</div>" +
        "</div>"
    );

    var params = JSON.stringify({
        "idsolicitudrecursos": primarykey,
        "numerosolicitud": '',
        "area": 0,
        "responsable": 0,
        "fechainicio": '',
        "fechafin": '',
        "estado": ''
    });

    $.ajax({
        type: "POST",
        url: wsnode + "wsCompras.svc/GetSolicitudRecursos_Busqueda",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: params,
        async: false,
        processData: false,
        cache: false,
        success: function (response) {
            if (response.messageType == "ERR")
                showError(response.message);
            else if (response.messageType == "OK") {

                $("#txtFechaRegistroModal").val(response.rows[0].DesFecha);
                $("#txtAreaModal").val(response.rows[0].Empleado.Area.Descripcion);
                $("#txtResponsableModal").val(response.rows[0].Empleado.Nombres_Completo);
                $("#ddlPrioridadModal").val(response.rows[0].Prioridad);
                $("#ddlPrioridadModal").prop('disabled', true);
                $("#txtNroSolicitudModal").val(response.rows[0].NumSolicitudRecursos);
                $("#txtEstadoModal").val(response.rows[0].DesEstado);
                $("#taObservacion").val(response.rows[0].Observacion);
                $("#taMotivo").val(response.rows[0].Motivo);
                $("#taObservacion").prop('disabled', true);
                $("#taMotivo").prop('disabled', true);
                VerItemsSolicitud('GetItemSolicitudRecurso_A', primarykey, 0, 0);
            }
        },
        error: function (response) {
            showError(response);
        }
    });

    //getSolicitudModal();
}

function SetAnularSolicitudId(primarykey)
{
    $('#AnularSolicitudModal').modal("show");

    //Clean
    $('#AnularSolicitud_header').empty();
    $('#AnularSolicitud_footer').empty();

    var params = JSON.stringify({
        "idsolicitudrecursos": primarykey,
        "numerosolicitud": '',
        "area": 0,
        "responsable": 0,
        "fechainicio": '',
        "fechafin": '',
        "estado": ''
    });
    $('#taMotivoAnulacionPopup').val('');
    $.ajax({
        type: "POST",
        url: wsnode + "wsCompras.svc/GetSolicitudRecursos_Busqueda",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: params,
        async: false,
        processData: false,
        cache: false,
        success: function (response) {
            if (response.messageType == "ERR")
                showError(response.message);
            else if (response.messageType == "OK") {
                //Add Controls
                if (response.rows[0].Estado == "EMI") {
                    $('#AnularSolicitud_header').empty();
                    $('#AnularSolicitud_header').append(
                        "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">×</button>" +
                        "&nbsp;<i class=\"fa fa-trash-o\">&nbsp;&nbsp;</i><label class=\"label-md modal-title custom_align\"> Anular Solicitud de Recursos: " + response.rows[0].NumSolicitudRecursos + "</label>"
                    );

                    $('#nrosolictudAnularPopup').val(response.rows[0].NumSolicitudRecursos);
                    $('#idsolicitudAnularPopup').val(primarykey);
                }
                else
                {
                    showError("La solicitud no puede ser anulada.");
                    $('#AnularSolicitudModal').modal("hide");
                }
            }
        },
        error: function (response) {
            showError(response);
        }
    });



    //Add Controls
    
    $('#AnularSolicitud_footer').append(
        "<div class=\"col-xs-12 \">" +
        "<div class=\"col-xs-10\"></div>" +
        "<div class=\"col-xs-2 home-buttom\" onclick=\"AnularSolicitud(); return false;\">" +
        "    <center><i class=\"fa fa-cancel\">&nbsp;&nbsp;</i> Anular</center>" +
        "</div>" +
        "</div>"
    );
}


function modificarSolicitud() {
    var params = JSON.stringify({
        "codigosolicitud": $('#txtNroSolicitudModal').val() || '',
        "observacion": $("#taObservacion").val() || ''
    });
    $.ajax({
        type: "POST",
        url: wsnode + "wsCompras.svc/ActualizarSolicitudRecursos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: params,
        async: false,
        processData: false,
        cache: false,
        success: function (response) {
            $("#pages").append(response);
        },
        error: function (response) {
            showError(response);
        }
    });
}

function AnularSolicitud() {
    var params = JSON.stringify({
        "solicitud": $('#idsolicitudAnularPopup').val(),
        "motivo": $("#taMotivoAnulacionPopup").val() || ''
    });
    $.ajax({
        type: "POST",
        url: wsnode + "wsCompras.svc/AnularSolicitudRecursos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: params,
        async: false,
        processData: false,
        cache: false,
        success: function (response) {
            $("#pages").append(response);
        },
        error: function (response) {
            showError(response);
        }
    });
}

function insertarSolicitud()
{
    var params = JSON.stringify({
        "prioridad": $('#ddlPrioridadModal').val(),
        "observacion": $("#taObservacion").val() || ''
    });
    $.ajax({
        type: "POST",
        url: wsnode + "wsCompras.svc/InsertarSolicitudRecursos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: params,
        async: false,
        processData: false,
        cache: false,
        success: function (response) {
            $("#pages").append(response);            
        },
        error: function (response) {
            showError(response);
        }
    });
}

function SetModificaSolicitudId(primarykey) {
    $('#SolicitudModal').modal("show");
    //Clean
    $('#SolicitudModal_header').empty();
    $('#SolicitudModal_footer').empty();


    $("#txtResponsableModal").prop('disabled', true);
    $("#txtAreaModal").prop('disabled', true);
    $("#txtTotalModal").prop('disabled', true);
    $("#txtFechaRegistroModal").mask('00/00/0000');
    $("#txtFechaRegistroModal").prop('disabled', true);
    $("#lblMotivo").hide();
    $("#taMotivo").hide();
    

    $('#divSeccionCodigo').empty();
    $('#divSeccionCodigo').append(
        "<div class=\"col-xs-6 col-sm-6 col-md-3\">"+
        "    <div class=\"input-group input-group-sm\">"+
        "        <span class=\"input-group-addon\" style=\"width:130px\">Nro Solicitud</span>"+
        "        <input type=\"text\" class=\"form-control input-lg text-uppercase\" id=\"txtNroSolicitudModal\" />"+
        "    </div>"+
        "</div>"+
        "<div class=\"col-xs-6 col-sm-6 col-md-3\">"+
        "    <div class=\"input-group input-group-sm\">"+
        "        <span class=\"input-group-addon\" style=\"width:130px\">Estado</span>"+
        "        <input id=\"txtEstadoModal\" class=\"form-control input-lg text-uppercase text-uppercase\" style=\"width: 200px\" />" +
        "    </div>"+
        "</div>"
    );
    $("#txtNroSolicitudModal").prop('disabled', true);
    $("#txtEstadoModal").prop('disabled', true);
    //Add Controls
    $('#SolicitudModal_header').append(
        "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">×</button>" +
        "&nbsp;<i class=\"fa fa-pencil\">&nbsp;&nbsp;</i><label class=\"label-md modal-title custom_align\"> Modificar Solicitud de Recursos</label>"
    );

    //Add Controls  
    $('#SolicitudModal_footer').append(
        "<div class=\"col-xs-12 \">" +
        "<div class=\"col-xs-8\"></div>" +
        "<div class=\"col-xs-2 home-buttom\" style=\"display:block;\" onclick=\"agregarItemRecurso(); return false;\">" +
        "    <center><i class=\"fa fa-plus\">&nbsp;&nbsp;</i> Agregar</center>" +
        "</div>" +
        "<div class=\"col-xs-2 home-buttom\" style=\"display:block;\" onclick=\"modificarSolicitud(); return false;\">" +
        "    <center><i class=\"fa fa-save\">&nbsp;&nbsp;</i> Guardar</center>" +
        "</div>" +
        "</div>"
    );

    var params = JSON.stringify({
        "idsolicitudrecursos": primarykey,
        "numerosolicitud":  '',
        "area": 0,
        "responsable": 0,
        "fechainicio": '',
        "fechafin": '',
        "estado": ''
    });

    $.ajax({
        type: "POST",
        url: wsnode + "wsCompras.svc/GetSolicitudRecursos_Busqueda",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: params,
        async: false,
        processData: false,
        cache: false,
        success: function (response) {
            if (response.messageType == "ERR")
                showError(response.message);
            else if (response.messageType == "OK") {
                if (response.rows[0].Estado == "EMI") {
                    $("#txtFechaRegistroModal").val(response.rows[0].DesFecha);
                    $("#txtAreaModal").val(response.rows[0].Empleado.Area.Descripcion);
                    $("#txtResponsableModal").val(response.rows[0].Empleado.Nombres_Completo);
                    $("#ddlPrioridadModal").val(response.rows[0].Prioridad);
                    $("#ddlPrioridadModal").prop('disabled', true);
                    $("#txtNroSolicitudModal").val(response.rows[0].NumSolicitudRecursos);
                    $("#txtEstadoModal").val(response.rows[0].DesEstado);
                    $("#taObservacion").val(response.rows[0].Observacion);
                    $("#taObservacion").prop('disabled', false);
                    GetItemsSolicitud('GetItemSolicitudRecurso_A', primarykey, 0, 0);
                }
                else
                {
                    $('#SolicitudModal').modal("hide");
                    showError("La solicitud no puede ser modificada.");
                }
            }
        },
        error: function (response) {
            showError(response);
        }
    });

    //getSolicitudModal();
}

function nuevaSolicitudRecursos() {
    $('#SolicitudModal').modal("show");

    //Clean
    $('#SolicitudModal_header').empty();
    $('#SolicitudModal_footer').empty();
    
    
    $("#txtResponsableModal").prop('disabled', true);
    $("#txtAreaModal").prop('disabled', true);
    $("#txtTotalModal").prop('disabled', true);    
    $("#txtFechaRegistroModal").mask('00/00/0000');
    $("#txtFechaRegistroModal").prop('disabled', true);
    $("#ddlPrioridadModal").prop('disabled', false);
    $('#divSeccionCodigo').empty();

    //Add Controls
    $('#SolicitudModal_header').append(
        "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">×</button>" +
        "&nbsp;<i class=\"fa fa-plus\">&nbsp;&nbsp;</i><label class=\"label-md modal-title custom_align\"> Nueva Solicitud de Recursos</label>"
    );

    //Add Controls
    $('#SolicitudModal_footer').append(
        "<div class=\"col-xs-12 \">" +
        "<div class=\"col-xs-8\"></div>" +
        "<div class=\"col-xs-2 home-buttom\" style=\"display:block;\" onclick=\"agregarItemRecurso(); return false;\">" +
        "    <center><i class=\"fa fa-plus\">&nbsp;&nbsp;</i> Agregar</center>" +
        "</div>" +
        "<div class=\"col-xs-2 home-buttom\" style=\"display:block;\" onclick=\"insertarSolicitud(); return false;\">" +
        "    <center><i class=\"fa fa-save\">&nbsp;&nbsp;</i> Guardar</center>" +
        "</div>" +
        "</div>"
    );
    $("#lblMotivo").hide();
    $("#taMotivo").hide();
    $("#taObservacion").prop('disabled', false);

    $.ajax({
        type: "POST",
        url: wsnode + "wsCompras.svc/GetCabeceraSolicitud",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        processData: false,
        cache: false,
        success: function (response) {
            $("#pages").append(response);
        },
        error: function (response) {
            showError(response);
        }
    });    
    GetItemsIncial();
    
}

function getSolicitudRecursos(idprimarykey) {

    $('#processingModal').modal('show');
    /*
    showError($("#txtNumeroSolicitud").val());
    showError($("#txtArea").val());
    showError($("#txtResponsable").val());
    showError($("#txtFechaInicio").val());
    showError($("#txtFechaFin").val());
    showError($("#ddlEstado").val());
    */
    var params = JSON.stringify({
        "idsolicitudrecursos": idprimarykey,
        "numerosolicitud": $("#txtNumeroSolicitud").val() || '',
        "area": $("#txtArea").val(),
        "responsable": $("#txtResponsable").val(),
        "fechainicio": $("#txtFechaInicio").val() || '',
        "fechafin": $("#txtFechaFin").val() || '',
        "estado": $("#ddlEstado").val() || ''
    });
    $("#gridSolicitudRecursos").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCompras.svc/GetSolicitudRecursos_Busqueda",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: params,
                        async: false,
                        processData: false,
                        cache: false,
                        success: function (response) {
                            options.success(response.rows);
                            $('#processingModal').modal('hide');
                        },
                        error: function (err) {
                            console.log(err);
                            $('#processingModal').modal('hide');
                        }

                    });
                },
                pageSize: 20
            }
        }
        ,
        height: 340,
        groupable: false,
        sortable: true,
        selectable: true,
        resizable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        rowTemplate: "<tr>" +
            "<td>#: NumSolicitudRecursos #</td>" +
            "<td>#= kendo.toString(kendo.parseDate(Fecha, 'dd-MM-yyyy'), 'dd/MM/yyyy') #</td>" +
            "<td>#: Empleado.Nombres_Completo #</td>" +
            "<td>#: Empleado.Area.Descripcion #</td>" +
            "<td>#: DesPrioridad #</td>" +
            "<td>#: DesEstado #</td>" +
            "<td width=\"280px\">" +
            "<div class=\"col-xs-12\">" +
                "<div class=\"col-xs-5 home-buttom\" style=\"width:70px;margin-right:3px;height:auto !important;height: 40px;\" onclick=\"SetModificaSolicitudId('#: idSolicitudRecursos #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Modificar</center></div>" +
                "<div class=\"col-xs-5 home-buttom\" style=\"width:70px;margin-right:3px;height:auto !important;height: 40px;\" onclick=\"SetAnularSolicitudId('#: idSolicitudRecursos #');return false;\" ><center><i class=\"fa fa-trash-o\" aria-hidden=\"true\" >&nbsp;</i> Anular</center></div>" +
                "<div class=\"col-xs-5 home-buttom\" style=\"width:70px;margin-right:3px;height:auto !important;height: 40px;\" onclick=\"SetVerSolicitudId('#: idSolicitudRecursos #');return false;\" ><center><i class=\"fa fa-eye\" aria-hidden=\"true\" >&nbsp;</i> Ver</center></div>" +
            "</div>" +
            "</tr>",
        altRowTemplate: "<tr>" +
            "<td>#: NumSolicitudRecursos #</td>" +
            "<td>#= kendo.toString(kendo.parseDate(Fecha, 'dd-MM-yyyy'), 'dd/MM/yyyy') #</td>" +
            "<td>#: Empleado.Nombres_Completo #</td>" +
            "<td>#: Empleado.Area.Descripcion #</td>" +
            "<td>#: DesPrioridad #</td>" +
            "<td>#: DesEstado #</td>" +
            "<td width=\"280px\">" +
            "<div class=\"col-xs-12\">" +
                "<div class=\"col-xs-5 home-buttom\" style=\"width:70px;margin-right:3px;height:auto !important;height: 40px;\" onclick=\"SetModificaSolicitudId('#: idSolicitudRecursos #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Modificar</center></div>" +
                "<div class=\"col-xs-5 home-buttom\" style=\"width:70px;margin-right:3px;height:auto !important;height: 40px;\" onclick=\"SetAnularSolicitudId('#: idSolicitudRecursos #');return false;\" ><center><i class=\"fa fa-trash-o\" aria-hidden=\"true\" >&nbsp;</i> Anular</center></div>" +
                "<div class=\"col-xs-5 home-buttom\" style=\"width:70px;margin-right:3px;height:auto !important;height: 40px;\" onclick=\"SetVerSolicitudId('#: idSolicitudRecursos #');return false;\" ><center><i class=\"fa fa-eye\" aria-hidden=\"true\" >&nbsp;</i> Ver</center></div>" +
            "</div>" +
            "</tr>",
        columns: [
        {
            field: "NumSolicitudRecursos",
            title: "Nro Solicitud",
            width: 100
        },
        {
            field: "Fecha",
            title: "Fecha Registro",
            width: 120
        }, {
            field: "Empleado.Nombres",
            title: "Responsable",
            width: 140
        },
        {
            field: "Empleado.Area.Descripcion",
            title: "Area",
            width: 140
        }, {
            field: "DesPrioridad",
            title: "Prioridad",
            width: 80
        }, {
            field: "DesEstado",
            title: "Estado",
            width: 80
        }
        , {
            title: "Accion",
            width: 280
        }
        ]
    });
    var grid = $('#gridSolicitudRecursos').data('kendoGrid');
    grid.dataSource.pageSize(10);
    grid.dataSource.page(1);
    grid.dataSource.read();
}

function agregarItemRecurso() {

    $('#RecursoModal').modal('show');
    cargarRecursos();
    cargarPresentacionRecurso(-1, 0);
    $("#txtCantidadModal").mask('0000');
    $("#txtCantidadModal").val('0');


    $('#RecursoModal_header').empty();
    $('#RecursoModal_footer').empty();
    $('#RecursoModal_header').append(
    "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">×</button>" +
    "&nbsp;<i class=\"fa fa-plus\">&nbsp;&nbsp;</i><label class=\"label-md modal-title custom_align\"> Agregar Item</label>"
    );
    $('#RecursoModal_footer').append(
        "<div class=\"col-xs-12 \">" +
        "<div class=\"col-xs-7\"></div>" +
        "<div class=\"col-xs-4 home-buttom\" onclick=\"AceptarPresentacionRecursoModal('GetItemSolicitudRecurso_I'); return false;\">" +
        "    <center><i class=\"fa fa-save\">&nbsp;&nbsp;</i> Aceptar</center>" +
        "</div></div>"
    );
    //$("#txtRecursoModal").removeAttr("disabled");
    //$("#txtPresentacionModal").removeAttr("disabled");
}

function AceptarPresentacionRecursoModal(accion) {
    
    if ($("#txtPresentacionModal").val() == "0")
    {
        showError("Complete todos los campos");
    }
    else {
        if ($("#txtCantidadModal").val() == "0") {
            showError("Complete todos los campos");
        }
        else {
            $('#RecursoModal').modal('hide');
            agregarItemGrilla(accion, "0", $("#txtPresentacionModal").val(), $("#txtCantidadModal").val())
        }
    }
}

function txtRecursoModal_filtering(e) {
    var filter = e.filter;
}

function txtPresentacionModal_filtering(e) {
    var filter = e.filter;
}


function ModificarItemGrilla(idsolicitudrecurso, idpresentacionrecurso, cantidad, idrecurso)
{    
    $('#RecursoModal').modal('show');
    cargarRecursos();
    cargarPresentacionRecurso(idrecurso, 0);
    $("#txtCantidadModal").mask('0000');
    $("#txtCantidadModal").val(cantidad);

    var dropdownRecursoModal = $("#txtRecursoModal").data("kendoDropDownList");
    dropdownRecursoModal.bind("filtering", txtRecursoModal_filtering);
    dropdownRecursoModal.value(idrecurso);

    var dropdownPresentacionModal = $("#txtPresentacionModal").data("kendoDropDownList");
    dropdownPresentacionModal.bind("filtering", txtPresentacionModal_filtering);
    dropdownPresentacionModal.value(idpresentacionrecurso);

    $('#RecursoModal_header').empty();
    $('#RecursoModal_footer').empty();
    $('#RecursoModal_header').append(
    "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">×</button>" +
    "&nbsp;<i class=\"fa fa-pencil\">&nbsp;&nbsp;</i><label class=\"label-md modal-title custom_align\"> Modificar Item</label>"
    );
    $('#RecursoModal_footer').append(
        "<div class=\"col-xs-12 \">" +
        "<div class=\"col-xs-7\"></div>" +
        "<div class=\"col-xs-4 home-buttom\" onclick=\"AceptarPresentacionRecursoModal('GetItemSolicitudRecurso_M'); return false;\">" +
        "    <center><i class=\"fa fa-save\">&nbsp;&nbsp;</i> Aceptar</center>" +
        "</div></div>"
    );
    //$("#txtPresentacionModal").prop('disabled', true); 
    //$("#txtRecursoModal").prop('disabled', true);
    

}

function QuitarItemGrilla(idsolicitudrecurso, idpresentacionrecurso, cantidad, idrecurso)
{    
    GetItemsSolicitud("GetItemSolicitudRecurso_D", idsolicitudrecurso, idpresentacionrecurso, cantidad);
}

function GetItemsIncial()
{
    var params = JSON.stringify({
        "idsolicitudrecurso": -1,
        "idpresentacionrecurso": 0,
        "cantidad": -1
    });

    $("#gridItemsSolicitudModal").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCompras.svc/GetItemSolicitudRecurso_A",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: params,
                        async: false,
                        processData: false,
                        cache: false,
                        success: function (response) {
                            options.success(response.rows);
                            $('#processingModal').modal('hide');
                        },
                        error: function (response) {
                            showError(response);
                            $('#processingModal').modal('hide');
                        }

                    });
                },
                pageSize: 20
            }
        }
        ,
        height: 220,
        groupable: false,
        sortable: true,
        selectable: true,
        resizable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        rowTemplate: "<tr>" +
            "<td>#: presentacionrecurso.codigo #</td>" +
            "<td>#: presentacionrecurso.recurso.descripcion #</td>" +
            "<td>#: presentacionrecurso.descripcion #</td>" +
            "<td>#: cantidad #</td>" +
            "<td>#: precioreferencial #</td>" +
            "<td>#: total #</td>" +
            "<td width=\"200px\">" +
            "<div class=\"col-xs-12\">" +
                "<div class=\"col-xs-5 home-buttom\" style=\"width:70px;margin-right:3px;height:auto !important;height: 40px;\" onclick=\"ModificarItemGrilla('#: solicitudrecurso.idSolicitudRecursos #','#: presentacionrecurso.idpresentacionrecurso #', '#:cantidad #','#: presentacionrecurso.recurso.idrecurso #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Modificar</center></div>" +
                "<div class=\"col-xs-5 home-buttom\" style=\"width:70px;margin-right:3px;height:auto !important;height: 40px;\" onclick=\"QuitarItemGrilla('#: solicitudrecurso.idSolicitudRecursos #','#: presentacionrecurso.idpresentacionrecurso #', '#:cantidad #','#: presentacionrecurso.recurso.idrecurso #');return false;\" ><center><i class=\"fa fa-trash-o\" aria-hidden=\"true\" >&nbsp;</i> Quitar</center></div>" +
            "</div>" +
            "</tr>",
        altRowTemplate: "<tr>" +
            "<td>#: presentacionrecurso.codigo #</td>" +
            "<td>#: presentacionrecurso.recurso.descripcion #</td>" +
            "<td>#: presentacionrecurso.descripcion #</td>" +
            "<td>#: cantidad #</td>" +
            "<td>#: precioreferencial #</td>" +
            "<td>#: total #</td>" +
            "<td width=\"200px\">" +
            "<div class=\"col-xs-12\">" +
                "<div class=\"col-xs-5 home-buttom\" style=\"width:70px;margin-right:3px;height:auto !important;height: 40px;\" onclick=\"ModificarItemGrilla('#: solicitudrecurso.idSolicitudRecursos #','#: presentacionrecurso.idpresentacionrecurso #', '#:cantidad #','#: presentacionrecurso.recurso.idrecurso #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Modificar</center></div>" +
                "<div class=\"col-xs-5 home-buttom\" style=\"width:70px;margin-right:3px;height:auto !important;height: 40px;\" onclick=\"QuitarItemGrilla('#: solicitudrecurso.idSolicitudRecursos #','#: presentacionrecurso.idpresentacionrecurso #', '#:cantidad #','#: presentacionrecurso.recurso.idrecurso #');return false;\" ><center><i class=\"fa fa-trash-o\" aria-hidden=\"true\" >&nbsp;</i> Quitar</center></div>" +
            "</div>" +
            "</td>" +
            "</tr>",
        columns: [
        {
            title: "Item",
            width: 140
        },
        {
            title: "Recurso",
            width: 200
        }, {
            title: "Presentación",
            width: 200
        },
        {
            title: "Cantidad",
            width: 80
        },
        {
            title: "Precio<br>Referencial",
            width: 80
        },
        {
            title: "Total",
            width: 80
        },
        {
            title: "Accion",
            width: 200
        }
        ]
    });
    var grid = $('#gridItemsSolicitudModal').data('kendoGrid');
    grid.dataSource.pageSize(10);
    grid.dataSource.page(1);
    grid.dataSource.read();
}

function VerItemsSolicitud(accion, idsolicitudrecurso, idpresentacionrecurso, cantidad) {
    var params = JSON.stringify({
        "idsolicitudrecurso": idsolicitudrecurso,
        "idpresentacionrecurso": idpresentacionrecurso,
        "cantidad": cantidad
    });

    $("#gridItemsSolicitudModal").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCompras.svc/" + accion,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: params,
                        async: false,
                        processData: false,
                        cache: false,
                        success: function (response) {
                            options.success(response.rows);
                            $('#processingModal').modal('hide');
                            if (response.messageType != "OK")
                                showError(response.message);
                            else {
                                $('#pages').append(response.message)
                            }
                        },
                        error: function (response) {
                            showError(response);
                            $('#processingModal').modal('hide');
                        }

                    });
                },
                pageSize: 20
            }
        }
        ,
        height: 220,
        groupable: false,
        sortable: true,
        selectable: true,
        resizable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        rowTemplate: "<tr>" +
            "<td>#: presentacionrecurso.codigo #</td>" +
            "<td>#: presentacionrecurso.recurso.descripcion #</td>" +
            "<td>#: presentacionrecurso.descripcion #</td>" +
            "<td>#: cantidad #</td>" +
            "<td>#: precioreferencial #</td>" +
            "<td>#: total #</td>" +
            "</tr>",
        altRowTemplate: "<tr>" +
            "<td>#: presentacionrecurso.codigo #</td>" +
            "<td>#: presentacionrecurso.recurso.descripcion #</td>" +
            "<td>#: presentacionrecurso.descripcion #</td>" +
            "<td>#: cantidad #</td>" +
            "<td>#: precioreferencial #</td>" +
            "<td>#: total #</td>" +
            "</td>" +
            "</tr>",
        columns: [
        {
            title: "Item",
            width: 140
        },
        {
            title: "Recurso",
            width: 200
        }, {
            title: "Presentación",
            width: 200
        },
        {
            title: "Cantidad",
            width: 80
        },
        {
            title: "Precio<br>Referencial",
            width: 80
        },
        {
            title: "Total",
            width: 80
        },
        ]
    });
    var grid = $('#gridItemsSolicitudModal').data('kendoGrid');
    grid.dataSource.pageSize(10);
    grid.dataSource.page(1);
    grid.dataSource.read();
}


function GetItemsSolicitud(accion, idsolicitudrecurso, idpresentacionrecurso, cantidad)
{
    var params = JSON.stringify({
        "idsolicitudrecurso": idsolicitudrecurso,
        "idpresentacionrecurso": idpresentacionrecurso,
        "cantidad": cantidad
    });

    $("#gridItemsSolicitudModal").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCompras.svc/" + accion,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: params,
                        async: false,
                        processData: false,
                        cache: false,
                        success: function (response) {
                            options.success(response.rows);
                            $('#processingModal').modal('hide');
                            if (response.messageType != "OK")
                                showError(response.message);
                            else {
                                $('#pages').append(response.message)
                            }
                        },
                        error: function (response) {
                            showError(response);
                            $('#processingModal').modal('hide');
                        }

                    });
                },
                pageSize: 20
            }
        }
        ,
        height:220,
        groupable: false,
        sortable: true,
        selectable: true,
        resizable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        rowTemplate: "<tr>" +
            "<td>#: presentacionrecurso.codigo #</td>" +
            "<td>#: presentacionrecurso.recurso.descripcion #</td>" +
            "<td>#: presentacionrecurso.descripcion #</td>" +
            "<td>#: cantidad #</td>" +
            "<td>#: precioreferencial #</td>" +
            "<td>#: total #</td>" +
            "<td width=\"200px\">" +
            "<div class=\"col-xs-12\">" +
                "<div class=\"col-xs-5 home-buttom\" style=\"width:70px;margin-right:3px;height:auto !important;height: 40px;\" onclick=\"ModificarItemGrilla('#: solicitudrecurso.idSolicitudRecursos #','#: presentacionrecurso.idpresentacionrecurso #', '#:cantidad #','#: presentacionrecurso.recurso.idrecurso #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Modificar</center></div>" +
                "<div class=\"col-xs-5 home-buttom\" style=\"width:70px;margin-right:3px;height:auto !important;height: 40px;\" onclick=\"QuitarItemGrilla('#: solicitudrecurso.idSolicitudRecursos #','#: presentacionrecurso.idpresentacionrecurso #', '#:cantidad #','#: presentacionrecurso.recurso.idrecurso #');return false;\" ><center><i class=\"fa fa-trash-o\" aria-hidden=\"true\" >&nbsp;</i> Quitar</center></div>" +
            "</div>" +
            "</tr>",
        altRowTemplate: "<tr>" +
            "<td>#: presentacionrecurso.codigo #</td>" +
            "<td>#: presentacionrecurso.recurso.descripcion #</td>" +
            "<td>#: presentacionrecurso.descripcion #</td>" +
            "<td>#: cantidad #</td>" +
            "<td>#: precioreferencial #</td>" +
            "<td>#: total #</td>" +
            "<td width=\"200px\">" +
            "<div class=\"col-xs-12\">" +
                "<div class=\"col-xs-5 home-buttom\" style=\"width:70px;margin-right:3px;height:auto !important;height: 40px;\" onclick=\"ModificarItemGrilla('#: solicitudrecurso.idSolicitudRecursos #','#: presentacionrecurso.idpresentacionrecurso #', '#:cantidad #','#: presentacionrecurso.recurso.idrecurso #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Modificar</center></div>" +
                "<div class=\"col-xs-5 home-buttom\" style=\"width:70px;margin-right:3px;height:auto !important;height: 40px;\" onclick=\"QuitarItemGrilla('#: solicitudrecurso.idSolicitudRecursos #','#: presentacionrecurso.idpresentacionrecurso #', '#:cantidad #','#: presentacionrecurso.recurso.idrecurso #');return false;\" ><center><i class=\"fa fa-trash-o\" aria-hidden=\"true\" >&nbsp;</i> Quitar</center></div>" +
            "</div>" +
            "</td>" +
            "</tr>",
        columns: [
        {
            title: "Item",
            width: 140
        },
        {
            title: "Recurso",
            width: 200
        }, {
            title: "Presentación",
            width: 200
        },
        {
            title: "Cantidad",
            width: 80
        },
        {
            title: "Precio<br>Referencial",
            width: 80
        },
        {
            title: "Total",
            width: 80
        },
        {
            title: "Accion",
            width: 200
        }
        ]
    });
    var grid = $('#gridItemsSolicitudModal').data('kendoGrid');
    grid.dataSource.pageSize(10);
    grid.dataSource.page(1);
    grid.dataSource.read();
}

function agregarItemGrilla(accion, idsolicitudrecurso, idpresentacionrecurso, cantidad) {
    $('#processingModal').modal('show'); 
    GetItemsSolicitud(accion, idsolicitudrecurso, idpresentacionrecurso, cantidad);
}

function cargarAreas() {
    var params = JSON.stringify({
        "idArea": 0
    });
    $("#txtArea").kendoDropDownList({
        filter: "contains",
        dataTextField: "Descripcion",
        dataValueField: "idArea",
        width: '350px',
        value: "",
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCompras.svc/GetArea",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: params,
                        async: false,
                        processData: false,
                        cache: false,
                        success: function (response) {
                            options.success(response.rows);
                            if (response.messageType != "OK")
                                showError(response.message);
                        },
                        error: function (response) {
                            showError(response);
                        }

                    });
                }
            }
        }
    });
}


function cargarEmpleados() {
    var params = JSON.stringify({
        "idempleado": 0,
        "idarea": $("#txtArea").val()
    });
    $("#txtResponsable").kendoDropDownList({
        filter: "contains",
        dataTextField: "Nombres_Completo",
        dataValueField: "id_Empleado",
        width: '350px',
        value: "",
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCompras.svc/GetEmpleado",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: params,
                        async: false,
                        processData: false,
                        cache: false,
                        success: function (response) {
                            options.success(response.rows);
                            if (response.messageType != "OK")
                                showError(response.message);
                        },
                        error: function (response) {
                            showError(response);
                        }

                    });
                }
            }
        }
    });
}

function cargarRecursos()
{    
    var params = JSON.stringify({
        "idrecurso": 0
    });
    $("#txtRecursoModal").kendoDropDownList({
        filter: "contains",
        dataTextField: "descripcion",
        dataValueField: "idrecurso",
        width: '350px',
        value: "",
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCompras.svc/GetRecurso",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: params,
                        async: false,
                        processData: false,
                        cache: false,
                        success: function (response) {
                            options.success(response.rows);
                            if (response.messageType != "OK")
                                showError(response.message);
                        },
                        error: function (response) {
                            showError(response);
                        }

                    });
                }
            }
        }
    });    
}

$('#txtRecursoModal').change(function () {
    cargarPresentacionRecurso($("#txtRecursoModal").val())
});

$('#txtArea').change(function () {
    cargarEmpleados();
});

function cargarPresentacionRecurso(recurso, idpresentacion)
{
    var params = JSON.stringify({
        "idrecurso": recurso,
        "idpresentacion": idpresentacion
    });
    $("#txtPresentacionModal").kendoDropDownList({
        filter: "contains",
        dataTextField: "descripcion",
        dataValueField: "idpresentacionrecurso",
        width: '350px',
        value: "",
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCompras.svc/GetPresentacionRecurso",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: params,
                        async: false,
                        processData: false,
                        cache: false,
                        success: function (response) {
                            options.success(response.rows);
                            if (response.messageType != "OK")
                                showError(response.message);
                        },
                        error: function (response) {
                            showError(response);
                        }

                    });
                }
            }
        }
    });
}



var tipoBusquedaGeneral = '';
function showModalCriterios(tipo, controName, tag) {

    tipoBusquedaGeneral = tipo;
    $('#lblHeaderBusquedaGeneral').html('Consulta ' + tag);
    $('#lblCriterioBusqueda').html(tag);

    var hidVaue = $('#hid' + controName).val();
    var txtValue = $('#txt' + controName).val();

    $('#busquedaGeneralModal').modal('show');

    switch (tipo) {

    }

    $("#gridBusquedaGeneral").kendoGrid({
        dataSource: [],
        height: 250,
        groupable: false,
        sortable: true,
        selectable: true,
        resizable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        rowTemplate: "<tr>" +
            "<td>#: Codigo #</td>" +
            "<td>#: DesTipoDocumento #</td>" +
            "</tr>",
        altRowTemplate: "<tr>" +
            "<td>#: Codigo #</td>" +
            "<td>#: DesTipoDocumento #</td>" +
            "</tr>",
        columns: [
        {
            field: "Codigo",
            title: "Codigo",
            width: 120
        },
        {
            field: "DesTipoDocumento",
            title: "Descripción"
        }]
    });
    var grid = $('#gridBusquedaGeneral').data('kendoGrid');
    grid.dataSource.pageSize(10);
    grid.dataSource.page(1);
    grid.dataSource.read();
}

function busquedaGeneral() {
    $('#processingModal').modal('show');

    var params = JSON.stringify({
        "criterioBusqueda": $("#txtCriterioBusqueda").val() || ''
    });
    $("#gridBusquedaGeneral").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCompras.svc/GetProveedores_Busqueda",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: params,
                        async: false,
                        processData: false,
                        cache: false,
                        success: function (response) {
                            options.success(response.rows);
                            $('#processingModal').modal('hide');
                        },
                        error: function (err) {
                            console.log(err);
                            $('#processingModal').modal('hide');
                        }

                    });
                },
                pageSize: 20
            }
        }
        ,
        height: 250,
        groupable: false,
        sortable: true,
        selectable: true,
        resizable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        rowTemplate: "<tr>" +
            "<td>#: Codigo #</td>" +
            "<td>#: DesTipoDocumento #</td>" +
            "</tr>",
        altRowTemplate: "<tr>" +
            "<td>#: Codigo #</td>" +
            "<td>#: DesTipoDocumento #</td>" +
            "</tr>",
        columns: [
        {
            field: "Codigo",
            title: "Nro Solicitud",
            width: 120
        },
        {
            field: "DesTipoDocumento",
            title: "Fecha Registro",
            width: 120
        }]
    });
    var grid = $('#gridBusquedaGeneral').data('kendoGrid');
    grid.dataSource.pageSize(10);
    grid.dataSource.page(1);
    grid.dataSource.read();
}