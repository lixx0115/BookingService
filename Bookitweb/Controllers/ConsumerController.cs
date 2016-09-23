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


    }
}