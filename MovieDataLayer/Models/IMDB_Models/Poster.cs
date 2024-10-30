using System.Text.Json.Serialization;

namespace MovieDataLayer.Models.IMDB_Models
{
    public class Poster
    {
        public string TitleId { get; set; }
        public string PosterUrl { get; set; }
        [JsonIgnore]
        public Title Title { get; set; } = null!; //required ref. navigation

    }
}