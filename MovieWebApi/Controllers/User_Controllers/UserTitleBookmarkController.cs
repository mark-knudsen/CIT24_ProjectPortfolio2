using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieDataLayer;
using MovieDataLayer.Data_Service.User_Framework_Repository;
using MovieDataLayer.DataService.IMDB_Repository;
using MovieWebApi.DTO.User_DTO;
using MovieWebApi.Extensions;

namespace MovieWebApi.Controllers.User_Controllers
{
    [Authorize]
    [EnableCors("_myAllowSpecificOrigins")]
    [ApiController]
    [Route("api/bookmarks/title")]
    public class UserTitleBookmarkController : GenericController
    {
        public record CreateUserTitleBookmark(string TitleId, string Annotation);
        public record UpdateUserTitleBookmark(string annotation);

        private readonly UserTitleBookmarkRepository _userTitleBookmarkRepository;
        public UserTitleBookmarkController(UserTitleBookmarkRepository userTitleBookmarkRepository, LinkGenerator linkGenerator, UserRepository userRepository, AuthenticatorExtension authenticatorHelper) : base(linkGenerator, userRepository, authenticatorHelper)
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

        [HttpGet(Name = nameof(GetAllTitleBookmarks))]
        public async Task<IActionResult> GetAllTitleBookmarks([FromHeader] string authorization, [FromQuery] int page = 0, int pageSize = 10)
        {
            // why not just set the defualt values if they values are invalid, no reason to throw a whole error in a ussers face?
            if (page < 0 || pageSize < 0) return BadRequest("Page and PageSize must be 0 or greater"); //If time, add this check to other endpoints too.. 

            int userId = _authenticatorExtension.ExtractUserID(authorization);

            var titleBookmarks = (await _userTitleBookmarkRepository.GetAll(userId, page, pageSize)).Select(x => x.Spawn_DTO_WithPagination<UserBookmarkDTO, UserTitleBookmarkModel>(HttpContext, _linkGenerator, nameof(GetTitleBookmark)));

            if (!titleBookmarks.Any() || titleBookmarks == null) return NotFound();

            var numberOfEntities = await _userTitleBookmarkRepository.NumOfElemInUserTable(userId);

            object result = CreatePaging(nameof(GetAllTitleBookmarks), page, pageSize, numberOfEntities, titleBookmarks);
            if (result == null) return StatusCode(500, "Error while creating paginating in GetAllTitleBookmarks"); //Custom StatusCode & message

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] string authorization, CreateUserTitleBookmark userTitleBookmark)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);

            var _userTitleBookmark = userTitleBookmark.Spawn_DTO<UserTitleBookmarkModel, CreateUserTitleBookmark>(); // from dto to domain model, we go in reverse
            _userTitleBookmark.UserId = userId;

            //var _userTitleBookmark = new UserTitleBookmarkModel(); // instead of all this
            //_userTitleBookmark.UserId = userId;
            //_userTitleBookmark.Annotation = userTitleBookmark.Annotation; // space for improvement
            //_userTitleBookmark.TitleId = userTitleBookmark.TitleId;

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
            int userId = _authenticatorExtension.ExtractUserID(authorization);

            bool success = await _userTitleBookmarkRepository.DeleteTitleBookmark(userId, titleId);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll([FromHeader] string authorization)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);

            bool success = await _userTitleBookmarkRepository.DeleteAllTitleBookmarks(userId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
