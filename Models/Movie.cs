using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        //the Name field is required.

        [Required]
        [StringLength(255)]
        public String Name { get; set; }
        //adding a genre to the movie
        public Genre Genre { get; set; }
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        public DateTime DateAddedToDatabase { get; set; }
        //The stock must be between the values of 1 and 20.
        [Range(1,20)]
        public int Stock { get; set; }
    }
}