using System.Text.Json.Serialization;

namespace MovieDataLayer;
public class UserSearchHistoryModel
{
    public int UserId { get; set; }
    public UserModel User { get; set; } = null!;
    public string SearchTerms { get; set; }
    public DateTime CreatedAt { get; set; }
}

