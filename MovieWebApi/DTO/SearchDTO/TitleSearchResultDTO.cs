using MovieDataLayer.Models.IMDB_Models.IMDB_DTO;

namespace MovieWebApi.DTO.SearchDTO
{
    public class TitleSearchResultDTO : TitleSearchResultModel
    {
        public string TitleId { get; set; }
        public string PrimaryTitle { get; set; }
    }
}
