function areEqual() {
    for (var e = arguments.length, i = 1; e > i; i++)
        if (null == arguments[i] || arguments[i] != arguments[i - 1]) return !1;
    return !0
}

function onmouseoverBtn(e) {
    $(e).addClass("ui-state-hover")
}

function onmouseoutBtn(e) {
    $(e).removeClass("ui-state-hover")
}

function onfocusBtn(e) {
    $(e).addClass("ui-state-focus")
}

function onblurBtn(e) {
    $(e).removeClass("ui-state-focus")
}

function addGenericObject(e) {
    $.each(arrDataObject.Item, function (i) {
        this.id == e.id && arrDataObject.Item.splice(i, 1)
    }), arrDataObject.Item.push(e)
}

function CompareFormsPage(e, i) {
    var t = !1;
    try {
        for (key in i)
            if (i.hasOwnProperty(key) && i[key] != e[key]) {
                if ("object" != typeof i[key]) {
                    t = !0;
                    break
                }
                if (t = RecursiveObjectVerification(i[key], e[key])) break
            }
    } catch (o) { }
    return t
}

function CompareForms(e) {
    var i = !1;
    if (void 0 != e.id && "0" != e.id && "" != e.id) {
        var t, o = e.data;
        try {
            $.each(arrDataObject.Item, function () {
                this.id == e.id && (t = this.data)
            });
            for (key in o)
                if (o.hasOwnProperty(key) && o[key] != t[key]) {
                    if ("object" != typeof o[key]) {
                        i = !0;
                        break
                    }
                    if (i = RecursiveObjectVerification(o[key], t[key])) break
                }
        } catch (n) { }
    }
    return i
}

function RecursiveObjectVerification(e, i) {
    var t = Object.size(e.Item),
        o = Object.size(i.Item),
        n = !1;
    if (parseInt(t) == parseInt(o)) {
        e: for (var a = 0; t > a; a++)
            for (key in e.Item[a])
                if (e.Item[a].hasOwnProperty(key) && e.Item[a][key] != (0 == o ? "" : "undefined" == i.Item[a] ? "" : i.Item[a][key]) && null != e.Item[a][key]) {
                    if ("object" != typeof e.Item[a][key]) {
                        n = !0;
                        break e
                    }
                    RecursiveObjectVerification(e.Item[a][key], i.Item[a][key])
                }
    } else n = !0;
    return n
}

function alert_msg(e) {
    $("#DialogMensaje .message_text").html(e), $("#DialogMensaje").dialog("open")
}

function CreateLoading() {
    $("#loadingScreen").dialog({
        autoOpen: !1,
        dialogClass: "loadingScreenWindow",
        closeOnEscape: !1,
        draggable: !1,
        width: 350,
        minHeight: 50,
        modal: !0,
        buttons: {},
        resizable: !1
    })
}

function CreateLoadingCustom() {
    $("#loadingScreenCustom").dialog({
        autoOpen: !1,
        dialogClass: "loadingScreenWindow",
        closeOnEscape: !1,
        draggable: !1,
        width: 350,
        minHeight: 50,
        modal: !0,
        buttons: {},
        resizable: !1
    })
}

function CreateLoadingMain() {
    $("#loadingScreenMain").dialog({
        autoOpen: !1,
        dialogClass: "loadingScreenWindow",
        closeOnEscape: !1,
        draggable: !1,
        width: 350,
        minHeight: 50,
        modal: !0,
        buttons: {},
        resizable: !1
    })
}

function CreateLoadingTree01() {
    $("#loadingScreenTree01").dialog({
        autoOpen: !1,
        dialogClass: "loadingScreenWindow",
        closeOnEscape: !1,
        draggable: !1,
        width: 350,
        minHeight: 50,
        modal: !0,
        buttons: {},
        resizable: !1
    })
}

function CreateLoadingTree02() {
    $("#loadingScreenTree02").dialog({
        autoOpen: !1,
        dialogClass: "loadingScreenWindow",
        closeOnEscape: !1,
        draggable: !1,
        width: 350,
        minHeight: 50,
        modal: !0,
        buttons: {},
        resizable: !1
    })
}

function CreateLoadingSearch() {
    $("#loadingScreenSearch").dialog({
        autoOpen: !1,
        dialogClass: "loadingScreenWindow",
        closeOnEscape: !1,
        draggable: !1,
        width: 350,
        minHeight: 50,
        modal: !0,
        buttons: {},
        resizable: !1
    })
}

function CreateMessageBox() {
    $("#DialogMensaje").dialog({
        autoOpen: !1,
        resizable: !1,
        modal: !0,
        closeOnEscape: !0,
        width: 400,
        draggable: !0,
        title: ":: Mensaje ::",
        buttons: {
            Aceptar: function () {
                if (window["onClick_OK"])
                    onClick_OK();
                $(this).dialog("close")
            }
        }
    })
}

function waitingDialog(e) {
    //void 0 != e && ($("#loadingScreen").html(e.message && "" != e.message ? e.message : "Espere, por favor..."), $("#loadingScreen").dialog("option", "title", e.title && "" != e.title ? e.title : "Cargando"), $("#loadingScreen").dialog("open"))
}

function closeWaitingDialog() {
    //$("#loadingScreen").dialog("close")
}

function waitingDialogCustom(e) {
    //void 0 != e && ($("#loadingScreenCustom").html(e.message && "" != e.message ? e.message : "Espere, por favor..."), $("#loadingScreenCustom").dialog("option", "title", e.title && "" != e.title ? e.title : "Cargando"), $("#loadingScreenCustom").dialog("open"))
}

function closeWaitingDialogCustom() {
    //$("#loadingScreenCustom").dialog("close")
}

function waitingDialogMain(e) {
    //void 0 != e && ($("#loadingScreenMain").html(e.message && "" != e.message ? e.message : "Espere, por favor..."), $("#loadingScreenMain").dialog("option", "title", e.title && "" != e.title ? e.title : "Cargando"), $("#loadingScreenMain").dialog("open"))
}

function waitingDialogSearch(e) {
    //void 0 != e && ($("#loadingScreenSearch").html(e.message && "" != e.message ? e.message : "Espere, por favor..."), $("#loadingScreenSearch").dialog("option", "title", e.title && "" != e.title ? e.title : "Cargando Búsqueda"), $("#loadingScreenSearch").dialog("open"))
}

function closeWaitingDialogSearch() {
    //$("#loadingScreenSearch").dialog("close")
}

function closeWaitingDialogMain() {
    //$("#loadingScreenMain").dialog("close")
}

function waitingDialogTree01(e) {
    //$("#loadingScreenTree01").html(e.message && "" != e.message ? e.message : "Espere, por favor..."), $("#loadingScreenTree01").dialog("option", "title", e.title && "" != e.title ? e.title : "Cargando"), $("#loadingScreenTree01").dialog("open")
}

function closeWaitingDialogTree01() {
    //$("#loadingScreenTree01").dialog("close")
}

function waitingDialogTree02(e) {
    //$("#loadingScreenTree02").html(e.message && "" != e.message ? e.message : "Espere, por favor..."), $("#loadingScreenTree02").dialog("option", "title", e.title && "" != e.title ? e.title : "Cargando"), $("#loadingScreenTree02").dialog("open")
}

function closeWaitingDialogTree02() {
    //$("#loadingScreenTree02").dialog("close")
}

function autoResize(e) {
    setInterval(function () {
        var i = $("#" + e).contents().find("body").height() + 20;
        CalcularScreenSizeAuto(e, i)
    }, 1)
}

function showDialog(e) {
    return $("#" + e).dialog("open"), !1
}

function HideDialog(e) {
    return $("#" + e).dialog("close"), !1
}

function CloseDialog(e) {
    $(e).find(".ui-dialog-titlebar-close").click();
    $(e)[0].innerHTML = "";
    $(e).remove();
}

function SetBackgroundField(e, i) {
    $(dlgrFocus).find("#" + e).css("background", i), "#FDE2E2" == i && $(dlgrFocus).find("#" + e).focus()
}

function SetBackgroundFieldWithoutFocus(e, i) {
    $(dlgrFocus).find("#" + e).css("background", i)
}

function SetBackgroundFieldPage(e, i) {
    $("#" + e).css("background", i), "#FDE2E2" == i && $("#" + e).focus()
}

function SetBorderField(e, i) {
    $(dlgrFocus).find("#" + e).css("background", i)
}

function AppendMessage(e, i) {
    var t = null == i ? "divMensaje" : i;
    $(dlgrFocus).find("#" + t + " ul").append("<li> * " + e + "</li>")
}

function AppendMessageList(e, i) {
    var t = null == i ? "divMensaje" : i;
    $(dlgrFocus).find("#" + t + " ul").append(e);
}

function AppendMessagePage(e, i) {
    var t = null == i ? "divMensaje " : i;
    $("#" + t + " ul").append("<li> * " + e + "</li>")
}

function AppendMessageLogin(e, i) {
    $("#" + e).append("<li> * " + i + "</li>")
}

function AppendMessageModal(e, i) {
    $("#" + e + " > ul").append("<li> * " + i + "</li>")
}

function isDialogUp() {
    var e = $("div[id^='DIALOG']"),
        t = 0,
        o = 0,
        n = !1;
    for (i = 0; i < e.length; i++) $(e[i].parentNode).is(":visible") && t++;
    for (i = 0; i < e.length; i++)
        if (estPopUp = $("#" + e[i].id).dialogExtend("state"), $(e[i].parentNode).is(":visible")) {
            if ("normal" == estPopUp) {
                n = !0;
                break
            }
            "minimized" == estPopUp && o++
        }
    return o == t && (n = !1), n
}

function setTooltipGridColGrid(e, i, t) {
    var o = {
        content: {
            title: "<strong><u>" + i + "</u><strong>",
            text: t
        },
        position: {
            viewport: $(window),
            adjust: {
                x: 10
            },
            my: "left top",
            at: "top right"
        },
        events: {
            show: function () {
                return !isDialogUp()
            }
        }
    };
    $("#" + e).qtip(o)
}

function setTooltipGridColGridInDialog(e, i, t) {
    var o = {
        content: {
            title: "<strong><u>" + i + "</u><strong>",
            text: t
        },
        position: {
            viewport: $(window),
            adjust: {
                x: 10
            },
            my: "left top",
            at: "top right"
        }
    };
    $.dlg("#" + e).qtip(o)
}

function setTooltipGridColOpcion(e) {
    var i = {
        content: {
            title: "<strong><u>Acciones</u><strong>",
            text: e
        },
        position: {
            viewport: $(window),
            adjust: {
                x: 10
            },
            my: "left top",
            at: "top right"
        },
        events: {
            show: function () {
                return !isDialogUp()
            }
        }
    };
    $("#idOpcion").qtip(i)
}

function get_DialogFocus() {
    v_indexMax = 0;
    for (var e = 0; e < $(".ui-dialog,ui-widget").length; e++) v_index = $(".ui-dialog,ui-widget")[e].style.zIndex, v_index > v_indexMax && (v_indexMax = v_index);
    for (var e = 0; e < $(".ui-dialog,ui-widget").length; e++)
        if ($(".ui-dialog,ui-widget")[e].style.zIndex == v_indexMax) {
            dlgrFocus = $(".ui-dialog,ui-widget")[e];
            break
        }
}

function set_MaxZIndex(e) {
    v_indexMax = 0;
    for (var i = 0; i < $(".ui-dialog,ui-widget").length; i++) v_index = $(".ui-dialog,ui-widget")[i].style.zIndex, v_index > v_indexMax && (v_indexMax = v_index);
    $(".ui-dialog,ui-widget").length > 0 && ($("#" + e).parent()[0].style.zIndex = parseInt(v_indexMax) + parseInt("1"))
}

function getParameterByName(e) {
    e = e.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var i = "[\\?&]" + e + "=([^&#]*)",
        t = new RegExp(i),
        o = t.exec(window.location.href);
    return null == o ? "" : decodeURIComponent(o[1].replace(/\+/g, " "))
}

function message(e, i, t) {
    $.pnotify({
        title: e,
        text: i,
        type: t,
        sticker: !1,
        animation: {
            effect_in: "show",
            effect_out: "slide"
        },
        hide: !0,
        opacity: .8,
        delay: "error" == t || "info" == t ? 5e3 : 4e3
    })
}

function messageErrorServer(e, i) {
    $.pnotify({
        title: e,
        text: i,
        type: "error",
        sticker: !1,
        animation: {
            effect_in: "show",
            effect_out: "slide"
        },
        hide: !1,
        closer_hover: !1,
        opacity: .8
    }), setTimeout(function () {
        CloseDialog(dlgrFocus)
    }, 100)
}

function messageErrorAjax(e, i, t, o, n) {
    var a = "";
    $.pnotify({
        title: e,
        text: i,
        type: "error",
        sticker: !1,
        animation: {
            effect_in: "show",
            effect_out: "slide"
        },
        hide: !1,
        opacity: .8
    }), 0 === t ? a = "No Existe conexión en la Red" : 404 == t ? a = "Url no encontrada" : 500 == t && (a = "Error Interno"), EnviarEmailJavascript(a, o, n), CloseDialog(dlgrFocus)
}

function EnviarEmailJavascript(e, i, t) {
    jQuery.ajax({
        type: "POST",
        url: baseUrl + "Base/EnviarEmailExceptionAjax",
        dataType: "json",
        async: !0,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            errorMessage: e,
            baseUrl: i,
            parametros: t
        }),
        success: function () { }
    })
}

function addFilterToGrid(e, i) {
    $("#" + e).jqGrid("filterToolbar", {
        stringResult: !0,
        searchOnEnter: !0,
        defaultSearch: "cn"
    }), $("#" + e).jqGrid("navButtonAdd", i, {
        id: "filter_" + e,
        caption: "Filtrar",
        title: "Mostrar/Ocultar filtros",
        buttonicon: "ui-icon-pin-s",
        onClickButton: function () {
            $("#" + e)[0].toggleToolbar()
        }
    })
}


