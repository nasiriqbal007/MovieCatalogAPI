using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCatalogAPI.Application.Dtos.Reviews;
using MovieCatalogAPI.Application.Services;
using System.Security.Claims;

namespace MovieCatalogAPI.Controllers
{
    [Route("review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewService _reviewService;
        public ReviewController(ReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview([FromBody] ReviewCreateDto dto, [FromQuery] int movieId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User not authenticated.");

            var userId = int.Parse(userIdClaim.Value);


            var review = await _reviewService.AddReviewAsync(dto, userId, movieId);
            if (review == null)
                return NotFound("Movie not found or user not found.");

            return Ok(review);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User not authenticated.");

            var userId = int.Parse(userIdClaim.Value);
            var deleted = await _reviewService.DeleteReviewAsync(id, userId);

            if (!deleted)
                return NotFound("Review not found or you are not allowed to delete it.");

            return NoContent();
        }

    }
}
