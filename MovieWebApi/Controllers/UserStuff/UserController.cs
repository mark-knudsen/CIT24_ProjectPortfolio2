using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MovieDataLayer;
using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.DTO;
using MovieWebApi.Extensions;
using MovieWebApi.Helpers;

namespace MovieWebApi.Controllers.UserStuff;
[ApiController]
[Route("api/user")]
public class UserController : GenericController
{
    public record UpdateUserModel(string email, string firstName, string password);
    readonly UserRepository _userRepository; //Private, explicit?
    private readonly LinkGenerator _linkGenerator;
    private readonly AuthenticatorHelper _authenticatorHelper;
    public UserController(UserRepository userRepository, LinkGenerator linkGenerator, AuthenticatorHelper authenticatorHelper) : base(linkGenerator)
    {
        _userRepository = userRepository;
        _linkGenerator = linkGenerator;
        _authenticatorHelper = authenticatorHelper;
    }



    [HttpGet("user-profile")] //Is this url ok?
    public async Task<IActionResult> GetById([FromHeader] int id)
    {

        var result = (await _userRepository.Get(id)).Spawn_DTO<UserDTO, User>(HttpContext, _linkGenerator, nameof(GetById));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int page = 0, int pageSize = 10)
    {
        var result = (await _userRepository.GetAllWithPaging(page = 0, pageSize = 10)).Select(user => user.Spawn_DTO<UserDTO, User>(HttpContext, _linkGenerator, nameof(GetAll))); // maybe never retrieve the password, just a thought you know!
        return Ok(result);
    }

    [HttpGet("search_history")]
    public async Task<IActionResult> GetAllUserHistory([FromHeader] int id)
    {
        var result = (await _userRepository.GetAllSearchHistoryByUserId(id)).Select(user => user.Spawn_DTO<UserSearchHistoryDTO, UserSearchHistory>(HttpContext, _linkGenerator, nameof(GetAll)));


        if (result == null) return NotFound();
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> RegisterUser(UserRegistrationDTO userRegistrationDTO)
    {
        var result = DTO_Extensions.Spawn_DTO_Old<User, UserRegistrationDTO>(userRegistrationDTO);
        bool success = await _userRepository.Add(result);

        if (!success) return BadRequest();
        return Created("", result); // add url later
    }

    [HttpPut]

    public async Task<IActionResult> Put([FromHeader] int id, UpdateUserModel updateUserModel)
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
    public async Task<IActionResult> Delete([FromHeader] int id)
    {
        bool success = await _userRepository.Delete(id);
        if (success) return NoContent();
        return NotFound();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
    {
        var user = await _userRepository.Login(userLoginDTO.Email, userLoginDTO.Password);

        if (user == null) return Unauthorized();
        var token = _authenticatorHelper.GenerateJWTToken(user);
        return Ok(token);
    }
}


