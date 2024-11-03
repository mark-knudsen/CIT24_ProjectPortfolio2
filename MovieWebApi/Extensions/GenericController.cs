using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

namespace MovieWebApi.Extensions
{
    [ApiController] //why?
    public class GenericController : ControllerBase
    {
        protected readonly LinkGenerator linkgenerator;
        private readonly Entity_To_DTO_Extensions _entity_To_DTO_Extensions;

        public GenericController(LinkGenerator linkgenerator, Entity_To_DTO_Extensions entity_To_DTO_Extensions)
        {
            this.linkgenerator = linkgenerator;
            _entity_To_DTO_Extensions = entity_To_DTO_Extensions;
        }

        protected string? GetUrl(string pathName, object entity)
        {
            return linkgenerator.GetUriByName(HttpContext, pathName, entity);
        }

        protected string? GetLink(string pathName, int page, int pageSize)
        {
            return GetUrl(pathName, new { page, pageSize });
        }

        protected object CreatePaging<T>(string pathName, int pageNumber, int pageSize, int total, IEnumerable<T>? entities)
        {
            const int maxPageSize = 10;

            pageSize = pageSize > maxPageSize ? maxPageSize : pageSize; //Sets pageSize to maxPageSize if greater than maxPageSize

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


    }
}