function addFilterToGridMultiSelect(e, i) {
    $("#" + e).jqGrid("filterToolbar", {
        stringResult: !0,
        searchOnEnter: !0,
        defaultSearch: "cn"
    }), $("#" + e).jqGrid("navButtonAdd", i, {
        id: "filter_" + e,
        caption: "Filtrar",
        title: "Mostrar/Ocultar filtros",
        buttonicon: "ui-icon-pin-s",
        onClickButton: function () {
            $("#" + e)[0].toggleToolbar()
        },
        beforeClear: function () {
            $(this.grid.hDiv).find(".ui-search-toolbar th>div>select[multiple] option").each(function () {
                this.selected = false; // unselect all options
            });

            $(this.grid.hDiv).find(".ui-search-toolbar button.ui-multiselect").each(function () {
                $(this).prev("select[multiple]").multiselect("refresh");
            }).css({
                width: "95%",
                marginTop: "1px",
                boxSizing: "border-box",
                marginBottom: "1px",
                paddingTop: "3px"
            });
        }
    })
}


function addFilterToGridMainViewWithoutFilter(e, i, t) {
    $("#" + e).jqGrid("filterToolbar", {
        stringResult: !0,
        searchOnEnter: !0,
        defaultSearch: "cn"
    }), $("#" + e).jqGrid("navButtonAdd", i, {
        id: "filter_" + e,
        caption: "",
        title: "Mostrar/Ocultar filtros",
        buttonicon: "ui-icon-pin-s",
        onClickButton: function () {
            $("#" + e)[0].toggleToolbar()
        }
    }), t || $("#" + e)[0].toggleToolbar()
}

function addFilterToGridMainView(e, i) {
    $("#" + e).jqGrid("filterToolbar", {
        stringResult: !0,
        searchOnEnter: !0,
        defaultSearch: "cn"
    }), $("#" + e).jqGrid("navButtonAdd", i, {
        id: "filter_" + e,
        caption: "",
        title: "Mostrar/Ocultar filtros",
        buttonicon: "ui-icon-pin-s",
        onClickButton: function () {
            $("#" + e)[0].toggleToolbar()
        }
    })
}

function addFilterToGridState(e, i, t) {
    $("#" + e).jqGrid("filterToolbar", {
        stringResult: !0,
        searchOnEnter: !0,
        defaultSearch: "cn"
    }), $("#" + e).jqGrid("navButtonAdd", i, {
        id: "filter_" + e,
        caption: "Filtrar",
        title: "Mostrar/Ocultar filtros",
        buttonicon: "ui-icon-pin-s",
        onClickButton: function () {
            $("#" + e)[0].toggleToolbar()
        }
    }), t || $("#" + e)[0].toggleToolbar()
}

function addFilterToGridStateSinCaption(e, i, t) {
    $("#" + e).jqGrid("filterToolbar", {
        stringResult: !0,
        searchOnEnter: !0,
        defaultSearch: "cn"
    }), $("#" + e).jqGrid("navButtonAdd", i, {
        id: "filter_" + e,
        caption: "",
        title: "Mostrar/Ocultar filtros",
        buttonicon: "ui-icon-pin-s",
        onClickButton: function () {
            $("#" + e)[0].toggleToolbar()
        }
    }), t || $("#" + e)[0].toggleToolbar()
}

function addFilterToGridModal(e, i, t) {
    $(dlgrFocus).find("#" + e).jqGrid("filterToolbar", {
        stringResult: !0,
        searchOnEnter: !0,
        defaultSearch: "cn"
    }), $(dlgrFocus).find("#" + e).jqGrid("navButtonAdd", i, {
        id: "filter_" + e,
        caption: "",
        title: "Mostrar/Ocultar filtros",
        buttonicon: "ui-icon-pin-s",
        onClickButton: function () {
            for (var i = !1, t = $(dlgrFocus).find("#" + e).find("tr"), o = 0; o < t.length; o++)
                if ("" != t[o].id && "1" == $(dlgrFocus).find("#" + e).find("#" + t[o].id).attr("editable")) {
                    i = !0;
                    break
                }
            return i ? !1 : void $(dlgrFocus).find("#" + e)[0].toggleToolbar()
        }
    }), t || $(dlgrFocus).find("#" + e)[0].toggleToolbar()
}

function addFilterToGridModalR(e, i, t) {
    $(dlgrFocus).find("#" + e).jqGrid("filterToolbar", {
        stringResult: !0,
        searchOnEnter: !0,
        defaultSearch: "cn"
    }), $(dlgrFocus).find("#" + e).jqGrid("navButtonAdd", i, {
        id: "filter_" + e,
        caption: "",
        title: "Mostrar/Ocultar filtros",
        buttonicon: "ui-icon-pin-s",
        onClickButton: function () {
            $(dlgrFocus).find("#" + e)[0].toggleToolbar()
        }
    }), t || $(dlgrFocus).find("#" + e)[0].toggleToolbar()
}

function hasValueString(e) {
    return null == e || "" == e ? !1 : !0
}

function addFilterToGridCustom(e, i, t) {
    $("#" + e).jqGrid("filterToolbar", {
        stringResult: !0,
        searchOnEnter: !0,
        defaultSearch: "cn"
    }), $("#" + e).jqGrid("navButtonAdd", i, {
        id: "filter_" + e,
        caption: "Filtrar",
        title: "Mostrar/Ocultar filtros",
        buttonicon: "ui-icon-pin-s",
        onClickButton: function () {
            $("#" + e)[0].toggleToolbar()
        }
    }), t || $("#" + e)[0].toggleToolbar()
}

function addColumnChosserToGrid(e, i) {
    $("#" + e).jqGrid("navButtonAdd", i, {
        id: "columns_" + e,
        caption: "Columnas",
        title: "Seleccionar columnas",
        buttonicon: "ui-icon-calculator",
        minWidth: 660,
        show: "blind",
        onClickButton: function () {
            var i = $("#" + e).jqGrid("getGridParam", "width");
            $("#" + e).jqGrid("columnChooser", {
                done: function (t) {
                    if (!t) return !1;
                    $("#" + e).setGridWidth(i), this.jqGrid("remapColumns", t, !0);
                    for (var o = [], n = $("#" + e).jqGrid("getGridParam", "userData").IdOpcion, a = $("#" + e).jqGrid("getGridParam", "colModel"), r = 0; r < a.length; r++) o.push({
                        name: a[r].name,
                        index: a[r].name,
                        stype: a[r].stype,
                        hidden: a[r].hidden,
                        hidedlg: a[r].hasOwnProperty("hidedlg") ? a[r].hidedlg : !1,
                        posicion: r
                    });
                    $.ajaxSetup({
                        cache: !1
                    }), jQuery.ajax({
                        type: "POST",
                        url: baseUrl + "Base/ActualizarColModelJQGrid",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({
                            IdOpcion: n,
                            lstColModel: o
                        }),
                        async: !0,
                        success: function (e) {
                            1 == e.success
                        },
                        error: function (e) {
                            alert(e.message)
                        }
                    })
                }
            })
        }
    }), $.extend(!0, $.jgrid.col, {
        modal: !0,
        msel_opts: {
            dividerLocation: .45
        },
        resizable: !1,
        dialog_opts: {
            show: "blind",
            hide: "blind"
        }
    })
}

function checkTimeout(e) {
    var i = !0;
    return e ? (e.responseText ? (e.responseText.indexOf("<title>Login</title>") > -1 || e.responseText.indexOf("<title>Object moved</title>") > -1 || '"_Logon_"' === e.responseText) && (i = !1) : "_Logon_" == e && (i = !1), i || (window.top.location.href = "http://localhost:1993/")) : $.ajax({
        url: baseUrl + "Base/SessionTimeOut",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        async: !1,
        complete: function (e) {
            i = checkTimeout(e)
        }
    }), i
}

function getDayWeekText(e, i) {
    var t;
    switch (e) {
        case 0:
            t = i ? "Domingo" : "Dom";
            break;
        case 1:
            t = i ? "Lunes" : "Lun";
            break;
        case 2:
            t = i ? "Martes" : "Mar";
            break;
        case 3:
            t = i ? "Miércoles" : "Mie";
            break;
        case 4:
            t = i ? "Jueves" : "Jue";
            break;
        case 5:
            t = i ? "Viernes" : "Vie";
            break;
        default:
            t = i ? "Sábado" : "Sáb"
    }
    return t
}

function getMonthText(e, i) {
    var t;
    switch (e) {
        case 0:
            t = i ? "Enero" : "Ene";
            break;
        case 1:
            t = i ? "Febrero" : "Feb";
            break;
        case 2:
            t = i ? "Marzo" : "Mar";
            break;
        case 3:
            t = i ? "Abril" : "Abr";
            break;
        case 4:
            t = i ? "Mayo" : "May";
            break;
        case 5:
            t = i ? "Junio" : "Jun";
            break;
        case 6:
            t = i ? "Julio" : "Jul";
            break;
        case 7:
            t = i ? "Agosto" : "Ago";
            break;
        case 8:
            t = i ? "Septiembre" : "Sep";
            break;
        case 9:
            t = i ? "Octubre" : "Oct";
            break;
        case 10:
            t = i ? "Noviembre" : "Nov";
            break;
        default:
            t = i ? "Diciembre" : "Dic"
    }
    return t
}

function GetDate() {
    d = new Array("Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado"), m = new Array("enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre"), today = new Date, day = today.getDate(), year = today.getYear(), year < 2e3 && (year += 1900), end = " de ", (1 == day || 21 == day || 31 == day) && (end = " de "), (2 == day || 22 == day) && (end = " de "), (3 == day || 23 == day) && (end = " de "), day += end, document.write(d[today.getDay()] + ", "), document.write(day), document.write(m[today.getMonth()] + end), document.write(year)
}

function GetToday() {
    var e = new Date,
        i = e.getDate(),
        t = e.getMonth() + 1,
        o = e.getFullYear();
    return 10 > i && (i = "0" + i), 10 > t && (t = "0" + t), e = i + "/" + t + "/" + o
}

function SetInputEventJQGrid() {
    $(".ui-search-input").find("input[type=text]").on("keypress", function (e) {
        var i = e.which ? e.which : window.event.keyCode;
        if (13 >= i) return !0;
        var t = String.fromCharCode(i),
            o = /[a-zA-Z0-9_.-ñÑáéíóú@@\/,; -]/;
        return o.test(t)
    }), $(".ui-pg-input").on("keyup", function (e) {
        var i = e || window.event,
            t = i.keyCode || i.which;
        t = String.fromCharCode(t);
        var o = /[0-9]|\./;
        o.test(t) || (i.returnValue = !1, i.preventDefault && i.preventDefault())
    }), $(".ui-pg-input").on("keypress", function (e) {
        return 8 != e.which && 0 != e.which && (e.which < 48 || e.which > 57) ? !1 : void 0
    })
}

