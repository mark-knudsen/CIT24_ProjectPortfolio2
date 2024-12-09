using MovieDataLayer.Models.IMDB_Models;
using System.Text.Json.Serialization;

namespace MovieWebApi.DTO.IMDB_DTO
{
    public class PrincipalCastDTO
    {
 
            public string PersonId { get; set; }
            public string PersonName { get; set; }

        }
}
