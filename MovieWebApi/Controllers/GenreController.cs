using Mapster;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.Interfaces;

namespace MovieWebApi.Controllers;
[ApiController]
[Route("api/genres")]
public class GenreController : ControllerBase
{
    private readonly IMovieDataRepository<Genre, int> _dataService;

    public GenreController(IMovieDataRepository<Genre, int> dataService)
    {
        _dataService = dataService;
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

    //private GenreModel? CreateGenreModel(Genre? genre)  // bro this has to be generic, it is every where
    //{
    //    if (genre == null) return null;

    //    var genreModel = genre.Adapt<GenreModel>();
    //    return genreModel;
    //}

    public static TModel? CreateModel<TModel, TEntity>(TEntity entity) where TEntity : class, new() where TModel : class // it works, look at that beauty
    {
        if (entity == null) return null;

        var model = entity.Adapt<TModel>();
        return model;
    }
}

