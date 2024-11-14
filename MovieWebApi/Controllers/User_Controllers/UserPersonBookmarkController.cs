using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.Data_Service.User_Framework_Repository;
using MovieWebApi.DTO.User_DTO;
using MovieWebApi.Extensions;

namespace MovieWebApi.Controllers.UserStuff
{
    [Authorize]
    [ApiController]
    [Route("api/bookmarks/person")]
    public class UserPersonBookmarkController : GenericController
    {
        public record CreateUserPersonBookmark(string PersonId, string Annotation);
        public record UpdateUserPersonBookmark(string Annotation);
        readonly UserPersonBookmarkRepository _userPersonBookmarkRepository;

        public UserPersonBookmarkController(UserPersonBookmarkRepository userPersonBookmarkRepository, LinkGenerator linkGenerator, UserRepository userRepository, AuthenticatorExtension authenticatorExtension) : base(linkGenerator, userRepository, authenticatorExtension)
        {
            _userPersonBookmarkRepository = userPersonBookmarkRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader] string authorization)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);

            var mappedResult = (await _userPersonBookmarkRepository.GetAll(userId)).Select(x => x.Spawn_DTO_WithPagination<UserBookmarkDTO, UserPersonBookmarkModel>(HttpContext, _linkGenerator, nameof(GetPersonBookmark)));

            if (!mappedResult.Any() || mappedResult == null) return NotFound();
            return Ok(mappedResult);
        }

        [HttpGet("{personId}", Name = nameof(GetPersonBookmark))]
        public async Task<IActionResult> GetPersonBookmark([FromHeader] string authorization, string personId)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);

            var result = (await _userPersonBookmarkRepository.Get(userId, personId));
            if (result == null) return NotFound();
            var finalResult = result.Spawn_DTO_WithPagination<UserBookmarkDTO, UserPersonBookmarkModel>(HttpContext, _linkGenerator, nameof(GetPersonBookmark));

            return Ok(finalResult);
        }

        [HttpPost(Name = nameof(Post))]
        public async Task<IActionResult> Post([FromHeader] string authorization, CreateUserPersonBookmark userPersonBookmark)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);

            var _userPersonBookmark = userPersonBookmark.Spawn_DTO<UserPersonBookmarkModel, CreateUserPersonBookmark>(); // from dto to domain model, we go in reverse
            _userPersonBookmark.UserId = userId;

            var success = await _userPersonBookmarkRepository.Add(_userPersonBookmark);
            if (!success) return BadRequest();

            var result = (await _userPersonBookmarkRepository.Get(userId, userPersonBookmark.PersonId));
            if (result == null) return NotFound();
            var finalResult = result.Spawn_DTO_WithPagination<UserBookmarkDTO, UserPersonBookmarkModel>(HttpContext, _linkGenerator, nameof(Post));

            return Ok(finalResult);
        }

        [HttpPut("{personId}")]
        public async Task<IActionResult> Put([FromHeader] string authorization, string personId, UpdateUserPersonBookmark updateUserPersonBookmark)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);

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
        public async Task<IActionResult> Delete([FromHeader] string authorization, string personId)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);

            bool success = await _userPersonBookmarkRepository.Delete(userId, personId);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll([FromHeader] string authorization)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);

            bool success = await _userPersonBookmarkRepository.DeleteAll(userId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
