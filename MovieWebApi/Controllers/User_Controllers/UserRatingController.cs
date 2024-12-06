using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieDataLayer;
using MovieDataLayer.Data_Service.User_Framework_Repository;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.DTO.User_DTO;
using MovieWebApi.Extensions;
using static MovieWebApi.Controllers.IMDB_Controllers.GenreController;

namespace MovieWebApi.Controllers.User_Controllers
{
    [Authorize]
    [EnableCors("_myAllowSpecificOrigins")]
    [ApiController]
    [Route("api/users/rating")]
    public class UserRatingController : GenericController
    {
        public record CreateUserRating(string TitleId, float Rating);
        public record PutUserRating(float Rating);

        readonly UserRatingRepository _userRatingRepository;

        public UserRatingController(UserRatingRepository userRatingRepository, UserRepository userRepository, AuthenticatorExtension authenticatorExtension, LinkGenerator linkGenerator) : base(linkGenerator, userRepository, authenticatorExtension)
        {
            _userRatingRepository = userRatingRepository;
        }

        [HttpGet("{titleId}", Name = nameof(Get))]
        public async Task<IActionResult> Get([FromHeader] string authorization, string titleId)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);
            var rating = await _userRatingRepository.GetUserRating(userId, titleId);
            if (rating == null) return NotFound();
            var result = rating.Spawn_DTO_WithPagination<UserRatingDTO, UserRatingModel>(HttpContext, _linkGenerator, nameof(Get));
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader] string authorization)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);
            var result = (await _userRatingRepository.GetAllUserRatingByUserId(userId)).Select(rating => rating.Spawn_DTO_WithPagination<UserRatingDTO, UserRatingModel>(HttpContext, _linkGenerator, nameof(Get)));
            //var result = (await _dataService.GetAllWithPaging(page, pageSize)).Select(genre => genre.Spawn_DTO_WithPagination<ReadGenreModel, GenreModel>(HttpContext, _linkGenerator, nameof(GetAll)));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] string authorization, CreateUserRating createUserRating)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);

            var _userRating = createUserRating.Spawn_DTO<UserRatingModel, CreateUserRating>(); // from dto to domain model, we go in reverse
            _userRating.UserId = userId;

            var success = await _userRatingRepository.Add(_userRating);

            if (!success) return BadRequest();
            return NoContent();
        }

        [HttpPut("{titleId}")]
        public async Task<IActionResult> Put([FromHeader] string authorization, PutUserRating rating, [FromRoute] string titleId)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);
            UserRatingModel userRating = await _userRatingRepository.GetUserRating(userId, titleId);
            if (userRating != null)
            {
                userRating.Rating = rating != default ? rating.Rating : userRating.Rating;
            }
            else return NotFound();

            bool success = await _userRatingRepository.Update(userRating);
            if (!success) return BadRequest();
            return NoContent();
        }

        [HttpDelete("{titleId}")]
        public async Task<IActionResult> Delete([FromHeader] string authorization, string titleId)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);
            bool success = await _userRatingRepository.DeleteUserRating(userId, titleId);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll([FromHeader] string authorization)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);
            bool success = await _userRatingRepository.DeleteAllUserRatings(userId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
