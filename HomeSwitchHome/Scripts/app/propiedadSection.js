$(document).ready(function () {

    var datenow = new Date();
    datenow.setDate(datenow.getDate() + 183);

    $('input[name="reservafecha"]').daterangepicker({
        opens: 'left',
        singleDatePicker: true,
        autoUpdateInput: false,
        startDate: datenow,
        minDate: datenow,
        locale: {
            cancelLabel: 'Clear'
        }
    });

    $('input[name="reservafecha"]').on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format('MM/DD/YYYY'));
    });

    $('input[name="reservafecha"]').on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });
    
});