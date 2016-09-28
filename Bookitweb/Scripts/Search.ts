
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
            jQuery('<a/>', {
                href: 'index?id'+r.Id,
                class: "list-group-item",
                text: r.Name
            }).appendTo('#searchResult');

        }
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

 