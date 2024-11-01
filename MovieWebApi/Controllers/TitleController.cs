using Microsoft.AspNetCore.Mvc;
using MovieDataLayer.DataService.IMDB_Repository;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.Extensions;
using MovieDataLayer;
using MovieDataLayer.DataService.UserFrameworkRepository;

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
            var titles = (await _titleRepository.GetAll()).Select(DTO_Extensions.Spawn_DTO<TitleDetailedDTO, Title>);

            if (titles == null) return NotFound();

            return Ok(titles);
        }

        [HttpGet("writers/{id}")]
        public async Task<IActionResult> GetWriters(string id) // id tt13689568
        {
            var writers = (await _titleRepository.GetWritersByMovieId(id)).Select(DTO_Extensions.Spawn_DTO<TitleWriterDTO, Person>); //Using subclass (TitleRepository) method to get writers by movie id
            if (writers == null || !writers.Any())
                return NotFound(); //return 404 if writers is null or if list is empty.

            return Ok(writers);
        }

        // Get one title - Currently not in use
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetTitle(string id) // id tt7856872
        //{
        //    var title = DTO_Extensions.Spawn_DTO<TitleDetailedDTO, Title>(await _titleRepository.Get(id));

        //    if (title == null) return NotFound();

        //    return Ok(title);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) // id tt7856872
        {
            var title = (await _titleRepository.GetTitle(id)).MapTitleToTitleDetailedDTO();
            if (title == null) return NotFound();

            return Ok(title);
        }

        [HttpGet("genre/{id}")]
        public async Task<IActionResult> GetByGenre(int id) // id tt7856872
        {
            var titles = (await _titleRepository.GetTitleByGenre(id)).MapTitleToTitleDetailedDTO();
            if (titles == null) return NotFound();

            return Ok(titles);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromHeader] int userId, string searchTerm) // should probably be authorized ALOT to be allowed to call this
        {
            var result = await _titleRepository.TitleSearch(userId, searchTerm);
            return Ok(result);
        }
    }
}
