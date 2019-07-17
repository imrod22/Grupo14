$(document).ready(function () {
    var datenow = moment().add(6, 'months');
    var datecurrent = moment().add(1, 'days');
    var lastDayHotSale = moment().add(6, 'months').add(1, 'days');

    $(document).on("click", ".add-nueva-propiedad", function () {

        $("#nombrePropiedad").val("");
        $("#descripcionPropiedad").val("");
        $("#paisPropiedad").val("");
        $("#ciudadPropiedad").val("");
        
        $('#nombrePropiedad').attr('readonly', false);
        $("#nombrePropiedad").attr('disabled', false);

        $('#paisPropiedad').attr('readonly', false);
        $("#paisPropiedad").attr('disabled', false);

        $('#ciudadPropiedad').attr('readonly', false);
        $("#ciudadPropiedad").attr('disabled', false);

        $('#boton-crear-propiedad').css('display', 'block');
        $('#boton-modificar-propiedad').css('display', 'none');
    });

    $(document).on("click", ".propiedad-modificar", function () {
        var propiedadId = $(this).attr('id');

        $('#nombrePropiedad').attr('readonly', true);
        $("#nombrePropiedad").attr('disabled', true);

        $('#paisPropiedad').attr('readonly', true);
        $("#paisPropiedad").attr('disabled', true);

        $('#ciudadPropiedad').attr('readonly', true);
        $("#ciudadPropiedad").attr('disabled', true);

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
                $("#ciudadPropiedad").val(response.Ciudad);

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
        $("#fechaSubasta").attr('disabled', true);

        $("#fechaReservaSubasta").attr('readonly', true);
        $("#fechaReservaSubasta").attr('disabled', true);

        $.ajax({
            type: "GET",
            url: "/Administrador/Administrador/ObtenerInformacionSubasta",
            data: { idSubasta: subastaId },
            success: function (response) {
                
                $("#identificadorSubasta").val(response.IdSubasta)
                $("#fechaSubasta").val(response.FechaComienzo);
                $("#fechaReservaSubasta").val(response.FechaReserva);

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

        $("#fechaSubasta").val("");
        $("#fechaReservaSubasta").val("");

        $("#fechaSubasta").attr('readonly', false);
        $("#fechaSubasta").attr('disabled', false);

        $("#fechaReservaSubasta").attr('readonly', false);
        $("#fechaReservaSubasta").attr('disabled', false);

        $("#valorMinimo").val("");

        $('#propiedad_select').attr('disabled', false);

        $('#boton-crear-subasta').css('display', 'block');
        $('#boton-modificar-subasta').css('display', 'none');

        $('input.calendar').pignoseCalendar({
            minDate: datenow,
            date: datenow,
            theme: 'dark',

        });
    });    
    
    $(document).on("click", ".hotsale-modificar", function () {
        var idHotSale = $(this).attr('id');

        $('#propiedad_hotsale').attr('disabled', true);

        $('#boton-modificar-hotsale').css('display', 'block');
        $('#boton-crear-hotsale').css('display', 'none');

        $("#fechahotsale").attr('readonly', true);
        $("#fechahotsale").attr('disabled', true);

        $.ajax({
            type: "GET",
            url: "/Administrador/Administrador/ObtenerInformacionHotSale",
            data: { idHotSale: idHotSale },
            success: function (response) {

                $("#identificadorHotSale").val(response.IdHotSale)
                $("#fechahotsale").val(response.FechaDisponible);

                $("#valorhotsale").val(response.Precio);
                $("#propiedad_hotsale").val(response.IdPropiedad);

            },
            error: function () {
                swal("Home Switch Home", "Ha habido un problema en el servidor.", "error");
            }
        });
    });

    $(document).on("click", ".add-nuevo-hotsale", function () {

        $("#identificadorHotSale").val("");

        $("#fechaHotSale").val("");

        $("#fechaHotSale").attr('readonly', false);
        $("#fechaHotSale").attr('disabled', false);

        $("#valor").val("");

        $('#propiedad_select').attr('disabled', false);

        $('#boton-crear-hotsale').css('display', 'block');
        $('#boton-modificar-hotsale').css('display', 'none');

        $('input.calendarhotsale').pignoseCalendar({
            minDate: moment(),
            maxDate: lastDayHotSale,
            theme: 'dark',

        });
    });

    $(document).on("click", ".propiedad-imagenes", function () {
        var idPropiedad = $(this).attr('id');

        $('#identificadorPropImagenes').val(idPropiedad);
        
    });
})