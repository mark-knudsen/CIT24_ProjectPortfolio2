using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieWebApi.DTO;
using MovieWebApi.Extensions;

namespace MovieWebApi.Controllers.UserStuff
{

    [ApiController]
    [Route("api/bookmarks/title")]
    public class UserTitleBookmarkController : ControllerBase
    {

        public record CreateUserTitleBookmark(string TitleId, string Annotation);
        public record UpdateUserTitleBookmark(string annotation);

        readonly UserTitleBookmarkRepository _userTitleBookmarkRepository;

        public UserTitleBookmarkController(UserTitleBookmarkRepository userTitleBookmarkRepository)
        {
            _userTitleBookmarkRepository = userTitleBookmarkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] int userId, CreateUserTitleBookmark userTitleBookmark)
        {
            var d = new UserTitleBookmark();
            d.UserId = userId;
            d.Annotation = userTitleBookmark.Annotation; // improve when use authentication
            d.TitleId = userTitleBookmark.TitleId;

            var success = await _userTitleBookmarkRepository.Add(d);
            if (!success) return BadRequest();
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader] int id)
        {
            var result = (await _userTitleBookmarkRepository.GetAllTitleBookmarks(id)).Select(DTO_Extensions.Spawn_DTO_Old<UserBookmarkDTO, UserTitleBookmark>);

            if (!result.Any() || result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{titleId}")]
        public async Task<IActionResult> Delete([FromHeader] int userId, string titleId)
        {
            bool success = await _userTitleBookmarkRepository.DeleteTitleBookmark(userId, titleId);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll([FromHeader] int userId)
        {
            bool success = await _userTitleBookmarkRepository.DeleteAllTitleBookmarks(userId);
            if (!success) return NotFound();
            return NoContent();
        }


        [HttpPut("{titleId}")]
        public async Task<IActionResult> Put([FromHeader] int userId, string titleId, UpdateUserTitleBookmark updateUserTitleBookmark)
        {
            UserTitleBookmark titleBookmark = await _userTitleBookmarkRepository.GetTitleBookmark(userId, titleId);
            if (titleBookmark != null)
            {
                titleBookmark.Annotation = updateUserTitleBookmark.annotation != "" ? updateUserTitleBookmark.annotation : titleBookmark.Annotation;
            }
            else return NotFound();

            bool success = await _userTitleBookmarkRepository.Update(titleBookmark);
            if (success) return NoContent();
            return BadRequest();
        }
    }
}
