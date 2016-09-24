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

        static BookableSearcher()
        {
            serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(apiKey));
            if (!serviceClient.Indexers.Exists("bookable"))
            {
                CreateHotelsIndex(serviceClient);
            }
        }

        private static SearchServiceClient serviceClient;
        public static List<Guid> SearchBookableByName(string name)
        {
            //serviceClient.Indexers.Create()
            return new List<Guid>();

        }

        private static void CreateHotelsIndex(SearchServiceClient serviceClient)
        {
            var definition = new Index()
            {
                Name = "bookable",
                Fields = new[]
                {
            new Field("Name", DataType.String)                       { IsKey = true },
           
            new Field("Tags", DataType.Collection(DataType.String))     { IsSearchable = true, IsFilterable = true, IsFacetable = true },
                    }
            };

            serviceClient.Indexes.Create(definition);
        }

        private class Bookablesearchable
        {

            public string Name;

            public List<string> tags;

        }

    }
}
