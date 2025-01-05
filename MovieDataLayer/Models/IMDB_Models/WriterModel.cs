namespace MovieDataLayer.Models.IMDB_Models
{
    public class WriterModel
    {
        public string PersonId { get; set; }
        public string TitleId { get; set; }
        public PersonModel Person { get; set; }
        public TitleModel Title { get; set; }

    }
}