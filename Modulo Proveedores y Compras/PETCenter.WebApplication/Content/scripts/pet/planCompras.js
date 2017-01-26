var itemSelected = '';

$(document).ready(function () {
    // create DatePicker from input HTML element
    $("#datepicker").kendoDatePicker({
        format: "dd/MM/yyyy"
    });

    $("#monthpicker").kendoDatePicker({
        // defines the start view
        start: "year",

        // defines when the calendar should return date
        depth: "year",

        // display month and year in the input
        format: "'dd/MM/yyyy'"
    });
    getPeriodo();
});

function getPeriodo() {
    $('#processingModal').modal('show');
    var selectPeriodo = $('#selectPeriodo');
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCompras.svc/GetPeriodoAnio",
        dataType: "json",
        async: false,
        processData: false,
        cache: false,
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                if (i == 0) {
                    selectPeriodo.append('<option value="' + data[i].Codigo + '" selected>' + data[i].Descripcion + '</option>');
                } else
                {
                selectPeriodo.append('<option value="' + data[i].Codigo + '">' + data[i].Descripcion + '</option>');
            }
            }
            getPlanCompras();
            $('#processingModal').modal('hide');
        },
        error: function (response) {
            showErrorMessage(response);
            $('#processingModal').modal('hide');
        }
    });
}

function dataSourcePlanCompras() {
    var params = JSON.stringify({
        "anio":  $("#selectPeriodo").val()  || ''
    });

    var dataSource = new kendo.data.DataSource({
        batch: true,
        transport: {
            read: {
                type: "POST",
                url: wsnode + "wsCompras.svc/GetPlanCompraAnio",
                contentType: "application/json; charset=utf-8",
                dataType: 'json'
            },
            parameterMap: function (options, operation) {
                return params;
            },
            pageSize: 10,
        }
    });

    return dataSource;
}

function getPlanCompras() {

    $("#grid").kendoGrid({
        dataSource: dataSourcePlanCompras(),
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
            "<td onclick=\"SetIdPlan('#: idPlanCompras #');return false;\">#: idPlanCompras #</td>" +
            "<td onclick=\"SetIdPlan('#: idPlanCompras #');return false;\">#= kendo.toString(kendo.parseDate(Fecha, 'dd-MM-yyyy'), 'dd/MM/yyyy') #</td>" +
            "<td onclick=\"SetIdPlan('#: idPlanCompras #');return false;\">#: UsuarioResponsable #</td>" +
            "<td onclick=\"SetIdPlan('#: idPlanCompras #');return false;\">#: Periodo #</td>" +
            "<td onclick=\"SetIdPlan('#: idPlanCompras #');return false;\">#: Estado #</td>" +
            "</tr>",
        altRowTemplate: "<tr>" +
            "<td onclick=\"SetIdPlan('#: idPlanCompras #');return false;\">#: idPlanCompras #</td>" +
            "<td onclick=\"SetIdPlan('#: idPlanCompras #');return false;\">#= kendo.toString(kendo.parseDate(Fecha, 'dd-MM-yyyy'), 'dd/MM/yyyy') #</td>" +
            "<td onclick=\"SetIdPlan('#: idPlanCompras #');return false;\">#: UsuarioResponsable #</td>" +
            "<td onclick=\"SetIdPlan('#: idPlanCompras #');return false;\">#: Periodo #</td>" +
            "<td onclick=\"SetIdPlan('#: idPlanCompras #');return false;\">#: Estado #</td>" +
            "</tr>",
        columns: [{
            field: "idPlanCompras",
            title: "Nro",
            width: 240
        }, {
            field: "Fecha",
            title: "Fecha Registro"
        }, {
            field: "UsuarioResponsable",
            title: "Responsable"
        }, {
            field: "Periodo",
            title: "Periodo"
        }, {
            field: "Estado",
            title: "Estado",
            width: 150
        }]
    });
    // });
}

/****************
    ACTUALIZAR PLAN
****************/
function loadInitialData() {
    //$('#processingModal').modal('show');
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCompras.svc/GetPresupuestoPendiente",
        dataType: "json",
        async: false,
        processData: false,
        cache: false,
        success: function (data) {
            $('#txtMonto').val(data.Monto);
            $('#txtPeriodo').val(data.Periodo);
            $('#txtEstado').val(data.Estado);
            $('#processingModal').modal('hide');
        },
        error: function (response) {            
            $('#processingModal').modal('hide');
            showErrorMessage(response);
        }
    });

    var d = new Date();

    var month = d.getMonth() + 1;
    var day = d.getDate();

    //var output = d.getFullYear() + '/' +
    //    (month < 10 ? '0' : '') + month + '/' +
        (day < 10 ? '0' : '') + day;

        $('#txtFechaRegistro').val((day < 10 ? '0' : '') + day + '/' + (month < 10 ? '0' : '') + month + '/' + d.getFullYear());
        $('#txtEstado').val('En Proceso de Registro');
}

