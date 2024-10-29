namespace MovieDataLayer.Models.IMDB_Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public ICollection<Genre> Genres { get; set; }  // why does this have a list of genres?

    }
}