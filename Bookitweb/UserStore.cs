using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
namespace Bookitweb
{
    public static class UserStore
    {
        private static MemoryCache userCache = MemoryCache.Default;

        private static Booking.Storage storage = new Booking.Storage("user");

       


        public static Guid CreateUser(string userName)
        {
            var newId =  Guid.NewGuid();
            var policy = new CacheItemPolicy();

            storage.Save(userName, newId.ToString());
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddDays(4);

            userCache.Add(new CacheItem(userName, newId), policy);
            return newId;

        }


        public static Nullable<Guid> GetId(string userName)
        {
            if(userCache.Contains(userName))
            {
                return (Guid) userCache[userName];
            }
            else
            {
                var storeResult = storage.Get(userName);
                if(string.IsNullOrWhiteSpace(storeResult))
                {
                    return null;
                }
                else
                {
                    return Guid.Parse(storeResult);
                }
            }
            
        }

    }
}