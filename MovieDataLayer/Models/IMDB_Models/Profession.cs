namespace MovieDataLayer.Models.IMDB_Models
{
    public class Profession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        PrimaryProfession PrimaryProfession { get; set; }

    }
}