namespace MovieDataLayer;
public class Writer
{
    public string PersonId { get; set; }
    public string TitleId { get; set; }
    public Person Person { get; set; } = null!; //required ref. navigation
    public Title Title { get; set; } = null!; //required ref. navigation

}
