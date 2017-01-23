function guardarProveedor() {
    
    if ($('#selectTipoDocumentoAdd').val() == '' ||
        $('#txtNumeroDocumentoAdd').val() == '' ||
        $('#txtRazonSocialAdd').val() == '' 
        //||
        //$('#txtDireccionAdd').val() == ''
        //||
        //$('#txtTelefonoAdd').val() == '' ||
        //$('#txtContactoAdd').val() == ''
    )
    {

        showError('Ingrese los campos obligatorios');
        return false;
    }

    var params = JSON.stringify({
        "tipoDocumento": $('#selectTipoDocumentoAdd').val() || "",
        "numeroDocumento": $('#txtNumeroDocumentoAdd').val() || "",
        "razonSocial": $('#txtRazonSocialAdd').val() || "",
        "direccion": $('#txtDireccionAdd').val() || "",
        "telefono": $('#txtTelefonoAdd').val() || "",
        "contacto": $('#txtContactoAdd').val() || "",
        "puntaje":50
    });

    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCompras.svc/GeneraProveedor",
        dataType: "json",
        data: params,
        async: false,
        processData: false,
        cache: false,
        success: function (response) {
            $('#pages').append(response);
        },
        error: function (response) {
            showError(response);
        }
    })
}

function actualizarProveedor() {

    var params = JSON.stringify({
        
        "tipoDocumento": $('#selectTipoDocumentoEdit').val() || "",
        "numeroDocumento": $('#txtNumeroDocumentoEdit').val() || "",
        "razonSocial": $('#txtRazonSocialEdit').val() || "",
        "direccion": $('#txtDireccionEdit').val() || "",
        "idProveedor": $('#hIdProveedor').val() || "",
        "telefono": $('#txtTelefonoEdit').val() || "",
        "contacto": $('#txtContactoEdit').val() || "",
        "estado": $('#selectEstadoEdit').val() || "",
        "puntaje": 50
    });

    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCompras.svc/ActualizarProveedor",
        dataType: "json",
        data: params,
        async: false,
        processData: false,
        cache: false,
        success: function (response) {
            $('#pages').append(response);
        },
        error: function (response) {
            showError(response);
        }
    })
}

function dataSourceProveedores() {
    var params = JSON.stringify({
        "codigoProveedor": $("#txtCodigoProveedor").val() || '',
        "nombreProveedor": $("#txtNombreProveedor").val() || ''
    });

    var dataSource = new kendo.data.DataSource({
        batch: true,
        transport: {
            read: {
                type: "POST",
                url: wsnode + "wsCompras.svc/GetProveedores_Busqueda",
                contentType: "application/json; charset=utf-8",
                dataType: 'json'
            },
            parameterMap: function (options, operation) {
                return params;
            },
            pageSize: 10
        }
    });

    return dataSource;
}
 

function SetIdProovedor(idProveedor, RazonSocial) {

    $("#selectrow").val(idProveedor);
    $("#selectestado").val(idProveedor);

  
}

var proveedor = {};

function SetProovedorId(idProveedor)
{
    $('#ProveedorModal').modal("show");
    //Clean
    $('#ProveedorModal_header').empty();
    $('#ProveedorModal_footer').empty();
    $('#divCodigoPopup').empty();
    $('#divCodigoPopup').append("<span class=\"input-group-addon\" style=\"width:130px\">Código</span>" +
                            "<input type=\"text\" class=\"form-control input-lg text-uppercase\" id=\"txtCodigoPopup\" />");
    //Add Controls
    $('#ProveedorModal_header').append(
        "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">×</button>" +
        "&nbsp;<i class=\"fa fa-pencil\">&nbsp;&nbsp;</i><label class=\"label-md modal-title custom_align\"> Modificar Proveedor</label>"
    );

    $('#ProveedorModal_footer').append(
        "<div class=\"col-xs-12 \">" +
        "<div class=\"col-xs-9\"></div>" +
        "<div class=\"col-xs-3 home-buttom\" onclick=\"actualizarProveedor(); return false;\" data-bb-handler=\"danger\" data-dismiss=\"modal\">" +
        "    <center><i class=\"fa fa-save\">&nbsp;&nbsp;</i> Guardar</center>" +
        "</div></div>"
    );
}

function SetProovedor(idProveedor, RazonSocial, Direccion, Puntaje, Estado, TipoDocumento, NumeroDocumento, Telefono, Contacto) {

    $("#selectrow").val(idProveedor);
    $("#selectEstadoEdit").val(Estado);
    $('#hIdProveedor').val(idProveedor)
    $("#selectTipoDocumentoEdit").val(TipoDocumento);
    $("#txtNumeroDocumentoEdit").val(NumeroDocumento);
    $("#txtRazonSocialEdit").val(RazonSocial);
    $("#txtDireccionEdit").val(Direccion);
    $("#txtTelefonoEdit").val(Telefono);
    $("#txtContactoEdit").val(Contacto);
    //showError($("#selectrow").val());

    $('#editarProveedorModal').modal('show');
}

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
            $('#txtRazonSocial').val(data.RazonSocial);
            $('#txtDireccion').val(data.Direccion);
        },
        error: function (response) {
            $('#processingModal').modal('hide');
            showErrorMessage(response);
        }
    });

}

function ChangeEstado()
{
    if ($("#imgEstadoPopup").attr('src') == "../Content/images/uncheck.png")
        $("#imgEstadoPopup").attr("src", "../Content/images/check.png");
    else
        $("#imgEstadoPopup").attr("src", "../Content/images/uncheck.png");
}

