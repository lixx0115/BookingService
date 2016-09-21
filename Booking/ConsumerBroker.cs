using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking
{
    public static class ConsumerBroker
    {

        private static readonly string tableName = "consumer";

        private static Storage consumerStore = new Storage(tableName);

        public static Consumer GetConsumer(Guid id)
        {

            var consumerString = consumerStore.Get(id);

            if (string.IsNullOrWhiteSpace(consumerString))

            {
              return  new Consumer { Id = id, Contact = new Contact(), MyBookableList = new List<Guid>() , myBookedSlotList = new List<BookedSlot>()};
            }
            return FromJson(consumerString);
        }

        public static void Save(Consumer consumer)
        {
            var consumerJson = ToJson(consumer);
            consumerStore.Save(consumer.Id, consumerJson);
        }

        private static string ToJson(Consumer consumer)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(consumer);
        }

        private static Consumer FromJson(string consumerJson)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Consumer>(consumerJson);
        }
    }
}
