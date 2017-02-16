function guardarProveedor() {
    var id = $('#txtCodigoPopup').val() == null ? "" : $('#txtCodigoPopup').val();
    var params = JSON.stringify({
        "tipoDocumento": $('#selTipoDocumentoPopup').val() || "",
        "numeroDocumento": $('#txtNroDocumentoPopup').val() || "",
        "razonSocial": $('#txtRazonSocialPopup').val() || "",
        "direccion": $('#txtDirecccionPopup').val() || "",
        "telefono": $('#txtTelefonoPopup').val() || "",
        "contacto": $('#txtContactoPopup').val() || "",
        "estado": $("#imgEstadoPopup").attr('src') || "",
        "idproveedor": id || ""
    });

    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsCompras.svc/GuardarProveedor",
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

function ValidarTipoDocumento(id, txtTipoDocu)
{
    var params = JSON.stringify({
        "idTipoDocumento": id,
        "control": txtTipoDocu || ''
    });
    $.ajax({
        type: "POST",
        url: wsnode + "wsCompras.svc/GetHTMLTipoDocumento",
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

$('#TipoDocumentoSearch').change(function () {
    ValidarTipoDocumento($('#TipoDocumentoSearch').val(), 'txtNroDocumentoSearch');
});

$('#selTipoDocumentoPopup').change(function () {
    ValidarTipoDocumento($('#selTipoDocumentoPopup').val(),'txtNroDocumentoPopup');
});

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
        "<div class=\"col-xs-3 home-buttom\" onclick=\"guardarProveedor(); return false;\">" +
        "    <center><i class=\"fa fa-save\">&nbsp;&nbsp;</i> Guardar</center>" +
        "</div></div>"
    );
    var params = JSON.stringify({
        "idproveddor": idProveedor
    });
    $.ajax({
        type: "POST",
        url: wsnode + "wsCompras.svc/GetProveedor_Id",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: params,
        async: false,
        processData: false,
        cache: false,
        success: function (response) {
            if (response.messageType == "OK")
            {
                $("#selTipoDocumentoPopup").val(response.rows[0].TipoDocumento);
                $("#txtNroDocumentoPopup").val(response.rows[0].Documento);
                $("#txtRazonSocialPopup").val(response.rows[0].RazonSocial);
                $("#txtPuntajePopup").val(response.rows[0].Puntaje);
                $("#txtPuntajePopup").prop('disabled', true);
                $("#txtDirecccionPopup").val(response.rows[0].Direccion);
                $("#txtTelefonoPopup").val(response.rows[0].Telefono);
                $("#txtContactoPopup").val(response.rows[0].Contacto);
                $("#txtCodigoPopup").val(response.rows[0].Codigo);
                $("#txtCodigoPopup").prop('disabled', true);
                $("#imgEstadoPopup").attr("src", response.rows[0].Estado);
                $("#txtTelefonoPopup").mask('(00)000-0000');
                ValidarTipoDocumento($('#selTipoDocumentoPopup').val(), 'txtNroDocumentoPopup');
            }
            else
                showError(response.message);
        },
        error: function (response) {
            showError(response);
        }
    });
}

$(document).ready(function () {
    getProveedores();
    ValidarTipoDocumento($('#TipoDocumentoSearch').val(), 'txtNroDocumentoSearch');
});

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
        "tipodocumento": $("#TipoDocumentoSearch").val() || '',
        "nrodocumento": $("#txtNroDocumentoSearch").val() || '',
        "codigoProveedor": $("#txtCodigoProveedor").val() || '',
        "nombreProveedor": $("#txtNombreProveedor").val() || ''
    });
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
            //"<td>#: Estado #</td>" +
            "<td><center><input type=\"image\" src=\"#: Estado #\" style=\"width:17px\" /></center></td>" +
            "<td><div class=\"home-buttom\" style=\"margin-right:3px;height:auto !important;height: 40px;\" onclick=\"SetProovedorId('#: idProveedor #');return false;\" ><center><i class=\"fa fa-pencil\" aria-hidden=\"true\" >&nbsp;</i> Modificar</center></div></td>" +
            "</tr>",
        altRowTemplate: "<tr>" +
            "<td>#: Codigo #</td>" +
            "<td>#: DesTipoDocumento #</td>" +
            "<td>#: Documento #</td>" +
            "<td>#: RazonSocial #</td>" +
            //"<td>#: Estado #</td>" +
            "<td><center><input type=\"image\" src=\"#: Estado #\" style=\"width:17px\" /></center></td>" +
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
            width: 180
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
            title: "Activo",
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
    var grid = $('#gridProveedor').data('kendoGrid');
    grid.dataSource.pageSize(10);
    grid.dataSource.page(1); 
    grid.dataSource.read();
}

function nuevoProveedor() {
    $('#ProveedorModal').modal("show");
    //Clean
    $('#ProveedorModal_header').empty();
    $('#ProveedorModal_footer').empty();
    
    $("#selTipoDocumentoPopup").val(2);
    $("#txtNroDocumentoPopup").val("");
    $("#txtRazonSocialPopup").val("");
    $("#txtPuntajePopup").val("");
    $("#txtPuntajePopup").prop('disabled', true);
    $("#txtDirecccionPopup").val("");
    $("#txtTelefonoPopup").val("");
    $("#txtContactoPopup").val("");
    $("#imgEstadoPopup").attr("src", "../Content/images/uncheck.png");

    $('#divCodigoPopup').empty();

    $("#txtTelefonoPopup").mask('(00)000-0000');

    //Add Controls
    $('#ProveedorModal_header').append(
        "<button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">×</button>" +
        "&nbsp;<i class=\"fa fa-male\">&nbsp;&nbsp;</i><label class=\"label-md modal-title custom_align\"> Nuevo Proveedor</label>"
    );    
    
    $('#ProveedorModal_footer').append(
        "<div class=\"col-xs-12 \">" +
        "<div class=\"col-xs-9\"></div>" +
        "<div class=\"col-xs-3 home-buttom\" onclick=\"guardarProveedor(); return false;\">" +
        "    <center><i class=\"fa fa-save\">&nbsp;&nbsp;</i> Guardar</center>" +
        "</div></div>"
    );

    ValidarTipoDocumento($('#selTipoDocumentoPopup').val(), 'txtNroDocumentoPopup');
}

