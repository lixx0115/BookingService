﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking
{
    public class BookedSlot
    {
       public BookedSlot()
        {
            Consumers = new List<Guid>();
        }

       public Guid Id { get; set; }
       public DateTimeOffset Start { get; set; }

       public DateTimeOffset End { get; set; }

       public List<Guid> Consumers { get; set; } 

       public Guid BookableId { get; set; }

       public string AdditionalNotes { get; set; }

       public bool IsOverLap(BookedSlot booking)
        {
            if (this.Start <= booking.End && this.End >= booking.End) //new book end in range
                return true;
            else if (this.Start <= booking.Start && this.End >= booking.Start) // new booking start in range
                return true;
            else if (this.Start >= booking.Start && this.End <= booking.End) // new booking covers 
                return true;
            else if (this.Start <= booking.Start && this.End >= booking.End) //new booking contain within 
                return true;

            return false;
        }
    }
}
