using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDataLayer;
public class UserPersonBookmarkModel
{
    public int UserId { get; set; }
    public string PersonId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? Annotation { get; set; }
}