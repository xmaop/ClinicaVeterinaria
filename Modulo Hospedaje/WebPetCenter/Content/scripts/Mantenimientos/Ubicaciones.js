$(document).ready(function () {
    GetZone();
    GetType();
    getUnit();
});

function SearchLocations() {
    //showLoading();
    $('#processingModal').modal('show');

    var type = ($("#Type1").is(":checked") === true ? 1 : 0).toString() + ($("#Type2").is(":checked") === true ? 1 : 0).toString();
    var LocationsRange_start = $("#txtLocationsRange_start").val().trim();
    var LocationsRange_end = $("#txtLocationsRange_end").val().trim();
    var Zone = $("#cboZone").val();
    var BusinessUnit = $("#cboBusinessUnit").val();
    var RangeTime_start = $("#txtRangeTime_start").val().trim();
    var RangeTime_end = $("#txtRangeTime_end").val().trim();
    var TypeLocation = $("#cboTypeLocation").val();

    switch (type) {
        case "00":
            showWarningMessage("Por favor seleccionar unos de los filtros de Búsqueda");
            return;
            break;
        case "10":
            if (LocationsRange_start === "" && LocationsRange_end === "" && Zone == "0" && BusinessUnit == "0") {
                showWarningMessage("Por favor completar los datos de Búsqueda");
                return;
            }
            break;
        case "01":
            if (RangeTime_start === "" && RangeTime_end === "" && TypeLocation == "0") {
                showWarningMessage("Por favor completar los datos de Búsqueda");
                return;
            }
            break;
        case "11":
            if (LocationsRange_start === "" && LocationsRange_end === "" && Zone == "0" && BusinessUnit == "0" && RangeTime_start === "" && RangeTime_end === "" && TypeLocation == "0") {
                showWarningMessage("Por favor completar los datos de Búsqueda");
                return;
            }
            break;
    }
    parametros = JSON.stringify({
        "loc_range_star": LocationsRange_start,
        "loc_range_fin": LocationsRange_end,
        "id_zone": Zone,
        "id_unit": BusinessUnit,
        "time_range_start": RangeTime_start,
        "time_range_end": RangeTime_end,
        "type_loc": TypeLocation,
        "type": type
    });
    $("#grid").html("");
    $("#grid").kendoGrid({
        dataSource: {
            //type: "odata",
            transport: {
                read: {
                    type: "POST",
                    url: wsnode + "wsUbicacion.svc/getLocations",
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json'
                },
                parameterMap: function (options, operation) {
                    return parametros;
                }
            },
            parameterMap: function (options, operation) {
                return parametros;
            },
            pageSize: 10
        },
        height: 393,
        pageable: {
            refresh: true,
            pageSizes: false,
            buttonCount: 5
        },
        columns: [{
            template: "<div ><input type='checkbox'onclick='ValidateCheckAll();' value='#:ID_UBIC#' ></div>",
            field: "",
            title: "<input id='chkAll' onclick='ChekedAllControl();' type='checkbox'>",
            width: 35
        }, {
            field: "UBICACION",
            title: "Ubicación"
        }, {
            field: "TIPO",
            title: "Tipo"
        }, {
            field: "PRODUCT_CODE",
            title: "Product Code"
        }, {
            field: "DESC_ZONA",
            title: "Zona"
        }, {
            field: "TIEMP_PERM",
            title: "Permanencia"
        }, {
            field: "UNID_NEG",
            title: "Unidad Negocio"
        }, {
            field: "CLASIF_UBIC",
            title: "Clasificación"
        }, {
            template: "<i class='#: ESTADO#'></i>",
            field: "",
            title: "Estado"
        }, {
            field: "CO_USUA_MODI",
            title: "Usua. Modif"
        }, {
            field: "FE_USUA_MODI",
            title: "Fech. Modif"
        }],
        dataBound: function (e) {
            //hideLoading
            $('#processingModal').modal('hide');
        }
    });

}