function getProveedores() {

    $('#processingModal').modal('show');

    var params = JSON.stringify({
        "codigoProveedor": $("#txtCodigoProveedor").val() || '',
        "nombreProveedor": $("#txtNombreProveedor").val() || ''
    });
    //dataSource: dataSourceProveedores()
    $("#gridProveedor").kendoGrid({
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
                        Showmodal(false);
                        $("#selectrow").val(0);
                        $("#selectestado").val("");
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
            "<td>#: Estado #</td>" +
            "<td><div class=\"home-buttom\" style=\"margin-right:3px;height:auto !important;height: 40px;\" onclick=\"SetProovedorId('#: idProveedor #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Modificar</center></div></td>" +
            "</tr>",
        altRowTemplate: "<tr>" +
            "<td>#: Codigo #</td>" +
            "<td>#: DesTipoDocumento #</td>" +
            "<td>#: Documento #</td>" +
            "<td>#: RazonSocial #</td>" +
            "<td>#: Estado #</td>" +
            "<td><div class=\"home-buttom\" style=\"margin-right:3px;height:auto !important;height: 40px;\" onclick=\"SetProovedorId('#: idProveedor #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Modificar</center></div></td>" +
            "</tr>",
        columns: [
        {
            field: "Codigo",
            title: "Código",
            width: 120
        },
        {
            field: "DesTipoDocumento",
            title: "Tipo de Documento",
            width: 120
        }, {
            field: "Documento",
            title: "Nro Documento",
            width: 120
        },
        {
            field: "RazonSocial",
            title: "Razón Social",
        }, {
            field: "Estado",
            title: "Estado",
            width: 80
        }, {
            title: "Acciones",
            width: 160
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
    // });
}

function nuevoProveedor() {
    $('#ProveedorModal').modal("show");
    //Clean
    $('#ProveedorModal_header').empty();
    $('#ProveedorModal_footer').empty();
    $('#divCodigoPopup').empty();    
    //Add Controls
    $('#ProveedorModal_header').append(
        "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">×</button>" +
        "&nbsp;<i class=\"fa fa-male\">&nbsp;&nbsp;</i><label class=\"label-md modal-title custom_align\"> Nuevo Proveedor</label>"
    );    
    
    $('#ProveedorModal_footer').append(
        "<div class=\"col-xs-12 \">" +
        "<div class=\"col-xs-9\"></div>" +
        "<div class=\"col-xs-3 home-buttom\" onclick=\"actualizarProveedor(); return false;\" data-bb-handler=\"danger\" data-dismiss=\"modal\">" +
        "    <center><i class=\"fa fa-save\">&nbsp;&nbsp;</i> Guardar</center>" +
        "</div></div>"
    );
}

function editarProveedor() {
    $('#ProveedorModal').modal("show");
    //Clean
    $('#ProveedorModal_header').empty();
    $('#ProveedorModal_footer').empty();
    //$('#divCodigoPopup').empty();
    //Add Controls
    $('#ProveedorModal_header').append(
        "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">×</button>" +
        "&nbsp;<i class=\"fa fa-male\">&nbsp;&nbsp;</i><label class=\"label-md modal-title custom_align\"> Modificar Proveedor</label>"
    );

    $('#ProveedorModal_footer').append(
        "<div class=\"col-xs-12 \">" +
        "<div class=\"col-xs-9\"></div>" +
        "<div class=\"col-xs-3 home-buttom\" onclick=\"actualizarProveedor(); return false;\" data-bb-handler=\"danger\" data-dismiss=\"modal\">" +
        "    <center><i class=\"fa fa-save\">&nbsp;&nbsp;</i> Guardar</center>" +
        "</div></div>"
    );
    /*
    if ($("#selectrow").val() == 0) {
        $('#sinSeleccionModal').modal('show');
    }else{
        $('#editarProveedorModal').modal('show');
    }
    */
}

function confimarEliminacion(idProveedor) {
    if ($("#selectrow").val()) {
        $('#sinSeleccionModal').modal('show');
    } else {
        $('#confirmarEliminacionModal').modal('show');
    }
}

var idSeleccionado = '';
function eliminarProveedor() {

    console.log($("#selectestado").val());

    var params = JSON.stringify({
        "idProveedor": idSeleccionado || "0",
        "estado": $("#selectestado").val() || ""
    });

    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCompras.svc/DeleteProveedor",
        dataType: "json",
        data: params,
        async: false,
        processData: false,
        cache: false,
        success: function (response) {
            var array = response.split('-');
            if (array[0] != 'ERR') {
                showSuccess('Se eliminó correctamente.');
                getProveedores();
                $("#selectrow").val(0);
                $("#selectestado").val("");
                //OpenPage('/Planificacion/pgGestionPlanCompra.html');
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


function trash(idProveedor) {

    idSeleccionado = idProveedor;
    $('#eliminarProveedorModal').modal('show');

}

function SetEdit() {
    var params = JSON.stringify({
        "idProveedor": $("#selectrow").val() || "0",
    });

    //console.log($("#selectrow").val());
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCompras.svc/GetProveedor",
        dataType: "json",
        data: params,
        async: false,
        processData: false,
        cache: false,
        success: function (data) {
            $('#txtRazonSocialEdit').val(data.RazonSocial);
            $('#txtDireccionEdit').val(data.Direccion);
            $('#hIdProveedor').val(data.idProveedor);

            $('#selectTipoDocumentoEdit').val(data.TipoDocumento);
            $('#txtNumeroDocumentoEdit').val(data.NumeroDocumento);
            $('#txtTelefonoEdit').val(data.Telefono);
            $('#txtContactoEdit').val(data.Contacto);

            editarProveedor();
            //OpenPage('/Planificacion/pgActualizarPlanCompras.html');
        },
        error: function (response) {
            //console.log("1");
            //alert("3");
            showError(response);
        }
    });
}
