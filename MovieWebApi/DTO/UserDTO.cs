using MovieDataLayer;

namespace MovieWebApi;
public class UserDTO
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    //public string Password { get; set; } // you should never be able to retrieve the password
    //public virtual ICollection<UserRating> UserRatings { get; set; }
    public ICollection<UserSearchHistory> UserSearchHistory { get; } = new List<UserSearchHistory>();
}

