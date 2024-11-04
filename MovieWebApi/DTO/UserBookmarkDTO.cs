﻿namespace MovieWebApi
{
    public class UserBookmarkDTO
    {
        public string? Url { get; set; }
        public string? TitleId { get; set; }
        public string? PersonId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Annotation { get; set; }
    }
}
