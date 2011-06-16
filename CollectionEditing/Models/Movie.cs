using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CollectionEditing.Models
{
    public class Movie
    {
        [Required]
        public string Title { get; set; }

        public int Rating { get; set; }
    }
}