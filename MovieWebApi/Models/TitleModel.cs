using MovieDataLayer.Extentions;

namespace MovieDataLayer
{
    public class TitleModel
    {
        public string Id { get; set; }
        public string TitleType { get; set; }
        public string PrimaryTitle { get; set; }
        public string OriginalTitle { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public int Runtime { get; set; }
        public bool IsAdult { get; set; }
        public string PosterUrl { get; set; }
        public string Plot { get; set; }
        public float AverageRating { get; set; }
        public int VoteCount { get; set; }
        public ICollection<Genre> GenresList { get; set; }
        public ICollection<LocalizedTitle> LocalizedTitlesList { get; set; }
        public ICollection<PrincipalCast> PrincipalCastList { get; set; }
        public ICollection<string> WritersList { get; set; }
        public ICollection<string> DirectorsList { get; set; }
        public ICollection<Person> Persons { get; set; }


    }
}