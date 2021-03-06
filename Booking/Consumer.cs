﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking
{
    public class Consumer
    {
        public Guid Id { get; set; }
        public Consumer()
        {
            MyBookableList = new List<Guid>();
            myBookedSlotList = new List<BookedSlot>();
            Contact = new Contact();

        }

        public Consumer(Guid id)
        {
            this.Id = id;
        }
         
        public Contact Contact { get; set; }

        public List<Guid> MyBookableList { get; set; }

        public List<BookedSlot> myBookedSlotList
        {
            get; set;
        }
        
        public List<BookedSlot> GetBookedSlots(DateTimeOffset start, DateTimeOffset end)
        {
            if (myBookedSlotList == null)
                return new List<BookedSlot>();
            return myBookedSlotList.FindAll(x => x.Start >= start || x.End <= end);
        }

        public bool BookSlots(List<BookedSlot> bookingAtempt)
        {

            if (!ValidateBookingSlots(bookingAtempt))
                return false;
            var bookable = BookableBroker.GetBookableById(bookingAtempt[0].BookableId);
            bookable.BookSlot(bookingAtempt);
            myBookedSlotList.AddRange(bookingAtempt);
            ConsumerBroker.Save(this);
            return true;
        }

        public bool BookSlot (BookedSlot bookingAtempt) {

            if (!ValidateBookingSlot(bookingAtempt))
                return false;
            var bookable = BookableBroker.GetBookableById(bookingAtempt.BookableId);
            if (bookable.BookSlot(bookingAtempt))
            {
                myBookedSlotList.Add(bookingAtempt);
                ConsumerBroker.Save(this);
                return true;
            }
            return false;
        }

        public bool CancelBooking(Guid bookingId)
        {
            
            var toRemove = this.myBookedSlotList.Find(x => x.Id == bookingId);
            this.myBookedSlotList.Remove(toRemove);

            if (toRemove != null ||toRemove.BookableId != null || toRemove.BookableId != Guid.Empty)
            {
                var bookable = BookableBroker.GetBookableById(toRemove.BookableId);
                bookable.CancelBooking(toRemove.BookableId);
            }
            ConsumerBroker.Save(this);
            return true;
        }

        public bool CancelBooking(List<BookedSlot> cancelAtempts)
        {
            var bookable = BookableBroker.GetBookableById(cancelAtempts[0].BookableId);
            ConsumerBroker.Save(this);
            return true;
        }

        private bool ValidateBookingSlot(BookedSlot booking)
        {
            foreach (var s in this.myBookedSlotList)
            {
                if (s.IsOverLap(booking))
                    return false;
            }
            return true;
        }

        private bool ValidateBookingSlots(List<BookedSlot> bookings)
        {

            foreach (var s in this.myBookedSlotList)
            {
                foreach (var b in bookings)
                    if (s.IsOverLap(b))
                        return false;
            }
            return true;
        }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
