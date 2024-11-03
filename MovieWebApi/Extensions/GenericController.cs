using System.Collections.Immutable;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.Models.IMDB_Models.IMDB_DTO;
using MovieWebApi.Controllers;

namespace MovieWebApi.Extensions
{


    public class GenericController : ControllerBase
    {
        private readonly LinkGenerator _linkgenerator;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public GenericController(LinkGenerator linkgenerator)
        {
            _linkgenerator = linkgenerator;

        }

        public string? GetUrl(string pathName, object entity)
        {

            return _linkgenerator.GetUriByName(HttpContext, pathName, entity);
        }

        public string? GetLink(string pathName, int pageNumber, int pageSize)
        {
            return GetUrl(pathName, new { pageNumber, pageSize });
        }

        public object CreatePaging<T>(string pathName, int pageNumber, int pageSize, int total, IEnumerable<T>? entities)
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

        public IEnumerable<T>? CreateNavigation<T>(IEnumerable<T> DTO, string routeName) where T : BaseDTO
        {
            var Jacob = DTO.ToList();

            if (DTO == null)
            {
                return null;
            }
            //took a different route, might not be needed.

            //var url = DTO.Select(x => x.Url);
            //var id = DTO.Select(x => x.Id);

            foreach (var entity in Jacob)
            {

                entity.Url = GetUrl(routeName, new { entity.Id });
            }

            return Jacob;
        }

    }
}
