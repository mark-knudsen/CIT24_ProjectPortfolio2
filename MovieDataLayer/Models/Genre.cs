using MovieDataLayer.Extentions;

namespace MovieDataLayer
{
    public class Genre : Item
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override object GetId()
        {
            return Id;
        }

        public override void SetId(object id)
        {
            Id = (int)id;
        }
    }
}