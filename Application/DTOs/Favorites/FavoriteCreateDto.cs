using System.ComponentModel.DataAnnotations;

namespace MovieCatalogAPI.Application.Dtos.Favorites
{
    public class FavoriteCreateDto
    {
        [Required]
        public int MovieId { get; set; }
    }
}
