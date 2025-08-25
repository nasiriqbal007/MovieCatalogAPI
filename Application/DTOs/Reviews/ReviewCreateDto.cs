using System.ComponentModel.DataAnnotations;

namespace MovieCatalogAPI.Application.Dtos.Reviews
{
    public class ReviewCreateDto
    {


        [Required]
        [StringLength(500)]
        public string Comment { get; set; } = null!;
    }
}
