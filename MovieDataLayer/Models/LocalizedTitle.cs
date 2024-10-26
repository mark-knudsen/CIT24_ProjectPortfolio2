using MovieDataLayer.Extentions;

namespace MovieDataLayer
{
    public class LocalizedTitle
    {
        // We do not use Ordering...
        public int Id { get; set; } //PK
        public string TitleId { get; set; } //FK to title
        public string LocalizedTitleName { get; set; }
        //public string Language { get; set; }
        //public string Region { get; set; }
        //public string Type { get; set; }
        //public string Attribute { get; set; }

        public Title title { get; set; } //Navigation property to title



    }
}