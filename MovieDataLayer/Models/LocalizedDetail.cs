namespace MovieDataLayer;
    public class LocalizedDetail : Item<int>
    {
        // We do not use Ordering...
        public string LocTitle { get; set; } 
        public string Language { get; set; }
        public string Region { get; set; }
        public string Type { get; set; }
        public string Attribute { get; set; }
        public Title Title { get; set; } = null!; //required ref. navigation
    }