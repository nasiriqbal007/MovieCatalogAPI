using Microsoft.EntityFrameworkCore;
using MovieCatalogAPI.Application.Dtos.Movies;
using MovieCatalogAPI.Application.Mapper;

using MovieCatalogAPI.Infrastructure;

namespace MovieCatalogAPI.Application.Services
{
    public class MovieService
    {
        private readonly AppDbContext _appDb;

        public MovieService(AppDbContext appDb)
        {
            _appDb = appDb;
        }

        public async Task<MovieResponseDto> AddMovieAsync(MovieCreateDto dto)
        {
            var movie = MovieMapper.ToEntity(dto);
            _appDb.Movies.Add(movie);
            await _appDb.SaveChangesAsync();
            return MovieMapper.ToResponse(movie);
        }

        public async Task<MovieResponseDto?> UpdateMovieAsync(int id, MovieUpdateDto dto)
        {
            var movie = await _appDb.Movies
                .Include(m => m.Reviews)
                .Include(m => m.Likes)
                .ThenInclude(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null) return null;

            MovieMapper.UpdateEntity(movie, dto);
            await _appDb.SaveChangesAsync();
            return MovieMapper.ToResponse(movie);
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _appDb.Movies.FindAsync(id);
            if (movie == null) return false;

            _appDb.Movies.Remove(movie);
            await _appDb.SaveChangesAsync();
            return true;
        }

        public async Task<List<MovieListDto>> GetAllMoviesAsync()
        {
            var movies = await _appDb.Movies

    .Include(m => m.ShowTimes)

    .ToListAsync();


            return movies.Select(MovieMapper.ToListDto).ToList();
        }

        public async Task<MovieResponseDto?> GetMovieByIdAsync(int id)
        {
            var movie = await _appDb.Movies
                .Include(m => m.Reviews).ThenInclude(r => r.User)
                .Include(m => m.Likes).ThenInclude(l => l.User).Include(st => st.ShowTimes).ThenInclude(c => c.Cinema)
                .FirstOrDefaultAsync(m => m.Id == id);


            if (movie == null) return null;

            return MovieMapper.ToResponse(movie);
        }

        public async Task<List<MovieListDto>> SearchMoviesAsync(
    double? rating,
    string? genre = null,
    int? year = null,
    string? title = null)
        {
            var query = _appDb.Movies
                .Include(m => m.ShowTimes)
                .AsQueryable();

            if (rating.HasValue)
                query = query.Where(m => m.Rating >= rating.Value);

            if (!string.IsNullOrEmpty(genre))
                query = query.Where(m => m.Genre.ToLower() == genre.ToLower());

            if (year.HasValue)
                query = query.Where(m => m.ReleaseYear == year.Value);

            if (!string.IsNullOrEmpty(title))
                query = query.Where(m => m.Title.ToLower().Contains(title.ToLower()));

            var movies = await query.ToListAsync();
            return movies.Select(MovieMapper.ToListDto).ToList();
        }
    }
}
