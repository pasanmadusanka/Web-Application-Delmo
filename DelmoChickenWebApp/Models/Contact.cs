using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DelmoChickenWebApp.Models
{
    public class Contact
    {
        [Display(Name = "Full Name"), Required]
        public String Name { get; set; }
        [Display(Name="E-Mail"),DataType(DataType.EmailAddress),Required]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber),Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }

        [Required,MaxLength(128)]
        public string Subject { get; set; }

        [Required, DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}