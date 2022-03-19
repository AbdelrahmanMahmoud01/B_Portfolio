$(document).ready(function () {
    $('#UploadedPhoto').on('change', function () {
        var selectedFile = $(this).val().split('\\').pop();
        $(this).siblings('label').text(selectedFile);

        var posterContainer = $('#poster-container');
        var image = window.URL.createObjectURL(this.files[0]);

        posterContainer.removeClass('d-none');
        posterContainer.find('img').attr('src', image);
    });
});