function EditInitialData() {

    var params = JSON.stringify({
        "id_plan": $("#selectrow").val() || 0

    });
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCompras.svc/GetPlanCompraId",
        dataType: "json",
        data: params,
        async: false,
        processData: false,
        cache: false,
        success: function (data) {
            $('#txtMonto').val(data.Presupuesto);
            $('#txtPeriodo').val(data.Periodo);
            $('#txtEstado').val(data.Estado);
            if ($('#txtEstado').val().toUpperCase() == "EJECUTADO") {
                showError('Ud. no puede realizar esta acción cuando el Periodo de Compra fue Ejecutado');
                //OpenPage('/Planificacion/pgGestionPlanCompra.html');
            }
            $('#processingModal').modal('hide');
            getSolicitudes();
        },
        error: function (response) {
            $('#processingModal').modal('hide');
            showErrorMessage(response);
        }
    });


    var params = JSON.stringify({
        "idplan": $("#selectrow").val() || 0,
        "usuario": $('#selectResponsables').val() || ""

    });
    Showmodal(true);
    //var record = 0;
    $("#gridItems").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
    $.ajax({
        type: "POST",
                        url: wsnode + "wsCompras.svc/GetTemporalItemsPlanID",
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
                                //showError(parseFloat(response.message));
                                //if (parseFloat(response.message) > $parseFloat($("#txtMonto").val()))
                                //    document.getElementById("txtMonto").style.color = 'red';
                                //else
                                //    document.getElementById("txtMonto").style.color = 'green';
                                $("#txtTotal").val(response.message);
                                $("#rowTotales").show();
                            }
                            options.success(response.rows);
                            Showmodal(false);
        },
                        error: function (err) {
                        }

                    });
                },
                pageSize: 20
            }
        },
        selectable: "row",
        navigatable: false,
        height: 340,
        resizable: true,
        pageable: {
            refresh: true,
            pageSize: 20,
            pageSizes: [20, 40, 60]
        },
        rowTemplate: "<tr onclick=\"asignarProveedorRecurso('#:IdPresentacionRecurso#','#:DescripcionRecurso#','#:RazonSocialProveedor#','#:Cantidad#','#:IdProveedor#');\"><td>#:IdItemPlanCompras#</td><td>#:RazonSocialProveedor#</td><td>#:DescripcionRecurso#</td><td>#:DescripcionPresentacionRecurso#</td><td>#:Precio#</td><td>#:Cantidad#</td><td>#:Total#</td></tr>",
        columns: [{
            field: "IdItemPlanCompras",
            title: "Item",
            template: "<span class='row-number'></span>",
            width: 40
        }, {

            field: "RazonSocialProveedor",
            title: "Proveedor",
            width: 150
        }, {
            field: "DescripcionRecurso",
            title: "Recurso",
            width: 120
        }, {
            field: "DescripcionPresentacionRecurso",
            title: "Presentación",
            width: 200
        }, {
            field: "Precio",
            title: "Precio Uni."
        }, {
            field: "Cantidad",
            title: "Cantidad"
        }, {
            field: "Total",
            title: "Total"
        }],
        dataBound: function () {
            var rows = this.items();
            $(rows).each(function () {
                var index = $(this).index() + 1;
                //+ ($("#gridItems").data("kendoGrid").dataSource.pageSize() * ($("#grid").data("kendoGrid").dataSource.page() - 1));;
                var rowLabel = $(this).find(".row-number");
                $(rowLabel).html(index);
            });
        }
    });

    
}

function getResponsables() {
    //$('#processingModal').modal('show');
    var selectResponsables = $('#selectResponsables');
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCompras.svc/GetResponsablesActivos",
        dataType: "json",
        async: false,
        processData: false,
        cache: false,
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                selectResponsables.append('<option value="' + data[i].Codigo + '">' + data[i].Nombre + '</option>');
            }
            $('#processingModal').modal('hide');
        },
        error: function (response) {
            showErrorMessage(response);
            $('#processingModal').modal('hide');
        }
    });
}

