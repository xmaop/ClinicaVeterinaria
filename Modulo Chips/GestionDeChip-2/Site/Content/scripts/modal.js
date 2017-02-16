function Showmodal(action) {
    if (action) {
        $("#modal").show();
    }
    else {
        setInterval(function () { $("#modal").hide(); }, 250);
        
    } 
}


function showLoading() {
    $('#processingModal').modal('show');
}

function hideLoading() {
    //$('#processingModal').modal('toggle');
    $('#processingModal').modal('hide');
    //$('#processingModal').modal('hide');
}