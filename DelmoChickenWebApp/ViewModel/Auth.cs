﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DelmoChickenWebApp.ViewModel
{
    public class AuthLogin
    {
        //public string Test { get; set; }
        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]//Annotations
        public string Password { get; set; }
    }
}