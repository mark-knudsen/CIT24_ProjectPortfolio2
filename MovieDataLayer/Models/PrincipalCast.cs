﻿namespace MovieDataLayer;
    public class PrincipalCast : Item<string>
    {
        public string PersonId { get; set; }
        public int Ordering { get; set; }
        public string TitleId { get; set; }
        public string CharacterName { get; set; }
        public string Category { get; set; }
        public string Job { get; set; }
    }
