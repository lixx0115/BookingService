
/// <reference path="typings/bootstrap/bootstrap.d.ts" /> 
/// <reference path="typings/jqueryui/jqueryui.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
class Searcher {

    private static searchCall (searchTerm: string) {
        
        $.get("/bookable/SearchByTerm/?searchTerm=" + searchTerm, function (data) {
            Searcher.Display(data);
        });
    }

    private static Display(resultList: Array<any>) {
        for (var r of resultList) {
            let item = jQuery('<button />', {
                type: "button",
                class: "list-group-item",
                text: r.Name
            });
            item.attr("data-id", r.Id);
            item.on("click",
                function () {
                    Searcher.GoToCalender(r.Id);
                    } ) ,
               item.appendTo('#searchResult');

        }
    }

    private static GoToCalender(bookableId:string) {
        $('#searchResult').hide();
        sessionStorage.setItem("bookableid", bookableId);
        var cal = new mycalendar("/Bookable/getevents/" + bookableId);
        cal.DisplayBookableCalendar($("#calendar"), $("#datepicker"), $('#cancel-event'));

    }

    private static ClearDisplay() {
        $("#searchResult").empty();
    }

    public static SearchAndDisplay(searchTerm: string) {
        Searcher.ClearDisplay();
          Searcher.searchCall(searchTerm);
    }

    public static BindSearch(searchBotton: JQuery) {
        searchBotton.on("click", function () {
            Searcher.SearchAndDisplay($("#searchText").val());
        });
    }

    
}

 