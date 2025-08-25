namespace MovieCatalogAPI.Application.Dtos.Reviews
{
    public class ReviewResponseDto
    {
        public int Id { get; set; }

        public string Comment { get; set; } = null!;
        public string Username { get; set; } = null!;
    }
}
