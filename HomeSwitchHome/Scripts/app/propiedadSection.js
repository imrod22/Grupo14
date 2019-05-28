$(document).ready(function () {
    $(document).on("click", ".propiedad-seleccionada", function () {
        var propiedadId = $(this).attr('id');

        $.ajax({
            type: "GET",
            url: "/Propiedad/Propiedad/ObtenerInformacionPropiedad",
            data: { idPropiedad : propiedadId },
            success: function (response) {

                console.log(response);
                $("#nombrePropiedad").val(response.Nombre);

                $("#modalAgregarEditar").modal('show'); 
            },
            error: function () {
                alert("Ha habido un problema en el servidor.");
            }
        });
    });
});



