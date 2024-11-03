using Microsoft.AspNetCore.Mvc;
using MovieDataLayer.DataService.IMDB_Repository;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi.Extensions;
using MovieDataLayer;
using MovieDataLayer.DataService.UserFrameworkRepository;
using Mapster;

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
            var title = CreatePageNavigation((await _titleRepository.GetTitle(id)).MapTitleToTitleDetailedDTO());
            if (title == null) return NotFound();
            return Ok(title);
        }

        [HttpGet(Name = nameof(GetAllTitles))]
        public async Task<IActionResult> GetAllTitles(int page = 1, int pageSize = 10) // We really just want the plot and poster at all times in the title, same with some of the collections
        {
            var titles = (await _titleRepository.GetAll(page, pageSize)).Select(DTO_Extensions.Spawn_DTO<TitleDetailedDTO, Title>);
            if (titles == null || !titles.Any()) return NotFound();

            var numberOfEntities = await _titleRepository.NumberOfTitles();
            titles = CreatePageNavigation(titles.ToList());

            object result = CreatePaging(nameof(GetAllTitles), page, pageSize, numberOfEntities, titles);

            return Ok(result);
        }


        [HttpGet("genre/{id}")]
        public async Task<IActionResult> GetByGenre(int id) // id tt7856872
        {
            var titles = (await _titleRepository.GetTitleByGenre(id)).MapTitleToTitleDetailedDTO();
            if (titles == null) return NotFound();

            return Ok(titles);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromHeader] int userId, string searchTerm) // should probably be authorized ALOT to be allowed to call this
        {
            var result = await _titleRepository.TitleSearch(userId, searchTerm);
            return Ok(result);
        }

        [HttpGet("similar-titles")] // Discuss if it is ok to use this URL!
        public async Task<IActionResult> SimilarTitles(string titleId) // should probably be authorized ALOT to be allowed to call this
        {
            var result = await _titleRepository.SimilarTitles(titleId);
            return Ok(result);
        }

        //Page Navigation
            private TitleDetailedDTO? CreatePageNavigation(TitleDetailedDTO? titleDTO)
            {
                if (titleDTO == null)
                {
                    return null;
                }

                titleDTO.Url = GetUrl(nameof(Get), new { titleDTO.Id });

                return titleDTO;
            }

            private IList<TitleDetailedDTO>? CreatePageNavigation(IList<TitleDetailedDTO>? titleDTO)
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

