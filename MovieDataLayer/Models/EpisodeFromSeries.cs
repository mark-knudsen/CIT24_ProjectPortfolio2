namespace MovieDataLayer;
    public class EpisodeFromSeries : Item<string> // id is SeriesTitleId
    {
        public string TitleId { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public Title Title { get; set; } = null!; //required ref. navigation


    }