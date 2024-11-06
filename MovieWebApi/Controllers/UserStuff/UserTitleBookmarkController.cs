using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using MovieDataLayer;
using MovieDataLayer.DataService.IMDB_Repository;
using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.DTO;
using MovieWebApi.Extensions;
using MovieWebApi.Helpers;
using System.Net;

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
        public UserTitleBookmarkController(UserTitleBookmarkRepository userTitleBookmarkRepository,LinkGenerator linkGenerator, UserRepository userRepository, AuthenticatorHelper authenticatorHelper) : base(linkGenerator, userRepository, authenticatorHelper)
        {
            _userTitleBookmarkRepository = userTitleBookmarkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] int userId, CreateUserTitleBookmark userTitleBookmark, [FromHeader] string Authorization)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;

            var d = new UserTitleBookmarkModel();
            d.UserId = userId;
            d.Annotation = userTitleBookmark.Annotation; // improve when use authentication
            d.TitleId = userTitleBookmark.TitleId;

            var success = await _userTitleBookmarkRepository.Add(d);
            if (!success) return BadRequest();
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader] int userId, [FromHeader] string Authorization)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;
            var result = (await _userTitleBookmarkRepository.GetAll(userId));

            var d = result.Select(x => x.Spawn_DTO<UserBookmarkDTO, UserTitleBookmarkModel>(HttpContext, _linkGenerator, nameof(GetTitleBookmark)));
            
            //var titles = (await _titleRepository.GetAllTitles(page, pageSize)).Select(title => title.Spawn_DTO<TitleSimpleDTO, Title>(HttpContext, _linkGenerator, nameof(Get)));

            if (!d.Any() || d == null) return NotFound();

            return Ok(d);
        }

        [HttpGet("{titleId}", Name = nameof(GetTitleBookmark))]
        public async Task<IActionResult> GetTitleBookmark([FromHeader] int userId, [FromHeader] string Authorization, string titleId)
        {
            StatusCodeResult code = await Validate(userId, Authorization);
            if (code != null) return code;

            var result = (await _userTitleBookmarkRepository.Get(userId, titleId));
            if (result == null) return NotFound();
            var finalResult = result.Spawn_DTO<UserBookmarkDTO, UserTitleBookmarkModel>(HttpContext, _linkGenerator, nameof(GetTitleBookmark));

            return Ok(finalResult);
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
            if (success) return NoContent();
            return BadRequest();
        }
    }
}
