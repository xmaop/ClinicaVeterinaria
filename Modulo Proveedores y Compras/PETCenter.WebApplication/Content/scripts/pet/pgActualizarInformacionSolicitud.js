$(document).ready(function () {
    $("#txtFechaInicio").mask("99/99/9999", { placeholder: 'DD/MM/YYYY' });
    $("#txtFechaFin").mask("99/99/9999", { placeholder: 'DD/MM/YYYY' });

    inicializarData();
    getSolicitudRecursos();

});

function anularSolicitudRecursos()
{
    $('#AnularSolicitudModal').modal("show");

    //Clean
    $('#AnularSolicitud_header').empty();
    $('#AnularSolicitud_footer').empty();

    //Add Controls
    $('#AnularSolicitud_header').append(
        "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">×</button>" +
        "&nbsp;<i class=\"fa fa-pencil\">&nbsp;&nbsp;</i><label class=\"label-md modal-title custom_align\"> Anular Solicitud de Recursos</label>"
    );

    //Add Controls
    
    $('#AnularSolicitud_footer').append(
        "<div class=\"col-xs-12 \">" +
        "<div class=\"col-xs-8\"></div>" +
        "<div class=\"col-xs-2 home-buttom\" onclick=\"realizaranulacionSolicitud(); return false;\">" +
        "    <center><i class=\"fa fa-save\">&nbsp;&nbsp;</i> Aceptar</center>" +
        "</div>" +
        "</div>"
    );
}

function modificarSolicitudRecursos() {
    $('#SolicitudModal').modal("show");

    //Clean
    $('#SolicitudModal_header').empty();
    $('#SolicitudModal_footer').empty();


    $("#txtResponsableModal").prop('disabled', true);
    $("#txtAreaModal").prop('disabled', true);
    $("#txtTotalModal").prop('disabled', true);
    $("#txtFechaRegistroModal").mask('00/00/0000');
    $("#txtFechaRegistroModal").prop('disabled', true);

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
        "<div class=\"col-xs-2 home-buttom\" onclick=\"agregarItemRecurso(); return false;\">" +
        "    <center><i class=\"fa fa-plus\">&nbsp;&nbsp;</i> Agregar</center>" +
        "</div>" +
        "<div class=\"col-xs-2 home-buttom\" onclick=\"guardarSolicitud(); return false;\">" +
        "    <center><i class=\"fa fa-save\">&nbsp;&nbsp;</i> Guardar</center>" +
        "</div>" +
        "</div>"
    );

    getSolicitudModal();
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
        "<div class=\"col-xs-2 home-buttom\" onclick=\"guardarProveedor(); return false;\">" +
        "    <center><i class=\"fa fa-plus\">&nbsp;&nbsp;</i> Agregar</center>" +
        "</div>" +
        "<div class=\"col-xs-2 home-buttom\" onclick=\"guardarProveedor(); return false;\">" +
        "    <center><i class=\"fa fa-save\">&nbsp;&nbsp;</i> Guardar</center>" +
        "</div>" +
        "</div>"
    );


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

    getSolicitudModal();
}

