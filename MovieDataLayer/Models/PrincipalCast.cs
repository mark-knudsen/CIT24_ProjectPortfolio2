using MovieDataLayer.Extentions;

namespace MovieDataLayer
{
    public class PrincipalCast //: Item
    {
        public string PersonId { get; set; }
        public int Ordering { get; set; }
        public string TitleId { get; set; }
        public string CharacterName { get; set; }
        public string Category { get; set; }
        public string Job { get; set; }

        //public override object GetId()
        //{
        //    return PersonId;
        //}

        //public override void SetId(object id)
        //{
        //    PersonId = (string)id;
        //}
    }
}