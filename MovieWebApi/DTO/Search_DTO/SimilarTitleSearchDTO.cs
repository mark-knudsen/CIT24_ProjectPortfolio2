namespace MovieWebApi.SearchDTO
{
    public class SimilarTitleSearchDTO
    {
        public string SimilarTitleId { get; set; }
        public string PrimaryTitle { get; set; }
        public string[] Genres { get; set; }
        public bool IsAdult { get; set; }
        public string TitleType { get; set; }
    }
}
