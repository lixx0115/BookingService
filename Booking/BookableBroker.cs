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
    }
}
