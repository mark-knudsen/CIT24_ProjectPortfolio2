using System.Text.Json.Serialization;

namespace MovieDataLayer.Models.IMDB_Models
{
    public class TitleGenreModel
    {
        public int GenreId { get; set; }
        public string TitleId { get; set; }
        [JsonIgnore]
        public TitleModel Title { get; set; } = null!; //required ref. navigation
        public GenreModel Genre { get; set; }
    }
}