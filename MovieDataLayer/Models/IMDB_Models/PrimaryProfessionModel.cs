namespace MovieDataLayer.Models.IMDB_Models
{
    public class PrimaryProfessionModel
    {
        public int ProfessionId { get; set; }
        public string PersonId { get; set; }
        public PersonModel Person { get; set; } = null!; //required ref. navigation
        public ProfessionModel Profession { get; set; }
    }
}