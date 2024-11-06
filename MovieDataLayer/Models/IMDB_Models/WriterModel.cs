namespace MovieDataLayer.Models.IMDB_Models
{
    public class WriterModel
    {
        public string PersonId { get; set; }
        public string TitleId { get; set; }
        public PersonModel Person { get; set; } = null!; //required ref. navigation
        public TitleModel Title { get; set; } = null!; //required ref. navigation

    }
}