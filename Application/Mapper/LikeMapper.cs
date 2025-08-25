using MovieCatalogAPI.Application.Dtos.Likes;
using MovieCatalogAPI.Application.DTOs.Likes;
using MovieCatalogAPI.Domain;

namespace MovieCatalogAPI.Application.Mapper
{
    public static class LikeMapper
    {
        public static Like ToEnity(LikeCreateDto dto, int userId)
        {
            return new Like
            {
                MovieId = dto.MovieId,

                UserId = userId,


            };

        }
        public static LikeResponseDto ToDto(Like like)
        {
            return new LikeResponseDto
            {
                Id = like.Id,
                MovieId = like.MovieId,
                Username = like.User.Username,
            };

        }
    }
}
