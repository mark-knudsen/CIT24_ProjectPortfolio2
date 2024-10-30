namespace MovieDataLayer.Models.IMDB_Models
{
    public class PrimaryProfession
    {
        public int ProfessionId { get; set; }
        public string PersonId { get; set; }
        public Person Person { get; set; } = null!; //required ref. navigation
        public Profession Profession { get; set; }
    }
}