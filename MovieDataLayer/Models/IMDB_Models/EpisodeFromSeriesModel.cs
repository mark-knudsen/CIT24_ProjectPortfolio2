namespace MovieDataLayer.Models.IMDB_Models
{
    public class EpisodeFromSeriesModel
    {
        public string TitleId { get; set; }
        public string SeriesTitleId { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public TitleModel Title { get; set; } = null!; //required ref. navigation

    }
}