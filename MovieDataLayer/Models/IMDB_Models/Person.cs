namespace MovieDataLayer.Models.IMDB_Models
{
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? BirthYear { get; set; }
        public int? DeathYear { get; set; }
        public ICollection<MostRelevant> MostRelevantTitles { get; set; }
        //public ICollection<Profession> Professions { get; set; }
        public ICollection<PrimaryProfession> PrimaryProfessions { get; set; }
        public ICollection<Writer> Writers { get; set; }


    }
}
