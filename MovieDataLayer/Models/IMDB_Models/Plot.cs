namespace MovieDataLayer.Models.IMDB_Models
{
    public class Plot
    {
        public string TitleId { get; set; }
        public string PlotOfTitle { get; set; }
        public Title Title { get; set; } = null!; //required ref. navigation

    }
}