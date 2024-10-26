using Microsoft.AspNetCore.Mvc;
using MovieDataLayer.Interfaces;
using MovieDataLayer;

namespace MovieWebApi.Controllers.UserStuff;
[ApiController]
[Route("api/users")]
public class UserController : Controller
{
    readonly UserDataRepository _userDataService;

    public UserController(UserDataRepository userDataService)
    {
        _userDataService = userDataService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_userDataService.GetAllUsers_InqludeAll());
    }
    
     [HttpGet("search_history/{id}")]
    public IActionResult GetAllUserHistory(int id)
    {
        return Ok(_userDataService.GetAllUSearchHistoryByUserId(id));
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(_userDataService.GetAll(id));
    }
}

