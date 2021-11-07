using Evidance_08.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evidance_08.Controllers
{
    public class SalesController : Controller
    {
        readonly ProductDbContext db = new ProductDbContext();
        // GET: Sales
        public ActionResult Index()
        {
            return View(db.Sales.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Sale s)
        {
            if (ModelState.IsValid)
            {
                db.Sales.Add(s);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            return View(db.Sales.First(x => x.SaleId == id));
        }
        [HttpPost]
        public ActionResult Edit(Sale s)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges(); 
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            return View(db.Sales.Include(p=> p.Products).First(x => x.SaleId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Sale s = new Sale { SaleId = id };
            db.Entry(s).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}