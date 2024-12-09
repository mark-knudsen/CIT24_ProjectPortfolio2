namespace MovieWebApi.DTO.IMDB_DTO;
public class TitleDetailedDTO
{
    public string? Url { get; set; }
    public string Id { get; set; }
    public string TitleType { get; set; }
    public string PrimaryTitle { get; set; }
    public string OriginalTitle { get; set; }
    public int StartYear { get; set; }
    public int? EndYear { get; set; }
    public int Runtime { get; set; }
    public bool IsAdult { get; set; }
    public string PosterUrl { get; set; }
    public string Plot { get; set; }
    public float? AverageRating { get; set; }
    public int? VoteCount { get; set; }
    public IList<string> GenresList { get; set; } 
    public IList<string> LocalizedTitlesList { get; set; }
   // public IList<string> PrincipalCastList { get; set; } // these two needs the id as well
    public List<PrincipalCastDTO> PrincipalCastList { get; set; }

    public List<WritersListDTO> WritersList { get; set; } // and me
    public List<DirectorsListDTO> DirectorsList { get; set; }
}