﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieWebApi.DTO.User_DTO;
using MovieWebApi.Extensions;
using MovieWebApi.Helpers;

namespace MovieWebApi.Controllers.UserStuff;

[Authorize]
[ApiController]
[Route("api/user")]
public class UserController : GenericController
{
    public record UpdateUserModel(string email, string firstName, string password);
    public UserController(UserRepository userRepository, LinkGenerator linkGenerator, AuthenticatorExtension authenticatorHelper) : base(linkGenerator, userRepository, authenticatorHelper)
    {
    }

    [HttpGet("user-profile")]
    public async Task<IActionResult> GetById([FromHeader] int id)
    {
        var result = (await _userRepository.Get(id)).Spawn_DTO<UserDTO, UserModel>();
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int page = 0, int pageSize = 10)
    {
        var result = (await _userRepository.GetAllWithPaging(page = 0, pageSize = 10)).Select(user => user.Spawn_DTO_WithPagination<UserDTO, UserModel>(HttpContext, _linkGenerator, nameof(GetAll))); // maybe never retrieve the password, just a thought you know!
        return Ok(result);
    }

    [HttpGet("search_history")]
    public async Task<IActionResult> GetAllUserHistory([FromHeader] int id)
    {
        var result = (await _userRepository.GetAllSearchHistoryByUserId(id)).Select(user => user.Spawn_DTO_WithPagination<UserSearchHistoryDTO, UserSearchHistoryModel>(HttpContext, _linkGenerator, nameof(GetAll)));

        if (result == null) return NotFound();
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Post(UserRegistrationDTO userRegistrationDTO)
    {
        var result = Extension.Spawn_DTO<UserModel, UserRegistrationDTO>(userRegistrationDTO);
        bool success = await _userRepository.Add(result);

        // this should return a token instead, no url plzz
        if (!success) return BadRequest();
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromHeader] int id, UpdateUserModel updateUserModel)
    {
        UserModel user = await _userRepository.Get(id);
        if (user != null)
        {
            user.Email = updateUserModel.email != "" ? updateUserModel.email : user.Email;
            user.FirstName = updateUserModel.firstName != "" ? updateUserModel.firstName : user.FirstName;
            user.Password = updateUserModel.password != "" ? updateUserModel.password : user.Password;
        }
        else return NotFound();
        bool success = await _userRepository.Update(user);
        if (!success) return BadRequest();
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromHeader] int id)
    {
        bool success = await _userRepository.Delete(id);
        if (!success) return NotFound();
        return NoContent();
    }

    [AllowAnonymous] // User does not need to be authenticated to be albe to login
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
    {
        var user = await _userRepository.Login(userLoginDTO.Email, userLoginDTO.Password);

        if (user == null) return Unauthorized();
        var token = _authenticatorHelper.GenerateJWTToken(user);
        return Ok(token);
    }
}