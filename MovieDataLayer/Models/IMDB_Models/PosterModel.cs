using System.Text.Json.Serialization;

namespace MovieDataLayer.Models.IMDB_Models
{
    public class PosterModel
    {
        public string TitleId { get; set; }
        public string? PosterUrl { get; set; }
        public TitleModel Title { get; set; }

    }
}