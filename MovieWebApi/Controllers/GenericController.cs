﻿using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieWebApi.Extensions;

namespace MovieWebApi.Controllers
{
    public class GenericController : ControllerBase
    {
        protected readonly LinkGenerator _linkGenerator;
        protected readonly UserRepository _userRepository;
        protected readonly AuthenticatorExtension _authenticatorExtension;

        public GenericController(LinkGenerator linkgenerator, UserRepository userRepository, AuthenticatorExtension authenticatorHelper)
        {
            _linkGenerator = linkgenerator;
            _userRepository = userRepository;
            _authenticatorExtension = authenticatorHelper;
        }

        protected string? GetUrl(string pathName, object entity)
        {
            return _linkGenerator.GetUriByName(HttpContext, pathName, entity);
        }

        protected string? GetLink(string pathName, int page, int pageSize, IComparable? id = null)
        {
            if (id == null) return GetUrl(pathName, new { page, pageSize });

            return GetUrl(pathName, new { page, pageSize, id }); //if id is not null, it will be added to the URL. E.g. if we want to have navigation url to a path with id
        }

        protected object CreatePaging<T>(string pathName, int pageNumber, int pageSize, int total, IEnumerable<T>? entities, IComparable? id = null) //id is the id of an entity, could fx. be id for a specific genre.
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
        protected async Task<StatusCodeResult> Validate(int id, string Authorization)
        {
            var user = await _userRepository.Get(id);
            if (user == null) return BadRequest();
            bool isValidUser = _authenticatorExtension.ValidateUser(Authorization, user.Id, user.Email);

            if (!isValidUser) return Unauthorized();
            else return null;
        }
    }
}
