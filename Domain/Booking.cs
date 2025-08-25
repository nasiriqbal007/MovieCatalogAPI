namespace MovieCatalogAPI.Domain
{
    public class Booking
    {
        public int Id { get; set; }
        public int ShowTimeId { get; set; }
        public ShowTime ShowTime { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int NumberOfSeats { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookedAt { get; set; } = DateTime.UtcNow;

    }
}
