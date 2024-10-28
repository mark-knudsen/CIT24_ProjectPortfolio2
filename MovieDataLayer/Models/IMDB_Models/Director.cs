namespace MovieDataLayer.Models.IMDB_Models
{
    public class Director
    {
        public string TitleId { get; set; }
        public string PersonId { get; set; }
        public Title Title { get; set; } = null!; //required ref. navigation
        public Person Person { get; set; } = null!; //required ref. navigation


    }
}