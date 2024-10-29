using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.Interfaces;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.Extensions;

namespace MovieWebApi.Controllers
{
    [ApiController]
    [Route("api/persons")]
    public class PersonController : ControllerBase
    {
        private readonly IRepository<Person> _dataService;
        public PersonController(IRepository<Person> dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = (await _dataService.GetAll()).Select(DTO_Extensions.Spawn_DTO<PersonModel, Person>);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTitle(string id)
        {
            var person = DTO_Extensions.Spawn_DTO<PersonModel, Person>(await _dataService.Get(id));

            if (person == null) return NotFound();

            return Ok(person);
        }
    }
}
