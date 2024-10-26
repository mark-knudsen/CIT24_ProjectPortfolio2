using MovieDataLayer.Extentions;

namespace MovieDataLayer
{
    public class Profession
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<Person> PersonsList { get; set; }


    }
}