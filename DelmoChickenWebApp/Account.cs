using DelmoChickenWebApp.DAL;
using DelmoChickenWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DelmoChickenWebApp
{
    public static class Account
    {
        private const string UserKey = "DelmoChickenWebApp.Account.UserKey";

        public static User User
        {
            get
            {
                DataContext db = new DataContext();

                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;

                var user = HttpContext.Current.Items[UserKey] as User;
                if (user == null)
                {
                    user = db.Users.FirstOrDefault(u => u.Username == HttpContext.Current.User.Identity.Name);

                    if (user == null)
                        return null;

                    HttpContext.Current.Items[UserKey] = user; 
                }
                return user;
            }
        }
    }
}