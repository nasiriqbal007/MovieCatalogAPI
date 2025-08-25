using Microsoft.EntityFrameworkCore;
using MovieCatalogAPI.Application.Dtos.Likes;
using MovieCatalogAPI.Application.DTOs.Likes;
using MovieCatalogAPI.Application.Mapper;
using MovieCatalogAPI.Infrastructure;

namespace MovieCatalogAPI.Application.Services
{
    public class LikeService
    {
        private readonly AppDbContext _appDb;
        public LikeService(AppDbContext appDb) { _appDb = appDb; }

        public async Task<LikeResponseDto?> ToggleLike(LikeCreateDto dto, int userId)
        {
            var movie = await _appDb.Movies.FindAsync(dto.MovieId);
            if (movie == null) throw new Exception("Movie not found.");

            var existing = await _appDb.Likes
                .FirstOrDefaultAsync(l => l.UserId == userId && l.MovieId == dto.MovieId);

            if (existing != null)
            {

                _appDb.Likes.Remove(existing);
                await _appDb.SaveChangesAsync();
                return null;
            }
            else
            {

                var like = LikeMapper.ToEnity(dto, userId);
                _appDb.Likes.Add(like);
                await _appDb.SaveChangesAsync();

                await _appDb.Entry(like).Reference(l => l.User).LoadAsync();
                return LikeMapper.ToDto(like);
            }
        }

    }
}
