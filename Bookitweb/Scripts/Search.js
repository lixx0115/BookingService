/// <reference path="typings/bootstrap/bootstrap.d.ts" /> 
/// <reference path="typings/jqueryui/jqueryui.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
var Searcher = (function () {
    function Searcher() {
    }
    Searcher.searchCall = function (searchTerm) {
        $.get("/bookable/SearchByTerm/?searchTerm=" + searchTerm, function (data) {
            Searcher.Display(data);
        });
    };
    Searcher.Display = function (resultList) {
        for (var _i = 0, resultList_1 = resultList; _i < resultList_1.length; _i++) {
            var r = resultList_1[_i];
            var item = jQuery('<button />', {
                type: "button",
                class: "list-group-item",
                text: r.Name
            });
            item.attr("data-id", r.Id);
            item.on("click", function () {
                Searcher.GoToCalender(r.Id);
            }),
                item.appendTo('#searchResult');
        }
    };
    Searcher.GoToCalender = function (bookableId) {
        $('#searchResult').hide();
        sessionStorage.setItem("bookableid", bookableId);
        var cal = new mycalendar("/Bookable/getevents/" + bookableId);
        cal.DisplayBookableCalendar($("#calendar"), $("#datepicker"), $('#cancel-event'));
    };
    Searcher.ClearDisplay = function () {
        $("#searchResult").empty();
    };
    Searcher.SearchAndDisplay = function (searchTerm) {
        Searcher.ClearDisplay();
        Searcher.searchCall(searchTerm);
    };
    Searcher.BindSearch = function (searchBotton) {
        searchBotton.on("click", function () {
            Searcher.SearchAndDisplay($("#searchText").val());
        });
    };
    return Searcher;
}());
//# sourceMappingURL=Search.js.map