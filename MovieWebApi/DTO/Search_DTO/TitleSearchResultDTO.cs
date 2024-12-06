namespace MovieWebApi.SearchDTO
{
    public class TitleSearchResultDTO
    {
        public string? Url { get; set; }
        //public string Id { get; set; }
        public string TitleId { get; set; }
        public string PrimaryTitle { get; set; }
        public int StartYear { get; set; }
        public int? EndYear { get; set; }
        public string OriginalTitle { get; set; }
        public string PosterUrl { get; set; }
    }
}
