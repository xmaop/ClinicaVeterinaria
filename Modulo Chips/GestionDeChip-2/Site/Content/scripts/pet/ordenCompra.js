$(document).ready(function () {

    $("#datepickerInicio").kendoDatePicker({
        format: "dd/MM/yyyy"
    });
    $("#datepickerFin").kendoDatePicker({
        format: "dd/MM/yyyy"
    });
    var todayDate = kendo.toString(kendo.parseDate(new Date()), 'dd/MM/yyyy');
    $("#datepickerInicio").data("kendoDatePicker").value(todayDate);
    var todayDate = kendo.toString(kendo.parseDate(new Date()), 'dd/MM/yyyy');
    $("#datepickerFin").data("kendoDatePicker").value(todayDate);

    getProveedores();
    getSelectSolitudes();
    getSelectOC();


    //getCargarOC();
    //getCargarItems();
});

function getOrdenesCompra() {
    var params = JSON.stringify({
        "fechaIni": $('#datepickerInicio').val() || 0,
        "fechaFin": $('#datepickerFin').val() || 0,
        "idProveedor": $('#selectProveedores').val() || "",
        "IsPlanificada": $('#chkPlanificada:checked').length
    });
    Showmodal(true);

    $("#grid").kendoGrid({
        //dataSource: dataSourcePlanCompras(),
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCompras.svc/GetOrdenCompra_Busqueda",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: params,
                        async: false,
                        processData: false,
                        cache: false,
                        success: function (response) {
                            //if (response.messageType == "ERR")
                            //    showError(response.message);
                            //else if (response.messageType == "OK") {
                            //    showError(parseFloat(response.message));

                            //    //$("#txtTotal").val(response.message);
                            //    //$("#rowTotales").show();
                            //}
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
        rowTemplate: "<tr onclick=\"SetIdPlan('#:idOrdenCompra#');\">"
            + "<td>#:idOrdenCompra#</td>"
            + "<td>#:NumeroOrdenCompra#</td>"
            + "<td>#:proveedor.RazonSocial#</td>"
            + "<td>#:Total#</td>"
            + "<td>#:Estado#</td>"
            + "<td>#:TipoOrdenCompra#</td>"
            + "<td><a href='javascript:void(0);' onclick=\"verOrdenCompra(#:idOrdenCompra#);\">Ver</a></td></tr>",
        columns: [{
            field: "idOrdenCompra",
            title: "Nro",
            width: 50
        }, {
            field: "NumeroOrdenCompra",
            title: "Num OC"
        }, {
            field: "proveedor.RazonSocial",
            title: "Proveedor"
        }, {
            field: "Total",
            title: "Total",
            width: 130
        }, {
            field: "Estado",
            title: "Estado",
            width: 130
        }, {
            field: "TipoOrdenCompra",
            title: "Tipo",
            width: 130
        }, {
            template: "<a href='javascript:void(0);' onclick=\"verOrdenCompra(#:idOrdenCompra#);\">Ver</a>",
            field: "idOrdenCompra",
            title: "Ver",
            width: 50
        }]
    });
}

function generarOrdenesCompra() {
    $('#generarOrdenesModal').modal('show');
}

function generarSolicitudes() {
    $('#generarSolicitudesModal').modal('show');
}

function getCargarOC() {
    var params = JSON.stringify({
        "IdPlan": $('#selectOC').val() || 0
    });
    Showmodal(true);

    $("#gridOrdenCompra").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCompras.svc/GetOrdenCompra_Plan",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: params,
                        async: false,
                        processData: false,
                        cache: false,
                        success: function (response) {

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
        height: 280,
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
            field: "idOrdenCompra",
            title: "Nro",
            width: 50
        }, {
            field: "NumeroOrdenCompra",
            title: "Num OC"
        }, {
            field: "proveedor.RazonSocial",
            title: "Proveedor"
        }, {
            field: "Total",
            title: "Total",
            width: 70
        }, {
            template: "<a href='javascript:void(0);' onclick=\"verOrdenCompra(#:idOrdenCompra#);\">Ver</a>",
            field: "idOrdenCompra",
            title: "Ver",
            width: 50
        }]
    });
}

