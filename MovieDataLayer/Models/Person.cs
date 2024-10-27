namespace MovieDataLayer
{
    public class Person : Item<string>
    {
        public string Name { get; set; }
        public int? BirthYear { get; set; }
        public int? DeathYear { get; set; }
        //public ICollection<Title> MostRelevantTitles { get; set; }
        public ICollection<Profession> PrimaryProfessions { get; set; }
        //public IEnumerable<Title> Titles { get; set; }
    }
}
