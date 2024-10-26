using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.DataService;
using MovieDataLayer.Interfaces;
using Npgsql;

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
    public IActionResult GetById(int id)
    {
        return Ok(_dataService.GetAll(id));
    }
}


public class CallSqlFunction(string connectionString)
{
    public void Yo()
    {
        using var connection = new NpgsqlConnection();
        connection.Open();

        using var command = new NpgsqlCommand();
        command.CommandText = "select get_customer(" + "gh1jv3n2@example.com" +")";

        using var reader = command.ExecuteReader(); 

        while (reader.Read())
        {
            Console.WriteLine($"{reader.GetInt32}");
        }
    }

}

