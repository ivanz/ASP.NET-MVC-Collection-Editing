using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CollectionEditing.Models
{
    public class User
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IList<Movie> FavouriteMovies { get; set; }
    }
}