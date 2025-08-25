using MovieCatalogAPI.Application.Dtos.Movies;
using MovieCatalogAPI.Domain;

namespace MovieCatalogAPI.Application.Mapper
{
    public static class MovieMapper
    {
        public static Movie ToEntity(MovieCreateDto dto)
        {
            return new Movie
            {
                Title = dto.Title,
                Description = dto.Description,
                Genre = dto.Genre,
                ReleaseYear = dto.ReleaseYear,
                Rating = dto.Rating
            };
        }

        public static void UpdateEntity(Movie movie, MovieUpdateDto dto)
        {
            if (!string.IsNullOrEmpty(dto.Title))
                movie.Title = dto.Title;

            if (!string.IsNullOrEmpty(dto.Description))
                movie.Description = dto.Description;

            if (!string.IsNullOrEmpty(dto.Genre))
                movie.Genre = dto.Genre;

            if (dto.ReleaseYear.HasValue)
                movie.ReleaseYear = dto.ReleaseYear.Value;

            if (dto.Rating.HasValue)
                movie.Rating = dto.Rating.Value;
        }


        public static MovieListDto ToListDto(Movie movie)
        {
            return new MovieListDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
                Rating = movie.Rating,
                ShowTimes = movie.ShowTimes.Any()
                    ? movie.ShowTimes.Select(st => new ShowTimeBriefDto
                    {
                        StartTime = st.StartTime,
                        EndTime = st.EndTime,
                        TicketPrice = st.TicketPrice
                    }).ToList()
                    : null
            };
        }

        public static MovieResponseDto ToResponse(Movie movie)
        {
            return new MovieResponseDto
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre,
                ReleaseYear = movie.ReleaseYear,
                Rating = movie.Rating,
                Reviews = movie.Reviews.Select(r => ReviewMapper.ToResponse(r)).ToList(),
                Likes = movie.Likes.Select(l => LikeMapper.ToDto(l)).ToList(),
                ShowTimes = movie.ShowTimes.Any()
                    ? movie.ShowTimes.Select(st => st.ToResponseDto()).ToList()
                    : null
            };
        }
    }
}
