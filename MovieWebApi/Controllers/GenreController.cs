using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.DataService;
using MovieDataLayer.Interfaces;

namespace MovieWebApi.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenreController : ControllerBase
    {
        private readonly DataService<Genre> _dataService;
        //private readonly IMovieDataRepository<Genre> _dataService;

        //public GenreController(IMovieDataRepository<Genre> dataService)
        //{
        //    _dataService = dataService;
        //}    
        public GenreController(DataService<Genre> dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_dataService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_dataService.GetById(id));
        }
    }
}