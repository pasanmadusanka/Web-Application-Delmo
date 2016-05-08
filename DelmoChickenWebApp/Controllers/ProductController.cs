using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DelmoChickenWebApp.DAL;
using DelmoChickenWebApp.Models;
using PagedList;

namespace DelmoChickenWebApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private DataContext db = new DataContext();

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //return View(db.Products.ToList());
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "price" ? "price_desc" : "price";

            if (searchString != null)
            {
                page = 1;
            }
            else { searchString = currentFilter; }

            ViewBag.CurrentFilter = searchString;
            

            var products = from p in db.Products
                           select p;
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.ProductName);
                    break; 
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case "price":
                    products = products.OrderBy(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.ProductName);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Image,Price,Discription")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Image,Price,Discription")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
                return HttpNotFound();
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
