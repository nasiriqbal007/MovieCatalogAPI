namespace MovieCatalogAPI.Domain
{

    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string Role { get; set; }
        public ICollection<Favorite> Favorites { get; set; } = [];
        public ICollection<Booking> Bookings { get; set; } = [];

    }

}
