using MovieDataLayer.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer;
public class User : Item<int>
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string Password { get; set; }
    //public virtual ICollection<UserRating> UserRatings { get; set; }
    public ICollection<UserSearchHistory> UserSearchHistory { get; } = new List<UserSearchHistory>();
    //public virtual ICollection<UserBookmark> UserBookmark { get; set; }
}

