namespace MovieCatalogAPI.Application.DTOs.ShowTime
{

    public class ShowTimeCreateDto
    {
        public int MovieId { get; set; }
        public int CinemaId { get; set; }
        public DateTime StartDate { get; set; }
        public int NumberOfDays { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public decimal TicketPrice { get; set; }
    }


}