function SetNoAlphanumericModal(e) {
    $(dlgrFocus).find("#" + e).on("keypress", function (e) {
        var i = e.which ? e.which : window.event.keyCode;
        if (13 >= i) return !0;
        var t = String.fromCharCode(i),
            o = /[a-zA-Z0-9_.-ñÑáéíóú@@\\// -]/;
        return o.test(t)
    })
}

function SetAlphanumericModalCustom(e) {
    $(dlgrFocus).find("#" + e).on("keypress", function (e) {
        var i = e.which ? e.which : window.event.keyCode;
        if (13 >= i) return !0;
        var t = String.fromCharCode(i),
            o = /[a-zA-Z0-9_.-ñÑáéíóú@@\\//,; -]/;
        return o.test(t)
    })
}

function SetAlphanumericCustomModal(e) {
    $(dlgrFocus).find("#" + e).on("keypress", function (e) {
        var i = e.which ? e.which : window.event.keyCode;
        if (13 >= i) return !0;
        var t = String.fromCharCode(i),
            o = /[a-zA-Z0-9_.-ñÑáéíóú@@\\//,; -]/;
        return o.test(t)
    })
}

function SetAlphanumericCustom(e) {
    $("#" + e).on("keypress", function (e) {
        var i = e.which ? e.which : window.event.keyCode;
        if (13 >= i) return !0;
        var t = String.fromCharCode(i),
            o = /[a-zA-Z0-9_.-ñÑáéíóú@@\\//,; -]/;
        return o.test(t)
    })
}

function SetAlphanumericBarSpace(e) {
    $("#" + e).on("keypress", function (e) {
        var i = e.which ? e.which : window.event.keyCode;
        if (13 >= i) return !0;
        var t = String.fromCharCode(i),
            o = /[a-zA-Z0-9_.-ñÑáéíóú@@\\//,;-]/;
        return o.test(t)
    })
}

function SetSoloNumerosyLetrasModal(e) {
    $(dlgrFocus).find("#" + e).on("keypress", function (e) {
        var i = e.which ? e.which : window.event.keyCode;
        if (13 >= i) return !0;
        var t = String.fromCharCode(i),
            o = /[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ]/;
        return o.test(t)
    })
}

function SetSoloLetrasModal(e) {
    $(dlgrFocus).find("#" + e).on("keypress", function (e) {
        var i = e.which ? e.which : window.event.keyCode;
        if (13 >= i) return !0;
        var t = String.fromCharCode(i),
            o = /[a-zA-Z ñÑáéíóúÁÉÍÓÚ]/;
        return o.test(t)
    })
}

function SetSoloLetras(e) {
    $("#" + e).on("keypress", function (e) {
        var i = e.which ? e.which : window.event.keyCode;
        if (13 >= i) return !0;
        var t = String.fromCharCode(i),
            o = /[a-zA-Z ñÑáéíóúÁÉÍÓÚ]/;
        return o.test(t)
    })
}

function SetAlphanumericModal(e) {
    $(dlgrFocus).find("#" + e).on("keypress", function (e) {
        var i = e.which ? e.which : window.event.keyCode;
        if (13 >= i) return !0;
        var t = String.fromCharCode(i),
            o = /[a-zA-Z0-9 ñÑáéíóú@@]/;
        return o.test(t)
    })
}

function SetAlphanumeric(e) {
    $("#" + e).on("keypress", function (e) {
        var i = e.which ? e.which : window.event.keyCode;
        if (13 >= i) return !0;
        var t = String.fromCharCode(i),
            o = /[a-zA-Z0-9 ñÑáéíóú@@]/;
        return o.test(t)
    })
}

function SetSoloLetrasModal(e) {
    $(dlgrFocus).find("#" + e).on("keypress", function (e) {
        var i = e.which ? e.which : window.event.keyCode;
        if (13 >= i) return !0;
        var t = String.fromCharCode(i),
            o = /[a-zA-Z ñÑáéíóú@@]/;
        return o.test(t)
    })
}

function SetAllAlphanumericModal() {
    $(dlgrFocus).find("input[type=text]").on("keypress", function (e) {
        var i = e.which ? e.which : window.event.keyCode;
        if (13 >= i) return !0;
        var t = String.fromCharCode(i),
            o = /[a-zA-Z0-9 ]/;
        return o.test(t)
    })
}

function restrictInput(e, i, t) {
    var o = t,
        n = String.fromCharCode(i.charCode);
    return o.test(n) || 0 == i.charCode ? void 0 : !1
}

function SoloNumerosKeyUp(e) {
    $(dlgrFocus).find("#" + e.id).on("keyup", function (e) {
        var i = e || window.event,
            t = i.keyCode || i.which;
        t = String.fromCharCode(t);
        var o = /[0-9]|\./;
        o.test(t) || (i.returnValue = !1, i.preventDefault && i.preventDefault())
    })
}

function SetNumericWithDotModal(e) {
    $(dlgrFocus).find("#" + e).on("keyup", function (e) {
        var i = e || window.event,
            t = i.keyCode || i.which;
        t = String.fromCharCode(t);
        var o = /[0-9]|\./;
        o.test(t) || (i.returnValue = !1, i.preventDefault && i.preventDefault())
    }), $(dlgrFocus).find("#" + e).on("keypress", function (e) {
        var i = e.which ? e.which : e.keyCode;
        return 45 != i && 46 != i && (48 > i || i > 57) ? !1 : !0
    })
}

function SetNumericWithOutDotModal(e) {
    $(dlgrFocus).find("#" + e).on("keyup", function (e) {
        var i = e || window.event,
            t = i.keyCode || i.which;
        t = String.fromCharCode(t);
        var o = /[0-9]/;
        o.test(t) || (i.returnValue = !1, i.preventDefault && i.preventDefault())
    }), $(dlgrFocus).find("#" + e).on("keypress", function (e) {
        var i = e.which ? e.which : e.keyCode;
        return 45 != i && 46 != i && (48 > i || i > 57) ? !1 : !0
    })
}

function SetNumericModal(e) {
    $(dlgrFocus).find("#" + e).unbind("keypress"), $(dlgrFocus).find("#" + e).bind("keyup", function (e) {
        var i = e || window.event,
            t = i.keyCode || i.which;
        t = String.fromCharCode(t);
        var o = /[0-9]|\./;
        o.test(t) || (i.returnValue = !1, i.preventDefault && i.preventDefault())
    }), $(dlgrFocus).find("#" + e).on("keypress", function (e) {
        return 8 != e.which && 0 != e.which && (e.which < 48 || e.which > 57) ? !1 : void 0
    })
    //, $(dlgrFocus).find("#" + e).unbind("paste"), $(dlgrFocus).find("#" + e).bind("paste", function () {
    //    return !1
    //}), $(dlgrFocus).find("#" + e).unbind("drop"), $(dlgrFocus).find("#" + e).bind("drop", function () {
    //    return !1
    //})
}

function SetNumericLimitModal(e) {
    $(dlgrFocus).find("#" + e).on("keyup", function (e) {
        var i = e || window.event,
            t = i.keyCode || i.which;
        t = String.fromCharCode(t);
        var o = /[0-9]|\./;
        o.test(t) || (i.returnValue = !1, i.preventDefault && i.preventDefault())
    }), $(dlgrFocus).find("#" + e).on("keypress", function (e) {
        return 8 != e.which && 0 != e.which && (e.which < 48 || e.which > 57) ? !1 : void 0
    }), $(dlgrFocus).find("#" + e).unbind("paste"), $(dlgrFocus).find("#" + e).bind("paste", function () {
        return !1
    }), $(dlgrFocus).find("#" + e).unbind("drop"), $(dlgrFocus).find("#" + e).bind("drop", function () {
        return !1
    })
}

function SetDecimalModal(e) {
    $(dlgrFocus).find("#" + e).unbind("keypress"), $(dlgrFocus).find("#" + e).bind("keypress", function (e) {
        var i = e || window.event,
            t = i.keyCode || i.which;
        return 45 == t || 46 == t && -1 == $(this).val().indexOf(".") || !(48 > t || t > 57) || 8 == t || 37 == t || 39 == t ? !0 : !1
    }), $(dlgrFocus).find("#" + e).unbind("paste"), $(dlgrFocus).find("#" + e).bind("paste", function () {
        return !1
    }), $(dlgrFocus).find("#" + e).unbind("drop"), $(dlgrFocus).find("#" + e).bind("drop", function () {
        return !1
    })
}

function SetDecimalDigitsLimitModal(e) {
    $(dlgrFocus).find("#" + e).autoNumeric("init", {
        aSep: ""
    }), $(dlgrFocus).find("#" + e).unbind("paste"), $(dlgrFocus).find("#" + e).bind("paste", function () {
        return !1
    }), $(dlgrFocus).find("#" + e).unbind("drop"), $(dlgrFocus).find("#" + e).bind("drop", function () {
        return !1
    })
}

function SetDecimalDigitsLimit(e) {
    $("#" + e).autoNumeric("init", {
        aSep: ""
    }), $("#" + e).unbind("paste"), $("#" + e).bind("paste", function () {
        return !1
    }), $("#" + e).unbind("drop"), $("#" + e).bind("drop", function () {
        return !1
    })
}

function hasDecimalPlace(e, i) {
    var t = e.indexOf(".");
    return t >= 0 && t < e.length - i
}

function SetBlurModalWithDecimal(e) {
    $(dlgrFocus).find("#" + e).on("blur", function () {
        var e = "";
        "" != this.value && $.isNumeric(this.value) && (e = parseFloat(this.value), e = e.toFixed(2)), this.value = e
    })
}

function SetBlurModalWithTwoDigits(e) {
    $(dlgrFocus).find("#" + e).on("blur", function () {
        var e = this.value;
        "" != this.value && $.isNumeric(this.value) && 1 == this.value.length && (e = "0" + e), this.value = e
    })
}

function SetDecimal(e) {
    $("#" + e).on("keypress", function (e) {
        var i = e.which ? e.which : e.keyCode;
        return (46 == i || 37 == i || 39 == i || 8 == i) || 45 == i || 46 == i && -1 == $(this).val().indexOf(".") || !(48 > i || i > 57) ? !0 : !1
    })
}

function SetBoldAutoCompleteSearch() {
    $.ui.autocomplete.prototype._renderItem;
    $.ui.autocomplete.prototype._renderItem = function (e, i) {
        var t = new RegExp("^" + this.term, "i"),
            o = "undefined" == i.label ? "" : i.label.replace(t, "<span style='font-weight:bold;color:Black;text-transform:uppercase;'>" + this.term + "</span>");
        return $("<li></li>").data("item.autocomplete", i).append("<a>" + o + "</a>").appendTo(e)
    }
}

function SetNumeric(e) {
    //$("#" + e).on("keyup", function (e) {
    //    var i = e || window.event,
    //        t = i.keyCode || i.which;
    //    t = String.fromCharCode(t);
    //    var o = /[0-9]|\./;
    //    o.test(t) || (i.returnValue = !1, i.preventDefault && i.preventDefault())
    //}), $("#" + e).on("keypress", function (e) {
    //    return 8 != e.which && 0 != e.which && (e.which < 48 || e.which > 57) ? !1 : void 0
    //})

    $("#" + e).unbind("keypress"), $("#" + e).bind("keyup", function (e) {
        var i = e || window.event,
            t = i.keyCode || i.which;
        t = String.fromCharCode(t);
        var o = /[0-9]|\./;
        o.test(t) || (i.returnValue = !1, i.preventDefault && i.preventDefault())
    }), $("#" + e).on("keypress", function (e) {
        return 8 != e.which && 0 != e.which && (e.which < 48 || e.which > 57) ? !1 : void 0
    }), $("#" + e).unbind("paste"), $("#" + e).bind("paste", function () {
        return !1
    }), $("#" + e).unbind("drop"), $("#" + e).bind("drop", function () {
        return !1
    })

}


function ShowCustomCloseButton() {
    
    0 == $(dlgrFocus).find(".ui-dialog-titlebar-customclose").length && $(dlgrFocus).find(".ui-dialog-titlebar-buttonpane").prepend("<a id='customClose' onmouseover=\"this.className='ui-dialog-titlebar-customclose ui-corner-all ui-state-default ui-state-hover';\" onmouseout=\"this.className='ui-dialog-titlebar-customclose ui-corner-all ui-state-default';\" class='ui-dialog-titlebar-customclose ui-corner-all ui-state-default' role='button' href='javascript:;' style='top: auto; right: auto; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; float: right; display: inline; position: relative;'><span class='ui-icon ui-icon-circle-close'>close</span></a>")
}

function AtatchModalCloseButton() {
    $($(dlgrFocus).find(".ui-dialog-titlebar-buttonpane")).find("#customClose").click(function () {
        return CloseDialog(dlgrFocus), !1
    })
}

function AtatchModalCloseButton(e) {
    $($(dlgrFocus).find(".ui-dialog-titlebar-buttonpane")).find("#customClose").click(function () {
        return CloseDialog(dlgrFocus), SetJQGridFocus(e), !1
    })
}

function AtatchSimpleModalEsc() {
    $(dlgrFocus).on("keydown", function (e) {
        return e.keyCode === $.ui.keyCode.ESCAPE ? (CloseDialog(dlgrFocus), !1) : void 0
    })
}

function AtatchSimpleModalEsc(e, i) {
    $(dlgrFocus).on("keydown", function (e) {
        return e.keyCode === $.ui.keyCode.ESCAPE ? (CloseDialog(dlgrFocus), SetJQGridFocus(i), !1) : void 0
    })
}

function onClickCancelar() {
    get_DialogFocus();
    var e = existChanges();
    return e ? ("maximized" != $("#" + $(dlgrFocus).find("#hdIdDialog").val()).dialogExtend("state") && $("#" + $(dlgrFocus).find("#hdIdDialog").val()).dialogExtend("restore"), CleanDivsValidations()) : void 0 != result && CloseDialog(dlgrFocus), e
}

function AttachModalEscMinimize() {
    $(dlgrFocus).parent().parent().unbind("click"), $(dlgrFocus).parent().parent().click(function () {
        "minimized" == $("#" + $(dlgrFocus).find("#hdIdDialog").val()).dialogExtend("state") && set_MaxZIndex($(dlgrFocus).find("#hdIdDialog").val())
    }), $(dlgrFocus).parent().parent().off("keydown"), $(dlgrFocus).parent().parent().on("keydown", function (e) {
        return e.keyCode === $.ui.keyCode.ESCAPE ? ($(dlgrFocus).parent().poshytip("hide"), onClickCancelar(), !1) : void 0
    }), $(dlgrFocus).parent().parent().focus()
}

function AttachToolTip(e) {
    $(dlgrFocus).parent().prop("title", e), $(dlgrFocus).parent().poshytip({
        className: "tip-yellow",
        bgImageFrameSize: 11,
        offsetX: -25
    })
}

function InitBottonsMant(e, i) {
    
    var t = $(dlgrFocus)[0].childNodes[1].id,
        o = null == i ? null : $.dlg("#" + i).val();
    AtatchModalEsc(t, e, o), ShowCustomCloseButton(), AtatchModalCloseValidationButton(e, o), SetShortCuts(), $.dlg("#btnGrabar").click(function () {
        get_DialogFocus(), onClickGrabar(e, o)
    }), $.dlg("#btnCancelar").click(function () {
        get_DialogFocus(), onClickCancelar(e, o)
    }), $.dlg("#btnSalir").click(function () {
        get_DialogFocus(), onClickSalir(e, o)
    })
}

function InitBottonsCons(e, i) {
    
    var t = $(dlgrFocus)[0].childNodes[1].id;
    AtatchSimpleModalEsc(t, e, $.dlg("#" + i).val()), ShowCustomCloseButton(), AtatchModalCloseButton(e, $.dlg("#" + i).val())
}

function onClickNo() {
    get_DialogFocus(), $(dlgrFocus).find("#divFields").unblock(), $(dlgrFocus).find("#divNotAjax").hide(), $(dlgrFocus).find("#divAjax").show();
    var e = $(dlgrFocus).find("input:not(:disabled):not([readonly]), select:not(:disabled)").not(":button,:hidden,:checkbox").not("[id!='']");
    return e.length > 0 && $(dlgrFocus).find("#" + e[0].id).focus(), !1
}

function onClickSalir() {
    get_DialogFocus(), $(dlgrFocus).find("#divFields").unblock(), $(dlgrFocus).find("#divNotAjax").hide(), $(dlgrFocus).find("#divAjax").show(), CloseDialog(dlgrFocus)
}

function onClickSalir(e) {
    get_DialogFocus(), $(dlgrFocus).find("#divFields").unblock(), $(dlgrFocus).find("#divNotAjax").hide(), $(dlgrFocus).find("#divAjax").show(), CloseDialog(dlgrFocus), SetJQGridFocus(e)
}

function onClickYes() {
    get_DialogFocus();
    var e = $(dlgrFocus)[0].childNodes[1].id,
        i = window["Guardar" + $("#" + e).attr("data-fn")];
    return i(), !1
}

function onClickGrabar(e, i) {
    get_DialogFocus();
    var t = $(dlgrFocus)[0].childNodes[1].id,
        o = window["Guardar" + $("#" + t).attr("data-fn")],/*
        n = window["existChanges" + $("#" + t).attr("data-fn")],
        a = n()*/a = true;
    a ? a && o() : (void 0 != a && CloseDialog(dlgrFocus), SetJQGridFocus(e, i))
}

function onClickCancelar(e, i) {
    get_DialogFocus();
    var t = $(dlgrFocus)[0].childNodes[1].id,
        o = window["existChanges" + $("#" + t).attr("data-fn")],
        n = window["CleanValidations" + $("#" + t).attr("data-fn")],
        a = o();
    return a ? ("maximized" != $("#" + $(dlgrFocus).find("#hdIdDialog").val()).dialogExtend("state") && $("#" + $(dlgrFocus).find("#hdIdDialog").val()).dialogExtend("restore"), n()) : void 0 != a && (CloseDialog(dlgrFocus), SetJQGridFocus(e, i)), a
}

function onClickSalir(e, i) {
    get_DialogFocus(), $(dlgrFocus).find("#divFields").unblock(), $(dlgrFocus).find("#divNotAjax").hide(), $(dlgrFocus).find("#divAjax").show(), CloseDialog(dlgrFocus), SetJQGridFocus(e, i)
}

function SerchGenericObject(e) {
    var i = [];
    return $.each(arrDataObject.Item, function () {
        this.id == e && (i = this.data)
    }), i
}

function UnblockandShowErrors() {
    get_DialogFocus(), $(dlgrFocus).find("#divFields").unblock(), $(dlgrFocus).find("#divNotAjax").hide(), $(dlgrFocus).find("#divAjax").show(), $(dlgrFocus).find("#divMensaje").fadeIn("slow")
}

function UnblockandShowErrorsPage() {
    $("#divMensaje").fadeIn("slow"), $.colorbox.resize()
}

function CleanDivsValidationsPage() {
    $("#divMensajePlant ul").html(""), $("#divMensajePlant").fadeOut("fast")
}

function CleanDivsValidations() {
    get_DialogFocus(), $(dlgrFocus).find("#divMensaje ul").html(""), $(dlgrFocus).find("#divMensaje").fadeOut("fast"), $(dlgrFocus).find("#divFields").block({
        message: null
    }), $(dlgrFocus).find("#divAjax").hide(), $(dlgrFocus).find("#divNotAjax").show(), $(dlgrFocus).find("#btnYes").focus()
}

function validateEmail(e) {
    var i = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    return i.test(e) ? !0 : !1
}

function SetJQgridFormatter(e, i, t, o) {
    return e.push({
        name: i,
        index: i,
        width: t,
        sortable: !1,
        resizable: !1,
        formatter: o,
        search: !1,
        hidedlg: !0
    }), e
}

function SetJWhiteFormatter(e, i) {
    return e.unshift({
        name: i,
        index: i,
        sortable: !1,
        resizable: !1,
        search: !1,
        hide: !0,
        hidedlg: !0
    }), e
}

function RemapJQgridColumns(e, i) {
    jQuery("#" + e).jqGrid("remapColumns", i, !0)
}

function RemapJQgridColumnsSubGrid(e, i) {
    var t = [];
    j = 0;
    for (var o = 0; o < i.length; o++) 0 == o ? t[j] = parseInt(i[o]) : 1 == o ? (t[j] = 1, t[j + 1] = parseInt(i[o]) + 1, j++) : o > 0 && (t[j] = parseInt(i[o]) + 1), j++;
    jQuery("#" + e).jqGrid("remapColumns", t, !0)
}

function AttachMapColumnsOnDrag(e) {
    for (var i = [], t = $("#" + e).jqGrid("getGridParam", "userData").IdOpcion, o = $("#" + e).jqGrid("getGridParam", "colModel"), n = 0; n < o.length; n++) i.push({
        nombre: o[n].name,
        orden: n
    });

    $.ajaxSetup({
        cache: !1
    }), jQuery.ajax({
        type: "POST",
        url: baseUrl + "Base/ActualizarMapColModelJQGrid",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            IdOpcion: t,
            lstPosition: i
        }),
        async: !0,
        success: function () { },
        error: function (e) {
            alert(e.message)
        }
    })
}

function DisableDragOnColumn(e, i) {
    return ">th:not(:has(#jqgh_list_cb,#jqgh_" + e + "_" + i + ",#jqgh_list_rn,#jqgh_list_subgrid),:hidden)"
}

function DisableReorderSubGridOnDrag(e) {
    var i = $("#" + e).getDataIDs();
    $.each(i, function (i, t) {
        $("#" + e).collapseSubGridRow(t)
    })
}

function GenerateHex() {
    return Math.floor(65536 * (1 + Math.random())).toString(16).substring(1)
}

function GetGuid() {
    return GenerateHex() + GenerateHex() + "-" + GenerateHex() + "-" + GenerateHex() + "-" + GenerateHex() + "-" + GenerateHex() + GenerateHex() + GenerateHex()
}

function SetShortCuts() {
    $(dlgrFocus).unbind("keydown.jwerty"), jwerty.key("Ctrl+G", function (e) {
        e.preventDefault(), onClickGrabar()
    }, $(dlgrFocus))
}

function SetJQGridFocus(e) {
    $("#" + e).jqGrid("setSelection", 1, !1), $("#" + e).focus()
}

function compareDate(e, i, t) {
    var o, n, a, r, l, d;
    return e = e.replace(/-|\//g, ""), i = i.replace(/-|\//g, ""), o = parseInt(e.substring(0, 2), 10), n = parseInt(e.substring(2, 4), 10), a = parseInt(e.substring(4, 8), 10), r = parseInt(i.substring(0, 2), 10), l = parseInt(i.substring(2, 4), 10), d = parseInt(i.substring(4, 8), 10), d > a ? !0 : d == a ? l > n ? !0 : l == n ? t ? r >= o ? !0 : !1 : r > o ? !0 : !1 : !1 : !1
}

function compareDateMmyyyy(e, i, t) {
    var n, a, l, d;
    return e = e.replace(/-|\//g, ""), i = i.replace(/-|\//g, ""), n = parseInt(e.substring(0, 2), 10), a = parseInt(e.substring(3, 7), 10), l = parseInt(i.substring(0, 2), 10), d = parseInt(i.substring(3, 7), 10), d > a ? !0 : d == a ? l > n ? !0 : l == n ? !0 : !1 : !1
}


function isValidDate(e) {
    var i = !0;
    e = e.replace(/-|\//g, "");
    var t = parseInt(e.substring(0, 2), 10),
        o = parseInt(e.substring(2, 4), 10),
        n = parseInt(e.substring(4, 8), 10);
    return 1 > o || o > 12 ? i = !1 : 1 > t || t > 31 ? i = !1 : (4 == o || 6 == o || 9 == o || 11 == o) && t > 30 ? i = !1 : 2 == o && (n % 400 == 0 || n % 4 == 0) && n % 100 != 0 && t > 29 ? i = !1 : 2 == o && n % 100 == 0 && t > 29 && (i = !1), i
}

function isValidDateFormatMmyyyy(e) {
    var i = !0;
    e = e.replace(/-|\//g, "");
    var o = parseInt(e.substring(1, 2), 10), //mes
        n = parseInt(e.substring(2, 6), 10); //año
    return 1 > o || o > 12 ? i = !1 : i = !0, i;
}


function ValidateDate(e) {
    return e.match(/^(0[1-9]|[12][0-9]|3[01])[- //.](0[1-9]|1[012])[- //.](19|20|21)\d\d$/) && (split = e.split("/"), fec = new Date(split[2], split[1] - 1, split[0]), fec && fec.getFullYear() == split[2] && fec.getMonth() == split[1] - 1 && fec.getDate() == split[0]) ? !0 : !1
}

function ValidateTime(e) {
    return e.match(/^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$/)
}

function AtatchSimpleModalEsc(e, i, t) {
    $(dlgrFocus).off("keydown"), $(dlgrFocus).on("keydown", function (e) {
        return e.keyCode === $.ui.keyCode.ESCAPE ? (CloseDialog(dlgrFocus), SetJQGridFocus(i, t), !1) : void 0
    })
}

function AtatchModalCloseButton(e, i) {
    $($(dlgrFocus).find(".ui-dialog-titlebar-buttonpane")).find("#customClose").click(function () {
        get_DialogFocus();
        return CloseDialog(dlgrFocus), SetJQGridFocus(e, i), !1
    })
}

function SetJQGridFocus(e, i) {
    e && i && ($("#" + e).jqGrid("setSelection", parseInt(i), !1), $("#" + e).focus())
}

function AtatchModalEsc(e, i, t) {
    $(dlgrFocus).off("keydown"), $(dlgrFocus).on("keydown", function (e) {
        return e.keyCode === $.ui.keyCode.ESCAPE && "minimized" != $("#" + $(dlgrFocus).find("#hdIdDialog").val()).dialogExtend("state") ? (onClickCancelar(i, t), SetJQGridFocus(i, t), !1) : void 0
    })
}

function AtatchModalExitEsc(e, i, t) {
    $(dlgrFocus).off("keydown"), $(dlgrFocus).on("keydown", function (e) {
        return e.keyCode === $.ui.keyCode.ESCAPE ? (onClickSalir(i, t), SetJQGridFocus(i, t), !1) : void 0
    })
}

function AtatchModalCloseValidationButton(e, i) {
    $(dlgrFocus).find(".ui-dialog-titlebar-buttonpane").find("#customClose").unbind("click"), $(dlgrFocus).find(".ui-dialog-titlebar-buttonpane").find("#customClose").click(function () {
        return onClickCancelar(e, i), SetJQGridFocus(e, i), !1
    })
}

function AtatchModalCloseExitButton(e, i) {
    $($(dlgrFocus).find(".ui-dialog-titlebar-buttonpane")).find("#customClose").unbind("click"), $($(dlgrFocus).find(".ui-dialog-titlebar-buttonpane")).find("#customClose").click(function () {
        return onClickSalir(e, i), SetJQGridFocus(e, i), !1
    })
}

function EnviarEmailExceptionAuditoria(e, i, t, o, n, a) {
    jQuery.ajax({
        type: "POST",
        url: baseUrl + "Base/EnviarEmailExceptionAuditoria",
        dataType: "json",
        async: !0,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            errorMessage: a,
            idTabla: e,
            nombreTabla: i,
            nombreTablaDet: t,
            arrayOldItem: JSON.stringify(o),
            arrayNewItem: JSON.stringify(n)
        }),
        success: function () { },
        error: function () { }
    })
}

function RegistrarAuditoriaVersionamiento(e, i, t, o, n) {
    var a = [];
    for (var r in i) "object" != typeof e[r] && a.push({
        idTablaMae: o,
        nombreTablaMae: t,
        nombreCampo: r,
        valorAnterior: "",
        valorNuevo: e[r],
        operacion: "INSERCION"
    });
    if (a.length > 0) {
        var l = {
            listaAuditoria: a
        };
        $.ajaxSetup({
            cache: !1
        }), jQuery.ajax({
            type: "POST",
            async: true,
            url: baseUrl + "Auditoria/Insert",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                model: l,
                IdAuditoria: n
            }),
            //async: !0,
            success: function () { }
        })
    }
    return a
}

function RegistarAuditoriaHeader(e, i, t) {
    var o = [];
    o.push({
        idTablaMae: i,
        nombreTablaMae: e,
        nombreCampo: "HEADER",
        valorAnterior: "",
        valorNuevo: "",
        operacion: "INSERCION"
    });
    var n = {
        listaAuditoria: o
    };
    $.ajaxSetup({
        cache: !1
    }), jQuery.ajax({
        type: "POST",
        url: baseUrl + "Auditoria/Insert",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            model: n,
            IdAuditoria: t
        }),
        async: !0,
        success: function () { }
    })
}

function RegistrarAuditoria(e, i, t, o, n, a) {
    var r = [];
    if ("ELIMINACION" != o)
        for (var l in i) "object" != typeof e[l] && e.hasOwnProperty(l) && e[l] != i[l] && r.push({
            idTablaMae: n,
            nombreTablaMae: t,
            nombreCampo: l,
            valorAnterior: i[l],
            valorNuevo: e[l],
            operacion: o
        });
    else r.push({
        idTablaMae: n,
        nombreTablaMae: t,
        nombreCampo: "",
        valorAnterior: "",
        valorNuevo: "",
        operacion: o
    }); if (r.length > 0) {
        var d = {
            listaAuditoria: r
        };
        $.ajaxSetup({
            cache: !1
        }), jQuery.ajax({
            type: "POST",
            url: baseUrl + "Auditoria/Insert",
            dataType: "json",
            async: true,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                model: d,
                IdAuditoria: a
            }),
            //async: !0,
            success: function () { }
        })
    }
    return r
}

function RegistrarAuditoriaDetalleVersionamiento(e, i, t, o, n, a) {
    for (var r = [], l = 0; l < e.length; l++)
        for (var d = 0; d < a.length; d++) r.push({
            idTablaMae: i,
            idTablaDet: e[l][n],
            nombreTablaMae: t,
            nombreTablaDet: o,
            nombreCampo: a[d],
            valorAnterior: "",
            valorNuevo: e[l][a[d]],
            operacion: "INSERCION"
        });
    return r
}

function RegistrarAuditoriaDetalle(e, i, t, o, n, a, r, l) {
   

    var d = r,
        s = [];
    if ("INSERCION" == i)
        for (var c = 0; c < e.length; c++)
            for (var u = 0; u < l.length; u++) s.push({
                idTablaMae: t,
                idTablaDet: e[c][a],
                nombreTablaMae: o,
                nombreTablaDet: n,
                nombreCampo: l[u],
                valorAnterior: "",
                valorNuevo: e[c][l[u]],
                operacion: i
            });
    else {
        for (var c = 0; c < d.Item.length; c++) {
            for (var f = !1, g = 0; g < e.length; g++)
                if (e[g][a] == d.Item[c][a]) {
                    f = !0;
                    break
                }
            f || s.push({
                idTablaMae: t,
                idTablaDet: d.Item[c][a],
                nombreTablaMae: o,
                nombreTablaDet: n,
                nombreCampo: "",
                valorAnterior: "",
                valorNuevo: "",
                operacion: "ELIMINACION"
            })
        }
        for (var c = 0; c < e.length; c++) {
            for (var p = [], g = 0; g < d.Item.length; g++)
                if (d.Item[g][a] == e[c][a]) {
                    p.push(d.Item[g]);
                    break
                }
            if (0 == p.length)
                for (var u = 0; u < l.length; u++) s.push({
                    idTablaMae: t,
                    idTablaDet: e[c][a],
                    nombreTablaMae: o,
                    nombreTablaDet: n,
                    nombreCampo: l[u],
                    valorAnterior: "",
                    valorNuevo: e[c][l[u]],
                    operacion: "INSERCION"
                })
        }
        for (var c = 0; c < d.Item.length; c++) {
            for (var p = [], g = 0; g < e.length; g++)
                if (d.Item[c][a] == e[g][a]) {
                    p.push(e[g]);
                    break
                }
            if (p.length > 0)
                for (var h in d.Item[c]) l.contains(h) && d.Item[c][h] != p[0][h] && s.push({
                    idTablaMae: t,
                    idTablaDet: d.Item[c][a],
                    nombreTablaMae: o,
                    nombreTablaDet: n,
                    nombreCampo: h,
                    valorAnterior: d.Item[c][h],
                    valorNuevo: p[0][h],
                    operacion: "ACTUALIZACION"
                })
        }
    }
    return s
}

function GetListCaracteristica(e, i, t) {
    for (var o = [], n = $.dlg("#" + e + " :input"), a = 0; a < n.length; a++) o.push({
        idCaracteristica: n[a].id.split(i)[1],
        valor: $.dlg("#" + n[a].id).val()
    }), o[a][t] = $.dlg("#" + n[a].id).attr("data-id");
    return o
}

function jsonRegistrarAuditoriaDetalle(e, i) {
    $.ajaxSetup({
        cache: !1
    }), jQuery.ajax({
        type: "POST",
        url: baseUrl + "Auditoria/InsertDetalle",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            lstAuditoria: e,
            IdAuditoria: i
        }),
        async: !0,
        success: function () { }
    })
}