function GetZone() {
    $("#divZone").html("");
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsUbicacion.svc/getZonas",
        dataType: "json",
        async: false,
        processData: false,
        cache: false,
        success: function (data) {
            $("#divZone").html(data);
        },
        error: function (response) {

        }
    });
}

function GetType() {
    $("#divType").html("");
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsUbicacion.svc/getTipoUbic",
        dataType: "json",
        async: false,
        processData: false,
        cache: false,
        success: function (data) {
            $("#divType").html(data);
        },
        error: function (response) {

        }
    });
}

function CopyText(origin, destination) {
    $("#" + destination).val($("#" + origin).val());
}

function EnableControl(nameCheck,nameControl) {
    if ($("#" + nameCheck).is(":checked")) {
        $("#" + nameControl).attr("disabled", false);
    } else {
        $("#" + nameControl).attr("disabled", true);
    }
}

function getUnit() {
    $("#divUnit").html("");
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsUbicacion.svc/getUnidNegoc",
        dataType: "json",
        async: false,
        processData: false,
        cache: false,
        success: function (data) {
            $("#divUnit").html(data);
        },
        error: function (response) {

        }
    });
}

function ChekedAllControl() {
    if ($('#chkAll').is(':checked')) {
        $('tbody tr td input[type="checkbox"]').each(function () {
            $(this).prop('checked', true);
        });
    }
    else {
        $('tbody tr td input[type="checkbox"]').each(function () {
            $(this).prop('checked', false);
        });
    }
}

function ValidateCheckAll() {
    var cant_chek = 0;
    $('tbody tr td input[type="checkbox"]').each(function () {
        cant_chek++;
    });
    if (CountCheck() == cant_chek && cant_chek != 0) {
        $('#chkAll').prop('checked', true);
    } else {
        $('#chkAll').prop('checked', false);
    }
}

function CountCheck() {
    var num_check = 0;
    $('tbody tr td input[type="checkbox"]').each(function () {
        if ($(this).is(":checked")) {
            num_check++;
        }
    });
    return num_check;
}

function ChangeCheck(name) {
    if ($("#" + name).is(":checked")) {
        switch (name) {            
            case "Type1":
                $("#txtLocationsRange_start").val("");
                $("#txtLocationsRange_start").attr("disabled", false);
                $("#txtLocationsRange_end").val("");
                $("#txtLocationsRange_end").attr("disabled", false);
                $("#cboZone option[value= '0']").prop("selected", true);
                $("#cboZone").attr("disabled", false);
                $("#cboBusinessUnit option[value= '0']").prop("selected", true);
                $("#cboBusinessUnit").attr("disabled", false);

                $("#txtRangeTime_start").val("");
                $("#txtRangeTime_start").attr("disabled", true);
                $("#txtRangeTime_end").val("");
                $("#txtRangeTime_end").attr("disabled", true);
                $("#cboTypeLocation option[value= '0']").prop("selected", true);
                $("#cboTypeLocation").attr("disabled", true);
                $("#Type2").attr("checked", false);
                break;
            case "Type2":
                $("#txtLocationsRange_start").val("");
                $("#txtLocationsRange_start").attr("disabled", true);
                $("#txtLocationsRange_end").val("");
                $("#txtLocationsRange_end").attr("disabled", true);
                $("#cboZone option[value= '0']").prop("selected", true);
                $("#cboZone").attr("disabled", true);
                $("#cboBusinessUnit option[value= '0']").prop("selected", true);
                $("#cboBusinessUnit").attr("disabled", true);

                $("#txtRangeTime_start").val("");
                $("#txtRangeTime_start").attr("disabled", false);
                $("#txtRangeTime_end").val("");
                $("#txtRangeTime_end").attr("disabled", false);
                $("#cboTypeLocation option[value= '0']").prop("selected", true);
                $("#cboTypeLocation").attr("disabled", false);
                $("#Type1").attr("checked", false);
                break;
        }
    } else {
        switch (name) {
            case "Type1":
                $("#txtLocationsRange_start").val("");
                $("#txtLocationsRange_start").attr("disabled", true);
                $("#txtLocationsRange_end").val("");
                $("#txtLocationsRange_end").attr("disabled", true);
                $("#cboZone option[value= '0']").prop("selected", true);
                $("#cboZone").attr("disabled", true);
                $("#cboBusinessUnit option[value= '0']").prop("selected", true);
                $("#cboBusinessUnit").attr("disabled", true);
                break;
            case "Type2":
                $("#txtRangeTime_start").val("");
                $("#txtRangeTime_start").attr("disabled", true);
                $("#txtRangeTime_end").val("");
                $("#txtRangeTime_end").attr("disabled", true);
                $("#cboTypeLocation option[value= '0']").prop("selected", true);
                $("#cboTypeLocation").attr("disabled", true);
                break;
        }
    }
}

