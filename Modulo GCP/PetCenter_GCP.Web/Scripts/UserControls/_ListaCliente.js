
function LoadColorboxCliente(idObj_IdCliente, idObj_NomCliente, idObj_CodCliente, isMain) {
    fnGrillaCliente(idObj_IdCliente, idObj_NomCliente, idObj_CodCliente, isMain);

    var $stuff = $('#divContentMainClientecbx');
    $.colorbox({
        inline: true,
        href: $stuff,
        onComplete: function () {
        },
        onClosed: function(){
            if (window["afterCloseModalCliente"])
                window["afterCloseModalCliente"]();
        }
    });
}

function fnGrillaCliente(idObj_IdCliente, idObj_NomCliente, idObj_CodCliente, isMain) {
    var URL="Base/ConsultarClienteBase";

    if (jQuery("#listClientecbx")[0].grid)
        jQuery("#listClientecbx").jqGrid('GridUnload');
    jQuery("#listClientecbx").jqGrid({
        url: baseUrl + URL,
        datatype: 'json',
        postData: {
        },
        mtype: 'GET',
        contentType: "application/json; charset=utf-8",
        colNames: ['', 'Nombre o Raz. Social', 'Código', 'Tipo Cliente', 'T. Documento', 'Nro. Documento'],
        colModel: [
            { name: 'id_Cliente', index: 'id_Cliente', hidden: true, hidedlg: true },
            { name: 'nomCliente', index: 'nomCliente' },
            { name: 'codigo', index: 'codigo' },
            { name: 'tipoCliente', index: 'tipoCliente',stype: 'select', searchoptions: { dataUrl: baseUrl + 'Base/GetTipoClienteBase' } },
            { name: 'descTipoDocumento', index: 'descTipoDocumento' },
            { name: 'nroDocumento', index: 'nroDocumento' }
        ],
        jsonReader:
            {
                root: "rows",
                page: "page",
                total: "total",
                records: "records",
                repeatitems: true,
                cell: "cell",
                id: "id"
            },
        pager: jQuery('#pagerClientecbx'),
        rowNum: 10,
        rownumbers: true,
        scrollOffset: 0,
        rowList: [10, 20, 30, 40, 50],
        sortname: 'id_Cliente',
        sortorder: 'asc',
        sortable: true,
        viewrecords: true,
        width: '870',
        height: 'auto',
        altRows: true,
        altclass: 'jQGridAltRowClass',
        loadComplete: function () {
            $.colorbox.resize();
        },
        ondblClickRow: function (rowid) {
            BindCliente(rowid, idObj_IdCliente, idObj_NomCliente, idObj_CodCliente, isMain);
        }
    });
    jQuery("#listClientecbx").jqGrid('navGrid', "#pagerClientecbx", { edit: false, add: false, refresh: true, del: false, search: false });
    addFilterToGrid('listClientecbx', 'pagerClientecbx');
    BindJQgridCliente(idObj_IdCliente, idObj_NomCliente, idObj_CodCliente, isMain);
    var $bdiv = $($("#listClientecbx")[0].grid.bDiv);
    var Height = screen.height;
    var Width = screen.width;
    var resolucion = 'R1920X1080';
    if (Width == resolucion.split("X")[0] && Height == resolucion.split("X")[1])
        $bdiv.attr('style', 'overflow: auto !important;overflow-x: hidden !important;max-height:580px');
    else
        $bdiv.attr('style', 'overflow: auto !important;overflow-x: hidden !important;max-height:340px');
}

function BindJQgridCliente(idObj_IdCliente, idObj_NomCliente, idObj_CodCliente, isMain){
    jQuery("#listClientecbx").unbindKeys();
    jQuery("#listClientecbx").jqGrid('bindKeys', {
        "onEnter": function (rowid) {
            BindCliente(rowid, idObj_IdCliente, idObj_NomCliente, idObj_CodCliente, isMain)
        }
    });
}

function BindCliente(rowid, idObj_IdCliente, idObj_NomCliente, idObj_CodCliente, isMain) {
    var id_Cliente = $('#listClientecbx').jqGrid('getCell', rowid, 'id_Cliente');
    var codigo = $('#listClientecbx').jqGrid('getCell', rowid, 'codigo');
    var nomCliente = $('#listClientecbx').jqGrid('getCell', rowid, 'nomCliente');

    if (isMain){
        $("#" + idObj_IdCliente).val(id_Cliente);
        $("#" + idObj_NomCliente).val(nomCliente);
        $("#" + idObj_CodCliente).val(codigo);
    }
    else{
        $.dlg("#" + idObj_IdCliente).val(id_Cliente);
        $.dlg("#" + idObj_NomCliente).val(nomCliente);
        $.dlg("#" + idObj_CodCliente).val(codigo);
    }

    $.dlg("#divMensaje ul").html("");
    $.dlg("#divMensaje").fadeOut("fast");
    $.fn.colorbox.close();
}