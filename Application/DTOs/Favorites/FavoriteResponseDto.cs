namespace MovieCatalogAPI.Application.Dtos.Favorites
{
    public class FavoriteResponseDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; } = null!;
    }
}
