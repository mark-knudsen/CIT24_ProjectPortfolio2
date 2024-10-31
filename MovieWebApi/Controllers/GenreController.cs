using Microsoft.AspNetCore.Mvc;
using MovieDataLayer.Interfaces;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.Extensions;

namespace MovieWebApi.Controllers;
[ApiController]
[Route("api/genres")]
public class GenreController : ControllerBase
{
    public record GenreModel(string Name);
    private readonly IRepository<Genre> _dataService;
    public GenreController(IRepository<Genre> dataService)
    {
        _dataService = dataService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = (await _dataService.GetAll()).Select(DTO_Extensions.Spawn_DTO<GenreModel, Genre>);
        if (result != null)
        {
            return Ok(result);
        }
        return NotFound();
    }
}