function getCargarItems() {
    var params = JSON.stringify({
        "idSolicitud": $('#selectSolicitudes').val() || 0
    });
    Showmodal(true);

    $("#gridItems").kendoGrid({
        //dataSource: dataSourcePlanCompras(),
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCompras.svc/GetDetalleSolicitudparaOC",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: params,
                        async: false,
                        processData: false,
                        cache: false,
                        success: function (response) {
                            console.log(response);
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
        height: 280,
        groupable: false,
        sortable: true,
        selectable: true,
        resizable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        rowTemplate: "<tr onclick=\"asignarProveedorRecurso('#:presentacionrecurso.idPresentacionRecurso#','#:recurso.descripcion#','#:proveedor.RazonSocial#','#:Cantidad#','#:proveedor.idProveedor#');\">"
            + "<td>#:idItemOrdenCompra#</td>"
            + "<td>#:proveedor.RazonSocial#</td>"
            + "<td>#:recurso.descripcion#</td>"
            + "<td>#:presentacionrecurso.Descripcion#</td>"
            + "<td>#:Cantidad#</td>"
            + "<td>#:Precio#</td>"
            + "<td>#:Total#</td></tr>",
        columns: [{
            field: "idItemOrdenCompra",
            title: "Nro",
            width: 50
        }, {
            field: "proveedor.RazonSocial",
            title: "Proveedor"
        }, {
            field: "recurso.descripcion",
            title: "Recurso"
        }, {
            field: "presentacionrecurso.Descripcion",
            title: "Presentacion"
        }, {
            field: "Cantidad",
            title: "Cantidad",
            width: 70
        }, {
            field: "Precio",
            title: "Precio",
            width: 60
        }, {
            field: "Total",
            title: "Total",
            width: 60
        }]
    });
}

function guardarOrdenCompra() {
    var params = JSON.stringify({
        "idPlan": $('#selectOC').val() || 0
    });

    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCompras.svc/GeneraOrdenessegunPlan",
        dataType: "json",
        data: params,
        async: false,
        processData: false,
        cache: false,
        success: function (data) {
            //console.log(data);
            if (data == "")
                showSuccessMessage('Se guardó correctamente');
            else
                showErrorMessage(data);
        },
        error: function (response) {
            showErrorMessage(response);
            $('#processingModal').modal('hide');
        }
    });

    $('#generarOrdenesModal').modal('hide');
}

function guardarSolicitudesCompra() {

    var params = JSON.stringify({
        "idSolicitud": $('#selectSolicitudes').val() || 0
    });

    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCompras.svc/GeneraOrdenessegunSolicitud",
        dataType: "json",
        data: params,
        async: false,
        processData: false,
        cache: false,
        success: function (data) {
            showSuccessMessage('Se guardó correctamente');
        },
        error: function (response) {
            showErrorMessage(response);
            $('#processingModal').modal('hide');
        }
    });

    $('#generarSolicitudesModal').modal('hide');
}

function getProveedores() {
    var selectProveedores = $('#selectProveedores');
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCompras.svc/GetProveedores",
        dataType: "json",
        async: false,
        processData: false,
        cache: false,
        success: function (data) {
            selectProveedores.append('<option value="">' + '[Todos]' + '</option>');
            for (var i = 0; i < data.length; i++) {
                selectProveedores.append('<option value="' + data[i].idProveedor + '">' + data[i].RazonSocial + '</option>');
            }
            $('#processingModal').modal('hide');
        },
        error: function (response) {
            showErrorMessage(response);
            $('#processingModal').modal('hide');
        }
    });
}

function getSelectSolitudes() {
    var selectSolicitudes = $('#selectSolicitudes');
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCompras.svc/GetSolicitudesPrioridad",
        dataType: "json",
        async: false,
        processData: false,
        cache: false,
        success: function (data) {

            for (var i = 0; i < data.length; i++) {
                selectSolicitudes.append('<option value="' + data[i].idSolicitudRecursos + '">' + data[i].NumSolicitud + '</option>');
            }
            $('#processingModal').modal('hide');
        },
        error: function (response) {
            showErrorMessage(response);
            $('#processingModal').modal('hide');
        }
    });
}

function getSelectOC() {
    var selectOC = $('#selectOC');
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCompras.svc/GetPlanCompraActivos",
        dataType: "json",
        async: false,
        processData: false,
        cache: false,
        success: function (data) {

            for (var i = 0; i < data.length; i++) {
                selectOC.append('<option value="' + data[i].idPlanCompras + '">' + data[i].Periodo + '</option>');
            }
            $('#processingModal').modal('hide');
        },
        error: function (response) {
            showErrorMessage(response);
            $('#processingModal').modal('hide');
        }
    });
}

function verOrdenCompra(idOC) {

    var params = JSON.stringify({
        "IdOrden": idOC || 0
    });
    Showmodal(true);
    console.log(params);

    $("#gridOCModal").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCompras.svc/GetDetalleOrdenCompra_Id",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: params,
                        async: false,
                        processData: false,
                        cache: false,
                        success: function (response) {
                            options.success(response.rows);
                            Showmodal(false);
                        },
                        error: function (err) {
                            console.log(err);
                        }

                    });
                },
                pageSize: 20
            }
        },
        height: 280,
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
            field: "idItemOrdenCompra",
            title: "Nro",
            width: 50
        }, {
            field: "proveedor.RazonSocial",
            title: "Proveedor"
        }, {
            field: "recurso.descripcion",
            title: "Recurso"
        }, {
            field: "Cantidad",
            title: "Cantidad",
            width: 70
        }, {
            field: "Precio",
            title: "Precio",
            width: 60
        }, {
            field: "Total",
            title: "Total",
            width: 60
        }]
    });

    $('#verOCModal').modal('show');
}

