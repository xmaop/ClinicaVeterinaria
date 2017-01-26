
function Entry() {
    Showmodal(true);
    
    var Usuario = $("#inUser").val();
    var Contrasenna = $("#inContrasenna").val();
    var isValid = true;
    if (Usuario == "") {
        isValid = false;
        showErrorMessage("Ud. debe ingresar un usuario");
        Showmodal(false);
        return;
    }

    if (Contrasenna == "") {
        isValid = false;
        showErrorMessage("Ud. debe ingresar una constraseña");
        Showmodal(false);
        return;
    }

    if (isValid) {
        $.ajax({
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            url: wsnode + "wsSeguridad.svc/UserValidate",
            dataType: "json",
            data: JSON.stringify({ "alias": Usuario, "clave": Contrasenna }),
            async: false,
            processData: false,
            cache: false,
            success: function (response) {
                $("#outMessage").append(response);
                Showmodal(false);
            },
            error: function (response) {
                showError(response);
                Showmodal(false);

            }
        })
    }

}

function showSuccess(message) {
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            positionClass: 'toast-top-center',
            timeOut: 4000
        };
        toastr.success(message, 'PETCenter');

    }, 300);
}

function showError(message) {
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            positionClass: 'toast-top-center',
            timeOut: 4000
        };
        toastr.error(message, 'PETCenter');

    }, 300);
}


$(document).keypress(operaEvento);

function operaEvento(evento) {
    OnKeyEnter();
}

function OnKeyEnter() {
    if (event.keyCode == 13) {
        Entry();
    }
}
