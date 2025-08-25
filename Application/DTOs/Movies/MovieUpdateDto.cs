using System.ComponentModel.DataAnnotations;

namespace MovieCatalogAPI.Application.Dtos.Movies
{
    public class MovieUpdateDto
    {
        [StringLength(100)]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [StringLength(50)]
        public string? Genre { get; set; }

        [Range(1900, 2100)]
        public int? ReleaseYear { get; set; }

        [Range(1, 10)]
        public double? Rating { get; set; }
    }

}
