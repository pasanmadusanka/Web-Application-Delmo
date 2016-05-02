using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DelmoChickenWebApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required, StringLength(50, MinimumLength = 3, ErrorMessage = "Product name should be more then 3 characters!")]
        public string ProductName { get; set; }

        [DataType(DataType.Url,ErrorMessage="Image path should be a url"), MaxLength(128)]
        public string Image { get; set; }

        [DataType(DataType.Text),AllowHtml]
        public string Discription { get; set; }

        [DataType(DataType.Currency,ErrorMessage="This should be a price!"),Required] 
        public decimal Price { get; set; } 
    }
}