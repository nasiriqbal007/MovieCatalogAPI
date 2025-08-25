using System.ComponentModel.DataAnnotations;

namespace MovieCatalogAPI.Application.DTOs.Likes
{




    public class LikeCreateDto
    {
        [Required]
        public int MovieId { get; set; }
    }


}
