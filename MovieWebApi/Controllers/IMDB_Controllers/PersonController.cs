using Microsoft.AspNetCore.Mvc;
using MovieDataLayer.DataService.IMDB_Repository;
using MovieDataLayer.Data_Service.User_Framework_Repository;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.DTO.IMDB_DTO;
using MovieWebApi.Extensions;
using Microsoft.AspNetCore.Cors;

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

        [HttpGet("{id}", Name = nameof(Get))]
        public async Task<IActionResult> Get(string id)
        {
            var person = (await _personRepository.GetPerson(id)).MapPersonToPersonDTO(HttpContext, _linkGenerator, nameof(Get));
            if (person == null) return NotFound();

            return Ok(person);
        }

        [HttpGet(Name = nameof(GetAll))]
        public async Task<IActionResult> GetAll(int page = 0, int pageSize = 10)
        {
            if (page < 0 || pageSize <= 0) return BadRequest("Page and PageSize must be 0 or greater");
            var result = (await _personRepository.GetAllWithPaging(page, pageSize)).Select(person => person.Spawn_DTO_WithPagination<PersonDetailedDTO, PersonModel>(HttpContext, _linkGenerator, nameof(Get)));
            if (result == null || !result.Any()) return NotFound();
            return Ok(result);
            //Properties MostRelevantTitles and PrimaryProfessions, should be considered removed from DTO, as they are not needed in the list?
        }
    }
}
