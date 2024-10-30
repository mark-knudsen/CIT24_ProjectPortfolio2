namespace MovieDataLayer.Models.IMDB_Models
{
    public class PrimaryProfession
    {
        public int Id { get; set; }
        public string PersonId { get; set; }
        public Person Person { get; set; } = null!; //required ref. navigation
        //public ICollection<Profession> Profession { get; set; }
        public Profession Profession { get; set; }

    }
}