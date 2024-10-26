using MovieDataLayer.Extentions;

namespace MovieDataLayer
{
    public class PrimaryProfession : Item<string>
    {
        public string PersonId { get; set; }
        public Person Person { get; set; } = null!; //required ref. navigation
        public ICollection<Profession> Profession { get; set; }

    }
}