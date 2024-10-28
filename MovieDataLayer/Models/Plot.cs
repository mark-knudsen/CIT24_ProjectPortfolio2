namespace MovieDataLayer;
    public class Plot : Item<string>
    {
        public string PlotOfTitle {  get; set; }
        public Title Title { get; set; } = null!; //required ref. navigation
       
    }
