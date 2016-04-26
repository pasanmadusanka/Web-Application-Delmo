using DelmoChickenWebApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;
using DelmoChickenWebApp.Models;

namespace DelmoChickenWebApp.Controllers
{
    public class HomeController : Controller
    {
        private DataContext db = new DataContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult Csr()
        {
            //ViewBag.Message = "Your app description page.";
            return Content("<h1>Currently Under Development stage</h1>");
        }
        public ActionResult News()
        {
            //ViewBag.Message = "Your app description page.";
            //return Content("<h1>Currently Under Development stage</h1>");

            return View(db.Posts.ToList());
        }
        public ActionResult ProductList(int? page, string searchTerm=null)
        {
            var products = from p in db.Products
                           select p;
            products = products.OrderBy(p => p.ProductName)
                .Where(p => searchTerm == null || p.ProductName.StartsWith(searchTerm));
            int pageSize = 6;
            int pageNumber = (page ?? 1);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Resturents", products);
            }

            return View(products.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Autocomplete(string term)
        {
            var product = db.Products
                .Where(p => p.ProductName.StartsWith(term))
                .Take(20)
                .Select(p => new
                {
                    label = p.ProductName
                });
            return Json(product,JsonRequestBehavior.AllowGet);
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

    }
}