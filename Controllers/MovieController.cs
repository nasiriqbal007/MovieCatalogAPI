using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCatalogAPI.Application.Dtos.Favorites;
using MovieCatalogAPI.Application.Dtos.Movies;
using MovieCatalogAPI.Application.DTOs.Likes;
using MovieCatalogAPI.Application.Services;
using System.Security.Claims;

namespace MovieCatalogAPI.Controllers
{
    [Route("movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;
        private readonly LikeService _likeService;
        private readonly FavoriteService _favoriteService;

        public MovieController(MovieService movieService, LikeService likeService, FavoriteService favoriteService)
        {
            _movieService = movieService;
            _likeService = likeService;
            _favoriteService = favoriteService;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMovie([FromBody] MovieCreateDto dto)
        {
            var movie = await _movieService.AddMovieAsync(dto);
            return Ok(movie);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieUpdateDto dto)
        {
            var movie = await _movieService.UpdateMovieAsync(id, dto);
            if (movie == null) return NotFound();
            return Ok(movie);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var deleted = await _movieService.DeleteMovieAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }


        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> SearchMovies([FromQuery] double? rating, [FromQuery] string? genre, [FromQuery] int? year, [FromQuery] string? title)
        {
            var movies = await _movieService.SearchMoviesAsync(rating, genre, year, title);
            return Ok(movies);
        }

        [HttpPost("{id}/like")]
        [Authorize]
        public async Task<IActionResult> ToggleLike(int id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out var userId))
                return Unauthorized("User not authenticated.");

            var result = await _likeService.ToggleLike(new LikeCreateDto { MovieId = id }, userId);
            if (result == null)
                return Ok(new { message = "Like removed" });

            return Ok(result);
        }

        [HttpPost("{id}/favorite")]
        [Authorize]
        public async Task<IActionResult> ToggleFavorite(int id)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out var userId))
                return Unauthorized("User not authenticated.");

            var result = await _favoriteService.ToggleFavorite(new FavoriteCreateDto { MovieId = id }, userId);
            if (result == null)
                return Ok(new { message = "Removed from favorites" });

            return Ok(result);
        }
    }
}
