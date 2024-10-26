using MovieDataLayer.Extentions;


namespace MovieDataLayer
{
    public class Title
    {
        public string Id { get; set; }
        public string TitleType { get; set; }
        public string PrimaryTitle { get; set; }
        public string OriginalTitle { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public int? Runtime { get; set; }
        public bool? IsAdult { get; set; }
        // public string PosterUrl { get; set; }
        //public string Plot { get; set; }
        //public float AverageRating { get; set; }
        //public int VoteCount { get; set; }
        public Rating rating { get; set; }
        public IList<Genre> GenresList { get; set; }
        public IList<LocalizedTitle> LocalizedTitlesList { get; set; }
        public IList<PrincipalCast> PrincipalCastList { get; set; }
        public IList<Person> WritersList { get; set; }
        //public IList<string> DirectorsList { get; set; }

        //public IList<Person> Persons { get; set; }
        public IList<MostRelevant> MostRelevantPersons { get; set; }


        //public override string GetId() => Id;
        //public override void SetId(string id) => Id = id;

        //public override void SetId(object id)
        //{
        //    Id = (string)id;
        //}
    }
}