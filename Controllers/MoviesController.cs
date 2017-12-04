using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext(); 
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

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

        /*
        public ActionResult Edit(int id)
        { 
            return Content("id = " + id);
        }
        */

        //routing so we can reach this page by simply doing /Movie.
        [Route("Movie")]
        public ActionResult Index(int? pageIndex, string sortBy)
        {
 
            var movies = _context.Movies.Include(c => c.Genre).ToList();


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
            return View(movies);
        }


        //creating an action for the movie form.
        public ActionResult New()
        {
            //going to the IdentityModel and adding the genre DbSet.
            var genres = _context.Genres.ToList();

            //creating a MovieFormViewModel so we can display the genres and the Movies.
            var viewModel = new MovieFormViewModel {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MovieFormViewModel
            {
                Movies = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        //action that saves the edits/adds an element in the database
        public ActionResult Save(MovieFormViewModel viewModel)
        {

            Movie movie = new Movie();
            movie = viewModel.Movies;
            //setting the time the movie was added to now.
            movie.DateAddedToDatabase = DateTime.Now;
            //movie = DateTime.Now;
         
            //checking if the entered movie is valid.
            if(!ModelState.IsValid)
            {
                var thisviewModel = new MovieFormViewModel
                {
                    Movies = new Movie(),
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", thisviewModel);
            }

            //movie.GenreId = 3;

            Console.WriteLine(movie.GenreId);

            //Console.WriteLine(movie.DateAddedToDatabase);
            //Console.WriteLine(movie.ReleaseDate);

            //checking to see if the movie has an id, if it has then it's a new element.
            if (movie.Id == 0)
            {
                //adding the movie to the local memory.
                _context.Movies.Add(movie);
            } else
            {
                //getting element from the database; using single here not singleOrDefault as if it's not found in db we 
                //want it to throw an exception.
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);

                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.Stock = movie.Stock;
            }

            try
            {
                _context.SaveChanges();
            } catch (Exception error)
            {
                Console.WriteLine(error);
            }

            return RedirectToAction("Index", "Movie");
        }
        
        //routing to Movie/Detail/ID.
        [Route("Movie/Details/{id}")]
        public ActionResult details(int id)
        {
            try
            {
                var movie = new Movie();
                movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);
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