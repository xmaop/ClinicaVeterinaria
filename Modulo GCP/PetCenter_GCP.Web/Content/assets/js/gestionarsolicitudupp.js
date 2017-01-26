var VentanaLimpiar = function () {
    $('[data-click="limpiar"]').bind("click", function () {
        $('select option:first-child').attr("selected", "selected");
    })
};
var ConfiguraModal = function () {
    //var closedModalHashStateId = "#modalClosed";
    //var openModalHashStateId = "#modalOpen";

    //window.location.hash = closedModalHashStateId;

    //$('#modal-dialog-establecimiento').on('show.bs.modal', function (e) {
    //    window.location.hash = openModalHashStateId;
    //});

    //$('#modal-dialog-establecimiento').on('hide.bs.modal', function (e) {
    //    //window.history.back();
    //    $('#modal-dialog-establecimiento').modal('hide');
    //});

    if (window.history && window.history.pushState) {
        $('#modal-dialog-partidapresupuestalarea').on('show.bs.modal', function (e) {
            window.history.pushState('forward', null, '#modal');
        });
        $(window).on('popstate', function () {
            $('#modal-dialog-partidapresupuestalarea').modal('hide');
        });
    }
}

var GestionarSolicitudUPP = function () {
    "use strict";
    return {
        init: function () {
            VentanaLimpiar(),
            ConfiguraModal()
        }
    }
}()