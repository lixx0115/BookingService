using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking;
namespace TestDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            //var cid = Guid.Parse("52cfecbc-d154-4486-bf44-682fac2c30f2");
            //var bid = Guid.Parse("1ad4b29e-db22-4015-8c45-1e487170f78f");

            //var slot = new BookedSlot();
            //slot.Id = Guid.NewGuid();
            //slot.AdditionalNotes = "stest";
            //slot.BookableId = bid;
            //if(slot.Consumers == null)
            //{
            //    slot.Consumers = new List<string>();
            //}
            //slot.Consumers.Add(cid.ToString());
            //slot.Start = DateTimeOffset.Parse("9/24/2016 13:00:05 PM +00:00");
            //slot.End = DateTimeOffset.Parse("9/24/2016 14:00:05 PM +00:00");
            //var consumer = ConsumerBroker.GetConsumer(cid);

            //var bookable = BookableBroker.GetBookableById(bid);
            //bookable.Name = "testClass";
            //BookableBroker.Save(bookable);
            //if(consumer.Contact == null)
            //{ consumer.Contact = new Contact(); }
            //consumer.Contact.Name = "testConsumer";
            //ConsumerBroker.Save(consumer);
            //Console.WriteLine( consumer.BookSlot(slot));

            //var storage = new Storage("user");
            //storage.Delete("lixx0115@hotmail.com");
            var tag = new List<string>();
            tag.Add("123");
            tag.Add("huanl");
            var bid = Guid.Parse("52cfecbc-d154-4486-bf44-682fac2c30f2");
            BookableSearcher.AddBookableToSearcher("abc", tag, bid);

            var result = BookableSearcher.Search("huanl");
            foreach(var s in result)
            {
                Console.WriteLine(s.Id);
            }






        }
    }
}
