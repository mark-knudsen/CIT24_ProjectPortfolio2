using System.Text.Json.Serialization;

namespace MovieDataLayer.Models.IMDB_Models
{
    public class PrincipalCast
    {
        public string PersonId { get; set; }
        public int Ordering { get; set; }
        public string TitleId { get; set; }
        public string CharacterName { get; set; }
        public string Category { get; set; }
        public string Job { get; set; }
        [JsonIgnore]
        public Title Title { get; set; } = null!; //required ref. navigation
        public Person Person { get; set; } = null!; //required ref. navigation
    }
}