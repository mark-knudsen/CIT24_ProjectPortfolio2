using System.Text.Json.Serialization;

namespace MovieDataLayer.Models.IMDB_Models
{
    public class RatingModel
    {
        public string TitleId { get; set; }
        public int AverageRating { get; set; }
        [JsonIgnore]
        public TitleModel Title { get; set; }
        public int VoteCount { get; set; }

    }
}