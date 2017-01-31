using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Vidly.Models;
using System.Net;
using Vidly.DTOs;
using AutoMapper;

namespace Vidly.Controllers.API
{
    public class MoviesController : ApiController
    {

        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }


        //GET: /api/movies
        public IHttpActionResult GetMovies()
        {
            var moviesInDb = _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);

            return Ok(moviesInDb);
        }


        //GET: /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movieDto = new MovieDto();
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                return NotFound();

            movieDto = Mapper.Map<MovieDto>(movieInDb);

            return Ok(movieDto);
        }


        //POST: /api/movies
        [HttpPost]
        public IHttpActionResult NewMovie(MovieDto movieDto)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var movie = new Movie();
            movie = Mapper.Map<Movie>(movieDto);


            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }


        //PUT: /api/movies/1
        [HttpPut]
        public IHttpActionResult EditMovie(int id, MovieDto movieDto)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();


            Mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();
            movieDto.Id = movieInDb.Id;

            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }


        //DELETE: /api/movies/1
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            return Ok();
        }






    }


}