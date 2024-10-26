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
        private readonly IMovieDataRepository<Title, string> _dataService;

        public TitleController(IMovieDataRepository<Title, string> dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("writers/{id}")]
        public IActionResult GetWriters(string id)
        {
            //var writers = _dataService.Get(id).Select(CreateWriterModel);
            //if (writers == null) return NotFound();
            //return Ok(writers);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var title = _dataService.Get(id);

            if (title != null)
            {
                var titleModel = CreateTitleModel(title);
                return Ok(titleModel);
            }
            return NotFound();
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_dataService.GetAll());
        }

        // Helper methods
        private PersonModel? CreateWriterModel(Title? title)
        {
            if (title == null) return null;

            var personModel = title.Adapt<PersonModel>(); // funky name...
            return personModel;
        }

        private TitleModel? CreateTitleModel(Title? title)
        {
            if (title == null) return null;

            var personModel = title.Adapt<TitleModel>(); // funky name...
            return personModel;
        }

    }
}