function getResponsablesEdit() {
    //$('#processingModal').modal('show');
    var selectResponsables = $('#selectResponsables');
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCompras.svc/GetResponsablesActivos",
        dataType: "json",
        async: false,
        processData: false,
        cache: false,
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                selectResponsables.append('<option value="' + data[i].Codigo + '">' + data[i].Nombre + '</option>');
            }
            $('#processingModal').modal('hide');
            EditInitialData();
        },
        error: function (response) {
            showErrorMessage(response);
            $('#processingModal').modal('hide');
        }
    });
}


function dataSourceSolicitudes() {
    var params = JSON.stringify({
        "periodo":  $("#txtPeriodo").val() || ''
    });

    var dataSource = new kendo.data.DataSource({
        batch: true,
        transport: {
            read: {
                type: "POST",
                url: wsnode + "wsCompras.svc/GetSolicitudRecursosPeriodo",
                contentType: "application/json; charset=utf-8",
                dataType: 'json'
            },
            parameterMap: function (options, operation) {
                return params;
            },
            pageSize: 10,
        }
    });

    return dataSource;
}

function getSolicitudes() {

    $("#gridSolicitudes").kendoGrid({
        dataSource: dataSourceSolicitudes(),
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
            columns: [{
                template: "<div style='color:white;background-color:#:Estado#;'><b><center>#:NumSolicitud#</center></b></div>",
                field: "NumSolicitud",
                title: "Nro Solicitud",
                width: 100
        }, {
            field: "Fecha",
            title: "Fecha",
            template: "#= kendo.toString(kendo.parseDate(Fecha, 'dd-MM-yyyy'), 'dd/MM/yyyy') #",
            width: 120
        }, {
            field: "Area",
            title: "Area",
            width: 150
        }, {
            template: "<a href='javascript:void(0);' onclick=\"getItemsSolicitud(#:idSolicitudRecursos#);\">Ver</a>",
            field: "CustomerID",
            title: "Ver",
            width: 50
        }]

        
    });
}

function dataSourceItems() {
    var params = JSON.stringify({
        "periodo": $("#txtPeriodo").val() || ''
    });

    var dataSource = new kendo.data.DataSource({
        batch: true,
        transport: {
            read: {
                type: "POST",
                url: wsnode + "wsCompras.svc/GetItemsGroupSolicitudRecursosPeriodo",
                contentType: "application/json; charset=utf-8",
                dataType: 'json'
            },
            parameterMap: function (options, operation) {
                return params;
            },
            pageSize: 10,
        }
    });

    return dataSource;
}

function getItems() {
    if ($('#txtEstado').val().toUpperCase() == "EJECUTADO") {
        showError('Ud. no puede realizar esta acción cuando el Periodo de Compra fue Ejecutado');
        return false;
    }
    var params = JSON.stringify({
        "periodo": $("#txtPeriodo").val() || '',
        "usuario": $('#selectResponsables').val() || ""
    });
    Showmodal(true);
    //var record = 0;
    $("#gridItems").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCompras.svc/GetItemsGroupSolicitudRecursosPeriodo",
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
                                //showError(parseFloat(response.message));
                                //if (parseFloat(response.message) > $parseFloat($("#txtMonto").val()))
                                //    document.getElementById("txtMonto").style.color = 'red';
                                //else
                                //    document.getElementById("txtMonto").style.color = 'green';
                                $("#txtTotal").val(response.message);
                                $("#rowTotales").show();
                            }
                            options.success(response.rows);
                            Showmodal(false);
                        }
                    });
                },
                pageSize: 20
            }
        },
        selectable: "row",
        navigatable: false,
        height: 340,
        resizable: true,
        pageable: {
            refresh: true,
            pageSize: 20,
            pageSizes: [20, 40, 60]
        },
        rowTemplate: "<tr onclick=\"asignarProveedorRecurso('#:IdPresentacionRecurso#','#:DescripcionRecurso#','#:RazonSocialProveedor#','#:Cantidad#','#:IdProveedor#');\"><td>#:IdItemPlanCompras#</td><td>#:RazonSocialProveedor#</td><td>#:DescripcionRecurso#</td><td>#:DescripcionPresentacionRecurso#</td><td>#:Precio#</td><td>#:Cantidad#</td><td>#:Total#</td></tr>",
        columns: [{
            field: "IdItemPlanCompras",
            title: "Item",
            template: "<span class='row-number'></span>",
            width: 40
        }, {

            field: "RazonSocialProveedor",
            title: "Proveedor",
            width: 150
        }, {
            field: "DescripcionRecurso",
            title: "Recurso",
            width: 120
        }, {
            field: "DescripcionPresentacionRecurso",
            title: "Presentación",
            width: 200
        }, {
            field: "Precio",
            title: "Precio Uni."
        }, {
            field: "Cantidad",
            title: "Cantidad"
        }, {
            field: "Total",
            title: "Total"
        }],
        dataBound: function () {
            var rows = this.items();
            $(rows).each(function () {
                var index = $(this).index() + 1;
                //+ ($("#gridItems").data("kendoGrid").dataSource.pageSize() * ($("#grid").data("kendoGrid").dataSource.page() - 1));;
                var rowLabel = $(this).find(".row-number");
                $(rowLabel).html(index);
            });
        }
    });
    // });
}

