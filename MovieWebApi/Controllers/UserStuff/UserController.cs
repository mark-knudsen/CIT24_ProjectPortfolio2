using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MovieDataLayer;
using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.DTO;
using MovieWebApi.Extensions;
using MovieWebApi.Helpers;
using System.Net;

namespace MovieWebApi.Controllers.UserStuff;

[Authorize]
[ApiController]
[Route("api/user")]
public class UserController : GenericController
{
    public record UpdateUserModel(string email, string firstName, string password);
    public UserController(UserRepository userRepository, LinkGenerator linkGenerator, AuthenticatorHelper authenticatorHelper) : base(linkGenerator, userRepository, authenticatorHelper)
    {
    }

    [HttpGet("user-profile")] //Is this url ok?
    public async Task<IActionResult> GetById([FromHeader] int id)
    {
        var result = (await _userRepository.Get(id)).Spawn_DTO_Old<UserDTO, UserModel>();
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int page = 0, int pageSize = 10)
    {
        var result = (await _userRepository.GetAllWithPaging(page = 0, pageSize = 10)).Select(user => user.Spawn_DTO<UserDTO, UserModel>(HttpContext, _linkGenerator, nameof(GetAll))); // maybe never retrieve the password, just a thought you know!
        return Ok(result);
    }

    [HttpGet("search_history")]
    public async Task<IActionResult> GetAllUserHistory([FromHeader] int id)
    {
        var result = (await _userRepository.GetAllSearchHistoryByUserId(id)).Select(user => user.Spawn_DTO<UserSearchHistoryDTO, UserSearchHistoryModel>(HttpContext, _linkGenerator, nameof(GetAll)));


        if (result == null) return NotFound();
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> RegisterUser(UserRegistrationDTO userRegistrationDTO)
    {
        var result = DTO_Extensions.Spawn_DTO_Old<UserModel, UserRegistrationDTO>(userRegistrationDTO);
        bool success = await _userRepository.Add(result);

        if (!success) return BadRequest();
        return Created("", result); // add url later
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
        if (success) return NoContent();
        return BadRequest();
    }


    [HttpDelete]
    public async Task<IActionResult> Delete([FromHeader] int id)
    {
        bool success = await _userRepository.Delete(id);
        if (success) return NoContent();
        return NotFound();
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
