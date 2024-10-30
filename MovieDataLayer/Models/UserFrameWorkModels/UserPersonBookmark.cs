namespace MovieDataLayer;
public class UserPersonBookmark
{
    public int UserId { get; set; }
    public string? PersonId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Annotations { get; set; }
}