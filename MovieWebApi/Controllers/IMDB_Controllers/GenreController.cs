using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer.Data_Service.User_Framework_Repository;
using MovieDataLayer.Interfaces;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.Extensions;

namespace MovieWebApi.Controllers.IMDB_Controllers
{
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/genres")]
    public class GenreController : GenericController
    {
        public record ReadGenreModel(string Name);

        private readonly IRepository<GenreModel> _dataService;
        public GenreController(IRepository<GenreModel> dataService, LinkGenerator linkGenerator, UserRepository userRepository, AuthenticatorExtension authenticatorHelper) : base(linkGenerator, userRepository, authenticatorHelper)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 0, int pageSize = 30)
        {
            if (page < 0 || pageSize <= 0) return BadRequest("Page and PageSize must be 0 or greater");

            //Generic use of Spawn_DTO, including URL mapped to the DTO
            var result = (await _dataService.GetAllWithPaging(page, pageSize)).Select(genre => genre.Spawn_DTO_WithPagination<ReadGenreModel, GenreModel>(HttpContext, _linkGenerator, nameof(GetAll)));
            if (result == null || !result.Any())
            {

                return NotFound();
            }

            return Ok(result);
        }
    }
}
