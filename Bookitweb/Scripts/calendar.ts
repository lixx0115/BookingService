
/// <reference path="typings/bootstrap/bootstrap.d.ts" />
/// <reference path="typings/fullcalendar/fullcalendar.d.ts" />
/// <reference path="typings/jqueryui/jqueryui.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/moment/moment.d.ts" />
 
class mycalendar {

    private evenUrl: string;


    public constructor(eventUrl: string) {
        this.evenUrl = eventUrl;
    }
    


    public DisplayCalendar(cal: JQuery, datapicker: JQuery, modal: JQuery) {
        var that = this;
        this.ShowCalendar(cal);
        modal.click(function (evt) { that.EventCancel(evt, cal) });
        datapicker.datepicker(
            {
                onSelect: function (dateText, inst) {
                    var d = new Date(dateText);
                    cal.fullCalendar('gotoDate', d);
                }
            });

    }

    public  guid(): string {

        return this.s4() + this.s4() + '-' + this.s4() + '-' + this.s4() + '-' +
            this.s4() + '-' + this.s4() + this.s4() + this.s4();
    }

    private  s4(): string {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1);

    }

    private  EventClickeOn(calEvent: FullCalendar.EventObject, jsEvent: MouseEvent, view: FullCalendar.ViewObject) {
        $('#cancel-event').attr("data-id", calEvent.id);
        //  alert('Event: ' + calEvent.title + ' Start: ' + calEvent.start);
        $('#cancelText').text('Cancel ' + 'Event: ' + calEvent.title + ' Start: ' + calEvent.start.utc().format() + ' with id of ' + calEvent.id + '?');
        $('#cancelModal').modal("show");
        // change the border color just for fun
        $(this).css('border-color', 'red');

    }

    private  EventSelection(start: moment.Moment, end: moment.Moment, item: JQuery) {
        var title = prompt('Event Title:');
        var eventData;
        if (title) {
            eventData = {
                id: this.guid(),
                title: title,
                start: start,
                end: end
            };
            $.post( this.evenUrl, { eventStart: eventData.start.utc().format(), eventEnd: eventData.end.utc().format(), name: eventData.title, id: eventData.id }, function (data) {
                $(".result").html(data);
            });
            item.fullCalendar('renderEvent', eventData, true);

        }
        item.fullCalendar('unselect');
    }

    private  EventCancel(event: JQueryEventObject, item: JQuery) {
        $.post("/Consumer/CancelEvent/", { id: $('#cancel-event').attr("data-id") }, function (data) {
            $(".result").html(data);
        });
        item.fullCalendar('removeEvents', $('#cancel-event').attr("data-id"));
    }

    public  ShowCalendar(item: JQuery) {

        var that = this;
        item.fullCalendar({
            theme: true,
            header: {
                left: '',
                center: '',
                right: 'today prev,next'
            },
           
            defaultView: 'agendaDay',
            editable: true,
            events: this.evenUrl,
            selectable: true,
            selectHelper: true,
            eventClick: that.EventClickeOn,
            select: function (start, end) {
                that.EventSelection(start, end, item);
            }

        });
    }
}
