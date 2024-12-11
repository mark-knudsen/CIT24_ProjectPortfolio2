using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.Data_Service.User_Framework_Repository;
using MovieWebApi.DTO.User_DTO;
using MovieWebApi.Extensions;

namespace MovieWebApi.Controllers.User_Controllers
{
    [Authorize]
    [EnableCors("_myAllowSpecificOrigins")]
    [ApiController]
    [Route("api/search-history")]
    public class UserSearchHistoryController : GenericController
    {
        readonly UserSearchHistoryRepository _repository;
        public UserSearchHistoryController(UserSearchHistoryRepository userSearchHistoryRepository, UserRepository userRepository, LinkGenerator linkGenerator, AuthenticatorExtension authenticatorExtension) : base(linkGenerator, userRepository, authenticatorExtension)
        {
            _repository = userSearchHistoryRepository;
        }

        // get all
        // delete one
        // delete all

        // we will not create it here
        // we will NEVER update a search

        // question, would we want a user to see their whole search history or even be able to delete one or all of them?
        // what is the functionallity of search history of a user, sure maybe to see recent 10+ searches
        // would a user only see this when they want to search and see a list of what they have search for previously?
        // would you then ever want to delete user history, and technically you would want that data to inform the inverse index of what is popular titles,
        // altough that might be above our goals of implementation for the project.

        // in that aspect you would only have a get that takes a string search

        [HttpGet]
        public async Task<IActionResult> GetAllUserHistory([FromHeader] string authorization)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);
            var result = (await _repository.GetAllSearchHistoryByUserId(userId)).Select(user => user.Spawn_DTO_WithPagination<UserSearchHistoryDTO, UserSearchHistoryModel>(HttpContext, _linkGenerator, "GetAll"));

            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{createdAt}")]
        public async Task<IActionResult> Delete([FromHeader] string authorization, DateTime createdAt)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);

            bool success = await _repository.Delete(userId, createdAt);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll([FromHeader] string authorization)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);

            bool success = await _repository.DeleteAll(userId);
            if (!success) return NotFound();
            return NoContent();
        }

    }
}
