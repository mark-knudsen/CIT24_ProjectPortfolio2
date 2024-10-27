namespace MovieDataLayer;
public class UserRating // : Item<int>
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public string TitleId { get; set; } 
    public Title Title { get; set; } = null!;
    public double Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
