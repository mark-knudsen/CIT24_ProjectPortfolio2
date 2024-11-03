using Microsoft.AspNetCore.Mvc;
using MovieDataLayer.DataService.IMDB_Repository;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.Extensions;
using MovieDataLayer;
using MovieDataLayer.DataService.UserFrameworkRepository;
using Mapster;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieWebApi.DTO.SearchDTO;

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

        [HttpGet(Name = nameof(GetAllTitles))]
        public async Task<IActionResult> GetAllTitles(int page = 0, int pageSize = 10) // We really just want the plot and poster at all times in the title, same with some of the collections
        {
            //var fecthedTitles = (await _titleRepository.GetAll(page, pageSize));
            //if (fecthedTitles == null || !fecthedTitles.Any()) return NotFound();

            //var titleToDTO = fecthedTitles.Select(title => title.MapTitleToTitleDetailedDTO()).ToList();
            //var numberOfEntities = await _titleRepository.NumberOfTitles();
            //var titleList = CreateTitleModel(titleToDTO);

            //object result = CreatePaging(nameof(GetAllTitles), page, pageSize, numberOfEntities, titleList);

            //return Ok(result);

            var titles = (await _titleRepository.GetAll(page, pageSize)).Select(DTO_Extensions.Spawn_DTO<TitleDetailedDTO, Title>);
            if (titles == null || !titles.Any()) return NotFound();

            var numberOfEntities = await _titleRepository.NumberOfElementsInTable();
            titles = CreateNavigation<TitleDetailedDTO, TitleController>(titles); //Why does this need to be converted to a list? It is already a list? Maybe use the above code? Possible issue with using Select on await task

            object result = CreatePaging(nameof(GetAllTitles), page, pageSize, numberOfEntities, titles);

            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(Get))]
        public async Task<IActionResult> Get(string id) // id tt9126600
        {
            var title = CreateTitleModel((await _titleRepository.GetTitle(id)).MapTitleToTitleDetailedDTO());
            if (title == null) return NotFound();
            return Ok(title);
        }

        [HttpGet("genre/{id}")]
        public async Task<IActionResult> GetByGenre(int id, int page = 0, int pageSize = 10) // id tt7856872
        {
            var titles = (await _titleRepository.GetTitleByGenre(id, page, pageSize)).MapTitleToTitleDetailedDTO();
            if (titles == null) return NotFound();

            var numberOfEntities = await _titleRepository.NumberOfElementsInTable();
            object result = CreatePaging(nameof(GetByGenre), page, pageSize, numberOfEntities, titles);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromHeader] int userId, string searchTerm, int page = 0, int pageSize = 1) // should probably be authorized ALOT to be allowed to call this
        {
            var queryResult = (await _titleRepository.TitleSearch(userId, searchTerm, page, pageSize)).MapTitleSearchResultModelToTitleSearchResultDTO();
            var numberOfentities = await _titleRepository.NumberOfElementsInTable();

            queryResult = CreateNavigation<TitleSearchResultDTO, TitleController>(queryResult);
            object s = CreatePaging(nameof(Search), page, pageSize, numberOfentities, queryResult);

            return Ok(s);
        }

        [HttpGet("similar-titles")] // Discuss if it is ok to use this URL!
        public async Task<IActionResult> SimilarTitles(string titleId) // should probably be authorized ALOT to be allowed to call this
        {
            var result = await _titleRepository.SimilarTitles(titleId);
            return Ok(result);

        }
        private TitleDetailedDTO? CreateTitleModel(TitleDetailedDTO? titleDTO)
        {
            if (titleDTO == null)
            {
                return null;
            }

            titleDTO.Url = GetUrl(nameof(Get), new { titleDTO.Id });

            return titleDTO;
        }

        private IList<TitleDetailedDTO>? CreateTitleModel(IList<TitleDetailedDTO>? titleDTO)
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
    }
}
