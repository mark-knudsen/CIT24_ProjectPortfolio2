using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieWebApi.DTO.User_DTO;
using MovieWebApi.Extensions;
using MovieWebApi.Helpers;

namespace MovieWebApi.Controllers.UserStuff
{
    [ApiController]
    [Route("api/bookmarks/person")]
    public class UserPersonBookmarkController : GenericController
    {
        public record CreateUserPersonBookmark(string PersonId, string Annotation);
        public record UpdateUserPersonBookmark(string Annotation);
        readonly UserPersonBookmarkRepository _userPersonBookmarkRepository;

        public UserPersonBookmarkController(UserPersonBookmarkRepository userPersonBookmarkRepository, LinkGenerator linkGenerator, UserRepository userRepository, AuthenticatorExtension authenticatorHelper) : base(linkGenerator, userRepository, authenticatorHelper)
        {
            _userPersonBookmarkRepository = userPersonBookmarkRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader] int userId, [FromHeader] string Authorization)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;

            var mappedResult = (await _userPersonBookmarkRepository.GetAll(userId)).Select(x => x.Spawn_DTO_WithPagination<UserBookmarkDTO, UserPersonBookmarkModel>(HttpContext, _linkGenerator, nameof(GetPersonBookmark)));

            if (!mappedResult.Any() || mappedResult == null) return NotFound();
            return Ok(mappedResult);
        }

        [HttpGet("{personId}", Name = nameof(GetPersonBookmark))]
        public async Task<IActionResult> GetPersonBookmark([FromHeader] int userId, [FromHeader] string Authorization, string personId)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;

            var result = (await _userPersonBookmarkRepository.Get(userId, personId));
            if (result == null) return NotFound();
            var finalResult = result.Spawn_DTO_WithPagination<UserBookmarkDTO, UserPersonBookmarkModel>(HttpContext, _linkGenerator, nameof(GetPersonBookmark));

            return Ok(finalResult);
        }

        [HttpPost(Name = nameof(Post))]
        public async Task<IActionResult> Post([FromHeader] int userId, CreateUserPersonBookmark userPersonBookmark, [FromHeader] string Authorization)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;

            var _userPersonBookmark = new UserPersonBookmarkModel();
            _userPersonBookmark.UserId = userId;
            _userPersonBookmark.Annotation = userPersonBookmark.Annotation; // space for improvement
            _userPersonBookmark.PersonId = userPersonBookmark.PersonId;

            var success = await _userPersonBookmarkRepository.Add(_userPersonBookmark);
            if (!success) return BadRequest();

            var result = (await _userPersonBookmarkRepository.Get(userId, userPersonBookmark.PersonId));
            if (result == null) return NotFound();
            var finalResult = result.Spawn_DTO_WithPagination<UserBookmarkDTO, UserPersonBookmarkModel>(HttpContext, _linkGenerator, nameof(Post));

            return Ok(finalResult);
        }

        [HttpPut("{personId}")]
        public async Task<IActionResult> Put([FromHeader] int userId, string personId, UpdateUserPersonBookmark updateUserPersonBookmark, [FromHeader] string Authorization)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;

            UserPersonBookmarkModel personBookmark = await _userPersonBookmarkRepository.Get(userId, personId);
            if (personBookmark != null)
            {
                personBookmark.Annotation = updateUserPersonBookmark.Annotation != "" ? updateUserPersonBookmark.Annotation : personBookmark.Annotation;
            }
            else return NotFound();

            bool success = await _userPersonBookmarkRepository.Update(personBookmark);
            if (!success) return BadRequest();
            return NoContent();
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
    }
}
