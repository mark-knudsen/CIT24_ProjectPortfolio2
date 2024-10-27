using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer.Interfaces;
using MovieDataLayer;
using Mapster;
using MovieDataLayer.DataService;
using MovieWebApi.Models;

namespace MovieWebApi.Controllers
{
    [ApiController]
    [Route("api/titles")]
    public class TitleController : ControllerBase
    {
        private readonly TitleRepository _titleRepository;

        public TitleController(TitleRepository titleRepository)
        {
            _titleRepository = titleRepository;
        }

        [HttpGet("writers/{id}")]
        public IActionResult GetWriters(string id)
        {


            var writers = _titleRepository.GetWritersByMovieId(id).Select(CreateWriterModel); //Using subclass (TitleRepository) method to get writers by movie id
            if (writers == null || !writers.Any())
                return NotFound(); //return 404 if writers is null or if list is empty.


            return Ok(writers);
        }

        [HttpGet("{id}")]
        public IActionResult GetTitle(string id)
        {

            var title = _titleRepository.Get(id); //Using the Get method from inherited Repository class

            if (title == null) return NotFound();


            return Ok(title);
        }

        // Helper methods
        private TitleWriterModel? CreateWriterModel(Person? title)
        {
            if (title == null) return null;

            var personModel = title.Adapt<TitleWriterModel>(); // funky name...
            return personModel;
        }

    }
}
