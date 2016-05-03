
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DelmoChickenWebApp.Models
{
    public class User
    {
        private const int workFactor = 13;

        public static void FakeHash()
        {
            BCrypt.Net.BCrypt.HashPassword("", workFactor);
        }
        public User() {
            Roles = new List<Role>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public virtual IList<Role> Roles { get; set; }
        public virtual void SetPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, workFactor);
        }
        public virtual bool CheckPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        } 
    }
}