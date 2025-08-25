namespace MovieCatalogAPI.Application.DTOs
{
    public class BookingRequestDto
    {
        public int ShowTimeId { get; set; }

        public int NumberOfSeats { get; set; }
    }

    public class BookingResponseDto
    {
        public int Id { get; set; }
        public int ShowTimeId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public DateTime ShowTime { get; set; }
        public int UserId { get; set; }
        public int NumberOfSeats { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookedAt { get; set; }
    }
}
