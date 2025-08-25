using Microsoft.EntityFrameworkCore;
using MovieCatalogAPI.Application.Dtos.Reviews;
using MovieCatalogAPI.Application.Mapper;
using MovieCatalogAPI.Infrastructure;

namespace MovieCatalogAPI.Application.Services
{
    public class ReviewService
    {
        private readonly AppDbContext _appDb;

        public ReviewService(AppDbContext appDb)
        {
            _appDb = appDb;
        }

        public async Task<ReviewResponseDto?> AddReviewAsync(ReviewCreateDto dto, int userId, int movieId)
        {
            var user = await _appDb.Users.FindAsync(userId);
            if (user == null) throw new Exception("User not found.");

            var movie = await _appDb.Movies.FindAsync(movieId);
            if (movie == null) throw new Exception("Movie not found.");

            var existing = await _appDb.Reviews
                .FirstOrDefaultAsync(r => r.UserId == userId && r.MovieId == movieId);
            if (existing != null) throw new Exception("You already reviewed this movie.");

            var review = ReviewMapper.ToEntity(dto, userId, movieId);
            _appDb.Reviews.Add(review);
            await _appDb.SaveChangesAsync();

            await _appDb.Entry(review).Reference(r => r.User).LoadAsync();

            return ReviewMapper.ToResponse(review);
        }

        public async Task<bool> DeleteReviewAsync(int reviewId, int userId)
        {
            var review = await _appDb.Reviews.FindAsync(reviewId);
            if (review == null || review.UserId != userId) return false;

            _appDb.Reviews.Remove(review);
            await _appDb.SaveChangesAsync();

            return true;
        }

        public async Task<List<ReviewResponseDto>> GetReviewsByMovieAsync(int movieId)
        {
            var reviews = await _appDb.Reviews
                .Include(r => r.User)
                .Where(r => r.MovieId == movieId)
                .ToListAsync();

            return reviews.Select(ReviewMapper.ToResponse).ToList();
        }
    }
}
