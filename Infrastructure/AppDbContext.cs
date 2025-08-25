using Microsoft.EntityFrameworkCore;
using MovieCatalogAPI.Domain;

namespace MovieCatalogAPI.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cinema>().HasData(
                new Cinema { Id = 1, Name = "Cinema 1", TotalSeats = 100 },
                new Cinema { Id = 2, Name = "Cinema 2", TotalSeats = 120 },
                new Cinema { Id = 3, Name = "Cinema 3", TotalSeats = 80 },
                new Cinema { Id = 4, Name = "Cinema 4", TotalSeats = 150 },
                new Cinema { Id = 5, Name = "Cinema 5", TotalSeats = 5 }
            );
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Like> Likes { get; set; } = null!;
        public DbSet<Favorite> Favorites { get; set; } = null!;
        public DbSet<Cinema> Cinemas { get; set; } = null!;
        public DbSet<ShowTime> ShowTimes { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
    }
}
