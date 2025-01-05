namespace MovieDataLayer.Models.IMDB_Models
{
    public class LocalizedTitleModel
    {
        // We do not use Ordering...
        public int Id { get; set; }
        public string TitleId { get; set; }
        public string LocTitle { get; set; }
        public string Language { get; set; }
        public string Region { get; set; }
        public string Type { get; set; }
        public string Attribute { get; set; }
        public TitleModel Title { get; set; }
        public LocalizedDetailModel LocalizedDetail { get; set; } = null!;

    }
}