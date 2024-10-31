namespace MovieWebApi.DTO
{
    public class UserBookmarkDTO
    {
        public string? TitleId { get; set; }
        public string? PersonId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Annotation { get; set; }
    }
}
