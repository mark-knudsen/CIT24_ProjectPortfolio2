using System.Collections.Immutable;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer.Models.IMDB_Models.IMDB_DTO;
using MovieWebApi.Controllers;

namespace MovieWebApi.Extensions
{
    [ApiController] //why?
    public class GenericController : ControllerBase
    {
        private readonly LinkGenerator _linkgenerator;

        public GenericController(LinkGenerator linkgenerator)
        {
            _linkgenerator = linkgenerator;
        }

        protected string? GetUrl(string pathName, object entity)
        {
            return _linkgenerator.GetUriByName(HttpContext, pathName, entity);
        }

        protected string? GetLink(string pathName, int pageNumber, int pageSize)
        {
            return GetUrl(pathName, new { pageNumber, pageSize });
        }

        protected object CreatePaging<T>(string pathName, int pageNumber, int pageSize, int total, IEnumerable<T>? entities)
        {


            const int maxPageSize = 10;

            pageSize = pageSize > maxPageSize ? maxPageSize : pageSize; //Sets pageSize to maxPageSize if greater than maxPageSize
            //Above 2 possibly not needed, as we can set the maxPageSize in the GetAll method in the repository

            var numberOfPages = (int)Math.Ceiling(total / (double)pageSize); //Calculates the number of pages and adds an extra page if there is a remainder

            var currentPageUrl = GetLink(pathName, pageNumber, pageSize); //Gets the current page

            var nextPageUrl = pageNumber < numberOfPages - 1 ? GetLink(pathName, pageNumber + 1, pageSize) : null; //NumberOfPages - 1, because we start on page 1. If 10 pages we can click "next page" 9 times

            var previousPageUrl = pageNumber > 0 ? GetLink(pathName, pageNumber - 1, pageSize) : null;

            var result = new
            {
                CurrentPage = currentPageUrl,
                NextPage = nextPageUrl,
                PreviousPage = previousPageUrl,
                NumberOfEntities = total,
                NumberOfPages = numberOfPages,
                Entities = entities
            };
            return result;

        }

        public IEnumerable<T>? CreateNavigation<T, C>(IEnumerable<T> DTO) where T : BaseDTO where C: TitleController 
        {
            if (DTO == null)
            {
                return null;
            }
            //took a different route, might not be needed.

            //var url = DTO.Select(x => x.Url);
            //var id = DTO.Select(x => x.Id);

            foreach ( var entity in DTO) {
                entity.Url = GetUrl(nameof(TitleController.Get), new {entity.Id});
            }

            return DTO;
        }

    }
}
