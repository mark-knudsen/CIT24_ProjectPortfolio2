using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MovieDataLayer;
using MovieDataLayer.DataService.IMDB_Repository;
using MovieDataLayer.Interfaces;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.Extensions;

namespace MovieWebApi.Controllers
{
    [ApiController]
    [Route("api/persons")]
    public class PersonController : GenericController
    {
        private readonly PersonRepository _personRepository;
        private readonly LinkGenerator _linkGenerator;
        public PersonController(PersonRepository personRepository, LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _personRepository = personRepository;
            _linkGenerator = linkGenerator;
        }

        [HttpGet("{id}", Name = nameof(GetPerson))]
        public async Task<IActionResult> GetPerson(string id)
        {

            var person = (await _personRepository.GetPerson(id)).MapPersonToPersonDTO(HttpContext, _linkGenerator, nameof(GetPerson));
            if (person == null) return NotFound();

            return Ok(person);
        }

        [HttpGet(Name = nameof(GetAll))]
        public async Task<IActionResult> GetAll(int page = 0, int pageSize = 10)
        {
            if (page < 0 || pageSize <= 0) return BadRequest("Page and PageSize must be 0 or greater");
            var result = (await _personRepository.GetAllWithPaging(page = 0, pageSize = 10)).Select(person => person.Spawn_DTO<PersonDetailedDTO, Person>(HttpContext, _linkGenerator, nameof(GetPerson)));
            if (result == null || !result.Any()) return NotFound();
            return Ok(result);
            //Properties MostRelevantTitles and PrimaryProfessions, should be considered removed from DTO, as they are not needed in the list?
        }

    }
}
