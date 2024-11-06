using System.Text.Json.Serialization;

namespace MovieDataLayer.Models.IMDB_Models
{
    public class TitleModel
    {
        public string Id { get; set; }
        public string TitleType { get; set; }
        public string? PrimaryTitle { get; set; }
        public string? OriginalTitle { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public int? Runtime { get; set; }
        public bool IsAdult { get; set; }
        public PosterModel Poster { get; set; }
        public PlotModel Plot { get; set; }
        //[JsonIgnore]
        public ICollection<TitleGenreModel> GenresList { get; set; }
        public RatingModel Rating { get; set; }
        // public ICollection<LocalizedTitle> LocalizedTitlesList { get; } = new List<LocalizedTitle>();
        //[JsonIgnore]
        public ICollection<PrincipalCastModel> PrincipalCastList { get; set; }
       // [JsonIgnore]
        public ICollection<WriterModel> WritersList { get; set; }
        //[JsonIgnore]
        public ICollection<DirectorModel> DirectorsList { get; set; }

    }
}