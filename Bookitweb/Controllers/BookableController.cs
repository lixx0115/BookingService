using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Booking;

namespace Bookitweb.Controllers
{
    public class BookableController : Controller
    {
        // GET: Bookable
        public ActionResult Index(string id)
        { 
   
            var bookable = BookableBroker.GetBookableById(Guid.Parse(id));
            return View(bookable);
        }

        // GET: Bookable/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: Bookable/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public JsonResult SearchByTerm(string searchTerm)
        {
            var result = BookableSearcher.Search(searchTerm);
            var rows = result.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetEvents(DateTime start, DateTime end)
        {
            var userName = User.Identity.Name;
            var userId = UserStore.GetId(userName);

            var bookable = BookableBroker.GetBookableById(userId.Value);
            var events = bookable.GetBookedSlots(start, end);

            var eventList = from e in events
                            select new
                            {
                                id = e.Id,
                                title = e.AdditionalNotes,
                                start = e.Start.ToString("s"),
                                end = e.End.ToString("s"),
                                allDay = false
                            };

            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        // POST: Bookable/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var bookable = new Bookable();
                bookable.BookingContact = new Contact();
                bookable.Name = collection["Name"];
                var tagstring = collection["Tags"];
                bookable.Description = collection["Description"];
                bookable.BookingContact.Name = collection["ContactName"];
                bookable.BookingContact.PhoneNumber = collection["ContactPhoneNumber"];
                bookable.BookingContact.Email = collection["ContactEmail"];

                bookable.Id = Guid.NewGuid();
                BookableBroker.Save(bookable);
                BookableSearcher.AddBookableToSearcher(bookable.Name, bookable.Tags, bookable.Id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // GET: Bookable/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Bookable/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Bookable/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Bookable/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
