$(document).ready(function () {
    $('#boton-registro').click(function () {

        var cliente = $('#registro-form').serialize();

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
    });
})