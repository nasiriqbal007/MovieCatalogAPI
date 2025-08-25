using MovieCatalogAPI.Application.Dtos.Favorites;

namespace MovieCatalogAPI.Application.DTOs.Profile
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public List<FavoriteResponseDto> Favorites { get; set; } = new();

        public List<UserBookingDto> Bookings { get; set; } = new();

    }

}
