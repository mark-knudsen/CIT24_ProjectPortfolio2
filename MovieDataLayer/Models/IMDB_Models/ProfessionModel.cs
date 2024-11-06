namespace MovieDataLayer.Models.IMDB_Models
{
    public class ProfessionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PrimaryProfessionModel> PrimaryProfession { get; set; }
    }
}