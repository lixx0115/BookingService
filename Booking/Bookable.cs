using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking
{
    public class Bookable
    {
        public List<BookedSlot> BookedSlots { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public List<BookedSlot> GetBookedSlots(DateTimeOffset start, DateTimeOffset end)
        {
            return BookedSlots.FindAll(x => x.Start >= start || x.End <= end);
        }

        public bool BookSlot(List<BookedSlot> booking)
        {
            this.BookedSlots.AddRange(booking);
            return true;
        }

        public bool BookSlot(BookedSlot booking)
        {
            this.BookedSlots.Add(booking);
            return true;
        }

        public bool CancelBooking(BookedSlot cancelAtempt)
        {
            var bookable = BookableBroker.GetBookable(cancelAtempt.BookableId);
            var toRemove = this.BookedSlots.Find(x => x.Id == cancelAtempt.Id);
            this.BookedSlots.Remove(toRemove);
            return true;
        }
    }
}
