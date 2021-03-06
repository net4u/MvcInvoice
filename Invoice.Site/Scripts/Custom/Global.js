﻿var bsMSG = function (msg, type, seprator, bClose) {
    html = '';
    msg = (msg instanceof Array ? msg : [msg]);
    type = type || 'success';
    seprator = seprator || '<br/>';
    b = '';
    for (i = 0; i < msg.length; i++) {
        html += b + msg[i];
        b = seprator;
    }

    if (typeof bClose == 'boolean') {
        bClose ? html = "<div class='alert alert-" + type + "'><a href=' ' class='close' data-dismiss='alert' aria-label='close';>&times;</a>" + html + "</div>" : html;
    }

    return html;
}

function ValidateHidden(formId) {
    var form = $("#" + formId)
    if (form.length > 0) {
        var validator = form.data('validator');
        validator.settings.ignore = "";
    }

}

function LoadCurrencyTracker() {
    var currencyTrackerUrl = $('#currency-tracker-url').val();
    $.ajax({
        type: 'GET',
        url: currencyTrackerUrl,
        datatype: 'json',
        success: function (response) {
            if (response && response.success) {
                $("#currenny-tracker-placeholder").html(response.data);
                $("#curr-stop").click(function () {
                    $("#curr-list").toggleClass("animation-paused");
                });
            }
            else {
                $("#ajax-error-placeholder").html(bsMSG("unhandled error while loading currencies", 'danger', false, true));
                $("#currenny-tracker-placeholder").hide();
            }
        },
        beforeSend: function () {
            $("#currenny-tracker-placeholder").show();
        },
        error: function () {
            $("#ajax-error-placeholder").html(bsMSG("unhandled error while loading currencies", 'danger', false, true));
            $("#currenny-tracker-placeholder").hide();
        }

    });

}