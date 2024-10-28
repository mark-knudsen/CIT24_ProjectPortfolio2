namespace MovieDataLayer;
public class UserBookmark
{
    public int UserId { get; set; }
    //public string? TitleId { get; set; }
    //public string? PersonId { get; set; }
    public string? ItemId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Annotations { get; set; }
}