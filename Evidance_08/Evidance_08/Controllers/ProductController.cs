using Evidance_08.Models;
using Evidance_08.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evidance_08.Controllers
{
    public class ProductController : Controller
    {
        readonly ProductDbContext db = new ProductDbContext();
        // GET: Product
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Sales = db.Sales.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductViewVM p)
        {
            if (ModelState.IsValid)
            {
                Product pr = new Product { ProductName = p.ProductName, Stock = p.Stock, SaleId=p.SaleId, Picture = "no-pic.png" };
                if (p.Picture != null)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(p.Picture.FileName);
                    p.Picture.SaveAs(Server.MapPath("~/Images/") + fileName);
                    pr.Picture = fileName;
                }
                db.Products.Add(pr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Sales = db.Sales.ToList();
            return View(p);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Products = db.Products.ToList();
            var p = db.Products.First(x => x.ProductId == id);
            ViewBag.Picture = p.Picture;
            return View(new ProductViewVM { ProductName = p.ProductName, Stock = p.Stock, SaleId = p.SaleId});
        }
        [HttpPost]
        public ActionResult Edit(ProductViewVM p)
        {
            var pr = db.Products.First(x => x.ProductId == p.ProductId);
            if (ModelState.IsValid)
            {

                pr.ProductName = p.ProductName;
                pr.Stock= p.Stock;
                pr.SaleId = p.SaleId;
                if (p.Picture != null)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(p.Picture.FileName);
                    p.Picture.SaveAs(Server.MapPath("~/Images/") + fileName);
                    pr.Picture = fileName;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Pic = p.Picture;
            ViewBag.Products = db.Products.ToList();
            return View(p);
        }
        public ActionResult Delete(int id)
        {
            return View(db.Products.First(x => x.ProductId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Product pr = new Product { ProductId = id };
            db.Entry(pr).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}