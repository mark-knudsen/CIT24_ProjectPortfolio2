namespace MovieDataLayer;
    public class MostRelevant : Item<string> // id is personid
    {
        public string TitleId { get; set; }
        public Person person { get; set; }
        public Title title { get; set; }
    }
