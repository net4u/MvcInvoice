var pageIndex = 0;
var pageSize = 10;

(function ($) {
    SetClientValidators();
})(jQuery);

$(function () {
    $(document).on('change', ':file', function () {
        var input = $(this),
            filesCount = input.get(0).files ? input.get(0).files.length : 0,
            url = input.val(),
            fileName = "";
        if (filesCount == 1) {
            fileName = url.replace(/\\/g, '/').replace(/.*\//, '');
        }
        var label = $(this).parents(".input-group").find(":text");
        label.val(fileName);
    });

    $('.post-row-height-equal').matchHeight();

    $('#form-post').submit(function (e) {
        $("#post-ajax-error-placeholder").hide();
        if (!$(this).valid()) {
            return false;
        }
           
        $('.validation-summary-errors').removeClass("validation-summary-errors")
            .addClass("validation-summary-valid")
            .find("ul").empty();

        e.preventDefault();
        //var token = $("[name='__RequestVerificationToken']").val();
        var token = $(this).find("[name='__RequestVerificationToken']").eq(0).val()
        var formData = new FormData(this);
        //formData.append("__RequestVerificationToken", token);

        $.ajax({
            type: 'POST',
            url: "/Home/CREATE",
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            headers: {
                '__RequestVerificationToken': token
            }, 
            success: function (response) {
                if ((response != null) && (response.success) && (response.data != null)) {
                    $("#post-ajax-error-placeholder").html(bsMSG("post has been saved correctly", 'success', false, true));
                    $('#form-post').trigger("reset");
                    $("#content-container").prepend(CreatePostView(response.data));
                }
                else {
                    var errors
                    if ((response == null) || (response.data == null)) {
                        errors = "unhandled exception";
                    }
                    else {
                        errors = [];
                        errors.push(("error in:"));
                        for (var err in response.errors) {
                            errors.push(response.errors[err]);
                        }
                    }
                    $("#post-ajax-error-placeholder").html(bsMSG(errors, 'danger', false, true));
                }
                $("#post-ajax-error-placeholder").show();
            },
            error: function () {
                $("#post-ajax-error-placeholder").html(bsMSG('unhandled error while creating post', 'danger', false, true));
                $("#post-ajax-error-placeholder").show();
            }
        });
    });

    //from Global.js
    ValidateHidden("form-post");
    LoadCurrencyTracker();
    GetPosts();
    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            GetPosts();
        }
    });
});

function GetPosts() {
    $.ajax({
        type: 'GET',
        url: '/home/GetPosts',
        data: { "pageindex": pageIndex, "pagesize": pageSize },
        datatype: 'json',
        success: function (response) {
            if ((response != null) && (response.data != null)) {
                var className = "";
                for (var i = 0; i < response.data.length; i++) {
                    $("#content-container").append(CreatePostView(response.data[i]));
                }
                pageIndex++;
            }
        },
        beforeSend: function () {
            $("#content-load-progress").show();
        },
        complete: function () {
            $("#content-load-progress").hide();
        },
        error: function () {
            $("#ajax-error-placeholder").html(bsMSG("unhandled error while loading posts", 'danger', false, true));
        }
    });
}

function CreatePostView(post) {
    var className = post.IsImportant ? "home-post-important" : "";
    var postHtml =
        '<div id="post-' + post.Id + '" class="home-post row ' + className + '">' +
                            '<div class="col-md-2 home-post-image">' +
                                '<img src="data:image/png;base64,' + post.ImageBaseStr + '" />' +
                            '</div>' +
                            '<div class="col-md-10">' +
                                '<h2 class="home-post-title">' + post.Header + '</h2>' +
                                '<div>' +
                                    ListCategories(post.Categories) +
                                '</div>' +
                                '<div class="home-post-content text-justify">' +
                                    post.Message +
                                '</div>' +
                            '</div>' +
                        '</div>';
    return postHtml;

}

function ListCategories(categories) {
    html = '';
    html = '<ul class = home-post-categories>'
    for (i = 0; i < categories.length; i++) {
        html += '<li style="background-color: ' + categories[i].Color + ' ">' + categories[i].Symbol + '</li>';
    }

    html += '</ul>';

    return html;
};

function ShowSelectedFile(input) {
    var filesCount = input.get(0).files ? input.get(0).files.length : 0;
    var url = input.get(0).value;
    var fileName = "";
    if (filesCount == 1) {
        fileName = url.replace(/\\/g, '/').replace(/.*\//, '');
    }
    var label = $(this).parents(".input-group").find(":text");
    label.val(fileName);
}

function SetClientValidators()
{
    $.validator.unobtrusive.adapters.add('filetype', ['validtypes'], function (options) {
        options.rules['filetype'] = { validtypes: options.params.validtypes.split(',') };
        options.messages['filetype'] = options.message;
    });

    $.validator.addMethod("filetype", function (value, element, param) {
        if (!value) {
            return true;
        }
        var extension = GetFileExtension(value);
        return $.inArray(extension, param.validtypes) !== -1;
    });
}

function GetFileExtension(fileName) {
    if (/[.]/.exec(fileName)) {
        return /[^.]+$/.exec(fileName)[0].toLowerCase();
    }
    return null;
}