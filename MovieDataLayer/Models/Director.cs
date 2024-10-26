using MovieDataLayer.Extentions;

namespace MovieDataLayer
{
    public class Director : Item<string> // when junction, maybe not have the one of the id be the id and just keep the id's in the class like in here
    {
        public string TitleId { get; set; }
        public string PersonId { get; set; }
        public Title Title { get; set; } = null!; //required ref. navigation
        public Person Person { get; set; } = null!; //required ref. navigation

     
    }
}