function fnCleanDivisionesGen() {
    var e = $.dlg("#ddlDivisionHD");
    e.empty()
}

function fnLoadDivisionesGen() {
    var e = "";
    $.ajaxSetup({
        cache: !1
    }), waitingDialog({}), $.getJSON(baseUrl + "Base/ObtenerDivisionesUsuaurioBase", function (i) {
        if (i.success) {
            fnCleanDivisionesGen();
            var t = $("#ddlDivisionHD");
            if (i.lista.length > 0) {
                t.append($("<option/>", {
                    value: "",
                    text: "-- TODAS --"
                }));
                for (var o = 0; o < i.lista.length; o++) "S" == i.lista[o].flgDivisionDefecto && (e = i.lista[o].idDivision), t.append($("<option/>", {
                    value: i.lista[o].idDivision,
                    text: i.lista[o].nombreDivision
                }))
            }
            $("#ddlDivisionHD").val(e), 1 == i.lista.length && $("#ddlDivisionHD").attr("disabled", !0), LoadIniGrid()
        }
        closeWaitingDialog()
    })
}

function fnLoadDivisionesGenCalendar() {
    var e = "";
    $.ajaxSetup({
        cache: !1
    }), waitingDialog({}), $.getJSON(baseUrl + "Base/ObtenerDivisionesUsuaurioBase", function (i) {
        if (i.success) {
            fnCleanDivisionesGen();
            var t = $("#ddlDivisionHD");
            if (t.empty(), i.lista.length > 0) {
                t.append($("<option/>", {
                    value: "",
                    text: "-- TODOS --"
                }));
                for (var o = 0; o < i.lista.length; o++) "S" == i.lista[o].flgDivisionDefecto && (e = i.lista[o].idDivision), t.append($("<option/>", {
                    value: i.lista[o].idDivision,
                    text: i.lista[o].nombreDivision
                }))
            }
            $("#ddlDivisionHD").val(e), 1 == i.lista.length && $("#ddlDivisionHD").attr("disabled", !0), LoadIniCalendar()
        }
        closeWaitingDialog()
    })
}

