using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DelmoChickenWebApp.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required, StringLength(128, MinimumLength = 3)]
        public string Title { get; set; } 

        [Required, DataType(DataType.MultilineText), AllowHtml]
        public string Content { get; set; }
                
        [Required, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true),
        Display(Name = "Date")]
        public DateTime CreatedAt { get; set; }

        [MaxLength(128), DataType(DataType.Url, ErrorMessage = "Image path should be a url")]
        public string ImagePath { get; set; }
    }
}