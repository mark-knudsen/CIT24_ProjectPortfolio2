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

        [HttpGet("{titleId}")]
        public async Task<IActionResult> Get([FromHeader] string authorization, string titleId)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);
            var rating = Extension.Spawn_DTO<UserRatingDTO, UserRatingModel>(await _userRatingRepository.GetUserRating(userId, titleId));
            if (rating == null) return NotFound();
            return Ok(rating);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader] string authorization)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);
            var result = (await _userRatingRepository.GetAllUserRatingByUserId(userId)).Select(Extension.Spawn_DTO<UserRatingDTO, UserRatingModel>);
            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] string authorization, CreateUserRating createUserRating)
        {
            int userId = _authenticatorExtension.ExtractUserID(authorization);
            var _userRating = new UserRatingModel();
            _userRating.UserId = userId;
            _userRating.TitleId = createUserRating.TitleId;
            _userRating.Rating = createUserRating.Rating;
            _userRating.CreatedAt = DateTime.Now;
            _userRating.UpdatedAt = DateTime.Now;

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
