using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {


        // GET: Movies/Random
        public ViewResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" }

            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };
            return View(viewModel);
        }

        public ActionResult Edit(int id)
        { 
            return Content("id = " + id);
        }

        //routing so we can reach this page by simply doing /Movie.
        [Route("Movie")]
        public ActionResult Index(int? pageIndex, string sortBy)
        {


            //setting the MovieViewModel to be the movies we have created here.
            var viewModel = new MoviesViewModel
            {

                Movies = getMovies()

            };

            /*
            //if page index does not have a value intitialise it to 1.
            if(!pageIndex.HasValue)
            {
                pageIndex = 1;
            }

            if(String.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "Name";

            }
            */

            //setting the view to be the view model.
            return View(viewModel);
        }

        
        //routing to Movie/Detail/ID.
        [Route("Movie/Details/{id}")]
        public ActionResult details(int id)
        {
            try
            {
                var movie = new Movie();
                movie = getMovies()[id];
                return View(movie);
            }
            catch
            {
                //getting the movie based on the id we was given.
                return HttpNotFound();
            }
        }
        

        
        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
        

        //creating a new function that creates a list of movies, and then returns it.
        public List<Movie> getMovies()
        {
            //creating a new list for some movies (the Movie Model).
            var movies = new List<Movie>
            {

                new Movie { Name = "Shrek!", Id = 1 },
                new Movie { Name = "Wall-E", Id = 2 },
                new Movie { Name = "Dawn of the Dead", Id = 3 },
                new Movie { Name = "Frozen", Id = 4 }

            };

            return movies;
        }

    }
}