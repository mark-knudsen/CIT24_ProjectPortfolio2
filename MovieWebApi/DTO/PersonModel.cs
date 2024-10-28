

using MovieDataLayer.Models.IMDB_Models;

namespace MovieDataLayer
{
    public class PersonModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? BirthYear { get; set; }
        public int? DeathYear { get; set; }
        public IList<Title> MostRelevantTitles { get; set; }
        public IList<Profession> PrimaryProfessions { get; set; }
        public IList<Title> Titles { get; set; }



    }
}
