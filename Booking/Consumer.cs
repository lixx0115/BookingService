using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking
{
    public class Consumer
    {

        private List<BookedSlot> myBookedSlotList = new List<BookedSlot>();
        public Contact Contact { get; set; }

        public List<string> MyBookableList { get; set; }

        public List<BookedSlot> GetMyBookedSlotList()
        { return 
                myBookedSlotList
                ;
        }
        
        public List<BookedSlot> GetBookedSlots(DateTimeOffset start, DateTimeOffset end)
        {
            return myBookedSlotList.FindAll(x => x.Start >= start || x.End <= end);
        }

        public bool BookSlots(List<BookedSlot> bookingAtempt)
        {
            var bookable = BookableBroker.GetBookable(bookingAtempt[0].BookableId);
            bookable.BookSlot(bookingAtempt);
            myBookedSlotList.AddRange(bookingAtempt);
            return true;
        }

        public bool BookSlot (BookedSlot bookingAtempt) { 
            var bookable = BookableBroker.GetBookable(bookingAtempt.BookableId);
            bookable.BookSlot(bookingAtempt);
            myBookedSlotList.Add(bookingAtempt);
            return true;
        }

        public bool CancelBooking(BookedSlot cancelAtempt)
        {
            var bookable = BookableBroker.GetBookable(cancelAtempt.BookableId);
            var toRemove = this.myBookedSlotList.Find(x => x.Id == cancelAtempt.Id);
            this.myBookedSlotList.Remove(toRemove);
            bookable.CancelBooking(cancelAtempt);
            return true;
        }

        public bool CancelBooking(List<BookedSlot> cancelAtempts)
        {
            var bookable = BookableBroker.GetBookable(cancelAtempts[0].BookableId);
            return true;
        }
    }
}