function dataSourceItemsSolicitud(idSolicitudRecursos) {
    var params = JSON.stringify({
        "id": idSolicitudRecursos || 0,
    });

    var dataSource = new kendo.data.DataSource({
        batch: true,
        transport: {
            read: {
                type: "POST",
                url: wsnode + "wsCompras.svc/GetItemsGroupSolicitudRecursosId",
                contentType: "application/json; charset=utf-8",
                dataType: 'json'
            },
            parameterMap: function (options, operation) {
                return params;
            },
            pageSize: 10,
        }
    });

    return dataSource;
}

function getItemsSolicitud(idSolicitudRecursos) {
    
    var record = 0;
    $("#gridItemsSolicitud").kendoGrid({
        dataSource: dataSourceItemsSolicitud(idSolicitudRecursos),
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
        columns: [{
            field: "DescripcionRecurso",
            title: "Recurso"
        }, {
            field: "DescripcionPresentacionRecurso",
            title: "Presentación"
        },{
            field: "Cantidad",
            title: "Cantidad",
            width: 90
        }],
        dataBound: function () {
            var rows = this.items();
            $(rows).each(function () {
                var index = $(this).index() + 1;
                //+ ($("#gridItems").data("kendoGrid").dataSource.pageSize() * ($("#grid").data("kendoGrid").dataSource.page() - 1));;
                var rowLabel = $(this).find(".row-number");
                $(rowLabel).html(index);
            });
        }
    });

    $('#verItemsSolicitudModal').modal('show');
}

function saveActualizarPlan() {

    if ($('#txtEstado').val().toUpperCase() == "EJECUTADO")
    {
        showError('Ud. no puede realizar esta acción cuando el Periodo de Compra fue Ejecutado');
        return false;
    }

    var params = JSON.stringify( {
        "usuario":  $('#selectResponsables').val()  || "",
        "fecha":  $('#txtFechaRegistro').val() ,
        "periodo": $('#txtPeriodo').val() || ""
    });

    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCompras.svc/SavePlanCompra",
        dataType: "json",
        data: params,
        async: false,
        processData: false,
        cache: false,
        success: function (response) {
            var array = response.split('-');
            if (array[0] != 'ERR') {
                showSuccess(array[1]);
                OpenPage('/Planificacion/pgGestionPlanCompra.html');
            }
            else {
                showError(array[1]);
            }
            
            //$("#outMessage").append(response);
            //Showmodal(false);
        },
        error: function (response) {
            showError(response);
            //Showmodal(false);

        }
    })
}

function SetIdPlan(id)
{
    console.log(id);
    $("#selectrow").val(id);
    //showError($("#selectrow").val());
}

function SetEdit() {
    var id = $("#selectrow").val();
    //showError($("#selectrow").val());
    if (id == 0)
        showError('Ud. debe seleccionar un registro para modificar o consultar un plan de compras');
    else {
        OpenPage('/Planificacion/pgActualizarPlanCompras.html'); return false;
    }

}

function SetNew() {

    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCompras.svc/GetPlanCompraVigente",
        dataType: "json",
        async: false,
        processData: false,
        cache: false,
        success: function (response) {
            if (response == '') {
                OpenPage('/Planificacion/pgActualizarPlanCompras.html');
            }
            else {
                showError(response);
            }
            //OpenPage('/Planificacion/pgActualizarPlanCompras.html');
        },
        error: function (response) {
            showError(response);
        }
    });
}

// variables para guardar 
var idPresentacionSave = 0;
var idProveedorSave = 0;