function CallPopupLocations() {
    if (CountCheck() == 0) {
        showWarningMessage("Seleccionar por lo menos una ubicación");
        return;
    }
    $('#inPopupLocations').modal('show');
    //parametros = JSON.stringify({ "nu_mani": nu_mani_, "nu_guia": nu_guia_, "nu_vola": nu_vola_ });
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsUbicacion.svc/callPopupLocations",
        dataType: "json",
        //data: parametros,
        async: false,
        processData: false,
        cache: false,
        success: function (data) {
            $("#content-Locations").html(data);
        },
        error: function (response) {

        }
    });
}

function SaveLocations() {
    $('#processingModal').modal('show');
    var locations = [];
    $('tbody tr td input[type="checkbox"]').each(function () {
        if ($(this).is(":checked")) {
            locations.push($(this).val());
        }
    });
    var chks = ($("#chkUnit").is(":checked") ? "1" : "0").toString() +
               ($("#chkShc").is(":checked") ? "1" : "0").toString() +
               ($("#chkTime").is(":checked") ? "1" : "0").toString() +
               ($("#chkType").is(":checked") ? "1" : "0").toString() +
               ($("#chkClasf").is(":checked") ? "1" : "0").toString() +
               ($("#chkZone").is(":checked") ? "1" : "0").toString(); //111111

    var UnitSave = $("#cboUnitSave").val();
    var TimeSave = $("#txtTimeSave").val();
    var ClasifSave = $("#cboClasifSave").val();
    var ShcSave = $("#cboShcSave").val(); //== "GEN" ? "" : $("#cboShcSave").val();
    var TypeSave = $("#cboTypeSave").val();
    var ZoneSave = $("#cboZoneSave").val();

    if(
        UnitSave == "0" && TimeSave == "" && ClasifSave == "0" && ShcSave == "0" && TypeSave == "0" && ZoneSave == "0"
    ) {
        showWarningMessage("Por favor llenar almenos un de los datos solicitados");
        return;
    }
    parametros = JSON.stringify({
        "idLocations": locations,
        "unit": UnitSave,
        "shc": ShcSave,
        "time": TimeSave,
        "typeLocation": TypeSave,
        "clasf": ClasifSave,
        "idZone": ZoneSave,
        "chks": chks
    });
    $.ajax({
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        url: wsnode + "wsUbicacion.svc/InsertLocations",
        dataType: "json",
        data: parametros,
        async: false,
        processData: false,
        cache: false,
        success: function (data) {
            if (data == "OK") {
                showSuccessMessage("Datos guardados correctamente");
                $("#btnClosePopup").click();
                $("#btnSearchLocations").click();
                $('#processingModal').modal('hide');
            } else {
                showWarningMessage("Error: " + data);
                $('#processingModal').modal('hide');
            }
        },
        error: function (response) {
            $('#processingModal').modal('hide');
        }
    });
}