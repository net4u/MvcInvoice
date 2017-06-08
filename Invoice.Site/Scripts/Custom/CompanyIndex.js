function CompanySearchOnBeginSubmit() {
    $("#post-ajax-error-placeholder").hide();
    $("#content-load-progress").show();
    return true;
}

function CompanySearchOnSuccess(response) {
    $("#content-load-progress").hide();
    $("#content-container").html(response.data);
}