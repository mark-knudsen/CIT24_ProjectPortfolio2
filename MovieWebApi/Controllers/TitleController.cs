using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer.Interfaces;
using MovieDataLayer;
using Mapster;

namespace MovieWebApi.Controllers
{
    [ApiController]
    [Route("api/titles")]
    public class TitleController : ControllerBase
    {
        private readonly IMovieDataRepository<Title> _dataService;

        public TitleController(IMovieDataRepository<Title> dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("writers/{id}")]
        public IActionResult GetWriters(string id)
        {
            var writers = _dataService.Get(id).Select(CreateWriterModel);
            if (writers == null) return NotFound();
            return Ok(writers);
        }

        // Helper methods
        private PersonModel? CreateWriterModel(Title? title)
        {
            if (title == null) return null;

            var personModel = title.Adapt<PersonModel>(); // funky name...
            return personModel;
        }

    }
}
