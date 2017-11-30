using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        //list of genres in the database.
        public IEnumerable<Genre> Genres { get; set; }
        //getting the movie so we can create/edit it.
        public Movie Movies { get; set; }
    }
}