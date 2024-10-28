namespace MovieDataLayer.Models.IMDB_Models
{
    public class MostRelevant
    {
        public string PersonId { get; set; }
        public string TitleId { get; set; }
        public Title Title { get; set; } = null!; //required ref. navigation
        public Person Person { get; set; } = null!; //required ref. navigation

    }
}
