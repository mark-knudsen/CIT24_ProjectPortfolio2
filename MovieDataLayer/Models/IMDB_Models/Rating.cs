﻿using System.Text.Json.Serialization;

namespace MovieDataLayer.Models.IMDB_Models
{
    public class Rating
    {
        public string TitleId { get; set; }
        public int AverageRating { get; set; }
        [JsonIgnore]
        public Title Title { get; set; } = null!; //required ref. navigation
        public int VoteCount { get; set; }

    }
}