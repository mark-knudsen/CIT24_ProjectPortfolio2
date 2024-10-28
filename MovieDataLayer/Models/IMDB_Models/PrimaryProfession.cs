namespace MovieDataLayer.Models.IMDB_Models
{
    public class PrimaryProfession
    {
        public string Id { get; set; }
        public string PersonId { get; set; }
        public Person Person { get; set; } = null!; //required ref. navigation
        public ICollection<Profession> Profession { get; set; }

    }
}