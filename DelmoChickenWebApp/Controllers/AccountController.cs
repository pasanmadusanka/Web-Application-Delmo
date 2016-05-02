using DelmoChickenWebApp.DAL;
using DelmoChickenWebApp.Models;
using DelmoChickenWebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;

namespace DelmoChickenWebApp.Controllers
{ 
    public class AccountController : Controller
    {
        private DataContext db = new DataContext();
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("home");
        }
        public ActionResult Login()
        {
            return View(new AuthLogin
            {
                //Test="This is my test value set in my controller"
            });
        }
        [HttpPost]
        public ActionResult Login(AuthLogin form, string returnUrl)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == form.Username);
            //var user = Database.session.Query<User>().FirstOrDefault(u => u.Username == form.Username);
            if (user == null || !user.CheckPassword(form.Password))
                ModelState.AddModelError("Username", "Username or Password incorrect");
            DelmoChickenWebApp.Models.User.FakeHash();

            if (!ModelState.IsValid)
                return View(form);

            //authontication Library
            FormsAuthentication.SetAuthCookie(user.Username, true);

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            return RedirectToRoute("home");
        }
    }
}