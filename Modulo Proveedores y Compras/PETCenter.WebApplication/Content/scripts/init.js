var wsnode = '/Controllers/ajax/';
var pgnode = '/';

function showSuccessMessage(message) {
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

function showErrorMessage(message) {
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

function showWarningMessage(message) {
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            positionClass: 'toast-top-center',
            timeOut: 4000
        };
        toastr.warning(message, 'PETCenter');

    }, 300);
}

function showSuccessRight(message) {
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            positionClass: 'toast-top-right',
            timeOut: 4000
        };
        toastr.success(message, 'PETCenter');

    }, 300);
}