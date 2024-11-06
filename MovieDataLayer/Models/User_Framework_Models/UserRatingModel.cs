using MovieDataLayer.Models.IMDB_Models;

namespace MovieDataLayer;
public class UserRatingModel
{
    public int UserId { get; set; }
    public UserModel User { get; set; } = null!;
    public string TitleId { get; set; }
    public TitleModel Title { get; set; } = null!;
    public double Rating { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; }  = null!; // if the value is null it still shows a default time, would want the value to actually be null
}
