
/// <reference path="typings/bootstrap/bootstrap.d.ts" />
/// <reference path="typings/fullcalendar/fullcalendar.d.ts" />
/// <reference path="typings/jqueryui/jqueryui.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/moment/moment.d.ts" />
 
class mycalendar {
    public static main(): number {
        console.log('Hello World me');
        return 0;
    }

    public static DisplayCalendar(cal: JQuery, datapicker: JQuery, modal: JQuery) {
        mycalendar.ShowCalendar(cal);
        modal.click(function (evt) { mycalendar.EventCancel(evt, cal) });
        datapicker.datepicker(
            {
                onSelect: function (dateText, inst) {
                    var d = new Date(dateText);
                    cal.fullCalendar('gotoDate', d);
                }
            });

    }

    public static guid(): string {

        return mycalendar.s4() + mycalendar.s4() + '-' + mycalendar.s4() + '-' + mycalendar.s4() + '-' +
            mycalendar.s4() + '-' + mycalendar.s4() + mycalendar.s4() + mycalendar.s4();
    }

    private static s4(): string {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1);

    }

    private static EventClickeOn(calEvent: FullCalendar.EventObject, jsEvent: MouseEvent, view: FullCalendar.ViewObject) {
        $('#cancel-event').attr("data-id", calEvent.id);
        //  alert('Event: ' + calEvent.title + ' Start: ' + calEvent.start);
        $('#cancelText').text('Cancel ' + 'Event: ' + calEvent.title + ' Start: ' + calEvent.start.utc().format() + ' with id of ' + calEvent.id + '?');
        $('#cancelModal').modal("show");
        // change the border color just for fun
        $(this).css('border-color', 'red');

    }

    private static EventSelection(start: moment.Moment, end: moment.Moment, item: JQuery) {
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
    }

    private static EventCancel(event: JQueryEventObject, item: JQuery) {
        $.post("/Consumer/CancelEvent/", { id: $('#cancel-event').attr("data-id") }, function (data) {
            $(".result").html(data);
        });
        item.fullCalendar('removeEvents', $('#cancel-event').attr("data-id"));
    }

    public static ShowCalendar(item: JQuery) {


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
    }
}
