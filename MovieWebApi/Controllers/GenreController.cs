using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using MovieDataLayer.DataService.IMDB_Repository;
using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieDataLayer.Interfaces;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.Extensions;
using MovieWebApi.Helpers;

namespace MovieWebApi.Controllers;
[ApiController]
[Route("api/genres")]
public class GenreController : GenericController
{
    public record GenreModel(string Name); // Reocrds in a good way to make

    private readonly IRepository<Genre> _dataService;
    public GenreController(IRepository<Genre> dataService, LinkGenerator linkGenerator, UserRepository userRepository, AuthenticatorHelper authenticatorHelper) : base(linkGenerator, userRepository, authenticatorHelper)
    {
        _dataService = dataService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int page = 0, int pageSize = 30)
    {
        if (page < 0 || pageSize <= 0) return BadRequest("Page and PageSize must be 0 or greater");

        //Generic use of Spawn_DTO, including URL mapped to the DTO
        var result = (await _dataService.GetAllWithPaging(page, pageSize)).Select(genre => genre.Spawn_DTO<GenreModel, Genre>(HttpContext, _linkGenerator, nameof(GetAll)));
        if (result == null || !result.Any())
        {

            return NotFound();
        }
        //var numberOfEntities = await _dataService.NumberOfElementsInTable();
        //object paging = CreatePaging(nameof(GetAll), page, pageSize, numberOfEntities, result);
        return Ok(result);
    }
}
