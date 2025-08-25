using Microsoft.EntityFrameworkCore;
using MovieCatalogAPI.Application.Dtos.Favorites;
using MovieCatalogAPI.Application.Mapper;
using MovieCatalogAPI.Infrastructure;

namespace MovieCatalogAPI.Application.Services
{
    public class FavoriteService
    {
        private readonly AppDbContext _appDb;
        public FavoriteService(AppDbContext appDb) { _appDb = appDb; }

        public async Task<FavoriteResponseDto?> ToggleFavorite(FavoriteCreateDto dto, int userId)
        {
            var movie = await _appDb.Movies.FindAsync(dto.MovieId);
            if (movie == null) throw new Exception("Movie not found.");
            var existing = await _appDb.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.MovieId == dto.MovieId);
            if (existing != null)
            {
                _appDb.Favorites.Remove(existing);
                await _appDb.SaveChangesAsync();
                return null;
            }
            else
            {
                var favorite = FavoriteMapper.ToEntity(dto, userId);
                await _appDb.Favorites.AddAsync(favorite);
                await _appDb.SaveChangesAsync();
                await _appDb.Entry(favorite).Reference(f => f.Movie).LoadAsync();
                return FavoriteMapper.ToDto(favorite);
            }
        }
    }
}
