using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;
using System.ComponentModel.DataAnnotations;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public List<Genre> Genres { get; set; }



        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Relase date")]
        public string RelaseDate { get; set; }
        [Required]
        public string DateAdded { get; set; }
        [Required]
        [Display(Name = "Number in stock")]
        [Range(1, 20)]
        public int? StockNumber { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public int? GenreId { get; set; }


        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Name = movie.Name;
            RelaseDate = movie.RelaseDate;
            DateAdded = movie.DateAdded;
            StockNumber = movie.StockNumber;
            GenreId = movie.GenreId;
        }
    }
}