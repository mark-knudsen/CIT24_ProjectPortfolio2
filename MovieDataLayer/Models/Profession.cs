namespace MovieDataLayer;
    public class Profession : Item<string>
    {
        public string Name { get; set; }
        public Person Person { get; set; }
    }
