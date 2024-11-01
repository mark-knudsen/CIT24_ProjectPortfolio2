using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieWebApi.DTO;
using MovieWebApi.Extensions;
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
        public UserPersonBookmarkController(UserPersonBookmarkRepository userPersonBookmarkRepository)
        {
            _userPersonBookmarkRepository = userPersonBookmarkRepository;
        }
        [HttpPost]
        public async Task<IActionResult> PostUserPersonBookmark([FromHeader] int userId, CreateUserPersonBookmark userPersonBookmark)
        {
            var _userPersonBookmark = new UserPersonBookmark();
            _userPersonBookmark.UserId = userId;
            _userPersonBookmark.Annotation = userPersonBookmark.Annotation; // improve when use authentication
            _userPersonBookmark.PersonId = userPersonBookmark.PersonId;

            var success = await _userPersonBookmarkRepository.Add(_userPersonBookmark);
            if (!success) return BadRequest();
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersonBookmarks([FromHeader] int id)
        {
            var result = (await _userPersonBookmarkRepository.GetAllPersonBookmarks(id)).Select(DTO_Extensions.Spawn_DTO<UserBookmarkDTO, UserPersonBookmark>);

            if (!result.Any() || result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{personId}")]
        public async Task<IActionResult> DeletePersonBookmark([FromHeader] int userId, string personId)
        {
            bool success = await _userPersonBookmarkRepository.DeletePersonBookmark(userId, personId);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllPersonBookmarks([FromHeader] int userId)
        {
            bool success = await _userPersonBookmarkRepository.DeleteAllPersonBookmarks(userId);
            if (!success) return NotFound();
            return NoContent();
        }

        
        [HttpPut("{personId}")]
        public async Task<IActionResult> PutPersonBookmark([FromHeader] int userId, string personId, UpdateUserPersonBookmark updateUserPersonBookmark)
        {
            UserPersonBookmark personBookmark = await _userPersonBookmarkRepository.GetPersonBookmark(userId, personId);
            if (personBookmark != null)
            {
                personBookmark.Annotation = updateUserPersonBookmark.Annotation != "" ? updateUserPersonBookmark.Annotation : personBookmark.Annotation;
            }
            else return NotFound();

            bool success = await _userPersonBookmarkRepository.Update(personBookmark);
            if (success) return NoContent();
            return BadRequest();
        }
    }
}
