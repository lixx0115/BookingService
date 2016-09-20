using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking
{
    class BookableOwner
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Contact Contact { get; set; }

        public List<Bookable> BookableResource { get; set; }

       
    }
}
