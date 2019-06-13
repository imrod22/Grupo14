$(document).ready(function () {

    var datenow = moment().add(6, 'months');

    $('input.calendar').pignoseCalendar({
        minDate: datenow,
        date: datenow,
        theme: 'blue',
    });    
});