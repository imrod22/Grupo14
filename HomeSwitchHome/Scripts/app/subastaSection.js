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
});