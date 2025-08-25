using MovieCatalogAPI.Application.Dtos.Auth;
using MovieCatalogAPI.Application.DTOs.Profile;
using MovieCatalogAPI.Domain;

namespace MovieCatalogAPI.Application.Mapper
{
    public static class UserMapper
    {
        public static User ToEntity(UserRegisterDto dto)
        {
            return new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                Role = "User"
            };
        }

        public static UserResponseDto ToResponse(User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }


        private static string HashPassword(string password)
        {

            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }


        public static UserProfileDto ToProfile(User user)
        {
            return new UserProfileDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                Favorites = user.Favorites
                    .Select(FavoriteMapper.ToDto)
                    .ToList(),
                Bookings = user.Bookings.Select(b => new UserBookingDto
                {
                    Id = b.Id,
                    MovieTitle = b.ShowTime.Movie.Title,
                    CinemaName = b.ShowTime.Cinema.Name,
                    ShowTime = b.ShowTime.StartTime,
                    NumberOfSeats = b.NumberOfSeats,
                    TicketPrice = b.TotalPrice,


                }).ToList(),


            };
        }

        public static void UpdateFromDto(User user, UpdateProfileDto dto)
        {
            if (!string.IsNullOrEmpty(dto.Username))
                user.Username = dto.Username;

            if (!string.IsNullOrEmpty(dto.Email))
                user.Email = dto.Email;

            if (!string.IsNullOrEmpty(dto.Password))
                user.PasswordHash = HashPassword(dto.Password);
        }
    }
}
