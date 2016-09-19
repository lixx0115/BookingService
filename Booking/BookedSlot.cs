using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking
{
    public class BookedSlot
    {
       public string Id { get; set; }
       public DateTimeOffset Start { get; set; }

       public DateTimeOffset End { get; set; }

       public List<string> Consumers { get; set; } 

       public string BookableId { get; set; }

       public string AdditionalNotes { get; set; }
    }
}