function fnChangeDivisioGen(e) {
    $("#ddlDivisionHD").change(function () {
        jQuery("#" + e).setGridParam({
            datatype: "json",
            page: 1
        }).trigger("reloadGrid")
    })
}

function HideJQDialog() {
    $("#info_dialog").parent().find(".ui-widget-overlay").css("display", "none"), $("#info_dialog").css("visibility", "hidden"), $("#info_dialog").hide(), $("#info_head").css("visibility", "hidden"), $("#info_content").css("visibility", "hidden")
}

function afterLoading() {
    parent.closeWaitingDialogCustom(), closeWaitingDialogCustom()
}

function ExportExcelMain(e, i) {
    var t = !0;
    if (0 == $("#" + e).getGridParam("reccount") && (t = confirm("La grilla se encuentra vacía.\n¿ Está seguro(a) que desea continuar con la operación ?")), t) {
        $(".modalScreen").css("display", "inline");
        parent.waitingDialogCustom({}), waitingDialogCustom({});
        var o = document.createElement("iframe");
        o.style.display = "none", o.id = "ifrmExport", o.setAttribute("src", i);
        var n = window.navigator.userAgent,
            a = n.indexOf("MSIE "),
            r = n.indexOf("Trident/");

        document.body.appendChild(o), document.body.onload = r > 0 || a > 0 ? checkIframeLoaded(!0) : checkIframeLoadedNotIE()
    }
}

function checkIframeLoadedNotIE() {
    var e = getCookie("excelExport");
    "1" == e ? (parent.closeWaitingDialogCustom(), closeWaitingDialogCustom(), $.ajax({
        type: "GET",
        url: baseUrl + "Base/CleanCookie",
        dataType: "json",
        data: {
            CookieName: "excelExport"
        },
        contentType: "application/json; charset=utf-8",
        async: !1,
        cache: !1,
        success: function (data) {
            $(".modalScreen").css("display", "none");
        }
    })) : window.setTimeout("checkIframeLoadedNotIE();", 100)
}

function checkIframeLoaded(e) {
    if (iframe = document.getElementById("ifrmExport"), null != iframe) {
        var i = iframe.contentDocument || iframe.contentWindow.document;
        if (e) try {
            "" != i.fileCreatedDate && afterLoading();
            $(".modalScreen").css("display", "none");
        } catch (t) {
            window.setTimeout(function () {
                checkIframeLoaded(e)
            }, 100)
        } else "complete" == i.readyState ? afterLoading() : window.setTimeout(function () {
            checkIframeLoaded(e)
        }, 100)
    } else window.setTimeout(function () {
        checkIframeLoaded(e)
    }, 100)
}

function getCookie(e) {
    var i, t, o, n = document.cookie.split(";");
    for (i = 0; i < n.length; i++)
        if (t = n[i].substr(0, n[i].indexOf("=")), o = n[i].substr(n[i].indexOf("=") + 1), t = t.replace(/^\s+|\s+$/g, ""), t == e) return unescape(o)
}

function get_TabFocus() {
    tabFocus = $(".ctab", parent.document)
}

function autoResize_ByModal() { }

function InicializarBotonesModalSession() {
    get_DialogFocus(), $(dlgrFocus).find("#btnGrabar").unbind("click"), $(dlgrFocus).find("#btnGrabar").click(function () {
        get_DialogFocus(), onClickCancelarCustom("list", $.dlg("#hdIndex").val(), "2")
    }), $(dlgrFocus).find("#btnCancelar").unbind("click"), $(dlgrFocus).find("#btnCancelar").click(function () {
        get_DialogFocus(), onClickCancelarCustom("list", $.dlg("#hdIndex").val(), "1")
    }), $(dlgrFocus).find("#btnSalir").unbind("click"), $(dlgrFocus).find("#btnSalir").click(function () {
        get_DialogFocus(), onClickSalir("list", $.dlg("#hdIndex").val())
    })
}

function CompareLists(e, i) {
    var t = Object.size(e),
        o = Object.size(i),
        n = !1;
    if (parseInt(t) == parseInt(o)) {
        for (var a = 0; a < Object.size(e) ; a++)
            for (key in e[a])
                if (e[a].hasOwnProperty(key) && e[a][key] != (0 == Object.size(i) ? "" : "undefined" == i[a] ? "" : i[a][key])) {
                    n = !0;
                    break
                }
    } else n = !0;
    return n
}

function onClickGrabar_ColorBox() {
    var e = existChangesColorBox();
    return e ? GuardarColorBox() : $.colorbox.close(), e
}

function onClickCancelar_ColorBox() {
    var e = existChangesColorBox();
    return e ? (CleanValidations_Colorbox(), setTimeout(function () {
        $.colorbox.resize()
    }, 0)) : $.colorbox.close(), e
}

function onClickNo_ColorBox() {
    return $.cbx("#divFieldsColorBox").unblock(), $.cbx("#divNotAjaxColorBox").hide(), $.cbx("#divAjaxColorBox").show(), !1
}

function onClickSalir_ColorBox() {
    $.cbx("#divFieldsColorBox").unblock(), $.cbx("#divNotAjaxColorBox").hide(), $.cbx("#divAjaxColorBox").show(), $.colorbox.close()
}

function onClickYes_ColorBox() {
    return GuardarColorBox(), !1
}

function CleanDivsValidations_ColorBox() {
    $.cbx("#divMensajePlant ul").html(""), $.cbx("#divMensajePlant").fadeOut("fast"), $.cbx("#divFieldsColorBox").block({
        message: null
    }), $.cbx("#divAjaxColorBox").hide(), $.cbx("#divNotAjaxColorBox").show(), $.cbx("#btnYesColorBox").focus()
}

function onClickGrabarWithParam_ColorBox(e, i) {
    var t = e();
    return t ? i() : $.colorbox.close(), t
}

function onClickCancelarWithParam_ColorBox(e, i) {
    var t = e();
    return t ? (i(), setTimeout(function () {
        $.colorbox.resize()
    }, 0)) : $.colorbox.close(), t
}

function onClickYesWithParam_ColorBox(e) {
    return e(), !1
}

function AppendMessage_ColorBox(e) {
    $.cbx("#divMensajePlant ul").append("<li> * " + e + "</li>")
}

function SetBackgroundField_ColorBox(e, i) {
    $.cbx("#" + e).css("background", i), "#FDE2E2" == i && $.cbx("#" + e).focus()
}

function UnblockandShowErrors_ColorBox() {
    $.cbx("#divMensajePlant").fadeIn("slow"), $.cbx("#divFieldsColorBox").unblock(), $.cbx("#divNotAjaxColorBox").hide(), $.cbx("#divAjaxColorBox").show(), $.colorbox.resize()
}

function sortJsonList(e, i, t) {
    var o = e.sort(function (e, o) {
        return t ? e[i] > o[i] ? 1 : e[i] < o[i] ? -1 : 0 : o[i] > e[i] ? 1 : o[i] < e[i] ? -1 : 0
    });
    return o
}

function InitFocusGrid(e) {
    $(dlgrFocus).find("#" + e).focus(function () {
        var i = $(dlgrFocus).find("#" + e).jqGrid("getGridParam", "selrow");
        if (null == i || void 0 == i) {
            var t = $(dlgrFocus).find("#" + e).find("#1").length;
            if (parseInt(t) > 0) $(dlgrFocus).find("#" + e).jqGrid("setSelection", 1, !0);
            else {
                var o = $(dlgrFocus).find("#" + e).find("tr");
                o.length >= 2 && $(dlgrFocus).find("#" + e).jqGrid("setSelection", o[1].id, !0)
            }
        } else 1 != i && ($(dlgrFocus).find("#" + e).jqGrid("setSelection", 1, !0), $(dlgrFocus).find("#" + e).focus())
    }), $(dlgrFocus).find("#" + e).focusout(function () {
        {
            var i = $(dlgrFocus).find("#" + e).jqGrid("getGridParam", "selrow");
            document.activeElement.id
        }
        if (void 0 == i || null == i) {
            for (var t = $(dlgrFocus).find("#" + e).find("tr"), o = 0; o < t.length; o++)
                if ("" != t[o].id && "1" == $(dlgrFocus).find("#" + e).find("#" + t[o].id).attr("editable")) return !1;
            $(dlgrFocus).find("#" + e).jqGrid("resetSelection")
        }
    }), $(dlgrFocus).find("input, select").focus(function () {
        for (var i = $(dlgrFocus).find("#" + e).find("tr"), t = 0; t < i.length; t++)
            if ("" != i[t].id && "1" == $(dlgrFocus).find("#" + e).find("#" + i[t].id).attr("editable")) return !1;
        $(dlgrFocus).find("#" + e).jqGrid("resetSelection")
    })
}

function AddExcelButtonToGrid(e, i, t) {
    $("#" + e).jqGrid("navButtonAdd", "#" + i, {
        caption: "Excel",
        buttonicon: "ui-icon-extlink",
        onClickButton: t,
        position: "last"
    })
}

function getColumnIndexByName(e, i) {
    var t, o = e,
        n = o.length;
    for (t = 0; n > t; t++)
        if (o[t].name === i) return t;
    return -1
}

