using Evidance_10.Models;
using Evidance_10.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evidance_10.Controllers
{
    public class ProductsController : Controller
    {
        readonly ProductDbContext db = new ProductDbContext();
        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.Include(x => x.Customer).ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Customers = db.Customers.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductInputModel p)
        {
            if (ModelState.IsValid)
            {
                Product pr = new Product
                {
                    ProductName = p.ProductName,
                    Price = p.Price,
                    SalesDate = p.SalesDate,
                    Discontinued = p.Discontinued,
                    CustomerId = p.CustomerId,
                    Picture = "on2.jpg"
                };
                if (p.Picture != null && p.Picture.ContentLength > 0)
                {
                    string filePath = Server.MapPath("~/Uploads/");
                    string fileName = Guid.NewGuid() + Path.GetExtension(p.Picture.FileName);
                    p.Picture.SaveAs(Path.Combine(filePath, fileName));
                    pr.Picture = fileName;
                }
                db.Products.Add(pr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customers = db.Customers.ToList();
            return View(p);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Customers = db.Customers.ToList();
            var p = db.Products.First(x => x.ProductId == id);
            ViewBag.CurrentPic = p.Picture;
            return View(new ProductEditModel { ProductId = p.ProductId, ProductName = p.ProductName, Price = p.Price, SalesDate = p.SalesDate, Discontinued = p.Discontinued, CustomerId = p.CustomerId });
        }
        [HttpPost]
        public ActionResult Edit(ProductEditModel p)
        {
            if (ModelState.IsValid)
            {
                Product pr = db.Products.First(x => x.ProductId == p.ProductId);
                pr.ProductName = p.ProductName;
                pr.Price = p.Price;
                pr.SalesDate = p.SalesDate;
                pr.Discontinued = p.Discontinued;
                pr.CustomerId = p.CustomerId;
                if (p.Picture != null && p.Picture.ContentLength > 0)
                {
                    string filePath = Server.MapPath("~/Uploads/");
                    string fileName = Guid.NewGuid() + Path.GetExtension(p.Picture.FileName);
                    p.Picture.SaveAs(Path.Combine(filePath, fileName));
                    pr.Picture = fileName;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customers = db.Customers.ToList();
            var d = db.Products.First(x => x.ProductId == p.ProductId);
            ViewBag.CurrentPic = d.Picture;
            return View(p);
        }
        public ActionResult Delete(int id)
        {
            return View(db.Products.Include(x => x.Customer).First(x => x.ProductId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            Product p = new Product { ProductId = id };
            db.Entry(p).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            ViewBag.Customers = db.Customers.ToList();
            return RedirectToAction("Index");
        }
    }
}