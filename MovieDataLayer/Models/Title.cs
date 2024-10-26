using MovieDataLayer.Extentions;

namespace MovieDataLayer
{
    public class Title : Item<string>
    {
        public string TitleType { get; set; }
        public string PrimaryTitle { get; set; }
        public string OriginalTitle { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public int? Runtime { get; set; }
        public bool IsAdult { get; set; }
        //public string PosterUrl { get; set; }
        //public string Plot { get; set; }
        //public float AverageRating { get; set; }
        //public int VoteCount { get; set; }
      //  public IEnumerable<Genre> GenresList { get; set; }   // the genre list needs to be combined with the titlegenre to find the relevant genres
        //public IEnumerable<LocalizedTitle> LocalizedTitlesList { get; set; }
        //public IEnumerable<PrincipalCast> PrincipalCastList { get; set; }
        //public IEnumerable<string> WritersList { get; set; }
        //public IEnumerable<string> DirectorsList { get; set; }
        //public IEnumerable<Person> Persons { get; set; }

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