using MovieCatalogAPI.Application.Dtos.Favorites;
using MovieCatalogAPI.Domain;

namespace MovieCatalogAPI.Application.Mapper
{
    public static class FavoriteMapper
    {
        public static Favorite ToEntity(FavoriteCreateDto dto, int userId)
        {
            return new Favorite
            {
                MovieId = dto.MovieId,
                UserId = userId
            };
        }

        public static FavoriteResponseDto ToDto(Favorite favorite)
        {
            return new FavoriteResponseDto
            {
                Id = favorite.Id,
                MovieId = favorite.MovieId,
                MovieTitle = favorite.Movie.Title
            };
        }
    }
}
