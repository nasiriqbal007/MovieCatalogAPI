namespace MovieCatalogAPI.Domain
{

    public class Movie
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Genre { get; set; }
        public required int ReleaseYear { get; set; }
        public required double Rating { get; set; }
        public ICollection<Review> Reviews { get; set; } = [];
        public ICollection<ShowTime> ShowTimes { get; set; } = [];
        public ICollection<Like> Likes { get; set; } = new HashSet<Like>();
    }

}
