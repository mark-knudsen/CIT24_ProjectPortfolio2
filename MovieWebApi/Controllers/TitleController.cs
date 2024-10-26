﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieDataLayer.Interfaces;
using MovieDataLayer;
using Mapster;
using MovieDataLayer.DataService;

namespace MovieWebApi.Controllers
{
    [ApiController]
    [Route("api/titles")]
    public class TitleController : ControllerBase
    {
        private readonly TitleRepository _titleRepository;

        public TitleController(TitleRepository titleRepository)
        {
            _titleRepository = titleRepository;
        }

        [HttpGet("writers/{id}")]
        public IActionResult GetWriters(string id)
        {
            //var writers = _dataService.Get(id).Select(CreateWriterModel);
            //var writers = _dataService.GetTitleWithWriters(id).Select(CreateWriterModel);

            //if (writers == null) return NotFound();

            var writers = _titleRepository.GetWritersByMovieId(id);
            if (writers == null || !writers.Any())
                return NotFound();

            var writerModels = writers.Select(CreateWriterModel).ToList();
            return Ok(writerModels);
        }

        [HttpGet("{id}")]
        public IActionResult GetTitle(string id)
        {
            //var title = _dataService.Get(id).FirstOrDefault();
            var title = _titleRepository.GetAllTitleButWithLimit(10).ToList();

            var test = _titleRepository.ge
            if (title == null) return NotFound();

            var titleModel = title.Adapt<TitleModel>();
            return Ok(titleModel);
        }

        // Helper methods
        private PersonModel? CreateWriterModel(Person? title)
        {
            if (title == null) return null;

            var personModel = title.Adapt<PersonModel>(); // funky name...
            return personModel;
        }

    }
}
