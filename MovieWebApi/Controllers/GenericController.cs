using Microsoft.AspNetCore.Mvc;
using MovieDataLayer.Data_Service.User_Framework_Repository;
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

        protected string? GetLink(string pathName, int page, int pageSize, string? parameterName = null, IComparable? value = null)
        {
            var queryParams = new Dictionary<string, object> { }; //Key is string, and each key has a value of type object. Eg. key is pageSize and value is the int value, eg. 10


            if (!string.IsNullOrEmpty(parameterName) && value != null)
            {
                queryParams.Add(parameterName, value); //Append the parameter to url. Otherwise queryParams just has page and pageSize
            }
            //The two below could also be added when queryParams gets initialized. However, no AddFirst method avaible for Dictionary, hence we want to append page and pageSize last (as parameterName is to be desired added first??
            queryParams.Add("page", page); //Order seems to be preserved in this case, however not ensured as nature of Dictionary. However, benefits of Dictionary seems to uphold, as it does ensure unique entries. 
            queryParams.Add("pageSize", pageSize);



            return GetUrl(pathName, queryParams); //if id is not null, it will be added to the URL. E.g. if we want to have navigation url to a path with id
        }

        protected object CreatePaging<T>(string pathName, int pageNumber, int pageSize, int total, IEnumerable<T>? entities, string? parameterName = null, IComparable? value = null) //parameterName is what we want parameter to be named in URL, so e.g. "id" or "searchTerm".                                                                                                                                                                            
        {
            const int maxPageSize = 10;

            pageSize = pageSize > maxPageSize ? maxPageSize : pageSize; //Sets pageSize to maxPageSize if greater than maxPageSize

            var numberOfPages = (int)Math.Ceiling(total / (double)pageSize); //Calculates the number of pages and adds an extra page if there is a remainder

            var currentPageUrl = GetLink(pathName, pageNumber, pageSize, parameterName, value); //Gets the current page

            var nextPageUrl = pageNumber < numberOfPages - 1 ? GetLink(pathName, pageNumber + 1, pageSize, parameterName, value) : null; //NumberOfPages - 1, because we start on page 1. If 10 pages we can click "next page" 9 times

            var previousPageUrl = pageNumber > 0 ? GetLink(pathName, pageNumber - 1, pageSize, parameterName, value) : null;

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
        //protected async Task<StatusCodeResult> Validate(int id, string Authorization)
        //{
        //    var user = await _userRepository.Get(id);
        //    if (user == null) return BadRequest();
        //    bool isValidUser = _authenticatorExtension.ValidateUser(Authorization, user.Id, user.Email);

        //    if (!isValidUser) return Unauthorized();
        //    else return null;
        //}
    }
}
