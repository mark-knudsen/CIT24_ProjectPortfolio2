using MovieDataLayer.Extentions;

namespace MovieDataLayer
{
    public class EpisodeFromSeries
    {
        public string TitleId { get; set; }
        public string SeriesTitleId { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public Title Title { get; set; } = null!; //required ref. navigation


    }
}