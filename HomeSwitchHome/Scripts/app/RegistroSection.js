$(document).ready(function () {
    $('#boton-registro').click(function () {
        
        $.ajax({
            type: "POST",
            url: "/Home/RegistrarUsuario",
            data: $('#registro-form').serialize(),
            success: function (response) {
                if (response == "") {
                    alert("Ha ocurrido un error en el servidor.");
                }
                else {
                    window.location.href = response;
                }                
            },
            error: function () {
                alert("No se puede registrar el usuario con los campos ingresados, verifique y reintente nuevamente.");
            }
        });
    });
})