function OpenDialogId(e, i, t, o, n, a) {
    var r = t.split("?").length,
        l = "DIALOG" + ("0" == o ? dialogCounter : o);
    return t = t + (r > 1 ? "&IdDialog=" : "?IdDialog=") + l, 0 == $("#" + l).length && (parent.waitingDialog({}), $("#ContenidogDialog").append("<div id='" + l + "' data-fn='" + (void 0 == a || null == a ? "" : a) + "' data-url='' style='overflow: hidden !important' ></div>"), $("#" + l).dialog({
        modal: !0,
        autoOpen: !1,
        closeOnEscape: !1,
        resizable: !1,
        width: i,
        height: "auto",
        position: "center",
        draggable: !0,
        title: e
    }), $("#" + l).dialogExtend({
        minimizable: !1,
        maximizable: !1,
        titlebar: "transparent",
        minimizeLocation: "right",
        icons: {
            minimize: "ui-icon-circle-minus",
            maximize: "ui-icon-circle-plus"
        },
        beforeMaximize: function () {
            get_DialogFocus(), $(dlgrFocus).parent().poshytip("hide"), $(dlgrFocus).find("#hdDialogState").val($(dlgrFocus).find("#divNotAjax").css("display"))
        },
        beforeRestore: function () {
            get_DialogFocus(), $(dlgrFocus).parent().poshytip("hide"), $(dlgrFocus).find("#hdDialogState").val($(dlgrFocus).find("#divNotAjax").css("display"))
        },
        maximize: function () {
            get_DialogFocus(), "block" == $(dlgrFocus).find("#divAjax").css("display") && "block" == $(dlgrFocus).find("#hdDialogState").val() ? ($(dlgrFocus).find("#divNotAjax").css("display", "block"), $(dlgrFocus).find("#divAjax").css("display", "none")) : ($(dlgrFocus).find("#divNotAjax").css("display", "none"), $(dlgrFocus).find("#divAjax").css("display", "block")), $(dlgrFocus).focus()
        },
        minimize: function () {
            get_DialogFocus(), "block" == $(dlgrFocus).find("#divAjax").css("display") ? ($(dlgrFocus).find("#divNotAjax").css("display", "none"), $(dlgrFocus).find("#divAjax").css("display", "block")) : ($(dlgrFocus).find("#divNotAjax").css("display", "block"), $(dlgrFocus).find("#divAjax").css("display", "none")), AttachToolTip(n), AttachModalEscMinimize()
        },
        restore: function () {
            "none" == $(dlgrFocus).find("#hdDialogState").val() ? ($(dlgrFocus).find("#divNotAjax").css("display", "none"), $(dlgrFocus).find("#divAjax").css("display", "block")) : ($(dlgrFocus).find("#divNotAjax").css("display", "block"), $(dlgrFocus).find("#divAjax").css("display", "none")), $(dlgrFocus).focus()
        }
    })), $.ajaxSetup({
        cache: !1
    }), $("#" + l).data("url", baseUrl + t), $("#" + l).load(baseUrl + t, function () {
        parent.closeWaitingDialog()
    }), $("#" + l).dialog("open"), $("#" + l).dialogExtend("restore"), $("#" + l).focus(), set_MaxZIndex(l), dialogCounter++, l
}

function OpenDialogCustom(e, i, t, o, n, a, r) {
    var l = o.split("?").length,
        d = "DIALOG" + ("0" == n ? dialogCounter : n);
    return o = o + (l > 1 ? "&IdDialog=" : "?IdDialog=") + d, 0 == $("#" + d).length && (parent.waitingDialog({}), $("#ContenidogDialog").append("<div id='" + d + "' data-fn='" + (void 0 == r || null == r ? "" : r) + "' data-url='' style='overflow-x: hidden !important' ></div>"), $("#" + d).dialog({
        modal: !1,
        autoOpen: !1,
        closeOnEscape: !1,
        resizable: !1,
        width: i,
        height: t,
        position: "top",
        draggable: !0,
        title: e
    }), $("#" + d).dialogExtend({
        minimizable: !0,
        maximizable: !0,
        titlebar: "transparent",
        minimizeLocation: "right",
        icons: {
            minimize: "ui-icon-circle-minus",
            maximize: "ui-icon-circle-plus"
        },
        beforeMaximize: function () {
            get_DialogFocus(), $(dlgrFocus).parent().poshytip("hide"), $(dlgrFocus).find("#hdDialogState").val($(dlgrFocus).find("#divNotAjax").css("display"))
        },
        beforeRestore: function () {
            get_DialogFocus(), $(dlgrFocus).parent().poshytip("hide"), $(dlgrFocus).find("#hdDialogState").val($(dlgrFocus).find("#divNotAjax").css("display"))
        },
        maximize: function () {
            get_DialogFocus(), "block" == $(dlgrFocus).find("#divAjax").css("display") && "block" == $(dlgrFocus).find("#hdDialogState").val() ? ($(dlgrFocus).find("#divNotAjax").css("display", "block"), $(dlgrFocus).find("#divAjax").css("display", "none")) : ($(dlgrFocus).find("#divNotAjax").css("display", "none"), $(dlgrFocus).find("#divAjax").css("display", "block")), $(dlgrFocus).focus()
        },
        minimize: function () {
            get_DialogFocus(), "block" == $(dlgrFocus).find("#divAjax").css("display") ? ($(dlgrFocus).find("#divNotAjax").css("display", "none"), $(dlgrFocus).find("#divAjax").css("display", "block")) : ($(dlgrFocus).find("#divNotAjax").css("display", "block"), $(dlgrFocus).find("#divAjax").css("display", "none")), AttachToolTip(a), AttachModalEscMinimize()
        },
        restore: function () {
            "none" == $(dlgrFocus).find("#hdDialogState").val() ? ($(dlgrFocus).find("#divNotAjax").css("display", "none"), $(dlgrFocus).find("#divAjax").css("display", "block")) : ($(dlgrFocus).find("#divNotAjax").css("display", "block"), $(dlgrFocus).find("#divAjax").css("display", "none")), $(dlgrFocus).focus()
        }
    })), $.ajaxSetup({
        cache: !1
    }), $("#" + d).data("url", baseUrl + o), $("#" + d).load(baseUrl + o, function () {
        parent.closeWaitingDialog()
    }), $("#" + d).dialog("open"), $("#" + d).dialogExtend("restore"), $("#" + d).focus(), set_MaxZIndex(d), dialogCounter++, d
}

function ValidateGrids(e, i) {
    var t = !0,
        o = [];
    if (e.length > 0)
        for (var n = 0; n < e.length; n++) {
            arrayGrid = e[n].split("||");
            var a = $.dlg("#" + arrayGrid[0]).val(),
                r = $(dlgrFocus).find("#" + a).find("tbody tr");
            e: for (var l = 0; l < r.length; l++) {
                if ("" != r[l].id && "1" == $(r[l]).attr("editable")) {
                    t = !1, o.push(a);
                    break e
                }
                if ("ui-subgrid" == r[l].className)
                    for (var d = $($(r[l]).find(".tablediv").children()[1]).find(".ui-jqgrid-btable").find("tbody tr"), s = 0; s < d.length; s++)
                        if ("" != d[s].id && "1" == $(d[s]).attr("editable")) {
                            t = !1, o.push(a);
                            break e
                        }
            }
        }
    if (!t) {
        $.dlg("#divMensaje ul").html("");
        for (var n = 0; n < e.length; n++) {
            splitArray = e[n].split("||");
            for (var a = $.dlg("#" + splitArray[0]).val(), l = 0; l < o.length; l++) o[l] == a && (AppendMessage("La rejilla " + splitArray[1] + " está en edición, complete la operación."), void 0 != i && $.dlg("#" + i).tabs("select", "#" + splitArray[2]))
        }
        return UnblockandShowErrors(), t
    }
    return t
}

function ValidateGridsMain(e, i) {
    var t = !0,
        o = [];
    if (e.length > 0)
        for (var n = 0; n < e.length; n++) {
            arrayGrid = e[n].split("||");
            var a = $("#" + arrayGrid[0]).val(),
                r = $("#" + a).find("tbody tr");
            e: for (var l = 0; l < r.length; l++) {
                if ("" != r[l].id && "1" == $(r[l]).attr("editable")) {
                    t = !1, o.push(a);
                    break e
                }
                if ("ui-subgrid" == r[l].className)
                    for (var d = $($(r[l]).find(".tablediv").children()[1]).find(".ui-jqgrid-btable").find("tbody tr"), s = 0; s < d.length; s++)
                        if ("" != d[s].id && "1" == $(d[s]).attr("editable")) {
                            t = !1, o.push(a);
                            break e
                        }
            }
        }
    if (!t) {
        $("#divMensaje ul").html("");
        for (var n = 0; n < e.length; n++) {
            splitArray = e[n].split("||");
            for (var a = $("#" + splitArray[0]).val(), l = 0; l < o.length; l++) o[l] == a && (AppendMessagePage("La rejilla " + splitArray[1] + " está en edición, complete la operación."), void 0 != i && $("#" + i).tabs("select", "#" + splitArray[2]))
        }
        return UnblockandShowErrorsPage(), t
    }
    return t
}

function GetEditableRowId(e) {
    RowId = "";
    for (var i = $("#" + e).find("tr"), t = 0; t < i.length; t++) "" != i[t].id && "1" == $("#" + i[t].id).attr("editable") && (RowId = i[t].id);
    return RowId
}

function InitFocusGridColorBox(e, i) {
    $("#" + e).focus(function () {
        var i = $("#" + e).jqGrid("getGridParam", "selrow");
        if (null == i || void 0 == i) {
            var t = $("#" + e).find("#1").length;
            if (parseInt(t) > 0) $("#" + e).jqGrid("setSelection", 1, !0);
            else {
                var o = $("#" + e).find("tr");
                o.length >= 2 && $("#" + e).jqGrid("setSelection", o[1].id, !0)
            }
        } else 1 != i && ($("#" + e).jqGrid("setSelection", 1, !0), $("#" + e).focus())
    }), $("#" + e).focusout(function () {
        {
            var i = $("#" + e).jqGrid("getGridParam", "selrow");
            document.activeElement.id
        }
        if (void 0 == i || null == i) {
            for (var t = $("#" + e).find("tr"), o = 0; o < t.length; o++)
                if ("" != t[o].id && "1" == $("#" + e).find("#" + t[o].id).attr("editable")) return !1;
            $("#" + e).jqGrid("resetSelection")
        }
    }), $("#" + i).find("input, select").focus(function () {
        for (var i = $("#" + e).find("tr"), t = 0; t < i.length; t++)
            if ("" != i[t].id && "1" == $("#" + e).find("#" + i[t].id).attr("editable")) return !1;
        $("#" + e).jqGrid("resetSelection")
    })
}

function setFocusAtPosition(e, i) {
    if (e.setSelectionRange) e.focus(), e.setSelectionRange(i, i);
    else if (e.createTextRange) {
        var t = e.createTextRange();
        t.collapse(!0), t.moveEnd("character", i), t.moveStart("character", i), t.select()
    }
}

function InitFocusGridWithOutFocus(e) {
    $(dlgrFocus).find("#" + e).focus(function () {
        var i = $(dlgrFocus).find("#" + e).jqGrid("getGridParam", "selrow");
        if (null == i || void 0 == i) {
            var t = $(dlgrFocus).find("#" + e).find("#1").length;
            if (parseInt(t) > 0) $(dlgrFocus).find("#" + e).jqGrid("setSelection", 1, !0);
            else {
                var o = $(dlgrFocus).find("#" + e).find("tr");
                o.length >= 2 && $(dlgrFocus).find("#" + e).jqGrid("setSelection", o[1].id, !0)
            }
        } else 1 != i && ($(dlgrFocus).find("#" + e).jqGrid("setSelection", 1, !0), $(dlgrFocus).find("#" + e).focus())
    }), $(dlgrFocus).find("#" + e).focusout(function () {
        {
            var i = $(dlgrFocus).find("#" + e).jqGrid("getGridParam", "selrow");
            document.activeElement.id
        }
        if (void 0 == i || null == i) {
            for (var t = $(dlgrFocus).find("#" + e).find("tr"), o = 0; o < t.length; o++)
                if ("" != t[o].id && "1" == $(dlgrFocus).find("#" + e).find("#" + t[o].id).attr("editable")) return !1;
            $(dlgrFocus).find("#" + e).jqGrid("resetSelection")
        }
    })
}

function toTitleCase(e) {
    return e.replace(/\w\S*/g, function (e) {
        return e.charAt(0).toUpperCase() + e.substr(1).toLowerCase()
    })
}

function Pad(e, i, t, o) {
    if ("undefined" == typeof i) var i = 0;
    if ("undefined" == typeof t) var t = " ";
    if ("undefined" == typeof o) var o = STR_PAD_RIGHT;
    if (e = null == e ? "" : e.toString(), "" != e && i + 1 >= e.length) switch (o) {
        case STR_PAD_LEFT:
            e = Array(parseInt(i) + 1 - e.length).join(t) + e;
            break;
        case STR_PAD_BOTH:
            var n = Math.ceil((padlen = parseInt(i) - e.length) / 2),
                a = padlen - n;
            e = Array(a + 1).join(t) + e + Array(n + 1).join(t);
            break;
        default:
            e += Array(parseInt(i) + 1 - e.length).join(t)
    }
    return e
}

function RPadDecimal(e, i) {
    var t = e;
    if ("" != e) {
        var o = parseInt(i) - parseInt(e.length);
        parseInt(o) > 0 && (t = parseInt(e).toFixed(parseInt(o) - 1))
    }
    return t
}

function InitDialog() {
    get_DialogFocus(), $(dlgrFocus).click(function () {
        get_DialogFocus()
    })
}

function addButtonDisabled() {
    return message("Información:", "La opción está deshabilitada para tu Usuario", "info"), !1
}

function GetCaracteristicasGeneralByTablaDestino(e, i, t) {
    $.ajax({
        type: "GET",
        url: baseUrl + "Base/GetCaracteristicaByTablaDestinoBase",
        data: {
            TablaDestino: e
        },
        cache: !1,
        async: !0,
        dataType: "Json",
        success: function (e) {
            if (null != e && e.success) {
                for (var o = "", n = 0; n < e.lista.length; n++) o += "<option data-tipoValor='" + e.lista[n].tipoValor + "' value='" + e.lista[n].idCaracteristica + "'>" + e.lista[n].descripcion + "</option>";
                $("#ddlAtributosFilter").html(o), $("#ddlAtributosFilter").multipleSelect({
                    width: t,
                    filter: !0,
                    placeholder: i,
                    allowTipoValor: !0,
                    selectAll: !1
                }), $("#btnAddFilter").css("display", ""), $("#btnRemoveFilter").css("display", ""), $("#btnSearchGrid").css("display", "")
            }
        }
    })
}

function InitEventsCaracteristicasFilter() {
    $("#btnAddFilter").click(function () {

        var htmlToAppend = "";
        var arrayStringItems = $("#ddlAtributosFilter").multipleSelect("getSelects");
        if (arrayStringItems.length > 0) {
            var arrayStringTextItems = $("#ddlAtributosFilter").multipleSelect("getSelects", "text");
            var Items = arrayStringItems.toString().split(',');
            htmlToAppend += '<div class="item_form">';
            for (var i = 0; i < Items.length; i++) {
                var idCaracteristica = $.trim(Items[i].toString().split('|')[0]);
                var tipoValor = $.trim(Items[i].toString().split('|')[1]);
                var idElement = CONST_ID + idCaracteristica;
                htmlToAppend += '<div class="item_label">' + arrayStringTextItems[i] + ':</div>';
                htmlToAppend += GetCaracteristicasHTML(idCaracteristica, tipoValor, idElement, CONST_TABLADESTINO);
                if ((i + 1) % 4 == 0) {
                    htmlToAppend += '</div>';
                    htmlToAppend += '<div class="item_form">';
                }
            }
            htmlToAppend += '</div>';
            $("#divAtributos").html(htmlToAppend);
        } else
            alert("Debe seleccionar Atributos para agregarlos al Filtro");
    });

    $("#btnRemoveFilter").click(function () {
        $("#divAtributos").html("");
        $("#ddlAtributosFilter").multipleSelect("uncheckAll");
        jQuery("#list")[0].triggerToolbar();
    });

    $("#btnSearchGrid").click(function () {
        jQuery("#list")[0].triggerToolbar();
    });
}

