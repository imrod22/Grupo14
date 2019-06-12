$(document).ready(function () {
    $('#boton-registro').click(function () {

        var cliente = $('#registro-form').serialize();

        $.ajax({
            type: "POST",
            url: "/Home/RegistrarUsuario",
            data: cliente,
            success: function (response) {
                if (response == "") {
                    swal("Home Switch Home", "Ha ocurrido un error en el servidor.", "error");
                }
                else {
                    swal("Home Switch Home", "Su solicitud de registro esta siendo procesada.", "error");
                    window.location.href = response;
                }                
            },
            error: function () {
                swal("Home Switch Home", "No se puede registrar el usuario con los campos ingresados, verifique y reintente nuevamente.", "error");
                
            }
        });
    });
})