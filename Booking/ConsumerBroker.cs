using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking
{
    public static class ConsumerBroker
    {
        public static Consumer GetConsumer(string id)
        {
            return new Consumer();
        }
    }
}
