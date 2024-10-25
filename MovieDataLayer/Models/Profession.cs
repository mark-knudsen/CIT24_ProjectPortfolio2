using MovieDataLayer.Extentions;

namespace MovieDataLayer
{
    public class Profession //: Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Person Person { get; set; }

        //public override object GetId()
        //{
        //    return Id;
        //}

        //public override void SetId(object id)
        //{
        //    Id = (string)id;
        //}
    }
}