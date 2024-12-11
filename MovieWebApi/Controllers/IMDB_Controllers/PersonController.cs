using Microsoft.AspNetCore.Mvc;
using MovieDataLayer.DataService.IMDB_Repository;
using MovieDataLayer.Data_Service.User_Framework_Repository;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.DTO.IMDB_DTO;
using MovieWebApi.Extensions;
using Microsoft.AspNetCore.Cors;
using MovieDataLayer.Models.IMDB_Models.IMDB_Temp_Tables;
using MovieWebApi.DTO.Search_DTO;
using MovieWebApi.SearchDTO;

namespace MovieWebApi.Controllers.IMDB_Controllers
{
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/persons")]
    public class PersonController : GenericController
    {
        private readonly PersonRepository _personRepository;
        public PersonController(PersonRepository personRepository, LinkGenerator linkGenerator, UserRepository userRepository, AuthenticatorExtension authenticatorHelper) : base(linkGenerator, userRepository, authenticatorHelper)
        {
            _personRepository = personRepository;
        }

        [HttpGet("{id}", Name = nameof(GetPerson))]
        public async Task<IActionResult> GetPerson(string id)
        {
            var person = (await _personRepository.GetPerson(id)).MapPersonToPersonDTO(HttpContext, _linkGenerator, nameof(GetPerson));
            if (person == null) return NotFound();

            return Ok(person);
        }

        [HttpGet(Name = nameof(GetAllPersons))]
        public async Task<IActionResult> GetAllPersons(int page = 0, int pageSize = 10)
        {
            if (page < 0 || pageSize <= 0) return BadRequest("Page and PageSize must be 0 or greater");
            var result = (await _personRepository.GetAllWithPaging(page, pageSize)).Select(person => person.Spawn_DTO_WithPagination<PersonDetailedDTO, PersonModel>(HttpContext, _linkGenerator, nameof(GetPerson)));
            if (result == null || !result.Any()) return NotFound();
            return Ok(result);
            //Properties MostRelevantTitles and PrimaryProfessions, should be considered removed from DTO, as they are not needed in the list?
        }

        [HttpGet("search", Name = nameof(SearchPerson))]
        public async Task<IActionResult> SearchPerson([FromHeader] string? authorization, [FromQuery] string searchTerm, int page = 0, int pageSize = 10)
        {
            int userId = 0;
            if (authorization != null) userId = _authenticatorExtension.ExtractUserID(authorization);
            var (searchResult, totalCount) = await _personRepository.PersonSearch(userId, searchTerm.Trim(), page, pageSize);
            if (!searchResult.Any()) return NotFound();
            var searchResultMapped = searchResult.Select(pSearch => pSearch.Spawn_DTO_WithPagination<PersonSearchResultDTO, PersonSearchResultTempTable>(HttpContext, _linkGenerator, nameof(GetPerson)));

            if (searchResultMapped == null || !searchResultMapped.Any()) return NotFound();
            //searchResult = CreateNavigationForSearchList(searchResult);
            object result = CreatePaging(nameof(SearchPerson), page, pageSize, totalCount, searchResultMapped, "searchTerm", searchTerm);
            return Ok(result);
        }


        // should still use the extension spawn dto instead
        private IEnumerable<PersonSearchResultDTO>? CreateNavigationForSearchList(IEnumerable<PersonSearchResultDTO>? searchResultDTO)
        {
            if (searchResultDTO == null || !searchResultDTO.Any())
            {
                return null;
            }

            foreach (var person in searchResultDTO)
            {
                person.Url = GetUrl(nameof(GetPerson), new { person.PersonId });
            }
            return searchResultDTO;
        }
    }
}
