using MovieCatalogAPI.Application.Dtos.Reviews;
using MovieCatalogAPI.Domain;

namespace MovieCatalogAPI.Application.Mapper
{
    public static class ReviewMapper
    {
        public static Review ToEntity(ReviewCreateDto dto, int userId, int movieId)
        {
            return new Review
            {

                Comment = dto.Comment,
                UserId = userId,
                MovieId = movieId
            };
        }

        public static ReviewResponseDto ToResponse(Review review)
        {
            return new ReviewResponseDto
            {
                Id = review.Id,

                Comment = review.Comment,
                Username = review.User.Username
            };
        }
    }
}