function getSolicitudRecursos() {

    $('#processingModal').modal('show');

    var params = JSON.stringify({
        "numeroSolicitud": $("#txtNumeroSolicitud").val() || '',
        "area": $("#ddlArea").val() || '',
        "responsable": $("#ddlResponsable").val() || '',
        "fechaInicio": $("#txtFechaInicio").val() || '',
        "fechaFin": $("#txtFechaFin").val() || '',
        "estado": $("#ddlEstado").val() || ''
    });
    $("#gridSolicitudRecursos").kendoGrid({
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
            "<td>#: Codigo #</td>" +
            "<td>#: DesTipoDocumento #</td>" +
            "<td>#: Documento #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td><div class=\"home-buttom\" style=\"margin-right:3px;height:auto !important;height: 40px;\" onclick=\"SetProovedorId('#: idProveedor #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Modificar</center></div></td>" +
            "<td><div class=\"home-buttom\" style=\"margin-right:3px;height:auto !important;height: 40px;\" onclick=\"SetProovedorId('#: idProveedor #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Anular</center></div></td>" +
            "<td><div class=\"home-buttom\" style=\"margin-right:3px;height:auto !important;height: 40px;\" onclick=\"SetProovedorId('#: idProveedor #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Ver</center></div></td>" +
            "</tr>",
        altRowTemplate: "<tr>" +
            "<td>#: Codigo #</td>" +
            "<td>#: DesTipoDocumento #</td>" +
            "<td>#: Documento #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td><div class=\"home-buttom\" style=\"margin-right:3px;height:auto !important;height: 40px;\" onclick=\"SetProovedorId('#: idProveedor #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Modificar</center></div></td>" +
            "<td><div class=\"home-buttom\" style=\"margin-right:3px;height:auto !important;height: 40px;\" onclick=\"SetProovedorId('#: idProveedor #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Anular</center></div></td>" +
            "<td><div class=\"home-buttom\" style=\"margin-right:3px;height:auto !important;height: 40px;\" onclick=\"SetProovedorId('#: idProveedor #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Ver</center></div></td>" +
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
        }, {
            field: "Documento",
            title: "Responsable",
            width: 120
        },
        {
            field: "RazonSocial",
            title: "Area",
        }, {
            field: "Estado",
            title: "Prioridad",
            width: 80
        }, {
            field: "Estado",
            title: "Estado",
            width: 80
        },
        {
            field: "Estado",
            title: "Modificar",
            width: 80
        },
        {
            field: "Estado",
            title: "Anular",
            width: 80
        },
        {
            field: "Estado",
            title: "Ver",
            width: 80
        }, {
            field: "TipoDocumento",
            hidden: true
        }, {
            field: "Documento",
            hidden: true
        }, {
            field: "Telefono",
            hidden: true
        }, {
            field: "Contacto",
            hidden: true
        }]
    });
    var grid = $('#gridSolicitudRecursos').data('kendoGrid');
    grid.dataSource.pageSize(10);
    grid.dataSource.page(1);
    grid.dataSource.read();
}

function getSolicitudModal() {

    $('#processingModal').modal('show');

    var params = JSON.stringify({
        "numeroSolicitud": $("#txtNumeroSolicitud").val() || '',
        "area": $("#ddlArea").val() || '',
        "responsable": $("#ddlResponsable").val() || '',
        "fechaInicio": $("#txtFechaInicio").val() || '',
        "fechaFin": $("#txtFechaFin").val() || '',
        "estado": $("#ddlEstado").val() || ''
    });
    $("#gridSolicitudModal").kendoGrid({
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
            "<td>#: Codigo #</td>" +
            "<td>#: DesTipoDocumento #</td>" +
            "<td>#: Documento #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td>#: RazonSocial #</td>" +

            "<td>" +
                "<div class=\"home-buttom\" style=\"margin-right:3px;height:auto !important;height: 40px;\" onclick=\"ModificarItemId('#: idProveedor #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Modificar</center></div>" +
                "<div class=\"home-buttom\" style=\"margin-right:3px;height:auto !important;height: 40px;\" onclick=\"QuitarItemId('#: idProveedor #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Quitar</center></div>" +
            "</td>" +
            "</tr>",
        altRowTemplate: "<tr>" +
            "<td>#: Codigo #</td>" +
            "<td>#: DesTipoDocumento #</td>" +
            "<td>#: Documento #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td>#: RazonSocial #</td>" +

            "<td>" +
                "<div class=\"home-buttom\" style=\"margin-right:3px;height:auto !important;height: 40px;\" onclick=\"ModificarItemId('#: idProveedor #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Modificar</center></div>" +
                "<div class=\"home-buttom\" style=\"margin-right:3px;height:auto !important;height: 40px;\" onclick=\"QuitarItemId('#: idProveedor #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Quitar</center></div>" +
            "</td>" +

            "</tr>",
        columns: [
        {
            field: "Codigo",
            title: "Item",
            width: 60
        },
        {
            field: "DesTipoDocumento",
            title: "Recurso"
        }, {
            field: "Documento",
            title: "Presentación"
        },
        {
            field: "RazonSocial",
            title: "Cantidad",
        }, {
            field: "Estado",
            title: "Precio Referencial",
            width: 140
        }, {
            field: "Estado",
            title: "Total",
            width: 80
        },
        {
            field: "Estado",
            title: "Acción",
            width: 80
        }, {
            field: "TipoDocumento",
            hidden: true
        }, {
            field: "Documento",
            hidden: true
        }, {
            field: "Telefono",
            hidden: true
        }, {
            field: "Contacto",
            hidden: true
        }]
    });
    var grid = $('#gridSolicitudModal').data('kendoGrid');
    grid.dataSource.pageSize(10);
    grid.dataSource.page(1);
    grid.dataSource.read();
}

