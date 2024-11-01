using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

namespace MovieWebApi.Extensions
{
    [ApiController] //why?
    public class GenericController : ControllerBase
    {
        private readonly LinkGenerator _linkgenerator;

        public GenericController (LinkGenerator linkgenerator)
        {
            _linkgenerator = linkgenerator;
        }

        protected string? GetUrl(string pathName, object entity)
        {
            return _linkgenerator.GetUriByName(HttpContext, pathName, entity);
        }


    }
}
