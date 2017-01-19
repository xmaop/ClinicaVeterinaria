$(document).ready(function () {
    
    $('#tablaClienteN tr').click(function (event) {

        $.ajax({
            url: "/Pacients/Filtra/" + $(this).attr('id'), success: function (result) {
                $("#contenedorDetalle").html(result);
                $("#contenedorPauta").html('');
            }
        });

    });

    $('#tablaClientePJ tr').click(function (event) {

        $.ajax({
            url: "/Pacients/Filtra/" + $(this).attr('id'), success: function (result) {
                $("#contenedorDetalle").html(result);
                $("#contenedorPauta").html('');
            }
        });

    });
});
