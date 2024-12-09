using MovieDataLayer.Models.IMDB_Models;

namespace MovieDataLayer;
public class UserPersonBookmarkModel
{
    public int UserId { get; set; }
    public string PersonId { get; set; }
    public PersonModel Person { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? Annotation { get; set; }
}