using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer.DataService;
using MovieDataLayer.Interfaces;
using MovieDataLayer.Models.IMDB_Models;

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
        public IActionResult GetAll()
        {
            return Ok(_dataService.GetAll());
        }
    }
}
