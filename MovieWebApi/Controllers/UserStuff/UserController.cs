﻿using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.DTO;
using MovieWebApi.Extensions;

namespace MovieWebApi.Controllers.UserStuff;
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    public record UpdateUserModel(string email, string firstName, string password);
    readonly UserRepository _userRepository;

    public UserController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = (await _userRepository.GetAll()).Select(DTO_Extensions.Spawn_DTO<UserDTO, User>); // maybe never retrieve the password, just a thought you know!
        return Ok(result);
    }

    [HttpGet("search_history/{id}")]
    public async Task<IActionResult> GetAllUserHistory(int id)
    {
        var result = (await _userRepository.GetAllSearchHistoryByUserId(id)).Select(DTO_Extensions.Spawn_DTO<UserSearchHistoryDTO, UserSearchHistory>);

        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = DTO_Extensions.Spawn_DTO<UserDTO, User>(await _userRepository.Get(id));

        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(UserRegistrationDTO userRegistrationDTO)
    {
        var result = DTO_Extensions.Spawn_DTO<User, UserRegistrationDTO>(userRegistrationDTO);
        bool success = await _userRepository.Add(result);

        if (!success) return BadRequest();
        return Created("", result); // add url later
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserModel updateUserModel)
    {
        User user = await _userRepository.Get(id);
        if (user != null)
        {
            user.Email = updateUserModel.email != "" ? updateUserModel.email : user.Email;
            user.FirstName = updateUserModel.firstName != "" ? updateUserModel.firstName : user.FirstName;
            user.Password = updateUserModel.password != "" ? updateUserModel.password : user.Password;
        }
        else return NotFound();
        bool success = await _userRepository.Update(user);
        if (success) return NoContent();
        return BadRequest();
    }


    [HttpDelete]
    public async Task<IActionResult> DeleteUser(int id) 
    {
        bool success = await _userRepository.Delete(id);
        if (success) return NoContent();
        return NotFound();
    }


    //[HttpGet("user")]
    //public async Task<IActionResult> GetById(string email) // should probably be authorized ALOT to be allowed to call this
    //{
    //    var result = await _userRepository.GetByEmail(email);
    //    return Ok(result);
    //}
}

