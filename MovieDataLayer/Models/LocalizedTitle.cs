using MovieDataLayer.Extentions;

namespace MovieDataLayer
{
    public class LocalizedTitle //: Item
    {
        // We do not use Ordering...
        public int Id { get; set; }
        public string TitleId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string Region { get; set; }
        public string Type { get; set; }
        public string Attribute { get; set; }

        //public override object GetId()
        //{
        //    return Id;
        //}

        //public override void SetId(object id)
        //{
        //    Id = (int)id;
        //}

    }
}