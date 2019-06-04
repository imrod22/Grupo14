$(document).ready(function () {

    $(document).on("click", ".subasta-puja", function () {
        var subastaId = $(this).attr('id');

        $.ajax({
            type: "GET",
            url: "/Subasta/Subasta/ObtenerInformacionSubasta",
            data: { idSubasta: subastaId },
            success: function (response) {

                $("#identificadorSubasta").val(response.IdSubasta)
            },
            error: function () {
                alert("Ha habido un problema en el servidor.");
            }
        });
    });



    $(document).on("click", ".subasta-seleccionada", function () {
        var subastaId = $(this).attr('id');

        $('#nombrePropiedad').attr('readonly', true);

        $('#boton-modificar-propiedad').css('display', 'block');
        $('#boton-crear-propiedad').css('display', 'none');

        $.ajax({
            type: "GET",
            url: "/Subasta/Subasta/ObtenerInformacionSubasta",
            data: { idSubasta: subastaId },
            success: function (response) {

                $("#identificadorSubasta").val(response.IdSubasta)
                $("#fechaComienzo").val(response.FechaComienzo);
                $("#valorMinimo").val(response.ValorMinimo);
            },
            error: function () {
                alert("Ha habido un problema en el servidor.");
            }
        });
    });

    $(document).on("click", ".add-nueva-subasta", function () {

        $("#identificadorSubasta").val("");

        $("#fechaComienzo").val("");
        $("#valorMinimo").val("");

        $('#nombrePropiedad').attr('readonly', false);

        $('#boton-crear-propiedad').css('display', 'block');
        $('#boton-modificar-propiedad').css('display', 'none');
    });
});