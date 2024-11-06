using Microsoft.AspNetCore.Mvc;
using MovieDataLayer.DataService.IMDB_Repository;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.Extensions;
using MovieDataLayer;
using MovieDataLayer.DataService.UserFrameworkRepository;
using Mapster;
using MovieWebApi.SearchDTO;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieWebApi.Controllers
{
    [ApiController]
    [Route("api/titles")]
    public class TitleController : GenericController
    {
        private readonly TitleRepository _titleRepository;

        private readonly LinkGenerator _linkGenerator;

        public TitleController(TitleRepository titleRepository, LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _titleRepository = titleRepository;
            _linkGenerator = linkGenerator;
        }

        [HttpGet("{id}", Name = nameof(Get))]
        public async Task<IActionResult> Get(string id) // id tt9126600
        {
            var title = (await _titleRepository.GetTitle(id)).MapTitleToTitleDetailedDTO(HttpContext, _linkGenerator, nameof(Get)); //Generic use of Method from DTO_Extensions, add URL to DTO
            if (title == null) return NotFound();

            return Ok(title);
        }

        [HttpGet(Name = nameof(GetAllTitles))]
        public async Task<IActionResult> GetAllTitles(int page = 0, int pageSize = 10) // We really just want the plot and poster at all times in the title, same with some of the collections
        {
            // why not just set the defualt values if they values are invalid, no reason to throw a whole error in a ussers face?
            if (page < 0 || pageSize < 0) return BadRequest("Page and PageSize must be 0 or greater"); //If time, add this check to other endpoints too.. 

            //Generic use of Spawn_DTO, including URL mapped to the DTO
            var titles = (await _titleRepository.GetAllTitles(page, pageSize)).Select(title => title.Spawn_DTO<TitleSimpleDTO, Title>(HttpContext, _linkGenerator, nameof(Get)));
            if (titles == null || !titles.Any()) return NotFound();

            var numberOfEntities = await _titleRepository.NumberOfElementsInTable();
            //titles = CreateNavigationForTitleList(titles.ToList());

            object result = CreatePaging(nameof(GetAllTitles), page, pageSize, numberOfEntities, titles);
            if (result == null) return StatusCode(500, "Error while creating paginating in GetAllTitles"); //Custom StatusCode & message


            return Ok(result);
        }


        //Not able to give URL/Path
        [HttpGet("genre/{id}", Name = nameof(GetTitleByGenre))]
        public async Task<IActionResult> GetTitleByGenre(int id, int page = 0, int pageSize = 10) // id tt7856872
        {
            if (id.Equals(null))
            {
                return BadRequest();
            }

            var titles = (await _titleRepository.GetTitleByGenre(id, page, pageSize));
            if (titles == null) return NotFound();

            var numberOfEntities = await _titleRepository.NumberOfElementsInTable();
            var titleDTOs = titles.Select(title => title.MapTitleToTitleDetailedDTO(HttpContext, _linkGenerator, nameof(Get)));
            object result = CreatePaging(nameof(GetTitleByGenre), page, pageSize, numberOfEntities, titleDTOs, id); //Now including id, so we can generate url for getting titles with a specific genre
            return Ok(result);
        }

        [HttpGet("search", Name = nameof(Search))] //This method only works when called with an userid. Aka no searching without being logged in... Should prop be fixed?..
        public async Task<IActionResult> Search([FromHeader] int userId, string searchTerm, int page = 0, int pageSize = 10) // should probably be authorized ALOT to be allowed to call this
        {
            if (userId <= 0) return BadRequest();

            var searchResult = (await _titleRepository.TitleSearch(userId, searchTerm)).MapTitleSearchResultModelToTitleSearchResultDTO();
            if (searchResult == null || !searchResult.Any()) return NotFound();
            searchResult = CreateNavigationForSearchList(searchResult);
            var numberOfEntities = await _titleRepository.NumberOfElementsInTable();

            object result = CreatePaging(nameof(GetAllTitles), page, pageSize, numberOfEntities, searchResult); //Navigation url currently doesn't work
            return Ok(result);
        }

        [HttpGet("similar-titles")] // Discuss if it is ok to use this URL!
        public async Task<IActionResult> SimilarTitles(string titleId) // should probably be authorized ALOT to be allowed to call this
        {
            var result = await _titleRepository.SimilarTitles(titleId);
            return Ok(result);
        }

        //Page Navigation

        private TitleDetailedDTO? CreateNavigationForTitle(TitleDetailedDTO? titleDTO)
        {
            if (titleDTO == null)
            {
                return null;
            }

            titleDTO.Url = GetUrl(nameof(Get), new { titleDTO.Id });

            return titleDTO;
        }

        private IEnumerable<TitleDetailedDTO>? CreateNavigationForTitleList(IEnumerable<TitleDetailedDTO>? titleDTO)
        {
            if (titleDTO == null || !titleDTO.Any())
            {
                return null;
            }

            foreach (var title in titleDTO)
            {
                title.Url = GetUrl(nameof(Get), new { title.Id });
            }
            return titleDTO;
        }

        private TitleSearchResultDTO? CreateNavigationForSearch(TitleSearchResultDTO? searchResultDTO)
        {
            if (searchResultDTO == null)
            {
                return null;
            }

            searchResultDTO.Url = GetUrl(nameof(Get), new { searchResultDTO.Id });

            return searchResultDTO;
        }

        private IEnumerable<TitleSearchResultDTO>? CreateNavigationForSearchList(IEnumerable<TitleSearchResultDTO>? searchResultDTO)
        {
            if (searchResultDTO == null || !searchResultDTO.Any())
            {
                return null;
            }

            foreach (var title in searchResultDTO)
            {
                title.Url = GetUrl(nameof(Get), new { title.Id });
            }
            return searchResultDTO;
        }
    }
}

