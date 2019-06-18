$(document).ready(function () {
    var datenow = moment().add(6, 'months');
    var datelimit = moment().add(8, 'months');
       
    $(document).on("change", ".propiedad-select", function () {

        var propiedadId = $('#propiedad_select option:selected').attr('id');

        $.ajax({
            type: "GET",
            url: "/Administrador/Administrador/ObtenerFechasOcupadasDePropiedad",
            data: { idPropiedad: propiedadId },
            success: function (response) {

                var resultado = [];

                $.each(response, function (key, value) {
                        resultado.push(value);
                });

                $('input.calendar').pignoseCalendar({
                    minDate: datenow,
                    date: datenow,
                    theme: 'blue',
                    disabledDates:[resultado],

                });

            },
            error: function () {
                swal("Home Switch Home", "Ha habido un problema en el servidor.", "error");
            }
        });

    })
    
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

        $('#propiedad_select').attr('disabled', true);

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
                $("#propiedad_select").val(response.IdPropiedad);
                                
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

        $('#propiedad_select').attr('disabled', false);

        $('#boton-crear-subasta').css('display', 'block');
        $('#boton-modificar-subasta').css('display', 'none');

        $('input.calendar').pignoseCalendar({
            minDate: datenow,
            date: datenow,
            theme: 'blue',

        });
    });
})