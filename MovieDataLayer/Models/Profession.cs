using MovieDataLayer.Extentions;

namespace MovieDataLayer
{
    public class Profession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        PrimaryProfession PrimaryProfession { get; set; }
       
    }
}