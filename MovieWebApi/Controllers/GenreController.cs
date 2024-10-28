using Mapster;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.DataService;
using MovieDataLayer.Interfaces;

namespace MovieWebApi.Controllers;
[ApiController]
[Route("api/genres")]
public class GenreController : ControllerBase
{
    private readonly MovieDataRepository<Genre, int> _dataService;

    public GenreController(IMovieDataRepository<Genre, int> dataService)
    {
        _dataService = (MovieDataRepository<Genre, int>?)dataService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_dataService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = (await _dataService.GetAll(id)).Select(CreateModel<GenreModel, Genre>);
        if (result != null)
        {
            return Ok(result);
        }
        return NotFound();
    }

    public static TModel? CreateModel<TModel, TEntity>(TEntity entity) where TEntity : class where TModel : class
    {
        if (entity == null) return null;

        var model = entity.Adapt<TModel>();
        return model;
    }
}

