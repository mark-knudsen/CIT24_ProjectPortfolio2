namespace MovieDataLayer
{
    public class SimilarTitleSearchTempTable
    {
        public string TitleId { get; set; }
        public string PrimaryTitle { get; set; }
        public string[] Genres { get; set; } //Using array instead of "List", because faster. Content isn't dynamic, is not navigation property. 
        public bool IsAdult { get; set; }
        public string TitleType { get; set; }
        public string? PosterUrl { get; set; }
    }
}
