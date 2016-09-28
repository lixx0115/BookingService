using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
namespace Booking
{
    public class BookableSearcher
    {
        private static string searchServiceName = "bookitsearch";

        private static string apiKey = "8260541A6BDAC73A568262A1E02FFE59";

        private static ISearchIndexClient index;
        static BookableSearcher()
        {
            serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(apiKey));
     
            if (!serviceClient.Indexes.Exists("bookable"))
            {
                CreateBookableIndex(serviceClient);
                
            }
            index = serviceClient.Indexes.GetClient("bookable");
        }


        public static void AddBookableToSearcher(string name, List<string> tags, Guid id)
        {
            var searchable = new Bookablesearchable { Name = name, Tags = tags, Id = id };
            var list = new List<Bookablesearchable>();
            list.Add(searchable);
            var batch = IndexBatch.Upload(list); ;
            index.Documents.Index(batch);
             
        }

        private static SearchServiceClient serviceClient;
        public static List<Bookablesearchable> Search(string searchText, string filter = null)
        {
            var searchResult = new List<Bookablesearchable>();
            var sp = new SearchParameters();
           
            if (!String.IsNullOrEmpty(filter))
            {
                sp.Filter = filter;
            }

            DocumentSearchResult<Bookablesearchable> response = index.Documents.Search<Bookablesearchable>(searchText, sp);
            foreach (SearchResult<Bookablesearchable> result in response.Results)
            {
                searchResult.Add(result.Document);
              
            }
            return searchResult;

        }

        private static void CreateBookableIndex(SearchServiceClient serviceClient)
        {
            var definition = new Index()
            {
                Name = "bookable",
                Fields = new[]
                {
                     new Field("Id", DataType.String)                       { IsKey = true },

                    new Field("Name", DataType.String)                       { IsSearchable = true, IsFilterable = true, IsFacetable = true  },
           
                    new Field("Tags", DataType.Collection(DataType.String))     { IsSearchable = true, IsFilterable = true, IsFacetable = true },
                    }
            };

            serviceClient.Indexes.Create(definition);
        }

    }


    public class Bookablesearchable
    {
        public Guid Id;

        public string Name;

        public List<string> Tags;

    }

}
