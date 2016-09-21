using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking
{
    public static class BookableBroker
    {
        private static readonly string tableName = "bookable";
            
        private static Storage bookableStore = new Storage(tableName);


        public static Bookable GetBookableById(Guid bookableId)
        {
            var bookableString = bookableStore.Get( bookableId);
            if (string.IsNullOrWhiteSpace(bookableString))
            {
                var bookable =  new Bookable(bookableId);
                 bookable.BookedSlots = new List<BookedSlot>();
                return bookable;
            }
            return FromJson(bookableString);
        }

        public static void Save(Bookable bookable)
        {
            bookableStore.Save(bookable.Id, ToJson(bookable));
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
