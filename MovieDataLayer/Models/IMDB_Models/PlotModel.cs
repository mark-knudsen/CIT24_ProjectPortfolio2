using System.Text.Json.Serialization;

namespace MovieDataLayer.Models.IMDB_Models
{
    public class PlotModel
    {
        public string TitleId { get; set; }
        public string PlotOfTitle { get; set; }
        //[JsonIgnore]
        public TitleModel Title { get; set; } = null!; //required ref. navigation

    }
}