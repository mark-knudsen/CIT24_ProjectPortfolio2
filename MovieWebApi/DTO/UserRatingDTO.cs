namespace MovieWebApi;

public class UserRatingDTO
{
    public string TitleId { get; set; }
    public double Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; } = null!;
}
