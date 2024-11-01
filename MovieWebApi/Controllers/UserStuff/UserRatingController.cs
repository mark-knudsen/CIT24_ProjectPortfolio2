﻿using Mapster;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.DataService.IMDB_Repository;
using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieWebApi.Extensions;

namespace MovieWebApi.Controllers.UserStuff
{
    [ApiController]
    [Route("api/users/rating")]
    public class UserRatingController : ControllerBase

    {
        public record CreateUserRating(string TitleId, float Rating);

       // public record UpdateUserModel(string email, string firstName, string password);
        readonly UserRatingRepository _userRatingRepository;

        public UserRatingController(UserRatingRepository userRatingRepository)
        {
            _userRatingRepository = userRatingRepository;
        }

        [HttpGet("{titleId}")]

        public async Task<IActionResult> Get([FromHeader]int userId, string titleId) 
        {

            var rating = DTO_Extensions.Spawn_DTO<UserRatingDTO, UserRating>(await _userRatingRepository.GetUserRating(userId, titleId));
            if (rating == null) return NotFound();


            return Ok(rating);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromHeader]int userId)
        {
            var result = (await _userRatingRepository.GetAllUserRatingByUserId(userId)).Select(DTO_Extensions.Spawn_DTO<UserRatingDTO, UserRating>);

            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromHeader] int userId, string titleId) 
        {
            bool success = await _userRatingRepository.DeleteUserRating(userId, titleId);
            if (!success) return NotFound();
            return NoContent();
        }

        //[HttpDelete]
        //public async Task<IActionResult> DeleteAll(int userId) {
        //    bool success = await _userRatingRepository.DeleteAllUserRatings(userId);
        //    if (!success) return NotFound();
        //    return NoContent();
        //}
        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] int userId, CreateUserRating userRating)
        {
            var _userRating = new UserRating();
           
            _userRating.Adapt<CreateUserRating>();
            _userRating.UserId = userId;

            var success = await _userRatingRepository.Add(_userRating);

            if (!success) return BadRequest();
            return NoContent();
            
        }
    }
}
