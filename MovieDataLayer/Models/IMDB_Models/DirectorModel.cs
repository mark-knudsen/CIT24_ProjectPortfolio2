namespace MovieDataLayer.Models.IMDB_Models
{
    public class DirectorModel
    {
        public string TitleId { get; set; }
        public string PersonId { get; set; }
        public TitleModel Title { get; set; } = null!; //required ref. navigation
        public PersonModel Person { get; set; } = null!; //required ref. navigation

    }
}