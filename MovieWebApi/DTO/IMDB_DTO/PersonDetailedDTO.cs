﻿namespace MovieWebApi.DTO.IMDB_DTO;
public class PersonDetailedDTO
{
    public string? Url { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public int? BirthYear { get; set; }
    public int? DeathYear { get; set; }
    public IList<string>? MostRelevantTitles { get; set; } 
    public IList<string>? PrimaryProfessions { get; set; } 

}
