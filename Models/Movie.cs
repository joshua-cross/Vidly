﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public String Name { get; set; }
        //adding a genre to the movie
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime DateAddedToDatabase { get; set; }
        public int Stock { get; set; }
    }
}