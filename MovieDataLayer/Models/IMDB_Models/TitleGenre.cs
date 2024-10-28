namespace MovieDataLayer.Models.IMDB_Models
{
    public class TitleGenre
    {
        public int Id { get; set; }
        public string TitleId { get; set; }
        public Title Title { get; set; } = null!; //required ref. navigation



    }
}