function agregarItemRecurso() {

    $('#RecursoModal').modal('show');
}

function agregarItemGrilla() {

    $('#RecursoModal').modal('hide');
}


function inicializarData() {
    //AREA
    $("#txtArea").kendoDropDownList({
        filter: "contains",
        dataTextField: "DE_ENTITY",
        dataValueField: "CO_ENTITY",
        //value: "07878195",
        width:'350px',
        value: "",
        dataSource: [{ DE_ENTITY: "VENTAS", CO_ENTITY: "1" },{ DE_ENTITY: "COMPRAS", CO_ENTITY: "2" }]
        /*
        dataSource: {
            type: "json",
            transport: {
                read: {
                    type: "POST",
                    url: wsnode + "wsNotificacion.svc/GetListPersonal",
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json'
                },
                parameterMap: function (options, operation) {
                    return JSON.stringify({ "tipo_personal": "JEFE" });
                }
            }
        }*/
    });

    //RESPONSABLE
    $("#txtResponsable").kendoDropDownList({
        filter: "contains",
        dataTextField: "DE_ENTITY",
        dataValueField: "CO_ENTITY",
        //value: "07878195",
        width: '350px',
        value: "",
        dataSource: [{ DE_ENTITY: "EDGAR VASQUEZ", CO_ENTITY: "1" }, { DE_ENTITY: "PEPITO PERES", CO_ENTITY: "2" }]
        /*
        dataSource: {
            type: "json",
            transport: {
                read: {
                    type: "POST",
                    url: wsnode + "wsNotificacion.svc/GetListPersonal",
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json'
                },
                parameterMap: function (options, operation) {
                    return JSON.stringify({ "tipo_personal": "JEFE" });
                }
            }
        }*/
    });

    //RECURSO MODAL
    $("#txtRecursoModal").kendoDropDownList({
        filter: "contains",
        dataTextField: "DE_ENTITY",
        dataValueField: "CO_ENTITY",
        //value: "07878195",
        width: '350px',
        value: "",
        dataSource: [{ DE_ENTITY: "SHAMPOO", CO_ENTITY: "1" }]
        /*dataSource: {
            type: "json",
            transport: {
                read: {
                    type: "POST",
                    url: wsnode + "wsNotificacion.svc/GetListPersonal",
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json'
                },
                parameterMap: function (options, operation) {
                    return JSON.stringify({ "tipo_personal": "JEFE" });
                }
            }
        }*/
    });


    //PRESENTACION MODAL
    $("#txtPresentacionModal").kendoDropDownList({
        filter: "contains",
        dataTextField: "DE_ENTITY",
        dataValueField: "CO_ENTITY",
        //value: "07878195",
        width: '350px',
        value: "",
        dataSource: [{ DE_ENTITY: "CAJA", CO_ENTITY: "1" }]
        /*
        dataSource: {
            type: "json",
            transport: {
                read: {
                    type: "POST",
                    url: wsnode + "wsNotificacion.svc/GetListPersonal",
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json'
                },
                parameterMap: function (options, operation) {
                    return JSON.stringify({ "tipo_personal": "JEFE" });
                }
            }
        }*/
    });
}



function limpiarControl(controName) {

    $('#hid' + controName).val('');
    $('#txt' + controName).val('');
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