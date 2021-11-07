using Evidance_10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evidance_10.Controllers
{
    public class CustomersController : Controller
    {
        readonly ProductDbContext db = new ProductDbContext();
        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Customer c)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(c);
                db.SaveChanges();
                return PartialView("_PartialResult", true);
            }
            return PartialView("_PartialResult", false);
        }
        public ActionResult Edit(int id)
        {
            return View(db.Customers.First(x => x.CustomerId == id));
        }
        [HttpPost]
        public ActionResult Edit(Customer c)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return PartialView("_PartialResult", true);
            }
            return PartialView("_PartialResult", false);
        }
        public ActionResult Delete(int id)
        {
            return View(db.Customers.First(x => x.CustomerId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            Customer c = new Customer { CustomerId = id };
            db.Entry(c).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}