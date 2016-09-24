using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bookitweb.Controllers
{
    public class BookableController : Controller
    {
        // GET: Bookable
        public ActionResult Index()
        {
            return View();
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

        // POST: Bookable/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

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
