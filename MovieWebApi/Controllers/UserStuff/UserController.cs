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
    public async Task<IActionResult> GetAll()
    {
        var result = await _userDataService.GetAllUsers_InqludeAll();
        return Ok(result);
    }
    
    [HttpGet("search_history/{id}")]
    public async Task<IActionResult> GetAllUserHistory(int id)
    {
        var result = await _userDataService.GetAllUSearchHistoryByUserId(id);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _userDataService.GetAll(id);
        return Ok(result);
    }
    
    
    [HttpGet("user")]
    public async Task<IActionResult> GetById(string email) // should probably be authorized ALOT to be allowed to call this
    {
        var result = await _userDataService.GetByEmail(email);
        return Ok(result);
    }
}

