using MovieDataLayer.Extentions;

namespace MovieDataLayer
{
    public class TitleGenre : Item<int>
    {
        public string TitleId { get; set; }
        public Title Title { get; set; } = null!; //required ref. navigation
    }
}