namespace MovieWebApi.DTO.User_DTO;

public class UserRatingDTO
{
    public int UserId { get; set; }
    public string TitleId { get; set; }
    public double Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; } = null!;
}
