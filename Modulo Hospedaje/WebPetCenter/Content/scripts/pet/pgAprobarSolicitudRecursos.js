$(document).ready(function () {
    inicializarData();

});

function AprobarSolicitud() {
    $('#SolicitudAprobacionModal').modal("show");
}


function getSolicitudAprobacion() {
    $('#processingModal').modal('show');

    var params = JSON.stringify({
        "numeroSolicitud": $("#txtNumeroSolicitud").val() || '',
        "area": $("#ddlArea").val() || '',
        "responsable": $("#ddlResponsable").val() || '',
        "fechaInicio": $("#txtFechaInicio").val() || '',
        "fechaFin": $("#txtFechaFin").val() || '',
        "estado": $("#ddlEstado").val() || ''
    });
    $("#gridSolicitudAprobacion").kendoGrid({
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
            "<td><div class=\"home-buttom\" style=\"margin-right:3px;height:auto !important;height: 40px;\" onclick=\"showDetalleSolicitudRecurso('#: Codigo #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Aprobar</center></div></td>" +
            "</tr>",
        altRowTemplate: "<tr>" +
            "<td>#: Codigo #</td>" +
            "<td>#: DesTipoDocumento #</td>" +
            "<td>#: Documento #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td><div class=\"home-buttom\" style=\"margin-right:3px;height:auto !important;height: 40px;\" onclick=\"showDetalleSolicitudRecurso('#: Codigo #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Aprobar</center></div></td>" +
           "</tr>",
        columns: [
        {
            field: "Codigo",
            title: "Nro Solicitud"
        },
        {
            field: "DesTipoDocumento",
            title: "Fecha Registro"
        }, {
            field: "Documento",
            title: "Responsable"
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
            title: "Aprobar",
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
    var grid = $('#gridSolicitudAprobacion').data('kendoGrid');
    grid.dataSource.pageSize(10);
    grid.dataSource.page(1);
    grid.dataSource.read();
}

function inicializarData() {

    var params = JSON.stringify({
        "numeroSolicitud": $("#txtNumeroSolicitud").val() || '',
        "area": $("#ddlArea").val() || '',
        "responsable": $("#ddlResponsable").val() || '',
        "fechaInicio": $("#txtFechaInicio").val() || '',
        "fechaFin": $("#txtFechaFin").val() || '',
        "estado": $("#ddlEstado").val() || ''
    });
    $("#gridSolicitudAprobacion").kendoGrid({
        dataSource: []
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
            "<td><div class=\"home-buttom\" style=\"margin-right:3px;height:auto !important;height: 40px;\" onclick=\"SetProovedorId('#: idProveedor #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Aprobar</center></div></td>" +
            "</tr>",
        altRowTemplate: "<tr>" +
            "<td>#: Codigo #</td>" +
            "<td>#: DesTipoDocumento #</td>" +
            "<td>#: Documento #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td><div class=\"home-buttom\" style=\"margin-right:3px;height:auto !important;height: 40px;\" onclick=\"SetProovedorId('#: idProveedor #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Aprobar</center></div></td>" +
           "</tr>",
        columns: [
        {
            field: "Codigo",
            title: "Nro Solicitud"
        },
        {
            field: "DesTipoDocumento",
            title: "Fecha Registro"
        }, {
            field: "Documento",
            title: "Responsable"
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
            title: "Aprobar",
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
    var grid = $('#gridSolicitudAprobacion').data('kendoGrid');
    grid.dataSource.pageSize(10);
    grid.dataSource.page(1);
    grid.dataSource.read();



    //AREA
    $("#txtArea").kendoDropDownList({
        filter: "contains",
        dataTextField: "DE_ENTITY",
        dataValueField: "CO_ENTITY",
        //value: "07878195",
        width: '350px',
        value: "",
        dataSource: [{ DE_ENTITY: "VENTAS", CO_ENTITY: "1" }, { DE_ENTITY: "COMPRAS", CO_ENTITY: "2" }]
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
                parameterMap: function (options, operatimason) {
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
   
}

function showDetalleSolicitudRecurso() {

    $('#processingModal').modal('show');

    var params = JSON.stringify({
        "numeroSolicitud": $("#txtNumeroSolicitud").val() || '',
        "area": $("#ddlArea").val() || '',
        "responsable": $("#ddlResponsable").val() || '',
        "fechaInicio": $("#txtFechaInicio").val() || '',
        "fechaFin": $("#txtFechaFin").val() || '',
        "estado": $("#ddlEstado").val() || ''
    });
    $("#gridSolicitudAprobacionModal").kendoGrid({
        dataSource: []/*{
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
        }*/
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
            "<td>#: RazonSocial #</td>" +
            "<td>#: RazonSocial #</td>" +
             "</tr>",
        altRowTemplate: "<tr>" +
            "<td>#: Codigo #</td>" +
            "<td>#: DesTipoDocumento #</td>" +
            "<td>#: Documento #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td>#: RazonSocial #</td>" +
            "</tr>",
        columns: [
        {
            field: "Codigo",
            title: "Item"
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
            title: "Cant. Solicitada",
        }
        ,
        {
            field: "RazonSocial",
            title: "Cant. Stock",
        }, {
            field: "Estado",
            title: "Cant. comprada"
        }, {
            field: "Estado",
            title: "vPrecio"
        },
        {
            field: "Estado",
            title: "SubTotal"
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
    var grid = $('#gridSolicitudAprobacionModal').data('kendoGrid');
    grid.dataSource.pageSize(10);
    grid.dataSource.page(1);
    grid.dataSource.read();

    $('#SolicitudAprobacionModal').modal('show');
}