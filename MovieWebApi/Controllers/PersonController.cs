using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.DataService;
using MovieDataLayer.Interfaces;

namespace MovieWebApi.Controllers;

[ApiController]
[Route("api/persons")]
public class PersonController : ControllerBase
{
    private readonly IMovieDataRepository<Person, string> _dataService;

    public PersonController(IMovieDataRepository<Person, string> dataService)
    {
        _dataService = dataService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_dataService.GetAll());
    }


    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        var person = _dataService.Get(id);

        if (person != null)
        {
            var personModel = CreatePersonModel(person);
            return Ok(personModel);
        }
        return NotFound();
    }

    private PersonModel? CreatePersonModel(Person? title)
    {
        if (title == null) return null;

        var personModel = title.Adapt<PersonModel>();
        return personModel;
    }
}

