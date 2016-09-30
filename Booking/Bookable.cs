using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking
{
    public class Bookable
    {
        public Bookable() {
        }

        public Bookable(Guid id)
        {
            this.Id = id;
        }
        public Guid Id { get; set; }

        public List<BookedSlot> BookedSlots { get; set; }
        public string Name { get; set; }

        public Contact BookingContact { get; set; }

        public List<string> Tags {get; set;}

        public string Description { get; set; }

        public List<BookedSlot> GetBookedSlots(DateTimeOffset start, DateTimeOffset end)
        {
            if(this.BookedSlots == null)
            {
                return new List<BookedSlot>();
            }
            return BookedSlots.FindAll(x => x.Start >= start || x.End <= end);
        }
        
        public bool BookSlot(List<BookedSlot> bookings)
        {
            if (!ValidateBookingSlots(bookings))
                return false;

            this.BookedSlots.AddRange(bookings);

            BookableBroker.Save(this);
            return true;
        }

        public bool BookSlot(BookedSlot booking)
        {
            if (ValidateBookingSlot(booking))
            {
                if(this.BookedSlots == null)
                {
                    this.BookedSlots = new List<BookedSlot>();
                }
                this.BookedSlots.Add(booking);
                BookableBroker.Save(this);
                return true;
            }
            return false;
        }

        public bool CancelBooking(Guid BookingId)
        {
            if (this.BookedSlots == null)
            {
                return true;
            }
            var toRemove = this.BookedSlots.Find(x => x.Id == BookingId);
            if(toRemove == null)
            {
                return true;
            }
            this.BookedSlots.Remove(toRemove);
            BookableBroker.Save(this);
            return true;
        }

        private bool ValidateBookingSlot(BookedSlot booking)
        {  if (this.BookedSlots == null)
            {
                return true;
            }

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
