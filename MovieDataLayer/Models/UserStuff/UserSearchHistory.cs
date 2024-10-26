using MovieDataLayer.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MovieDataLayer;
public class UserSearchHistory : Item<int>
{
    [JsonIgnore]
    public User User { get; set; } = null!;
    public string SearchTerms { get; set; }
    public DateTime CreatedAt { get; set; }
}

