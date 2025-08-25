namespace MovieCatalogAPI.Domain
{
    public class Cinema
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int TotalSeats { get; set; }
        public ICollection<ShowTime> ShowTimes { get; set; } = new HashSet<ShowTime>();
    }
}
