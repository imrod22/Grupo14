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

    $('#forget-mail').click(function () {

        $.ajax({
            type: "POST",
            url: "/Home/SolicitarMail",
            data: {
                usuario: $('#usuariobusqueda').val()
            },
            success: function (response) {

                swal("Home Switch Home", response, "success");
            },
            error: function (jqXHR) {
                swal("Home Switch Home", jqXHR.responseJSON, "error");
            }
        });

    });
})