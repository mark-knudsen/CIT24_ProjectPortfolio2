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
        public async Task<IActionResult> GetTitleBookmark([FromHeader] string authorization, string titleId)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);

            var result = (await _userTitleBookmarkRepository.Get(userId, titleId));
            if (result == null) return NotFound();
            var finalResult = result.Spawn_DTO_WithPagination<UserBookmarkDTO, UserTitleBookmarkModel>(HttpContext, _linkGenerator, nameof(GetTitleBookmark));

            return Ok(finalResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader] string authorization)
        {
            //StatusCodeResult code = await Validate(userId, Authorization);
            //if (code != null) return code;

            int userId = _authenticatorExtension.ExtractUserID(authorization);

            var mappedResult = (await _userTitleBookmarkRepository.GetAll(userId)).Select(x => x.Spawn_DTO_WithPagination<UserBookmarkDTO, UserTitleBookmarkModel>(HttpContext, _linkGenerator, nameof(GetTitleBookmark)));
            
            if (!mappedResult.Any() || mappedResult == null) return NotFound();

            return Ok(mappedResult);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] string authorization, CreateUserTitleBookmark userTitleBookmark)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);

            var _userTitleBookmark = new UserTitleBookmarkModel();
            _userTitleBookmark.UserId = userId;
            _userTitleBookmark.Annotation = userTitleBookmark.Annotation; // space for improvement
            _userTitleBookmark.TitleId = userTitleBookmark.TitleId;

            var success = await _userTitleBookmarkRepository.Add(_userTitleBookmark);
            if (!success) return BadRequest();

            var result = (await _userTitleBookmarkRepository.Get(userId, userTitleBookmark.TitleId));
            if (result == null) return NotFound();
            var finalResult = result.Spawn_DTO_WithPagination<UserBookmarkDTO, UserTitleBookmarkModel>(HttpContext, _linkGenerator, nameof(Post));

            return Ok(finalResult);
        }

        [HttpPut("{titleId}")]
        public async Task<IActionResult> Put([FromHeader] string authorization, string titleId, UpdateUserTitleBookmark updateUserTitleBookmark)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);

            //StatusCodeResult code = await Validate(userId, Authorization);
            //if (code != null) return code;


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
        public async Task<IActionResult> Delete([FromHeader] string authorization, string titleId)
        {
            //StatusCodeResult code = await Validate(userId, Authorization);
            //if (code != null) return code;
            int userId = _authenticatorExtension.ExtractUserID(authorization);

            bool success = await _userTitleBookmarkRepository.DeleteTitleBookmark(userId, titleId);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll([FromHeader] string authorization)
        {

            int userId = _authenticatorExtension.ExtractUserID(authorization);
            //StatusCodeResult code = await Validate(userId, Authorization);
            //if (code != null) return code;

            bool success = await _userTitleBookmarkRepository.DeleteAllTitleBookmarks(userId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
