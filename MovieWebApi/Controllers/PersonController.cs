﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer;
using MovieDataLayer.DataService;
using MovieDataLayer.Interfaces;

namespace MovieWebApi.Controllers
{
    [ApiController]
    [Route("api/persons")]
    public class PersonController : ControllerBase
    {
        private readonly IMovieDataRepository<Person> _dataService;

        public PersonController(IMovieDataRepository<Person> dataService)
        {
            _dataService = dataService;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_dataService.GetAll());
        }
    }
}
