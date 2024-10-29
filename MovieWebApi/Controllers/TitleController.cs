using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetAllTitles() // We really just want the plot and poster at all times in the title, same with some of the collections
        {
            var titles = (await _titleRepository.GetAll()).Select(DTO_Extensions.Spawn_DTO<TitleModel, Title>);

            if (titles == null) return NotFound();

            return Ok(titles);
        }

        [HttpGet("writers/{id}")]
        public async Task<IActionResult> GetWriters(string id) // id tt13689568
        {
            var writers = (await _titleRepository.GetWritersByMovieId(id)).Select(DTO_Extensions.Spawn_DTO<TitleWriterModel, Person>); //Using subclass (TitleRepository) method to get writers by movie id
            if (writers == null || !writers.Any())
                return NotFound(); //return 404 if writers is null or if list is empty.

            return Ok(writers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTitle(string id) // id tt7856872
        {
            var title = DTO_Extensions.Spawn_DTO<TitleModel, Title>(await _titleRepository.Get(id));

            if (title == null) return NotFound();

            return Ok(title);
        }
    }
}
