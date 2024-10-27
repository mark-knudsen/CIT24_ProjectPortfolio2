namespace MovieDataLayer;
public class UserBookmark : Item<int>
{
    public int TitleId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Annotations { get; set; }
}