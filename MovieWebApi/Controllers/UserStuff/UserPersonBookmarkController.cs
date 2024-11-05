using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MovieDataLayer;
using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.DTO;
using MovieWebApi.Extensions;
using MovieWebApi.Helpers;
using System.Net;
using static MovieWebApi.Controllers.UserStuff.UserTitleBookmarkController;

namespace MovieWebApi.Controllers.UserStuff
{
    [ApiController]
    [Route("api/bookmarks/person")]
    public class UserPersonBookmarkController : ControllerBase
    {
        public record CreateUserPersonBookmark(string PersonId, string Annotation);
        public record UpdateUserPersonBookmark(string Annotation);
        readonly UserPersonBookmarkRepository _userPersonBookmarkRepository;
        private readonly UserRepository _userRepository;
        private readonly AuthenticatorHelper _authenticatorHelper;
        private readonly LinkGenerator _linkGenerator;

        public UserPersonBookmarkController(UserPersonBookmarkRepository userPersonBookmarkRepository, UserRepository userRepository, AuthenticatorHelper authenticatorHelper, LinkGenerator linkGenerator)
        {
            _userPersonBookmarkRepository = userPersonBookmarkRepository;
            _authenticatorHelper = authenticatorHelper;
            _userRepository = userRepository;
            _linkGenerator = linkGenerator;
        }

        [HttpPost(Name = nameof(Post))]
        public async Task<IActionResult> Post([FromHeader] int userId, CreateUserPersonBookmark userPersonBookmark, [FromHeader] string Authorization)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;

            var _userPersonBookmark = new UserPersonBookmark();
            _userPersonBookmark.UserId = userId;
            _userPersonBookmark.Annotation = userPersonBookmark.Annotation; // improve when use authentication
            _userPersonBookmark.PersonId = userPersonBookmark.PersonId;

            var success = await _userPersonBookmarkRepository.Add(_userPersonBookmark);
            if (!success) return BadRequest();

            var result = (await _userPersonBookmarkRepository.Get(userId, userPersonBookmark.PersonId));
            if (result == null) return NotFound();
            var finalResult = result.Spawn_DTO<UserBookmarkDTO, UserPersonBookmark>(HttpContext, _linkGenerator, nameof(Post));


            return Ok(finalResult);
        }

        [HttpGet("{personId}", Name = nameof(GetBookmark))]
        public async Task<IActionResult> GetBookmark([FromHeader] int userId, [FromHeader] string Authorization, string personId)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;

            var result = (await _userPersonBookmarkRepository.Get(userId, personId));
            if (result == null) return NotFound();
            var finalResult = result.Spawn_DTO<UserBookmarkDTO, UserPersonBookmark>(HttpContext, _linkGenerator, nameof(GetBookmark));

            return Ok(finalResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader] int userId, [FromHeader] string Authorization)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;

            var result = (await _userPersonBookmarkRepository.GetAll(userId)).Select(DTO_Extensions.Spawn_DTO_Old<UserBookmarkDTO, UserPersonBookmark>);

            if (!result.Any() || result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{personId}")]
        public async Task<IActionResult> Delete([FromHeader] int userId, string personId, [FromHeader] string Authorization)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;

            bool success = await _userPersonBookmarkRepository.Delete(userId, personId);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll([FromHeader] int userId, [FromHeader] string Authorization)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;

            bool success = await _userPersonBookmarkRepository.DeleteAll(userId);
            if (!success) return NotFound();
            return NoContent();
        }


        [HttpPut("{personId}")]
        public async Task<IActionResult> Put([FromHeader] int userId, string personId, UpdateUserPersonBookmark updateUserPersonBookmark, [FromHeader] string Authorization)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;

            UserPersonBookmark personBookmark = await _userPersonBookmarkRepository.Get(userId, personId);
            if (personBookmark != null)
            {
                personBookmark.Annotation = updateUserPersonBookmark.Annotation != "" ? updateUserPersonBookmark.Annotation : personBookmark.Annotation;
            }
            else return NotFound();

            bool success = await _userPersonBookmarkRepository.Update(personBookmark);
            if (success) return NoContent();
            return BadRequest();
        }

        private async Task<StatusCodeResult> Validate(int id, string Authorization)
        {
            var user = await _userRepository.Get(id);
            if (user == null) return BadRequest();
            bool isValidUser = _authenticatorHelper.ValidateUser(Authorization, user.Id, user.Email);

            if (!isValidUser) return Unauthorized();
            else return null;
        }
    }
}
