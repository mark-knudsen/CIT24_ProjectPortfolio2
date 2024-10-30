using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.DataService.IMDB_Repository;
using MovieDataLayer.Interfaces;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.Extensions;

namespace MovieWebApi.Controllers
{
    [ApiController]
    [Route("api/persons")]
    public class PersonController : ControllerBase
    {
        private readonly PersonRepository _personRepository;
        public PersonController(PersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = (await _personRepository.GetAll()).Select(DTO_Extensions.Spawn_DTO<PersonDetailedDTO, Person>);
            return Ok(result);
        }

        //Gets one person through ID, currently not in use
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetPerson(string id)
        //{
        //    var person = DTO_Extensions.Spawn_DTO<PersonDTO, Person>(await _dataService.Get(id));

        //    if (person == null) return NotFound();

        //    return Ok(person);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var person  = (await _personRepository.GetPerson(id)).MapPersonToPersonDTO();
            if (person == null) return NotFound();

            return Ok(person);

        }
    }
}
