﻿using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.DataService.UserFrameworkRepository;
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
        var result = (await _userDataService.GetAll()).Select(DTO_Extensions.Spawn_DTO<UserModel, User>); // maybe never retrieve the password, just a thought you know!
        return Ok(result);
    }

    [HttpGet("search_history/{id}")]
    public async Task<IActionResult> GetAllUserHistory(int id)
    {
        var result = (await _userDataService.GetAllSearchHistoryByUserId(id)).Select(DTO_Extensions.Spawn_DTO<UserSearchHistoryModel, UserSearchHistory>);
        return Ok(result);
    }
    
    [HttpGet("ratings/{id}")]
    public async Task<IActionResult> GetAllUserRatings(int id)
    {
        var result = (await _userDataService.GetAllUserRatingByUserId(id)).Select(DTO_Extensions.Spawn_DTO<UserRatingModel, UserRating>);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = DTO_Extensions.Spawn_DTO<UserModel, User>(await _userDataService.Get(id));
        return Ok(result);
    }

    //[HttpGet("user")]
    //public async Task<IActionResult> GetById(string email) // should probably be authorized ALOT to be allowed to call this
    //{
    //    var result = await _userDataService.GetByEmail(email);
    //    return Ok(result);
    //}
}

