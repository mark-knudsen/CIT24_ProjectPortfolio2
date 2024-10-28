namespace MovieDataLayer;
    public class Rating : Item<string>
    {
        public int AverageRating { get; set; }
        public Title Title { get; set; } = null!; //required ref. navigation
        public int VoteCount { get; set; }

    }
