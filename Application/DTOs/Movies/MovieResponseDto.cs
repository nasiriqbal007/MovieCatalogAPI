using MovieCatalogAPI.Application.Dtos.Likes;
using MovieCatalogAPI.Application.Dtos.Reviews;
using MovieCatalogAPI.Application.DTOs.ShowTimes;

namespace MovieCatalogAPI.Application.Dtos.Movies
{
    public class MovieResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public double Rating { get; set; }
        public List<ReviewResponseDto> Reviews { get; set; } = [];
        public List<LikeResponseDto> Likes { get; set; } = [];
        public List<ShowTimeResponseDto>? ShowTimes { get; set; }

    }

    public class MovieListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public double Rating { get; set; }
        public List<ShowTimeBriefDto>? ShowTimes { get; set; }
    }

    public class ShowTimeBriefDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TicketPrice { get; set; }
    }

}
