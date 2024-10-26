using System.Reflection.Metadata;
using MovieDataLayer.Extentions;

namespace MovieDataLayer
{
    public class LocalizedDetail : Item
    {
        // We do not use Ordering...
        public int Id { get; set; }
        public string LocTitle { get; set; } 
        public string Language { get; set; }
        public string Region { get; set; }
        public string Type { get; set; }
        public string Attribute { get; set; }
        public Title Title { get; set; } = null!; //required ref. navigation

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