function verPlanOrdenCompra(idOC) {

    var params = JSON.stringify({
        "IdOrden": idOC || 0
    });
    Showmodal(true);
    console.log(params);

    $("#gridPlanOCModal").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCompras.svc/GetDetalleOrdenCompra_Id",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: params,
                        async: false,
                        processData: false,
                        cache: false,
                        success: function (response) {
                            options.success(response.rows);
                            Showmodal(false);
                        },
                        error: function (err) {
                            console.log(err);
                        }

                    });
                },
                pageSize: 20
            }
        },
        height: 280,
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
            field: "idItemOrdenCompra",
            title: "Nro",
            width: 50
        }, {
            field: "proveedor.RazonSocial",
            title: "Proveedor"
        }, {
            field: "recurso.descripcion",
            title: "Recurso"
        }, {
            field: "Cantidad",
            title: "Cantidad",
            width: 70
        }, {
            field: "Precio",
            title: "Precio",
            width: 60
        }, {
            field: "Total",
            title: "Total",
            width: 60
        }]
    });

    $('#verPlanOCModal').modal('show');
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

function SetIdPlan(id) {
    //console.log(id);
    $("#selectrow").val(id);

    //showError($("#selectrow").val());
}

function SetEdit() {
    var params = JSON.stringify({
        "idOrden": $("#selectrow").val() || "0",
    });
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCompras.svc/GetDatosCabeceraOrden",
        dataType: "json",
        data: params,
        async: false,
        processData: false,
        cache: false,
        success: function (response) {
            if (response == '') {
                generarSolicitudes();
                //OpenPage('/Planificacion/pgActualizarPlanCompras.html');
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
                                showSuccessMessage('Se actualizó correctamente');

                            }
                            options.success(response.rows);
                            Showmodal(false);
                            getCargarItems();
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

function verDetalleOC() {
    var params = JSON.stringify({
        "idSolicitud": $('#selectSolicitudes').val()
    });

    //$.ajax({
    //    type: "POST",
    //    contentType: 'application/json; charset=utf-8',
    //    url: wsnode + "wsCompras.svc/GetOrdenesporSolicitud",
    //    dataType: "json",
    //    data: params,
    //    async: false,
    //    processData: false,
    //    cache: false,
    //    success: function (data) {
    //        //console.log(data);
    //        var array = JSON.parse(data);
    //        console.log(array);

    //        $("#gridItemsDetalle").kendoGrid({
    //            dataSource: {
    //                data: data.rows,
    //                group: { field: "NumeroOrden" } // set grouping for the dataSource
    //            },
    //            groupable: false, // this will remove the group bar
    //            sortable: true,
    //            columns: ["Estado", "Precio", "Total", "presentacionrecurso.Descripcion", "proveedor.RazonSocial"]
    //        });

    //        //for (var i = 0; i < array.rows.length; i++) {
    //        //    console.log(array.rows[i].Cantidad);
    //        //    //selectOC.append('<option value="' + data[i].idPlanCompras + '">' + data[i].Periodo + '</option>');
    //        //}
    //        $('#processingModal').modal('hide');
    //    },
    //    error: function (response) {
    //        showErrorMessage(response);
    //        $('#processingModal').modal('hide');
    //    }
    //});


    /*******************************************/
    $("#gridItemsDetalle").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: wsnode + "wsCompras.svc/GetOrdenesporSolicitud",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: params,
                        async: false,
                        processData: false,
                        cache: false,
                        success: function (response) {

                            var array = JSON.parse(response);
                            console.log(array);
                            options.success(array.rows);
                            Showmodal(false);
                        },
                        error: function (err) {
                        }

                    });
                },
                pageSize: 20
            },
            group: [{ field: "NumeroOrden" }, { field: "proveedor.RazonSocial" }, { field: "Total_Final" }]
        },
        height: 350,
        groupable: false,
        sortable: true,
        selectable: true,
        resizable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
        },
        //columns: ["Estado", "Precio", "Total", "presentacionrecurso.Descripcion", "proveedor.RazonSocial"]
        columns: [{
            field: "idItemOrdenCompra",
            title: "Nro",
            width: 50
        }, {
            field: "presentacionrecurso.Descripcion",
            title: "Presentacion"
        }, {
            field: "Cantidad",
            title: "Cantidad"
        }, {
            field: "Precio",
            title: "Precio"
        }, {
            field: "Total",
            title: "Total",
            width: 70
        }]
    });

    $('#DetalleOCModal').modal('show');
}

