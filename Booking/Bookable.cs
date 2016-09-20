using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking
{
    public class Bookable
    {
        public Bookable() { }

        public Bookable(Guid id)
        {
            this.Id = id;
        }
        public Guid Id { get; set; }

        public List<BookedSlot> BookedSlots { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public List<BookedSlot> GetBookedSlots(DateTimeOffset start, DateTimeOffset end)
        {
            return BookedSlots.FindAll(x => x.Start >= start || x.End <= end);
        }

        public bool BookSlot(List<BookedSlot> bookings)
        {
            if (!ValidateBookingSlots(bookings))
                return false;

            this.BookedSlots.AddRange(bookings);
            return true;
        }

        public bool BookSlot(BookedSlot booking)
        {
            if (ValidateBookingSlot(booking))
            {
                this.BookedSlots.Add(booking);
                return true;
            }
            return false;
        }

        public bool CancelBooking(BookedSlot cancelAtempt)
        {
            var bookable = BookableBroker.GetBookableById(cancelAtempt.BookableId);
            var toRemove = this.BookedSlots.Find(x => x.Id == cancelAtempt.Id);
            this.BookedSlots.Remove(toRemove);
            return true;
        }

        private bool ValidateBookingSlot(BookedSlot booking)
        {
            foreach (var s in this.BookedSlots)
            {
                if (s.IsOverLap(booking))
                    return false;
            }
            return true;
        }

        private bool ValidateBookingSlots(List<BookedSlot> bookings)
        {
            foreach (var s in this.BookedSlots)
            {
                foreach(var b in bookings)
                if (s.IsOverLap(b))
                    return false;
            }
            return true;
        }
    }
}
