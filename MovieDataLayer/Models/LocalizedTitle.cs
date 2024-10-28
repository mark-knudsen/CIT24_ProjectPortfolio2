namespace MovieDataLayer;
    public class LocalizedTitle : Item<int>
    {
        // We do not use Ordering...
        public string TitleId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Region { get; set; }
        public string Type { get; set; }
        public string Attribute { get; set; }


    }