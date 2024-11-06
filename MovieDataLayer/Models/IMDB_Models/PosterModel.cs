using System.Text.Json.Serialization;

namespace MovieDataLayer.Models.IMDB_Models
{
    public class PosterModel
    {
        public string TitleId { get; set; }
        public string PosterUrl { get; set; }
        [JsonIgnore]
        public TitleModel Title { get; set; } = null!; //required ref. navigation

    }
}