using System.Text.Json.Serialization;

namespace MovieDataLayer;
public class UserSearchHistoryModel
{
    public int UserId { get; set; }
    [JsonIgnore] // Possibly should be removed, is it neccesary? This will prevent the User object from being serialized
    public UserModel User { get; set; } = null!;
    public string SearchTerms { get; set; }
    public DateTime CreatedAt { get; set; }
}

