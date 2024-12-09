using MovieDataLayer.Models.IMDB_Models;

namespace MovieDataLayer;
public class UserTitleBookmarkModel
{
    public int UserId { get; set; }
    public string TitleId { get; set; }
    public TitleModel Title { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? Annotation { get; set; }
}