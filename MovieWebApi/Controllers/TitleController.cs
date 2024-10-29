using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer.Interfaces;
using Mapster;
using MovieWebApi.Models;
using MovieDataLayer.DataService.IMDB_Repository;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.Extensions;
using MovieDataLayer;

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
        public async Task<IActionResult> GetWriters(string id)
        {


            var writers = (await _titleRepository.GetWritersByMovieId(id)).Select(DTO_Extensions.Spawn_DTO<TitleWriterModel, Person>); //Using subclass (TitleRepository) method to get writers by movie id
            if (writers == null || !writers.Any())
                return NotFound(); //return 404 if writers is null or if list is empty.


            return Ok(writers);
        }

        [HttpGet("{id}")]
        public IActionResult GetTitle(string id) //Get Simple Title DTO
        {

            //var title = _titleRepository.Get(id).Select(DTO_Extensions.Spawn_DTO<TitleModel, Title>); //Using the Get method from inherited Repository class
            var title = DTO_Extensions.Spawn_DTO<TitleSimpleModel, Title>(_titleRepository.Get(id)); //Using the Get method from inherited Repository class
            if (title == null) return NotFound();


            return Ok(title);
        }

        [HttpGet("{id}/details")]
        public IActionResult GetTitleDetails(string id)
        {
            //var title = _titleRepository.GetTitleDetails(id);
            var title = _titleRepository.GetTitleDetails(id).Select(DTO_Extensions.Spawn_DTO<TitleDetailedModel, Title>);
            //var title = DTO_Extensions.Spawn_DTO<TitleDetailedModel, Title>(_titleRepository.GetTitleDetails(id)); //Using subclass (TitleRepository) method to get title details
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
