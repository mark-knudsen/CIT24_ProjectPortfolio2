namespace MovieDataLayer.Models.IMDB_Models
{
    public class TitleSearchResultTempTable
    {
        public string TitleId { get; set; }
        public string PrimaryTitle { get; set; }
        public int TotalElements { get; set; }
        public int StartYear { get; set; }
        public int? EndYear { get; set; }
        public string OriginalTitle { get; set; }
        public string? PosterUrl { get; set; }
    }
}
