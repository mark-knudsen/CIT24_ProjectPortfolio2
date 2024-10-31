using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieWebApi.DTO;
using MovieWebApi.Extensions;
using static MovieWebApi.Controllers.UserStuff.UserController;

namespace MovieWebApi.Controllers.UserStuff
{

    [ApiController]
    [Route("api/bookmarks/title")]
    public class UserTitleBookmarkController : ControllerBase
    {

        public record UpdateUserTitleBookmark(string annotation);

        readonly UserTitleBookmarkRepository _userTitleBookmarkRepository;

        public UserTitleBookmarkController(UserTitleBookmarkRepository userTitleBookmarkRepository)
        {
            _userTitleBookmarkRepository = userTitleBookmarkRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTitleBookmarks([FromHeader] int id)
        {
            var result = (await _userTitleBookmarkRepository.GetAllTitleBookmarks(id)).Select(DTO_Extensions.Spawn_DTO<UserBookmarkDTO, UserTitleBookmark>);

            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{titleId}")]
        public async Task<IActionResult> DeleteTitleBookmark([FromHeader] int userId, string titleId)
        {
            bool success = await _userTitleBookmarkRepository.DeleteTitleBookmark(userId, titleId);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllTitleBookmarks([FromHeader] int userId)
        {
            bool success = await _userTitleBookmarkRepository.DeleteAllTitleBookmarks(userId);
            if (!success) return NotFound();
            return NoContent();
        }


        [HttpPut("{titleId}")]
        public async Task<IActionResult> UpdateTitleBookmark([FromHeader] int userId, string titleId, UpdateUserTitleBookmark updateUserTitleBookmark)
        {
            UserTitleBookmark titleBookmark = await _userTitleBookmarkRepository.GetTitleBookmark(userId, titleId);
            if (titleBookmark != null)
            {

                titleBookmark.Annotation = updateUserTitleBookmark.annotation != "" ? updateUserTitleBookmark.annotation : titleBookmark.Annotation;

            }
            else return NotFound();

            bool success = await _userTitleBookmarkRepository.UpdateTitleBookmark(titleBookmark);
            if (success) return NoContent();
            return BadRequest();
        }

        //[HttpGet("{id}/bookmarks/person")]
        //public async Task<IActionResult> GetAllPersonBookmarks(int id)
        //{
        //    var result = (await _userTitleBookmarkRepository.GetAllPersonBookmarks(id)).Select(DTO_Extensions.Spawn_DTO<UserBookmarkDTO, UserPersonBookmark>);

        //    if (result == null) return NotFound();
        //    return Ok(result);
        //}

    }
}
