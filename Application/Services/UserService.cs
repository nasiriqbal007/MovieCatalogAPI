using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieCatalogAPI.Application.Dtos.Auth;
using MovieCatalogAPI.Application.DTOs.Auth;
using MovieCatalogAPI.Application.DTOs.Profile;
using MovieCatalogAPI.Application.Mapper;
using MovieCatalogAPI.Domain;
using MovieCatalogAPI.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieCatalogAPI.Application.Services
{
    public class UserService
    {
        private readonly AppDbContext _appDb;
        private readonly IConfiguration _configuration;

        public UserService(AppDbContext appDb, IConfiguration configuration)
        {
            _appDb = appDb;
            _configuration = configuration;
        }

        public async Task<UserResponseDto> RegisterAsync(UserRegisterDto registerDto)
        {
            var existingUser = await _appDb.Users
                .FirstOrDefaultAsync(u => u.Email == registerDto.Email);

            if (existingUser != null)
                throw new Exception("Email already registered");

            var user = UserMapper.ToEntity(registerDto);
            _appDb.Users.Add(user);
            await _appDb.SaveChangesAsync();

            return UserMapper.ToResponse(user);
        }

        public async Task<UserLoginResponseDto?> LoginAsync(UserLoginDto loginDto)
        {
            var user = await _appDb.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null) return null;

            bool validPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!validPassword) return null;

            var token = GenerateJwtToken(user);

            return new UserLoginResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                Token = token
            };
        }


        public async Task<UserProfileDto> GetProfileAsync(int userId)
        {
            var user = await _appDb.Users
                .Include(u => u.Favorites)
                    .ThenInclude(f => f.Movie)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) throw new Exception("User not found.");

            return UserMapper.ToProfile(user);
        }

        public async Task<UserProfileDto> UpdateProfileAsync(int userId, UpdateProfileDto dto)
        {
            var user = await _appDb.Users.FindAsync(userId);
            if (user == null) throw new Exception("User not found.");

            UserMapper.UpdateFromDto(user, dto);
            await _appDb.SaveChangesAsync();

            await _appDb.Entry(user)
                        .Collection(u => u.Favorites)
                        .Query()
                        .Include(f => f.Movie)
                        .LoadAsync();

            return UserMapper.ToProfile(user);
        }
        private string GenerateJwtToken(User user)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"];
            if (string.IsNullOrEmpty(secretKey))
                throw new Exception("JWT SecretKey is missing in configuration.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var expiresInMinutes = _configuration.GetValue<int>("JwtSettings:ExpiresInMinutes", 30);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

}
