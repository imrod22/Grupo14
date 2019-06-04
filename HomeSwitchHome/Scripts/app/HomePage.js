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
            error: function () {
                alert("No hay usuario registrado con la información ingresada.");
            }
        });

    });
})