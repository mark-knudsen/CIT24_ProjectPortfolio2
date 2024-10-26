using MovieDataLayer.Extentions;

namespace MovieDataLayer
{
    public class Plot 
    {
        public string TitleId { get; set; }
        public string PlotOfTitle {  get; set; }
        public Title Title { get; set; } = null!; //required ref. navigation
       
    }
}