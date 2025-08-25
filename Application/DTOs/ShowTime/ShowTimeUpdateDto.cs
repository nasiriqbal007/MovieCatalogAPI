namespace MovieCatalogAPI.Application.DTOs.ShowTimes
{
    public class ShowTimeUpdateDto
    {
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public DateTime? StartDate { get; set; }

        public decimal? TicketPrice { get; set; }
        public int? CinemaId { get; set; }
        public int? MovieId { get; set; }
    }
}
