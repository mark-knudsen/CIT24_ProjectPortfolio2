namespace MovieDataLayer.Models.IMDB_Models
{
    public class LocalizedDetail
    {
        // We do not use Ordering...
        public int Id { get; set; }
        public string LocTitle { get; set; }
        public string Language { get; set; }
        public string Region { get; set; }
        public string Type { get; set; }
        public string Attribute { get; set; }
        public Title Title { get; set; } = null!; //required ref. navigation
        public string TitleId { get; set; } //FK
        public LocalizedTitle LocalizedTitle { get; set; } = null!;
    }
}