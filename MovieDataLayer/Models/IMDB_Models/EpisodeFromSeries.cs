namespace MovieDataLayer.Models.IMDB_Models
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