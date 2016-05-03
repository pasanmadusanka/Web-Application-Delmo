using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DelmoChickenWebApp.Areas.Admin.ViewModels;
using DelmoChickenWebApp.DAL;
using DelmoChickenWebApp.Models;

namespace DelmoChickenWebApp.Areas.Admin.Controllers
{
    [Authorize(Roles="admin")]
    public class UsersController : Controller
    {
        private DataContext db = new DataContext();
        public ActionResult Index()
        {
            return View(new UsersIndex
            {
                Users = db.Users.ToList()
            });
        }
        public ActionResult New()
        {
            return View(new UsersNew
                {
                    Roles = db.Roles.Select(r => new RoleCheckbox
                    {
                        Id = r.RoleId,
                        IsChecked = false,
                        Name = r.Name
                    }).ToList()
                });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(UsersNew form)
        {
            var user = new User();
            SyncRols(form.Roles, user.Roles);
            if (db.Users.Any(u => u.Username == form.Username))
                ModelState.AddModelError("Username", "Username must be unique");

            if (!ModelState.IsValid)
            { return View(form); }

            user.Email = form.Email;
            user.Username = form.Username;
            user.SetPassword(form.Password);

            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult Edit(int id)
        //{
        //    var user = Database.session.Load<User>(id);
        //    if (user == null)
        //        return HttpNotFound();

        //    return View(new UsersEdit
        //    {
        //        Username = user.Username,
        //        Email = user.Email,
        //        Roles = Database.session.Query<Role>().Select(role => new RoleCheckbox
        //        {
        //            Id = role.ID,
        //            IsChecked = user.Roles.Contains(role),
        //            Name = role.Name
        //        }).ToList()
        //    });
        //}

        public ActionResult Edit(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(new UsersEdit
            {
                Username = user.Username,
                Email = user.Email,
                Roles = db.Roles.Select(r => new RoleCheckbox
                {
                    Id = r.RoleId,
                    IsChecked = user.Roles.Contains(r),
                    Name = r.Name
                }).ToList()
            });

        }

        private void SyncRols(IList<RoleCheckbox> checkboxes, IList<Role> roles)
        {
            var selectedRols = new List<Role>();
            foreach (var role in db.Roles)
            {
                var checkbox = checkboxes.Single(c => c.Id == role.RoleId);
                checkbox.Name = role.Name;

                if (checkbox.IsChecked)
                    selectedRols.Add(role);
            }
            foreach (var toAdd in selectedRols.Where(t => !roles.Contains(t)))
                roles.Add(toAdd);

            foreach (var toRemove in roles.Where(t => !selectedRols.Contains(t)).ToList())
                roles.Remove(toRemove);
        }
    }
}