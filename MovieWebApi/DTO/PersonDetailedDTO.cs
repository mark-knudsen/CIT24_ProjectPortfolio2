using MovieDataLayer.Models.IMDB_Models;

namespace MovieDataLayer;
public class PersonDetailedDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int? BirthYear { get; set; }
    public int? DeathYear { get; set; }
    public IList<string> MostRelevantTitles { get; set; }
    public IList<string> PrimaryProfessions { get; set; }

}

