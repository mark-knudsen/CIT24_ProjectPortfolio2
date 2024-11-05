using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

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

        protected string? GetLink(string pathName, int page, int pageSize, object? id = null)
        {
            if (id == null) return GetUrl(pathName, new { page, pageSize });

            return GetUrl(pathName, new { page, pageSize, id }); //if id is not null, it will be added to the URL. E.g. if we want to have navigation url to a path with id


        }

        protected object CreatePaging<T>(string pathName, int pageNumber, int pageSize, int total, IEnumerable<T>? entities, object? id = null) //id is the id of an entity, could fx. be id for a specific genre.
                                                                                                                                                //is object, so accepts both string and int. 
        {
            const int maxPageSize = 10;

            pageSize = pageSize > maxPageSize ? maxPageSize : pageSize; //Sets pageSize to maxPageSize if greater than maxPageSize

            var numberOfPages = (int)Math.Ceiling(total / (double)pageSize); //Calculates the number of pages and adds an extra page if there is a remainder

            var currentPageUrl = GetLink(pathName, pageNumber, pageSize, id); //Gets the current page

            var nextPageUrl = pageNumber < numberOfPages - 1 ? GetLink(pathName, pageNumber + 1, pageSize, id) : null; //NumberOfPages - 1, because we start on page 1. If 10 pages we can click "next page" 9 times

            var previousPageUrl = pageNumber > 0 ? GetLink(pathName, pageNumber - 1, pageSize, id) : null;

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
