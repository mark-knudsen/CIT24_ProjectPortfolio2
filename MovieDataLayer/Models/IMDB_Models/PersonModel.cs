namespace MovieDataLayer.Models.IMDB_Models
{
    public class PersonModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? BirthYear { get; set; }
        public int? DeathYear { get; set; }
        public ICollection<MostRelevantModel> MostRelevantTitles { get; set; }
        public ICollection<PrimaryProfessionModel> PrimaryProfessions { get; set; }

    }
}