function GetCaracteristicasHTML(idCaracteristica, tipoValor, idElement, tableDestino) {
    var htmlToAppend = "";
    $.ajax({
        type: 'GET',
        url: baseUrl + 'Base/GetDetCaracteristicaByTablaDestinoBase',
        data: { IdCaracteristica: idCaracteristica, TablaDestino: tableDestino },
        cache: false,
        async: false,
        dataType: 'Json',
        success: function (data) {
            if (data != null) {
                if (tipoValor != CONST_VALORMANUAL) {
                    var optionItems = "";
                    optionItems += "<option value=''>-- Seleccionar --</option>";
                    for (var j = 0; j < data.lista.length; j++) {
                        optionItems += "<option value='" + data.lista[j].idDetCaracteristica + "'>" + data.lista[j].descripcion + "</option>";
                    }
                    $("#" + idElement).html(optionItems);
                }
                if (tipoValor == CONST_VALORMANUAL)
                    htmlToAppend += '<div class="item_campo"><input data-tipoValor="' + tipoValor + '" type="text" id="' + idElement + '" style="height:auto" /></div>';
                else
                    htmlToAppend += '<div class="item_campo"><select data-tipoValor="' + tipoValor + '" id="' + idElement + '" style="line-height:0;height:auto">' + optionItems + '</select></div>';
            }
        }
    });
    return htmlToAppend;
}

function limitPost(e, i, t, o, n, a) {
    return e.length > i && (e = "\n" == e.slice(t, o) ? e.slice(0, n) + "... " : e.slice(0, a) + "... "), e.replace(/\n/g, "<br />")
}

function showAllPost(e, i, t, o, n) {
    var a, r = $.dlg("#divPost" + i)[0],
        l = decodeHTML($.dlg("#hdPost" + i)[0].value),
        d = r.getAttribute("data-modo");
    "0" === d ? ($.dlg(r).html(l.replace(/\n/g, "<br />") + " " + e.outerHTML.replace(/ver mas/g, "ver menos")), a = $.dlg(r).height(), $.dlg(r.parentElement).animate({
        height: a + "px"
    }, {
        duration: 40
    }), r.setAttribute("data-modo", "1")) : (a = $.dlg(r).height(), $.dlg(r.parentElement).animate({
        height: "22px"
    }, {
        duration: a > t ? o : n,
        complete: function () {
            r.innerHTML = limitPost(l, 30, 25, 26, 30, 31) + e.outerHTML.replace(/ver menos/g, "ver mas")
        }
    }), r.setAttribute("data-modo", "0"))
}

function showAllPostMainView(e, i, t, o, n) {
    var a, r = $("#divPost" + i)[0],
        l = decodeHTML($("#hdPost" + i)[0].value),
        d = r.getAttribute("data-modo");
    "0" === d ? ($(r).html(l.replace(/\n/g, "<br />") + " " + e.outerHTML.replace(/ver mas/g, "ver menos")), a = $(r).height(), $(r.parentElement).animate({
        height: a + "px"
    }, {
        duration: 40
    }), r.setAttribute("data-modo", "1")) : (a = $(r).height(), $(r.parentElement).animate({
        height: "22px"
    }, {
        duration: a > t ? o : n,
        complete: function () {
            r.innerHTML = limitPost(l, 30, 25, 26, 30, 31) + e.outerHTML.replace(/ver menos/g, "ver mas")
        }
    }), r.setAttribute("data-modo", "0"))
}

function encodeHTML(e) {
    return encodeURIComponent(e)
}

function decodeHTML(e) {
    return decodeURIComponent(e)
}

function GetAtributosToSearch() {
    for (var e = $("#divAtributos :input"), i = "", t = 0; t < e.length; t++) "" != e[t].value && (i = "" == i ? e[t].getAttribute("data-tipoValor") + "|" + e[t].value.toString().toUpperCase() : i + "," + e[t].getAttribute("data-tipoValor") + "|" + e[t].value.toString().toUpperCase());
    return i
}

function messageErrorServerAgenda(e, i) {
    $.pnotify({
        title: e,
        text: i,
        type: "error",
        sticker: !1,
        animation: {
            effect_in: "show",
            effect_out: "slide"
        },
        hide: !1,
        opacity: .8
    }), $("#popUpInforme").hide(), $("#popupEventForm").hide()
}

function LlenarUnidadMedida(e, i, t) {
    var o = $.dlg("#" + e).val(),
        n = $.dlg("#" + $.dlg("#" + i).val()).jqGrid("getCell", o, "idArticulo");
    jQuery.ajax({
        type: "GET",
        url: baseUrl + "Articulo/GetArticuloById",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: {
            idArticulo: n
        },
        async: !1,
        cache: !1,
        success: function (e) {
            null != e && (e.success ? jQuery.ajax({
                type: "GET",
                url: baseUrl + "Base/GetUnidadConversionByMagnitudBase",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: {
                    idMagnitud: e.model.idMagnitud
                },
                async: !1,
                cache: !1,
                success: function (i) {
                    if (null != i)
                        if (i.success) {
                            var n = "";
                            n += optiondefaultText, $.each(i.lst, function (e, i) {
                                n += "<option value='" + i.idUnidadConversion + "'>" + i.descripcion + "</option>"
                            }), $.dlg("#" + o + t).html(n), $.dlg("#" + o + t).val(e.model.idUnidadVenta)
                        } else messageErrorServer("Error:", "@Constantes.MensajeOpciones.ERRORSISTEMA")
                },
                error: function (i) {
                    parent.closeWaitingDialog(), messageErrorAjax("Error:", "@Constantes.MensajeOpciones.ERRORSISTEMA", i.status, baseUrl + "Base/GetUnidadConversionByMagnitudBase", {
                        idMagnitud: e.model.idMagnitud
                    })
                }
            }) : messageErrorServer("Error:", "@Constantes.MensajeOpciones.ERRORSISTEMA"))
        },
        error: function (e) {
            parent.closeWaitingDialog(), messageErrorAjax("Error:", "@Constantes.MensajeOpciones.ERRORSISTEMA", e.status, baseUrl + "Articulo/GetArticuloById", {
                idArticulo: n
            })
        }
    })
}

function DowloadSSRSFile(e) {
    var i = document.createElement("iframe");
    i.style.display = "none", i.id = "ifrmSSRSDownload", i.setAttribute("src", e);
    var t = window.navigator.userAgent,
        o = t.indexOf("MSIE "),
        n = t.indexOf("Trident/");
    document.body.appendChild(i), document.body.onload = n > 0 || o > 0 ? checkIframeLoadedSSRS(!0) : checkIframeLoadedNotIESSRS()
}

function checkIframeLoadedSSRS(e) {
    if (iframe = document.getElementById("ifrmSSRSDownload"), null != iframe) {
        var i = iframe.contentDocument || iframe.contentWindow.document;
        if (e) try {
            "" != i.fileCreatedDate && afterLoading()
        } catch (t) {
            window.setTimeout(function () {
                checkIframeLoadedSSRS(e)
            }, 100)
        } else "complete" == i.readyState ? afterLoading() : window.setTimeout(function () {
            checkIframeLoadedSSRS(e)
        }, 100)
    } else window.setTimeout(function () {
        checkIframeLoadedSSRS(e)
    }, 100)
}

function checkIframeLoadedNotIESSRS() {
    var e = getCookie("SSRSCookie");
    "1" == e ? (parent.closeWaitingDialogCustom(), closeWaitingDialogCustom(), $.ajax({
        type: "GET",
        url: baseUrl + "Base/CleanCookie",
        dataType: "json",
        data: {
            CookieName: "SSRSCookie"
        },
        contentType: "application/json; charset=utf-8",
        async: !1,
        cache: !1
    })) : window.setTimeout("checkIframeLoadedNotIESSRS();", 100)
}

function ConvertDatetoString(e) {
    var i = navigator.userAgent.toLowerCase(),
        t = "",
        o = new Date(e);
    t = -1 != i.indexOf("chrome") ? (o.getDate() + 1).toString() : o.getDate().toString();
    var n = (o.getMonth() + 1).toString(),
        a = o.getFullYear().toString(),
        r = t + "/" + (n[1] ? n : "0" + n[0]) + "/" + a;
    return r
}

function ConvertJsonDateToString(e) {
    var json = { "dt": e };
    var ddmmyyyy = '';

    var m = json.dt.match(/Date\((\d+)([+-]\d+)?\)/);
    if (!m) return ddmmyyyy;

    var dt = new Date();
    dt.setTime(parseInt(m[1], 10));
    var mm = dt.getMonth() + 1;
    var dd = dt.getDate();
    var yyyy = dt.getFullYear();
    ddmmyyyy = ((dd < 10 ? '0' : '') + dd) + '/' + ((mm < 10 ? '0' : '') + mm) + '/' + yyyy;

    return ddmmyyyy;
}

function DisableGridButtonsModal(e, i) {
    i ? ($.dlg("#" + e + "_iledit").removeClass("ui-pg-button ui-corner-all"), $.dlg("#" + e + "_iladd").removeClass("ui-pg-button ui-corner-all"), $.dlg("#" + e + "_iledit").addClass("ui-pg-button ui-corner-all ui-state-disabled"), $.dlg("#" + e + "_iladd").addClass("ui-pg-button ui-corner-all ui-state-disabled")) : ($.dlg("#" + e + "_iledit").removeClass("ui-pg-button ui-corner-all ui-state-disabled"), $.dlg("#" + e + "_iladd").removeClass("ui-pg-button ui-corner-all ui-state-disabled"), $.dlg("#" + e + "_iledit").addClass("ui-pg-button ui-corner-all"), $.dlg("#" + e + "_iladd").addClass("ui-pg-button ui-corner-all"))
}

function disabledGridCustomButtonsModal(gid, state, ae) {
    state ? ($.dlg("#" + gid + "_il" + ae).removeClass("ui-pg-button ui-corner-all"),
         $.dlg("#" + gid + "_il" + ae).addClass("ui-pg-button ui-corner-all ui-state-disabled")) :
        ($.dlg("#" + gid + "_il" + ae).removeClass("ui-pg-button ui-corner-all ui-state-disabled"),
         $.dlg("#" + gid + "_il" + ae).addClass("ui-pg-button ui-corner-all"));
}

function parseDate(e) {
    var i = e.split("/");
    return new Date(i[2], i[1] - 1, i[0])
}

function daydiff(e, i) {
    return (i - e) / 864e5
}

function CreateDynamicModel(e, i, t, o, n) {
    var a = "";
    if ("text" == i) a = document.createElement("input"), a.setAttribute("role", "textbox"), a.type = i, a.value = "", a.id = e, a.name = e, $(a).css({
        width: "98%"
    }), $(a).addClass("editable");
    else {
        var a = "";
        a = document.createElement("select"), a.setAttribute("role", "select"), a.style.height = "20px", a.id = e, a.name = e, $(a).css({
            width: "98%"
        }), $(a).addClass("editable")
    }
    $.dlg("#" + o).find("#" + n).find("td").eq(t).append(a)
}

function CleanDynamicModel(e, i, t) {
    $.dlg("#" + e).find("#" + i).find("td").eq(t)[0].innerHTML = ""
}

//function disabledContextMenu(e) {
//    $(e).bind("contextmenu", function () {
//        return !1
//    }), $(e).bind("keydown", function (e) {
//        var i = e.which ? e.which : window.event.keyCode,
//            t = e.which ? e.ctrlKey : window.event.ctrlKey,
//            o = e.which ? e.shiftKey : window.event.shiftKey,
//            n = t && o && (67 == i || 73 == i || 74 == i);
//        return n || (n = 123 == i), n ? !1 : !0
//    })
//}



function getTipoDatosByControl(e, i, t, o) {

    $.ajaxSetup({
        cache: !1
    }), jQuery.ajax({
        type: "GET",
        url: baseUrl + e,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: {},
        async: !1,
        success: function (e) {
            if (parent.closeWaitingDialog(), e.success) {
                var n = "";
                n += optiondefaultText, $.each(e.lst, function (e, i) {
                    n += "<option value='" + i.idMultitabla_02 + "'>" + i.descripcion.toUpperCase() + "</option>"
                }), $.dlg("#" + t).html(n), "" != i && $.dlg("#" + t).val(i), $.dlg("#" + t).attr("disabled", o)
            } else messageErrorServer("Error:", "@Constantes.MensajeOpciones.ERRORSISTEMA")
        },
        error: function (i) {
            parent.closeWaitingDialog(), messageErrorAjax("Error:", "@Constantes.MensajeOpciones.ERRORSISTEMA", i.status, baseUrl + e, {})
        }
    })
}

function ContainsRol(e, i) {
    for (var t = !1, o = e.split(","), n = 0; n < o.length; n++)
        if (o[n] == i) {
            t = !0;
            break
        }
    return t
}

