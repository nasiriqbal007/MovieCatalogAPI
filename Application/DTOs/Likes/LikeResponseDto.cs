namespace MovieCatalogAPI.Application.Dtos.Likes
{
    public class LikeResponseDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string Username { get; set; } = null!;
    }
}
