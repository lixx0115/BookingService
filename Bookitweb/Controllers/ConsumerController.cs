using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Booking;
using Bookitweb;
namespace Bookitweb.Controllers
{
    public class ConsumerController : Controller
    {


        // GET: Consumer
        [Authorize]
        [HttpGet]
        public ActionResult Overview()
        {
            var userName = User.Identity.Name;
            var userId = UserStore.GetId(userName);
            Guid userIdGuid;

            if (userId.HasValue)
            {
                userIdGuid = userId.Value;
                var consumer = ConsumerBroker.GetConsumer(userIdGuid);

                return View(consumer);
            }

            else
            {
                return RedirectToAction("NewUser");

            }

        }

        [HttpPost]
        public HttpStatusCodeResult CancelEvent(string id)
        {
            return new HttpStatusCodeResult(200);
        }
        [HttpPost]
        public HttpStatusCodeResult BookEvent(DateTimeOffset eventStart, DateTimeOffset eventEnd, string name, string id)
        {
            var userName = User.Identity.Name;
            var userId = UserStore.GetId(userName);

            var consumer = ConsumerBroker.GetConsumer(userId.Value);
     

            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        public JsonResult GetEvents(DateTime start, DateTime end)
        {
            var userName = User.Identity.Name;
            var userId = UserStore.GetId(userName);
            
            var consumer = ConsumerBroker.GetConsumer(userId.Value);
            var events = consumer.GetBookedSlots(start, end);

            var eventList = from e in events
                            select new
                            {
                                id = e.Id,
                                title = "appointMent",
                                start = e.Start.ToString("s"),
                                end = e.End.ToString("s"),
                                allDay = false
                            };

            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NewUser()
        {
            return View("New");
        }
        [HttpPost]
        public ActionResult NewUser(Consumer consumer)
        {
            consumer.Id = UserStore.CreateUser(User.Identity.Name);
            ConsumerBroker.Save(consumer);
            return RedirectToAction("Overview", "Consumer", new { id = 5 });
        }


        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

    }
}