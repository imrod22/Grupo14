$(document).ready(function () {
    $('a[href$="#LoginModal"]').on("click", function () {
        $('#LoginModal').modal('show');
    });

    $('#boton-login').click(function () {

        $.ajax({
            type: "POST",
            url: "/Home/Login",
            data: {
                usuario: $('#nombreUsuario').val(),
                password: $('#contrasenia').val()
            },
            success: function (response) {
                window.location.href = response;
            },
            error: function (jqXHR) {
                swal("Home Switch Home", jqXHR.responseJSON, "error");
            }
        });

    });
})