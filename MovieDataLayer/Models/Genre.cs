using MovieDataLayer.Extentions;

namespace MovieDataLayer
{
    public class Genre : Item
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override int GetId()
        {
            return Id;
        }

        public override void SetId(int id)
        {
            Id = id;
        }
    }
}