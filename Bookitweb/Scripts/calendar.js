/// <reference path="typings/bootstrap/bootstrap.d.ts" />
/// <reference path="typings/fullcalendar/fullcalendar.d.ts" />
/// <reference path="typings/jqueryui/jqueryui.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/moment/moment.d.ts" />
var mycalendar = (function () {
    function mycalendar(eventUrl) {
        this.evenUrl = eventUrl;
    }
    mycalendar.prototype.DisplayConsumerCalendar = function (cal, datapicker, modal) {
        var that = this;
        this.ShowConsumerCalendar(cal);
        modal.click(function (evt) { that.EventCancel(evt, cal); });
        datapicker.datepicker({
            onSelect: function (dateText, inst) {
                var d = new Date(dateText);
                cal.fullCalendar('gotoDate', d);
            }
        });
    };
    mycalendar.prototype.DisplayBookableCalendar = function (cal, datapicker, modal) {
        var that = this;
        this.ShowBookableCalendar(cal);
        modal.click(function (evt) { that.EventCancel(evt, cal); });
        datapicker.datepicker({
            onSelect: function (dateText, inst) {
                var d = new Date(dateText);
                cal.fullCalendar('gotoDate', d);
            }
        });
    };
    mycalendar.prototype.guid = function () {
        return this.s4() + this.s4() + '-' + this.s4() + '-' + this.s4() + '-' +
            this.s4() + '-' + this.s4() + this.s4() + this.s4();
    };
    mycalendar.prototype.s4 = function () {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1);
    };
    mycalendar.prototype.EventClickeOn = function (calEvent, jsEvent, view) {
        $('#cancel-event').attr("data-id", calEvent.id);
        //  alert('Event: ' + calEvent.title + ' Start: ' + calEvent.start);
        $('#cancelText').text('Cancel ' + 'Event: ' + calEvent.title + ' Start: ' + calEvent.start.utc().format() + ' with id of ' + calEvent.id + '?');
        $('#cancelModal').modal("show");
        // change the border color just for fun
        $(this).css('border-color', 'red');
    };
    mycalendar.prototype.BookEvent = function (start, end, item) {
        var title = prompt('Event Title:');
        var eventData;
        if (title) {
            eventData = {
                id: this.guid(),
                title: title,
                start: start,
                end: end
            };
            $.post("bookevent", { eventStart: eventData.start.utc().format(), eventEnd: eventData.end.utc().format(), name: eventData.title, id: eventData.id, bookableId: sessionStorage["bookableid"] }, function (data) {
                $(".result").html(data);
            });
            item.fullCalendar('renderEvent', eventData, true);
        }
        item.fullCalendar('unselect');
    };
    mycalendar.prototype.EventCancel = function (event, item) {
        $.post("/Consumer/CancelEvent/", { id: $('#cancel-event').attr("data-id") }, function (data) {
            $(".result").html(data);
        });
        item.fullCalendar('removeEvents', $('#cancel-event').attr("data-id"));
    };
    mycalendar.prototype.ShowConsumerCalendar = function (item) {
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
            selectable: false,
            selectHelper: false,
            eventClick: that.EventClickeOn,
            select: function (start, end) {
                that.BookEvent(start, end, item);
            }
        });
    };
    mycalendar.prototype.ShowBookableCalendar = function (item) {
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
                that.BookEvent(start, end, item);
            }
        });
    };
    return mycalendar;
}());
//# sourceMappingURL=calendar.js.map