using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking
{
    public static class BookableBroker
    {
        public static Bookable GetBookableById(Guid bookableId)
        {
            return new Bookable(bookableId);
        }

        private static string ToJson(Bookable bookable)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(bookable);
        }

        private static Bookable FromJson(string bookableJson)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Bookable>(bookableJson);
        }
    }
}
