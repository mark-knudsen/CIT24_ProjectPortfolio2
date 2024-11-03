using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MovieDataLayer.DataService.IMDB_Repository;
using MovieDataLayer.Interfaces;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.Extensions;

namespace MovieWebApi.Controllers;
[ApiController]
[Route("api/genres")]
public class GenreController : GenericController
{
    public record GenreModel(string Name); // Reocrds in a good way to make

    private readonly IRepository<Genre> _dataService;
    private readonly LinkGenerator _linkGenerator;
    public GenreController(IRepository<Genre> dataService, LinkGenerator linkGenerator) : base(linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;

    }


    [HttpGet]
    public async Task<IActionResult> GetAll(int page = 0, int pageSize = 10)
    {
        var result = (await _dataService.GetAll(page = 0, pageSize = 10)).Select(genre => genre.Spawn_DTO<GenreModel, Genre>(HttpContext, _linkGenerator, nameof(GetAll)));
        if (result != null)
        {
            return Ok(result);
        }
        return NotFound();
    }



}
