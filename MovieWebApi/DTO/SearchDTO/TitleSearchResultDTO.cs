using MovieDataLayer.Models.IMDB_Models.IMDB_DTO;

namespace MovieWebApi.DTO.SearchDTO
{
    public class TitleSearchResultDTO : BaseDTO
    {

        public string Id { get; set; }
        public string PrimaryTitle { get; set; }
    }
}
