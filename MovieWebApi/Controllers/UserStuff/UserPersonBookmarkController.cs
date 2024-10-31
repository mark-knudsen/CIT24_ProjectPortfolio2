using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieWebApi.DTO;
using MovieWebApi.Extensions;

namespace MovieWebApi.Controllers.UserStuff
{
    [ApiController]
    [Route("api/bookmarks/person")]
    public class UserPersonBookmarkController : ControllerBase
    {
        readonly UserPersonBookmarkRepository _userPersonBookmarkRepository;
        public UserPersonBookmarkController(UserPersonBookmarkRepository userPersonBookmarkRepository)
        {
            _userPersonBookmarkRepository = userPersonBookmarkRepository;
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
        public async Task<IActionResult> DeleteAllTitleBookmarks([FromHeader] int userId)
        {
            bool success = await _userPersonBookmarkRepository.DeleteAllPersonBookmarks(userId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
