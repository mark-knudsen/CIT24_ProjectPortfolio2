using System.Text.Json.Serialization;

namespace MovieDataLayer.Models.IMDB_Models
{
    public class PrincipalCastModel
    {
        public string PersonId { get; set; }
        public int Ordering { get; set; }
        public string TitleId { get; set; }
        public string CharacterName { get; set; }
        public string Category { get; set; }
        public string Job { get; set; }
        [JsonIgnore]
        public TitleModel Title { get; set; } = null!; //required ref. navigation
        public PersonModel Person { get; set; } = null!; //required ref. navigation
    }
}