namespace MovieWebApi
{
    public class TitleSimpleDTO
    {
        public string? Url { get; set; }
        public string Id { get; set; }
        public string TitleType { get; set; }
        public string PrimaryTitle { get; set; }
        public int StartYear { get; set; }
        public bool IsAdult { get; set; }
        public string PosterUrl { get; set; }
        public float? AverageRating { get; set; }
        public IList<string> GenresList { get; set; }
    }
}
        //public int Runtime { get; set; }


        //public string Plot { get; set; }

