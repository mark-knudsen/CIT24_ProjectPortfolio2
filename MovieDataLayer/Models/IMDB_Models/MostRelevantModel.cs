namespace MovieDataLayer.Models.IMDB_Models
{
    public class MostRelevantModel
    {
        public string PersonId { get; set; }
        public string TitleId { get; set; }
        public TitleModel Title { get; set; } = null!; //required ref. navigation
        public PersonModel Person { get; set; } = null!; //required ref. navigation

    }
}
