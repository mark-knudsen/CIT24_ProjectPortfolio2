using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieWebApi.DTO.User_DTO;
using MovieWebApi.Extensions;

namespace MovieWebApi.Controllers.UserStuff
{
    [ApiController]
    [Route("api/users/rating")]
    public class UserRatingController : ControllerBase
    {
        public record CreateUserRating(string TitleId, float Rating);

        readonly UserRatingRepository _userRatingRepository;

        public UserRatingController(UserRatingRepository userRatingRepository)
        {
            _userRatingRepository = userRatingRepository;
        }

        [HttpGet("{titleId}")]
        public async Task<IActionResult> Get([FromHeader] int userId, string titleId)
        {
            var rating = Extension.Spawn_DTO<UserRatingDTO, UserRatingModel>(await _userRatingRepository.GetUserRating(userId, titleId));
            if (rating == null) return NotFound();

            return Ok(rating);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader] int userId)
        {
            var result = (await _userRatingRepository.GetAllUserRatingByUserId(userId)).Select(Extension.Spawn_DTO<UserRatingDTO, UserRatingModel>);

            if (result == null) return NotFound();
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] int userId, CreateUserRating createUserRating)
        {
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

        [HttpPut()]
        public async Task<IActionResult> Put([FromHeader] int userId, string titleId, double rating)
        {
            UserRatingModel userRating = await _userRatingRepository.GetUserRating(userId, titleId);
            if (userRating != null)
            {
                userRating.Rating = rating != default ? rating : userRating.Rating;
            }
            else return NotFound();

            bool success = await _userRatingRepository.Update(userRating);
            if (!success) return BadRequest();
            return NoContent();
        }
        [HttpDelete("{titleId}")]
        public async Task<IActionResult> Delete([FromHeader] int userId, string titleId)
        {
            bool success = await _userRatingRepository.DeleteUserRating(userId, titleId);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll([FromHeader] int userId)
        {
            bool success = await _userRatingRepository.DeleteAllUserRatings(userId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
