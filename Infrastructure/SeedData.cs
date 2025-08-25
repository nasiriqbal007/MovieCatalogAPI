using MovieCatalogAPI.Domain;

namespace MovieCatalogAPI.Infrastructure
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {

            if (!context.Users.Any(u => u.Role == "Admin"))
            {
                var admin = new User
                {
                    Username = "Admin",
                    Email = "admin@test.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123"),
                    Role = "Admin"
                };

                context.Users.Add(admin);
                context.SaveChanges();
            }
        }
    }
}
