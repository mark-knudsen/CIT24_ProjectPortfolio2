﻿using System.Linq;
using System.Net.NetworkInformation;
using Mapster;
using MovieDataLayer;
using MovieDataLayer.Models.IMDB_Models;
using MovieDataLayer.Models.IMDB_Models.IMDB_DTO;
using MovieWebApi.SearchDTO;

namespace MovieWebApi.Extensions
{
    public static class DTO_Extensions
    {
        public static TModel? Spawn_DTO<TModel, TEntity>(TEntity entity) where TEntity : class where TModel : class
        {
            if (entity == null) return null;

            var model = entity.Adapt<TModel>();
            return model;
        }

        public static TitleDetailedDTO MapTitleToTitleDetailedDTO(this Title title) // IMPORTANT, sometimes some values are null, but that will throw an axception when trying to set it here, add nullable in DTO and in here
        {
            var model = title.Adapt<TitleDetailedDTO>();
            model.GenresList = title.GenresList?.Select(x => x.Genre.Name).ToList();
            model.PosterUrl = title.Poster?.PosterUrl;
            model.WritersList = title.WritersList?.Select(x => x.Person.Name).ToList();
            model.Plot = title.Plot?.PlotOfTitle;
            model.VoteCount = title.Rating?.VoteCount;
            model.PrincipalCastList = title.PrincipalCastList?.Select(x => x.Person.Name).ToList();
            model.DirectorsList = title.DirectorsList?.Select(x => x.Person.Name).ToList();
            model.AverageRating = title.Rating?.AverageRating;

            return model;
        }
        public static IEnumerable<TitleDetailedDTO> MapTitleToTitleDetailedDTO(this IEnumerable<Title> _title)   
        {
            var models = new List<TitleDetailedDTO>();
            foreach (var title in _title) models.Add(title.MapTitleToTitleDetailedDTO());  // not that elegant looking

            return models;
        }

        public static PersonDetailedDTO MapPersonToPersonDTO(this Person person)
        {
            var model = person.Adapt<PersonDetailedDTO>();
            model.MostRelevantTitles = person.MostRelevantTitles.Select(x => x.Title.PrimaryTitle).ToList();
            model.PrimaryProfessions = person.PrimaryProfessions.Select(x => x.Profession.Name).ToList();

            return model;
        }
        public static PersonDetailedDTO MapUserTitleBookmarkToUserTitleBookmarkDTO(this Person person)
        {
            var model = person.Adapt<PersonDetailedDTO>();
            model.MostRelevantTitles = person.MostRelevantTitles.Select(x => x.Title.PrimaryTitle).ToList();
            model.PrimaryProfessions = person.PrimaryProfessions.Select(x => x.Profession.Name).ToList();

            return model;
        }

        public static TitleSearchResultDTO MapOneTitleSearchResultModelToTitleSearchResultDTO(this TitleSearchResultModel titleSearchResultModel)
        {
            var model = titleSearchResultModel.Adapt<TitleSearchResultDTO>();
            return model;
        }
        public static IEnumerable<TitleSearchResultDTO> MapTitleSearchResultModelToTitleSearchResultDTO(this IEnumerable<TitleSearchResultModel> titleSearchResultModel)
        {
            var models = new List<TitleSearchResultDTO>();
            foreach (var searchResult in titleSearchResultModel) models.Add(searchResult.MapOneTitleSearchResultModelToTitleSearchResultDTO());  // Really not that elegant looking

            return models;

        }
    }
}
