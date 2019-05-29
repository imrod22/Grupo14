$(document).ready(function () {

    $(document).on("click", ".propiedad-seleccionada", function () {
        var propiedadId = $(this).attr('id');
        $('#boton-modificar-propiedad').css('display', 'block');
        $('#boton-crear-propiedad').css('display', 'none');


        $.ajax({
            type: "GET",
            url: "/Propiedad/Propiedad/ObtenerInformacionPropiedad",
            data: { idPropiedad: propiedadId },
            success: function (response) {

                console.log(response);
                $("#identificadorPropiedad").val(response.IdPropiedad)
                $("#nombrePropiedad").val(response.Nombre);
                $("#ubicacionPropiedad").val(response.Ubicacion);
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
        $("#ubicacionPropiedad").val("");
        $("#descripcionPropiedad").val("");
        $("#paisPropiedad").val("");

        $('#boton-crear-propiedad').css('display', 'block');
        $('#boton-modificar-propiedad').css('display', 'none');
    });
});



