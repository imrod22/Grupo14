$(document).ready(function () {
    $('#boton-registro').click(function () {

        var cliente = $('#registro-form').serialize();

        if ($('#Password').val().length < 8) {
            console.log($('#Password').val().length);
            swal("Home Switch Home", "La contraseña tiene que ser mayor o igual a 8 caracteres", "error");
        }
        else {
            $.ajax({
                type: "POST",
                url: "/Home/RegistrarUsuario",
                data: cliente,
                success: function (response) {
                    swal("Home Switch Home", "Su solicitud de registro ha sido registrada. Se le notificará cuando pueda acceder.", "success");
                    window.location.href = response;

                },
                error: function (jqXHR) {
                    swal("Home Switch Home", jqXHR.responseJSON, "error");

                }
            });
        }
    });
})