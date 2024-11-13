using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.Data_Service.User_Framework_Repository;
using MovieWebApi.DTO.User_DTO;
using MovieWebApi.Extensions;
using System.Security.Claims;

namespace MovieWebApi.Controllers.UserStuff;

[Authorize]
[ApiController]
[Route("api/user")]
public class UserController : GenericController
{
    public record UpdateUserModel(string email, string firstName, string password);
    public UserController(UserRepository userRepository, LinkGenerator linkGenerator, AuthenticatorExtension authenticatorExtension) : base(linkGenerator, userRepository, authenticatorExtension)
    {
    }

    [HttpGet("user-profile")]
    public async Task<IActionResult> GetById([FromHeader] string authorization)
    {
        int userId = _authenticatorExtension.ExtractUserID(authorization);
        var result = (await _userRepository.Get(userId)).Spawn_DTO<UserDTO, UserModel>();
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromHeader] string authorization, int page = 0, int pageSize = 10)
    {
        var result = (await _userRepository.GetAllWithPaging(page = 0, pageSize = 10)).Select(user => user.Spawn_DTO_WithPagination<UserDTO, UserModel>(HttpContext, _linkGenerator, nameof(GetAll))); // maybe never retrieve the password, just a thought you know!
        return Ok(result);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Post(UserRegistrationDTO userRegistrationDTO) // password 8 char speciale
    {
        var result = Extension.Spawn_DTO<UserModel, UserRegistrationDTO>(userRegistrationDTO);
        bool success = await _userRepository.Add(result);

        // this should return a token instead, no url plzz
        if (!success) return BadRequest();

        //var token = _authenticatorExtension.GenerateJWTToken(user); // how do we get the newly created user?
        //return Ok(token);

        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromHeader] string authorization, UpdateUserModel updateUserModel)
    {
        int userId = _authenticatorExtension.ExtractUserID(authorization);
        UserModel user = await _userRepository.Get(userId);
        if (user != null)
        {
            user.Email = updateUserModel.email != "" ? updateUserModel.email : user.Email;
            user.FirstName = updateUserModel.firstName != "" ? updateUserModel.firstName : user.FirstName;
            user.Password = updateUserModel.password != "" ? updateUserModel.password : user.Password; // would argue to make a request solely for changeing password
        }
        else return NotFound();
        bool success = await _userRepository.Update(user);
        if (!success) return BadRequest();

        var token = _authenticatorExtension.GenerateJWTToken(user);

        return Ok(token); // changed from NoContent to OK(Token)
    }
    
    //[HttpPut] // make new password
    //public async Task<IActionResult> Put([FromHeader] string authorization, string password)
    //{
    //    int userId = _authenticatorExtension.ExtractUserID(authorization);
    //    UserModel user = await _userRepository.Get(userId);

    //    if (user != null) // should we care to check in the web api, or solely do it in db, and then depending on what the db does throws of error we then react accordingly
    //    {
    //         user.Password = password;   
    //    }
    //    else return NotFound();

    //    bool success = await _userRepository.Update(user);
    //    if (!success) return BadRequest();

    //    var token = _authenticatorExtension.GenerateJWTToken(user);

    //    return Ok(token); 
    //}

    [HttpDelete]
    public async Task<IActionResult> Delete([FromHeader] string authorization)
    {
        int userId = _authenticatorExtension.ExtractUserID(authorization);
        bool success = await _userRepository.Delete(userId);
        if (!success) return NotFound();
        return NoContent();
    }

    [AllowAnonymous] // User does not need to be authenticated to be albe to login
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
    {
        var user = await _userRepository.Login(userLoginDTO.Email, userLoginDTO.Password);

        if (user == null) return Unauthorized(); 
        var token = _authenticatorExtension.GenerateJWTToken(user);
        return Ok(token);
    }
}
