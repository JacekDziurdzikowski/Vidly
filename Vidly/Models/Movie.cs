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

        [Required]
        public string Name { get; set; }

        
        public Genre Genre { get; set; }

        [Required]
        [Display(Name = "Relase date")]
        public string RelaseDate { get; set; }

        [Required]
        public string DateAdded { get; set; }

        [Required]
        [Display(Name = "Number in stock")]
        [Range(1,20)]
        public int StockNumber { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
    }
}