namespace MovieCatalogAPI.Application.DTOs.ShowTimes
{
    public class ShowTimeResponseDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public double Rating { get; set; }
        public int CinemaId { get; set; }
        public string CinemaName { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int AvailableSeats { get; set; }
        public decimal TicketPrice { get; set; }
    }
}
