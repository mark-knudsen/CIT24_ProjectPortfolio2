using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.DTO;
using MovieWebApi.Extensions;

namespace MovieWebApi.Controllers.UserStuff;
[ApiController]
[Route("api/users")]
public class UserController : Controller
{
    readonly UserRepository _userDataService;

    public UserController(UserRepository userDataService)
    {
        _userDataService = userDataService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = (await _userDataService.GetAll()).Select(DTO_Extensions.Spawn_DTO<UserDTO, User>); // maybe never retrieve the password, just a thought you know!
        return Ok(result);
    }

    [HttpGet("search_history/{id}")]
    public async Task<IActionResult> GetAllUserHistory(int id)
    {
        var result = (await _userDataService.GetAllSearchHistoryByUserId(id)).Select(DTO_Extensions.Spawn_DTO<UserSearchHistoryDTO, UserSearchHistory>);

        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("ratings/{id}")]
    public async Task<IActionResult> GetAllUserRatings(int id)
    {
        var result = (await _userDataService.GetAllUserRatingByUserId(id)).Select(DTO_Extensions.Spawn_DTO<UserRatingDTO, UserRating>);

        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = DTO_Extensions.Spawn_DTO<UserDTO, User>(await _userDataService.Get(id));

        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("{id}/bookmarks/title")]
    public async Task<IActionResult> GetAllTitleBookmarks(int id)
    {
        var result = (await _userDataService.GetAllTitleBookmarks(id)).Select(DTO_Extensions.Spawn_DTO<UserBookmarkDTO, UserTitleBookmark>);

        if (result == null) return NotFound();
        return Ok(result);
    }
    [HttpGet("{id}/bookmarks/person")]
    public async Task<IActionResult> GetAllPersonBookmarks(int id)
    {
        var result = (await _userDataService.GetAllPersonBookmarks(id)).Select(DTO_Extensions.Spawn_DTO<UserBookmarkDTO, UserPersonBookmark>);

        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(UserRegistrationDTO userRegistrationDTO)
    {
        var result = DTO_Extensions.Spawn_DTO<User, UserRegistrationDTO>(userRegistrationDTO);
        await _userDataService.Add(result);

        if (result == null) return NotFound();
        return Ok(result);

    }

    //[HttpGet("user")]
    //public async Task<IActionResult> GetById(string email) // should probably be authorized ALOT to be allowed to call this
    //{
    //    var result = await _userDataService.GetByEmail(email);
    //    return Ok(result);
    //}
}

