$(document).ready(function () {

    var datenow = moment().add(6, 'months');

    $('input.calendar').pignoseCalendar({
        minDate: datenow,
        date: datenow,
        theme: 'blue',
    });

    $('.post-module').hover(function () {
        $(this).find('.description').stop().animate({
            height: "toggle",
            opacity: "toggle"
        }, 300);
    });
});