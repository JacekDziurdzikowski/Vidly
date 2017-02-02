using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private MyContext _context;
        private MovieList movies;
            
        public MoviesController()
        {
            _context = new MyContext();
            movies = new MovieList();

    }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("MoviesList");

            return View("ReadOnlyMoviesList");
        }

        [Route("Movies/released/{year}/{month:Regex(\\d{2})}")]
        public ActionResult ByReleaseDate (int year, int month)
        {
            return Content(year + "/" + month);
        }

        public ActionResult Details(int Id)
        {
            movies.movieList = _context.Movies.Include(m => m.Genre).ToList();
            foreach (var i in movies.movieList)
            {
                if(i.Id == Id)
                {
                    return View(i);
                }
           
            }
            return Content("nie znaleziono filmu");
        
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult MovieForm(int id = 0)
        { 
            if (id != 0)
            {

                var movie = _context.Movies.Single(m => m.Id == id);

                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                viewModel.Id = id;

                return View(viewModel);
                
            }

            else
            {
                var viewModel = new MovieFormViewModel();
                viewModel.Genres = _context.Genres.ToList();

               return View(viewModel);
            }


            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };


                return View("MovieForm", viewModel);

            }


                if (movie.Id == 0)
                _context.Movies.Add(movie);

            

            else
            {
                var movieInDB = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDB.Name = movie.Name;
                movieInDB.GenreId = movie.GenreId;
                movieInDB.RelaseDate = movie.RelaseDate;
                movieInDB.StockNumber = movie.StockNumber;
                movieInDB.DateAdded = movie.DateAdded;
            }

            _context.SaveChanges();


            return RedirectToAction("Index");
        }
    }

}