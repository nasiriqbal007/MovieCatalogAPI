namespace MovieCatalogAPI.Domain
{
    public class Like
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public Movie Movie { get; set; } = null!;
        public User User { get; set; } = null!;
    }

}
