using System.ComponentModel.DataAnnotations;

namespace MovieCatalogAPI.Application.Dtos.Movies
{



    public class MovieCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Genre { get; set; } = null!;

        [Required]
        [Range(1900, 2100)]
        public int ReleaseYear { get; set; }
        [Required]
        [Range(1, 10)]

        public double Rating { get; set; } = 0;
    }

}