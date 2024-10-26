using MovieDataLayer.Extentions;

namespace MovieDataLayer
{
    public class Poster : Item<string>
    {
        public string PosterUrl {  get; set; }
        public Title Title { get; set; } = null!; //required ref. navigation

    }
}