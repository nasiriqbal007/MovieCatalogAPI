using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCatalogAPI.Application.DTOs.ShowTime;
using MovieCatalogAPI.Application.DTOs.ShowTimes;
using MovieCatalogAPI.Application.Services;

namespace MovieCatalogAPI.Controllers
{
    [Route("showtimes")]
    [ApiController]
    public class ShowTimesController : ControllerBase
    {
        private readonly ShowTimeService _showTimeService;

        public ShowTimesController(ShowTimeService showTimeService)
        {
            _showTimeService = showTimeService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateShowTime([FromBody] ShowTimeCreateDto dto)
        {
            var result = await _showTimeService.CreateShowTimeAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateShowTime(int id, [FromBody] ShowTimeUpdateDto dto)
        {
            var result = await _showTimeService.UpdateShowTimeAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteShowTime(int id)
        {
            await _showTimeService.DeleteShowTimeAsync(id);
            return NoContent();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var result = await _showTimeService.GetAllShowTimesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _showTimeService.GetShowTimeByIdAsync(id);
            return Ok(result);
        }
    }
}