function asignarProveedorRecurso(idPresentacionRecurso, recursoSeleccionado, proveedorAsignado, cantidad, idProveedor) {
    idPresentacionSave = 0;
    idProveedorSave = 0;
    idPresentacionSave = idPresentacionRecurso;
    //idProveedorSave = idProveedor;

    $('#lblRecursoAsignado').text('Recurso: ' + recursoSeleccionado);
    $('#lblProveedorAsignado').text('Proveedor: ' + proveedorAsignado);
    
    var params = JSON.stringify({
        "idpresentacion": idPresentacionRecurso || 0,
        "cantidad": cantidad || 0,
    });
    Showmodal(true);
    //var record = 0;
    $("#gridProveedorRecurso").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCompras.svc/GetPresentacionRecursosProveedor",
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
                                
                            }
                            options.success(response);
                            Showmodal(false);
                        }
                    });
                },
                pageSize: 20
            }
        },
        selectable: "row",
        navigatable: false,
        height: 220,
        resizable: true,
        pageable: {
            refresh: true,
            pageSize: 20,
            pageSizes: [20, 40, 60]
        },
        rowTemplate: "<tr onclick=\"idProveedorSave = #:proveedor.idProveedor#;\"><td>#:proveedor.idProveedor#</td><td>#:proveedor.RazonSocial#</td><td>#:presentacionRecurso.Descripcion#</td><td>#:PrecioUnitario#</td><td>#:Cantidad#</td><td>#:PrecioTotal#</td></tr>",
        columns: [{
            field: "proveedor.idProveedor",
            title: "Id",
           // template: "<span class='row-number'></span>",
            width: 40
        }, {

            field: "proveedor.RazonSocial",
            title: "Proveedor"
        }, {
            field: "presentacionRecurso.Descripcion",
            title: "Presentación"
        }, {
            field: "PrecioUnitario",
            title: "Precio Uni.",
            width: 90
        }, {
            field: "Cantidad",
            title: "Cantidad",
            width: 90
        }, {
            field: "PrecioTotal",
            title: "Precio Total",
            width: 90
        }],
        dataBound: function () {
            var rows = this.items();
            $(rows).each(function () {
                var index = $(this).index() + 1;
                //var rowLabel = $(this).find(".row-number");
                //$(rowLabel).html(index);
            });
        }
    });

    $('#asignarProveedorRecursoModal').modal('show');    
}

function saveProveedorRecurso() {

    var params = JSON.stringify({
        "idproveedor": idProveedorSave || 0,
        "idpresentacion": idPresentacionSave || 0,
        "usuario": $('#selectResponsables').val() || ""
    });
    Showmodal(true);
    //var record = 0;
    $("#gridItems").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCompras.svc/GetTemporalItemsPlan",
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
                                //showError(parseFloat(response.message));
                                //if (parseFloat(response.message) > $parseFloat($("#txtMonto").val()))
                                //    document.getElementById("txtMonto").style.color = 'red';
                                //else
                                //    document.getElementById("txtMonto").style.color = 'green';
                                $("#txtTotal").val(response.message);
                                $("#rowTotales").show();
                            }
                            options.success(response.rows);
                            Showmodal(false);
                        },
                        error: function (err) {
                        }
                        
                    });
                },
                pageSize: 20
            }
        },
        selectable: "row",
        navigatable: false,
        height: 340,
        resizable: true,
        pageable: {
            refresh: true,
            pageSize: 20,
            pageSizes: [20, 40, 60]
        },
        rowTemplate: "<tr onclick=\"asignarProveedorRecurso('#:IdPresentacionRecurso#','#:DescripcionRecurso#','#:RazonSocialProveedor#','#:Cantidad#','#:IdProveedor#');\"><td>#:IdItemPlanCompras#</td><td>#:RazonSocialProveedor#</td><td>#:DescripcionRecurso#</td><td>#:DescripcionPresentacionRecurso#</td><td>#:Precio#</td><td>#:Cantidad#</td><td>#:Total#</td></tr>",
        columns: [{
            field: "IdItemPlanCompras",
            title: "Item",
            template: "<span class='row-number'></span>",
            width: 40
        }, {

            field: "RazonSocialProveedor",
            title: "Proveedor",
            width: 150
        }, {
            field: "DescripcionRecurso",
            title: "Recurso",
            width: 120
        }, {
            field: "DescripcionPresentacionRecurso",
            title: "Presentación",
            width: 200
        }, {
            field: "Precio",
            title: "Precio Uni."
        }, {
            field: "Cantidad",
            title: "Cantidad"
        }, {
            field: "Total",
            title: "Total"
        }],
        dataBound: function () {
            var rows = this.items();
            $(rows).each(function () {
                var index = $(this).index() + 1;
                //+ ($("#gridItems").data("kendoGrid").dataSource.pageSize() * ($("#grid").data("kendoGrid").dataSource.page() - 1));;
                var rowLabel = $(this).find(".row-number");
                $(rowLabel).html(index);
            });
        }
    });
}



