/// <reference path="typings/bootstrap/bootstrap.d.ts" />
/// <reference path="typings/fullcalendar/fullcalendar.d.ts" />
/// <reference path="typings/jqueryui/jqueryui.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/moment/moment.d.ts" />
var mycalendar = (function () {
    function mycalendar() {
    }
    mycalendar.main = function () {
        console.log('Hello World me');
        return 0;
    };
    mycalendar.DisplayCalendar = function (cal, datapicker, modal) {
        mycalendar.ShowCalendar(cal);
        modal.click(function (evt) { mycalendar.EventCancel(evt, cal); });
        datapicker.datepicker({
            onSelect: function (dateText, inst) {
                var d = new Date(dateText);
                cal.fullCalendar('gotoDate', d);
            }
        });
    };
    mycalendar.guid = function () {
        return mycalendar.s4() + mycalendar.s4() + '-' + mycalendar.s4() + '-' + mycalendar.s4() + '-' +
            mycalendar.s4() + '-' + mycalendar.s4() + mycalendar.s4() + mycalendar.s4();
    };
    mycalendar.s4 = function () {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1);
    };
    mycalendar.EventClickeOn = function (calEvent, jsEvent, view) {
        $('#cancel-event').attr("data-id", calEvent.id);
        //  alert('Event: ' + calEvent.title + ' Start: ' + calEvent.start);
        $('#cancelText').text('Cancel ' + 'Event: ' + calEvent.title + ' Start: ' + calEvent.start.utc().format() + ' with id of ' + calEvent.id + '?');
        $('#cancelModal').modal("show");
        // change the border color just for fun
        $(this).css('border-color', 'red');
    };
    mycalendar.EventSelection = function (start, end, item) {
        var title = prompt('Event Title:');
        var eventData;
        if (title) {
            eventData = {
                id: mycalendar.guid(),
                title: title,
                start: start,
                end: end
            };
            $.post("/Consumer/BookEvent/", { eventStart: eventData.start.utc().format(), eventEnd: eventData.end.utc().format(), name: eventData.title, id: eventData.id }, function (data) {
                $(".result").html(data);
            });
            item.fullCalendar('renderEvent', eventData, true);
            console.log(eventData.title);
        }
        item.fullCalendar('unselect');
    };
    mycalendar.EventCancel = function (event, item) {
        $.post("/Consumer/CancelEvent/", { id: $('#cancel-event').attr("data-id") }, function (data) {
            $(".result").html(data);
        });
        item.fullCalendar('removeEvents', $('#cancel-event').attr("data-id"));
    };
    mycalendar.ShowCalendar = function (item) {
        item.fullCalendar({
            theme: true,
            header: {
                left: '',
                center: '',
                right: ''
            },
            defaultView: 'agendaDay',
            editable: true,
            events: "/Consumer/GetEvents/",
            selectable: true,
            selectHelper: true,
            eventClick: mycalendar.EventClickeOn,
            select: function (start, end) {
                mycalendar.EventSelection(start, end, item);
            }
        });
    };
    return mycalendar;
}());
//# sourceMappingURL=calendar.js.map