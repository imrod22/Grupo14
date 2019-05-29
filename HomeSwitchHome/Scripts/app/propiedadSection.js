$(document).ready(function () {

    $(document).on("click", ".propiedad-seleccionada", function () {
        var propiedadId = $(this).attr('id');

        $('#nombrePropiedad').attr('readonly', true);

        $('#boton-modificar-propiedad').css('display', 'block');
        $('#boton-crear-propiedad').css('display', 'none');


        $.ajax({
            type: "GET",
            url: "/Propiedad/Propiedad/ObtenerInformacionPropiedad",
            data: { idPropiedad: propiedadId },
            success: function (response) {

                $("#identificadorPropiedad").val(response.IdPropiedad)
                $("#nombrePropiedad").val(response.Nombre);
                $("#descripcionPropiedad").val(response.Descripcion);
                $("#paisPropiedad").val(response.Pais);



            },
            error: function () {
                alert("Ha habido un problema en el servidor.");
            }
        });
    });

    $(document).on("click", ".add-nueva-propiedad", function () {

        $("#nombrePropiedad").val("");
        $("#descripcionPropiedad").val("");
        $("#paisPropiedad").val("");

        $('#nombrePropiedad').attr('readonly', false);

        $('#boton-crear-propiedad').css('display', 'block');
        $('#boton-modificar-propiedad').css('display', 'none');
    });
});



