$(document).ready(function () {
    $('#boton-registro').click(function () {

        var cliente = $('#registro-form').serialize();

        $.ajax({
            type: "POST",
            url: "/Home/RegistrarUsuario",
            data: cliente,
            success: function (response) {
                if (response == "") {
                    alert("Ha ocurrido un error en el servidor.");
                }
                else {

                    alert("Su solicitud de registro esta siendo procesada.");

                    window.location.href = response;
                }                
            },
            error: function () {
                alert("No se puede registrar el usuario con los campos ingresados, verifique y reintente nuevamente.");
            }
        });
    });
})