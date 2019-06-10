$(document).ready(function () {        

    var datenow = new Date();
    datenow.setDate(datenow.getDate() + 183);

    $('input[name="daterange"]').daterangepicker({
        opens: 'left',
        singleDatePicker: true,
        autoUpdateInput: false,
        startDate: datenow,
        minDate: datenow,
        locale: {
            cancelLabel: 'Clear'
        }
    });

    $(document).on("click", ".add-nueva-propiedad", function () {

        $("#nombrePropiedad").val("");
        $("#descripcionPropiedad").val("");
        $("#paisPropiedad").val("");

        $('#nombrePropiedad').attr('readonly', false);

        $('#boton-crear-propiedad').css('display', 'block');
        $('#boton-modificar-propiedad').css('display', 'none');        

    });

    $(document).on("click", ".propiedad-modificar", function () {
        var propiedadId = $(this).attr('id');

        $('#nombrePropiedad').attr('readonly', true);

        $('#boton-modificar-propiedad').css('display', 'block');
        $('#boton-crear-propiedad').css('display', 'none');

        $.ajax({
            type: "GET",
            url: "/Administrador/Administrador/ObtenerInformacionPropiedad",
            data: { idPropiedad: propiedadId },
            success: function (response) {

                $("#identificadorPropiedad").val(response.IdPropiedad)
                $("#nombrePropiedad").val(response.Nombre);
                $("#descripcionPropiedad").val(response.Descripcion);
                $("#paisPropiedad").val(response.Pais);

            },
            error: function () {
                swal("Home Switch Home", "Ha habido un problema en el servidor.", "error");
            }
        });
    });

    $(document).on("click", ".subasta-modificar", function () {
        var subastaId = $(this).attr('id');

        $('#nombrePropiedad').attr('readonly', true);

        $('#boton-modificar-subasta').css('display', 'block');
        $('#boton-crear-subasta').css('display', 'none');
        $("#fechaSubasta").attr('readonly', true);

        $.ajax({
            type: "GET",
            url: "/Administrador/Administrador/ObtenerInformacionSubasta",
            data: { idSubasta: subastaId },
            success: function (response) {
                
                $("#identificadorSubasta").val(response.IdSubasta)
                $("#fechaSubasta").val(response.FechaComienzo);
                $("#valorMinimo").val(response.ValorMinimo);
            },
            error: function () {
                swal("Home Switch Home", "Ha habido un problema en el servidor.", "error");
            }
        });
    });

    $(document).on("click", ".add-nueva-subasta", function () {

        $("#identificadorSubasta").val("");

        $("#fechaSubasta").attr('readonly', false);

        $("#fechaSubasta").val("");
        $("#valorMinimo").val("");

        $('#nombrePropiedad').attr('readonly', false);

        $('#boton-crear-subasta').css('display', 'block');
        $('#boton-modificar-subasta').css('display', 'none');
    });

    $('input[name="daterange"]').on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format('MM/DD/YYYY'));
    });

    $('input[name="daterange"]').on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });

})