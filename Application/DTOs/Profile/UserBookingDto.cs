namespace MovieCatalogAPI.Application.DTOs.Profile
{
    public class UserBookingDto
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public string CinemaName { get; set; } = string.Empty;
        public DateTime ShowTime { get; set; }
        public int NumberOfSeats { get; set; }
        public decimal TicketPrice { get; set; }
    }

}
