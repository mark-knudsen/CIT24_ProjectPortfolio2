using System.Text.Json.Serialization;

namespace MovieDataLayer;
public class UserSearchHistory : Item<int>
{
    [JsonIgnore]
    public User User { get; set; } = null!;
    public string SearchTerms { get; set; }
    public DateTime CreatedAt { get; set; }
}

