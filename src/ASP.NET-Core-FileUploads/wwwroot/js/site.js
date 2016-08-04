$(document).ready(function () {
    $("[data-form-uploader]").submit(function (evt) {
        evt.preventDefault();

        var fileUpload = $("#file").get(0);
        var files = fileUpload.files;
        var data = new FormData();
        for (var i = 0; i < files.length ; i++) {
            data.append(files[i].name, files[i]);
        }
        $.ajax({
            type: "POST",
            url: "/api/documents/",
            contentType: false,
            processData: false,
            data: data,
            success: function (message) {
                alert(message);
            },
            error: function () {
                alert("There was error uploading files!");
            }
        });
    });
});