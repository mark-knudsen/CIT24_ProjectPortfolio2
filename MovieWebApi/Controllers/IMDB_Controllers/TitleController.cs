﻿using Microsoft.AspNetCore.Mvc;
using MovieDataLayer.DataService.IMDB_Repository;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.Extensions;
using MovieDataLayer.Data_Service.User_Framework_Repository;
using MovieWebApi.SearchDTO;
using MovieWebApi.DTO.IMDB_DTO;
using Microsoft.AspNetCore.Cors;

namespace MovieWebApi.Controllers.IMDB_Controllers
{
   // [DisableCors]
    [EnableCors("_myAllowSpecificOrigins")]
    [ApiController]
    [Route("api/titles")]
    public class TitleController : GenericController
    {
        private readonly TitleRepository _titleRepository;
        public TitleController(TitleRepository titleRepository, LinkGenerator linkGenerator, UserRepository userRepository, AuthenticatorExtension authenticatorHelper) : base(linkGenerator, userRepository, authenticatorHelper)
        {
            _titleRepository = titleRepository;
        }

        [HttpGet("{id}", Name = nameof(GetTitle))] // this is not allowed to be name Get
        public async Task<IActionResult> GetTitle(string id) // id tt9126600
        {
            var title = (await _titleRepository.GetTitle(id)).MapTitleToTitleDetailedDTO(HttpContext, _linkGenerator, nameof(GetTitle)); //Generic use of Method from DTO_Extensions, add URL to DTO
            if (title == null) return NotFound();

            return Ok(title);
        }

        // [DisableCors]
        [HttpGet(Name = nameof(GetAllTitles))] // this is not allowed to be named GetAll
        public async Task<IActionResult> GetAllTitles(int page = 0, int pageSize = 10) // We really just want the plot and poster at all times in the title, same with some of the collections
        {
            // why not just set the defualt values if they values are invalid, no reason to throw a whole error in a ussers face?
            if (page < 0 || pageSize < 0) return BadRequest("Page and PageSize must be 0 or greater"); //If time, add this check to other endpoints too.. 

            //Generic use of Spawn_DTO, including URL mapped to the DTO
            var titles = (await _titleRepository.GetAllTitles(page, pageSize)).Select(title => title.Spawn_DTO_WithPagination<TitleSimpleDTO, TitleModel>(HttpContext, _linkGenerator, nameof(GetTitle)));
            if (titles == null || !titles.Any()) return NotFound();

            var numberOfEntities = await _titleRepository.NumberOfElementsInTable();
            //titles = CreateNavigationForTitleList(titles.ToList());

            object result = CreatePaging(nameof(GetAllTitles), page, pageSize, numberOfEntities, titles);
            if (result == null) return StatusCode(500, "Error while creating paginating in GetAllTitles"); //Custom StatusCode & message

            return Ok(result);
        }

        //Not able to give URL/Path
        //[HttpGet("genre/{id?}")]
        [HttpGet("genre/{id}", Name = nameof(GetTitleByGenre))]
        public async Task<IActionResult> GetTitleByGenre(int id, int page = 0, int pageSize = 10) // id tt7856872
        {
            var titles = await _titleRepository.GetTitleByGenre(id, page, pageSize);
            if (titles == null) return NotFound();

            var numberOfEntities = await _titleRepository.NumberOfElementsInTable();
            var titleDTOs = titles.Select(title => title.MapTitleToTitleDetailedDTO(HttpContext, _linkGenerator, nameof(GetTitle)));
            object result = CreatePaging(nameof(GetTitleByGenre), page, pageSize, numberOfEntities, titleDTOs, id);
            return Ok(result);
        }

        [HttpGet("search/{id}", Name = nameof(SearchTitle))]
        public async Task<IActionResult> SearchTitle([FromHeader] string? authorization, string id, int page = 0, int pageSize = 10) 
        {
            int userId = 0;
            if(authorization != null) userId = _authenticatorExtension.ExtractUserID(authorization);
            var searchResult = (await _titleRepository.TitleSearch(userId, id,page, pageSize)).Select(tSearch => tSearch.Spawn_DTO_WithPagination<TitleSearchResultDTO, TitleSearchResultTempTable>(HttpContext, _linkGenerator, nameof(GetTitle)));
           
            if (searchResult == null || !searchResult.Any()) return NotFound();
           // searchResult = CreateNavigationForSearchList(searchResult);
            //var numberOfEntities = await _titleRepository.NumberOfElementsInTable();

            //object result = CreatePaging(nameof(SearchTitle), page, pageSize, numberOfEntities, searchResult);
            object result = CreatePaging(nameof(SearchTitle), page, pageSize, searchResult.Count(), searchResult, id); //Does does work
            return Ok(result);
        }

        [HttpGet("similar-titles")] // Discuss if it is ok to use this URL!
        public async Task<IActionResult> SimilarTitles(string titleId) 
        {
            var result = await _titleRepository.SimilarTitles(titleId);
            return Ok(result);
        }

        // should use the extension spawn dto instead
        private IEnumerable<TitleSearchResultDTO>? CreateNavigationForSearchList(IEnumerable<TitleSearchResultDTO>? searchResultDTO)
        {
            if (searchResultDTO == null || !searchResultDTO.Any())
            {
                return null;
            }

            foreach (var title in searchResultDTO)
            {
                title.Url = GetUrl(nameof(GetTitle), new { title.TitleId });
            }
            return searchResultDTO;
        }
    }
}

