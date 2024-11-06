using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieWebApi.DTO.User_DTO;
using MovieWebApi.Extensions;
using MovieWebApi.Helpers;

namespace MovieWebApi.Controllers.UserStuff
{
    [Authorize]
    [ApiController]
    [Route("api/bookmarks/title")]
    public class UserTitleBookmarkController : GenericController
    {
        public record CreateUserTitleBookmark(string TitleId, string Annotation);
        public record UpdateUserTitleBookmark(string annotation);

        private readonly UserTitleBookmarkRepository _userTitleBookmarkRepository;
        public UserTitleBookmarkController(UserTitleBookmarkRepository userTitleBookmarkRepository,LinkGenerator linkGenerator, UserRepository userRepository, AuthenticatorExtension authenticatorHelper) : base(linkGenerator, userRepository, authenticatorHelper)
        {
            _userTitleBookmarkRepository = userTitleBookmarkRepository;
        }
        
        [HttpGet("{titleId}", Name = nameof(GetTitleBookmark))]
        public async Task<IActionResult> GetTitleBookmark([FromHeader] int userId, [FromHeader] string Authorization, string titleId)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;

            var result = (await _userTitleBookmarkRepository.Get(userId, titleId));
            if (result == null) return NotFound();
            var finalResult = result.Spawn_DTO_WithPagination<UserBookmarkDTO, UserTitleBookmarkModel>(HttpContext, _linkGenerator, nameof(GetTitleBookmark));

            return Ok(finalResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader] int userId, [FromHeader] string Authorization)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;

            var mappedResult = (await _userTitleBookmarkRepository.GetAll(userId)).Select(x => x.Spawn_DTO_WithPagination<UserBookmarkDTO, UserTitleBookmarkModel>(HttpContext, _linkGenerator, nameof(GetTitleBookmark)));
            
            if (!mappedResult.Any() || mappedResult == null) return NotFound();

            return Ok(mappedResult);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] int userId, CreateUserTitleBookmark userTitleBookmark, [FromHeader] string Authorization)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;

            var d = new UserTitleBookmarkModel();
            d.UserId = userId;
            d.Annotation = userTitleBookmark.Annotation;  // space for improvement
            d.TitleId = userTitleBookmark.TitleId;

            var success = await _userTitleBookmarkRepository.Add(d);
            if (!success) return BadRequest();
            return NoContent();
        }

        [HttpPut("{titleId}")]
        public async Task<IActionResult> Put([FromHeader] int userId, string titleId, UpdateUserTitleBookmark updateUserTitleBookmark, [FromHeader] string Authorization)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;
            UserTitleBookmarkModel titleBookmark = await _userTitleBookmarkRepository.Get(userId, titleId);
            if (titleBookmark != null)
            {
                titleBookmark.Annotation = updateUserTitleBookmark.annotation != "" ? updateUserTitleBookmark.annotation : titleBookmark.Annotation;
            }
            else return NotFound();

            bool success = await _userTitleBookmarkRepository.Update(titleBookmark);
            if (!success) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{titleId}")]
        public async Task<IActionResult> Delete([FromHeader] int userId, string titleId, [FromHeader] string Authorization)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;

            bool success = await _userTitleBookmarkRepository.DeleteTitleBookmark(userId, titleId);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll([FromHeader] int userId, [FromHeader] string Authorization)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;

            bool success = await _userTitleBookmarkRepository.DeleteAllTitleBookmarks(userId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
