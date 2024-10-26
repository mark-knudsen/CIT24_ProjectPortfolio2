using MovieDataLayer.Extentions;

namespace MovieDataLayer
{
    public class Poster 
    {
        public string TitleId { get; set; }
        public string PosterUrl {  get; set; }
        public Title Title { get; set; } = null!; //required ref. navigation

    }
}