function ScrollDynamicGridPage(GridName, maxHeight) {

    maxHeight = maxHeight === undefined ? 410 : maxHeight;
    var $bdiv = $($("#" + GridName)[0].grid.bDiv);
    var Height = screen.height;
    var Width = screen.width;
    var resolucion = '@Constantes.Resoluciones.R1920X1080';
    if (Width == resolucion.split("X")[0] && Height == resolucion.split("X")[1])
        $bdiv.attr('style', 'overflow: auto !important;max-height:250px');
    else
        $bdiv.attr('style', 'overflow: auto !important;max-height:' + maxHeight + 'px');
    //maxHeight = maxHeight === undefined || maxHeight == null ? 410 : maxHeight;
    //var $bdiv = $($("#" + GridName)[0].grid.bDiv);
    //var Height = screen.height;
    //var Width = screen.width;
    //var resolucion = '@Constantes.Resoluciones.R1920X1080';
    //var widthBdiv = $bdiv.css('width');
    //if (Width == resolucion.split("X")[0] && Height == resolucion.split("X")[1])
    //    $bdiv.attr('style', 'overflow: auto !important;overflow-x:auto !important;max-height:250px');
    //else {
    //    var newWidth = (parseFloat(widthBdiv) + 17);
    //    $bdiv.attr('style', 'overflow: auto !important;overflow-x:auto !important;max-height:' + maxHeight + 'px;width:' + newWidth + 'px;');
    //    //$bdiv.attr('style', 'overflow: auto !important;overflow-x:auto !important;max-height:' + maxHeight + 'px;');
    //}
}

function ScrollDynamicGrid(GridName, maxHeight) {

    maxHeight = maxHeight === undefined ? 150 : maxHeight;
    var $bdiv = $($.dlg("#" + GridName)[0].grid.bDiv);
    var Height = screen.height;
    var Width = screen.width;
    var resolucion = '@Constantes.Resoluciones.R1920X1080';
    if (Width == resolucion.split("X")[0] && Height == resolucion.split("X")[1])
        $bdiv.attr('style', 'overflow: auto !important;max-height:250px');
    else
        $bdiv.attr('style', 'overflow: auto !important;max-height:' + maxHeight + 'px');

    //maxHeight = maxHeight === undefined ? 150 : maxHeight;
    //var $bdiv = $($.dlg("#" + GridName)[0].grid.bDiv);
    //var Height = screen.height;
    //var Width = screen.width;
    //var resolucion = '@Constantes.Resoluciones.R1920X1080';
    //var widthBdiv = $bdiv.css('width');
    //if (Width == resolucion.split("X")[0] && Height == resolucion.split("X")[1])
    //    $bdiv.attr('style', 'overflow: auto !important;overflow-x: visible !important;max-height:250px');
    //else {
    //    var newWidth = (parseFloat(widthBdiv) + 17);
    //$bdiv.attr('style', 'overflow: auto !important;overflow-y: auto !important;max-height:' + maxHeight + 'px;width:' + newWidth + 'px;');
    //}
}

function dateFormatRango(e, i) {
    var t, o = "";
    null == i ? (o = "Creado el ", t = e) : (o = "Modificado el ", t = i);
    //var n = new Date(parseInt(t.replace(/(^.*\()|([+-].*$)/g, "")));

    var parts = t.split('/');
    var n = new Date(parts[2], parts[1] - 1, parts[0]);

    return o += getDayWeekText(n.getDay()) + " " + n.getDate() + " " + getMonthText(n.getMonth()) + ", " + n.getFullYear() + " a las " + (1 == n.getHours().toString().length ? "0" + n.getHours() : n.getHours()) + ":" + (1 == n.getMinutes().toString().length ? "0" + n.getMinutes() : n.getMinutes())
}
var dialogCounter = 1,
    ContadorAuditoria = 0,
    dlgrFocus = dlgrFocus || {}, tabFocus = tabFocus || {}, arrDataObject = arrDataObject || {
        Item: []
    }, autoResizeHandle = null,
    integerOnly = /[1234567890]/g,
    decimalOnly = /[0-9\.]/g,
    alphaOnly = /[A-Za-z]/g,
    usernameOnly = /[a-zA-Z0-9_.-ñÑáéíóú@@\\// -]/g,
    delete_cookie = function (e) {
        document.cookie = e + "=;expires=Thu, 01 Jan 1970 00:00:01 GMT;"
    };
window.console || (console = {
    log: function () { }
}), Array.prototype.contains = function (e) {
    return this.indexOf(e) > -1
}, Number.prototype.between = function (e, i, t) {
    var o = Math.min.apply(Math, [e, i]),
        n = Math.max.apply(Math, [e, i]);
    return t ? this >= o && n >= this : this > o && n > this
}, isObject = function (e) {
    return !!e && e.constructor === Object
}, Object.size = function (e) {
    var i, t = 0;
    for (i in e) e.hasOwnProperty(i) && t++;
    return t
}, Array.prototype.last = function () {
    return this[this.length - 1]
}, Array.prototype.first = function () {
    return this[0]
}, String.prototype.filename = function (e) {
    var i = this.replace(/\\/g, "/");
    return i = i.substring(i.lastIndexOf("/") + 1), e ? i.replace(/[?#].+$/, "") : i.split(".")[0]
};
var sort_by = function () {
    var e = [].slice.call(arguments),
        i = e.length;
    return function (t, o) {
        for (var n, a, r, l, d, s, c = 0, u = i; u > c && (s = 0, r = e[c], l = "string" == typeof r ? r : r.name, n = t[l], a = o[l], "undefined" != typeof r.primer && (n = r.primer(n), a = r.primer(a)), d = r.reverse ? -1 : 1, a > n && (s = -1 * d), n > a && (s = 1 * d), 0 === s) ; c++);
        return s
    }
}, STR_PAD_LEFT = 1,
    STR_PAD_RIGHT = 2,
    STR_PAD_BOTH = 3;
//Valida que el personal tenga el permiso para generar el número documento por la Pantalla Especifica.
function ValidarTipoDocumentoExistencia(idTipoDocEmpresa, nombreTabla) {

    var result = "";
    jQuery.ajax({
        type: 'GET',
        url: baseUrl + 'Base/ValidarExistenciaTipoDocumento',
        dataType: 'json',
        async: false,
        cache: false,
        data: {
            idTipoDocEmpresa: idTipoDocEmpresa,
            nombreTabla: nombreTabla
        },
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            if (data != null) {
                if (data.success) {
                    if (data.message != "")
                        result = data.message;
                } else
                    messageErrorServer('Error:', '@Constantes.MensajeOpciones.ERRORSISTEMA');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            messageErrorAjax('Error:', '@Constantes.MensajeOpciones.ERRORSISTEMA', xhr.status, baseUrl + 'Base/ValidarExistenciaTipoDocumento', JSON.stringify({ idTipoDocEmpresa: idTipoDocEmpresa, nombreTabla: nombreTabla }));
        }
    });
    return result;
}
//Obtiene la lista de unidades
function getUnidadArticulo(idMagnitud, Unidad, idUnidad, flag) {
   

    jQuery.ajax({
        type: 'GET',
        url: baseUrl + 'Base/GetUnidadConversionByMagnitudBase',
        dataType: 'json',
        async: false,
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: { idMagnitud: idMagnitud },
        success: function (data) {
            
            if (data.success) {
                var items = "";
                items += optiondefaultText;
                $.each(data.lst, function (i, item) {
                    items += "<option value='" + item.idUnidadConversion + "'>" + item.descripcion.toUpperCase() + "</option>";
                });
                $.dlg("#" + Unidad).html(items);

                if (idUnidad != '') {
                    $.dlg("#" + Unidad).val(idUnidad);
                }
                $.dlg("#" + Unidad).attr("disabled", flag);
                $.dlg("#" + Unidad).css("width", "95px");
            }
            else
                messageErrorServer('Error:', '@Constantes.MensajeOpciones.ERRORSISTEMA');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            parent.closeWaitingDialog();
            messageErrorAjax('Error:', '@Constantes.MensajeOpciones.ERRORSISTEMA', XMLHttpRequest.status, baseUrl + 'Base/GetUnidadConversionByMagnitudBase', { idMagnitud: idMagnitud });
        }

    });

}
function ObtenerDivisionesByCompania(hdIdDialog, idCompania, nameDivision, idDivision) {

    var IdDivision = "";
    var ddlDivision = $.dlg("#" + nameDivision);
    jQuery.ajax({
        type: 'GET',
        url: baseUrl + 'Administrado/ObtenerDivisionesByCompania',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: {
            IdCompania: idCompania
        },
        async: false,
        success: function (data) {
            parent.closeWaitingDialog();
            if (data != null) {
                if (data.success) {

                    var items = "";
                    items += optiondefaultText;
                    $.each(data.lista, function (i, item) {
                        items += "<option value='" + item.idDivision + "'>" + item.nombre.toUpperCase() + "</option>";
                        IdDivision = item.idDivision;
                    });
                    $.dlg("#" + nameDivision).html(items);

                    for (var i = 0; i < arrDataObject.Item.length; i++) {
                        if (arrDataObject.Item[i].id == $.dlg("#" + hdIdDialog).val()) {
                            arrDataObject.Item[i].data.idDivision = idDivision == "" ? IdDivision : idDivision;
                        }
                    }
                    $.dlg("#" + nameDivision).val(idDivision == "" ? IdDivision : idDivision);
                    if (data.lista.length == 1) {

                        $.dlg("#" + nameDivision).attr("disabled", true);
                    }

                } else
                    messageErrorServer('Error:', '@Constantes.MensajeOpciones.ERRORSISTEMA');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            parent.closeWaitingDialog();
            messageErrorAjax('Error:', '@Constantes.MensajeOpciones.ERRORSISTEMA', XMLHttpRequest.status, baseUrl + 'Personal/ObtenerDivisionesByCompania', JSON.stringify({ IdCompania: Id }));
        }
    });
}
function LimpiarDependencias(nameDivision) {
    var ddlDivision = $.dlg("#" + nameDivision);
    ddlDivision.empty();
    ddlDivision.append($('<option/>', {
        value: "",
        text: "-- Seleccionar --"
    }));
}
function fGetFacturaArticulo(idComprobanteVenta, idDetComprobanteVenta) {
    var obj;

    jQuery.ajax({
        type: 'GET',
        url: baseUrl + 'ServicioArticulo/fGetFacturaArticulo',
        dataType: 'json',
        async: false,
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: {

            idComprobanteVenta: idComprobanteVenta,
            idDetComprobanteVenta: idDetComprobanteVenta
        },
        success: function (data) {
            if (data != null) {

                parent.closeWaitingDialog();
                if (!data.success) {
                    messageErrorServer('Error:', '@Constantes.MensajeOpciones.ERRORSISTEMA');
                }
                else
                    obj = data.model;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            parent.closeWaitingDialog();
            messageErrorAjax('Error:', '@Constantes.MensajeOpciones.ERRORSISTEMA', XMLHttpRequest.status, baseUrl + Controller + 'fGetCompVentaItemById', { IdSession: $.dlg("#hdIdDialog").val() + '@Constantes.MenuOpciones.MANTRECEPCIONARTICULO' });
        }
    });
    return obj;
}

function getDecimalNumber(d) {
    return d.replace(/[^\d.]/g, ''); //replace(/,/g, '');
}

function currencyFormat(currency, n) {
    return currency + " " + parseFloat(n).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,")
}

//Validación de Nro. Serie
function validarNroSerieInBD(idTipoDocIngreso, idPadre, idHijo, nroSerie) {
    var obj;
    $.ajaxSetup({ cache: false });
    jQuery.ajax({
        type: 'GET',
        url: baseUrl + "RecepcionArticulo/fValidarNroSerieInBD",
        dataType: 'json',
        async: false,
        contentType: 'application/json; charset=utf-8',
        data: {
            idTipoDocIngreso: idTipoDocIngreso,
            idPadre: idPadre,
            idHijo: idHijo,
            nroSerie: nroSerie,
        },
        success: function (data) {
            if (data != null) {
                parent.closeWaitingDialog();
                if (!data.success) {
                    messageErrorServer('Error:', '@Constantes.MensajeOpciones.ERRORSISTEMA');
                }
                else
                    obj = data.result;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            parent.closeWaitingDialog();
            messageErrorAjax('Error:', '@Constantes.MensajeOpciones.ERRORSISTEMA', XMLHttpRequest.status, baseUrl + 'RecepcionArticulo/fValidarNroSerieInBD', { IdSession: $.dlg("#hdIdDialog").val() + '@Constantes.MenuOpciones.MANTRECEPCIONARTICULO' });
        }
    });
    return obj;
}

function AppendMessageWithoutList(e, i) {
    var t = null == i ? "divMensaje" : i;
    $(dlgrFocus).find("#" + t + " ul").append(e)
}

function DisableDragOnColumnGeneral(e, i) {
    return ">th:not(:has(#jqgh_" + e + "_cb,#jqgh_" + e + "_" + i + ",#jqgh_" + e + "_rn,#jqgh_list_subgrid),:hidden)"
}

function AttachMapColumnsOnDragColumnasPivot(e) {
    for (var i = [], t = $("#" + e).jqGrid("getGridParam", "userData").IdOpcion, o = $("#" + e).jqGrid("getGridParam", "colModel"), n = 0; n < o.length; n++) i.push({
        nombre: o[n].name,
        orden: n
    });

    $.ajaxSetup({
        cache: !1
    }), jQuery.ajax({
        type: "POST",
        url: baseUrl + "Base/ActualizarMapColModelJQGridColumnasPivot",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            IdOpcion: t,
            lstPosition: i
        }),
        async: !0,
        success: function () { },
        error: function (e) {
            alert(e.message)
        }
    })
}

function StringToHtmlEncode(s) {
    return s.replace(/[\x26\x0A\<>'"]/g, function (r) { return "&#" + r.charCodeAt(0) + ";" });
}

function getListMultiTablaCustom(tabla, obj, url) {
    $.ajaxSetup({ cache: false });
    jQuery.ajax({
        type: 'GET',
        url: baseUrl + url,
        dataType: 'json',
        async: false,
        contentType: 'application/json; charset=utf-8',
        data: { tabla: tabla },
        success: function (data) {
            if (data != null) {
                if (data.success) {
                    
                    var items = "";
                    items += optiondefaultText;
                    $.each(data.lst, function (i, item) {
                        items += "<option value='" + item.idMultitabla_02 + "'>" + item.descripcion.toUpperCase() + "</option>";
                    });
                    $.dlg("#" + obj).html(items);

                }
                else messageErrorServer('Error:', '@Constantes.MensajeOpciones.ERRORSISTEMA');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            parent.closeWaitingDialog();
            messageErrorAjax('Error:', '@Constantes.MensajeOpciones.ERRORSISTEMA', XMLHttpRequest.status, baseUrl + url, { tabla: tabla });
        }
    });

}