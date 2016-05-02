using DelmoChickenWebApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;
using DelmoChickenWebApp.Models;
using System.Net.Mail;

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

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";
        //    return View();
        //}
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
        public ActionResult ProductList(int? page,string currentFilter, string searchString = null)
        {
            if (searchString != null)
                if (searchString != null)
                {
                    page = 1;
                }
                else { searchString = currentFilter; }

            ViewBag.CurrentFilter = searchString;

            //var students = from s in db.Students
            //               select s;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    students = students.Where(s => s.LastName.Contains(searchString)
            //        || s.FirstMidName.Contains(searchString));
            //}

            var products = from p in db.Products
                           select p;

            //products = products.OrderBy(p => p.ProductName)
            //    .Where(p => searchString == null || p.ProductName.StartsWith(searchString));

            products = products.OrderBy(p => p.ProductName)
                .Where(p => searchString == null || p.ProductName.Contains(searchString));

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    products =  products.OrderBy(p => p.ProductName).Where(p => p.ProductName.Contains(searchString) 
            //        || p.Price.ToString().Contains(searchString));
            //}
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Products", products);
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
            return Json(product, JsonRequestBehavior.AllowGet);
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

        /*Form Submission at contact page*/

        [HttpGet]
        public ActionResult Contact()
        {
            Contact temp = new Contact(); 
            return View(temp);
        }


        [HttpPost]
        public ActionResult Contact(Contact Model)
        {
            string Text = "<html> <head> </head>" +
            " <body style= \" font-size:12px; font-family: Arial\">" +
            Model.Message + " By " +Model.Email+ " Teliphone :"+Model.PhoneNumber+
            "</body></html>";

            SendEmail(Text,Model.Subject);
            Contact tempForm = new Contact();
            return View(tempForm);
        }


        public static bool SendEmail(string Text,string subject)
        {
            MailMessage msg = new MailMessage();
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("pasanmadusanka29@gmail.com", ""),
                EnableSsl = true
            };

            //set the addresses
            msg.From = new MailAddress("pasanmadusanka29@gmail.com");
            msg.To.Add("pasanmadusanka29@gmail.com");

            //set the content
            msg.Subject = subject;
            msg.Body = Text;
            msg.IsBodyHtml = true;
            //client.Send(msg);  
            try
            {
                client.Send(msg);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

    }
}