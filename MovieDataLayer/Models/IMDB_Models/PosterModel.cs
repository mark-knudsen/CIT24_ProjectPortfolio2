using System.Text.Json.Serialization;

namespace MovieDataLayer.Models.IMDB_Models
{
    public class PosterModel
    {
        public string TitleId { get; set; }
        public string? PosterUrl { get; set; }
        [JsonIgnore] //Doesn't make sense to use JsonIgnore, we never serialize PosterModel object
        public TitleModel Title { get; set; } = null!; //required ref. navigation

    }
}