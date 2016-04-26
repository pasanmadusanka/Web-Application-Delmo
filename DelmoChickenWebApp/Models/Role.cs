using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DelmoChickenWebApp.Models
{
    public class Role
    {
        public Role() 
        { 

        }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}