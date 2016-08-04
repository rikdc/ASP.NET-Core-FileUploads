$(document).ready(function () {
    $("[data-form-uploader]").submit(function (evt) {
        evt.preventDefault();

        var data = new FormData(this);

        debugger;
        $.ajax({
            type: "POST",
            url: "/api/documents/",
            contentType: false,
            processData: false,
            data: data,
            success: function () {
                alert('The file has been uploaded');
            },
            error: function () {
                alert("There was error uploading files!");
            }
        });
    });
});