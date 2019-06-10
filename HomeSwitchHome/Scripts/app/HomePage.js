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

                if (response == "") {

                    swal({
                        title: "HOME SWITCH HOME",
                        text: "Su solicitud de registro se esta procesando. Se le notificara cuando pueda acceder con la cuenta ingresada.",
                        icon: "danger",
                        closeOnClickOutside: false,
                        closeOnEsc: false,
                        allowOutsideClick: false,
                        buttons: [
                            'OK'
                        ],
                        dangerMode: true,
                    },
                        function (isConfirm) {
                            if (isConfirm) {
                                window.location.href = response;
                            } else {
                                window.location.href = response;
                            }

                        })
                }
                else {
                    window.location.href = response;
                }
            },
            error: function () {
                alert("No hay usuario registrado con la información ingresada.");
            }
        });

    });
})