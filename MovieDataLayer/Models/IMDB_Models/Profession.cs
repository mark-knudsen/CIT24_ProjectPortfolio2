namespace MovieDataLayer.Models.IMDB_Models
{
    public class Profession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // public PrimaryProfession PrimaryProfession {get; set; }
        public ICollection<PrimaryProfession> PrimaryProfession { get; set; }

    }
}