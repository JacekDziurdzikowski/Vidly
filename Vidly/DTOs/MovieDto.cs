using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string RelaseDate { get; set; }

        [Required]
        public string DateAdded { get; set; }

        [Required]
        [Range(1, 20)]
        public int StockNumber { get; set; }

        [Required]
        public int GenreId { get; set; }
    }
}