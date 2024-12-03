using System;
using Mapster;
using MovieDataLayer;
using MovieDataLayer.Models.IMDB_Models;
using MovieDataLayer.Models.IMDB_Models.IMDB_Temp_Tables;
using MovieWebApi.DTO.IMDB_DTO;
using MovieWebApi.DTO.Search_DTO;
using MovieWebApi.DTO.User_DTO;
using MovieWebApi.SearchDTO;

namespace MovieWebApi.Extensions
{
    public static class Extension
    {
        public static TModel? Spawn_DTO_WithPagination<TModel, TEntity>(this TEntity entity, HttpContext httpContext, LinkGenerator linkGenerator, string routeName) where TEntity : class where TModel : class
        {
            if (entity == null) return null;

            var model = entity.Adapt<TModel>();

            switch (model) //Casting to TModel object, idk maybe we can improve?
            {
                case TitleDetailedDTO titleDetailedDTO when entity is TitleModel title && typeof(TModel) == typeof(TitleDetailedDTO):
                    model = (TModel)(object)MapTitleToTitleDetailedDTO(title, httpContext, linkGenerator, routeName);
                    break;
                case TitleSimpleDTO titleSimpleDTO when entity is TitleModel title && typeof(TModel) == typeof(TitleSimpleDTO):
                    model = (TModel)(object)MapTitleToTitleSimpleDTO(title, httpContext, linkGenerator, routeName);
                    break;

                case PersonDetailedDTO personDetailedDTO when entity is PersonModel person && typeof(TModel) == typeof(PersonDetailedDTO):
                    model = (TModel)(object)MapPersonToPersonDTO(person, httpContext, linkGenerator, routeName);
                    break;
                case UserBookmarkDTO userBookmarkDTO when entity is UserTitleBookmarkModel userTitleBookmark && typeof(TModel) == typeof(UserBookmarkDTO):
                    model = (TModel)(object)MapUserTitleBookmarkToUserBookmarkDTO(userTitleBookmark, httpContext, linkGenerator, routeName);
                    break;
                case UserBookmarkDTO userBookmarkDTO when entity is UserPersonBookmarkModel userPersonBookmark && typeof(TModel) == typeof(UserBookmarkDTO):
                    model = (TModel)(object)MapUserPersonBookmarkToUserBookmarkDTO(userPersonBookmark, httpContext, linkGenerator, routeName);
                    break;
                case TitleSearchResultDTO titleSearchResultDTO when entity is TitleSearchResultTempTable titleSearchResultTempTable && typeof(TModel) == typeof(TitleSearchResultDTO):
                    model = (TModel)(object)MapTitleSearchResultModelToTitleSearchResultDTO(titleSearchResultTempTable, httpContext, linkGenerator, routeName);
                    break;
                case PersonSearchResultDTO personSearchResultDTO when entity is PersonSearchResultTempTable personSearchResultTempTable && typeof(TModel) == typeof(PersonSearchResultDTO):
                    model = (TModel)(object)MapPersonSearchResultModelToPersonSearchResultDTO(personSearchResultTempTable, httpContext, linkGenerator, routeName);
                    break;
                case UserRatingDTO userRatingDTO when entity is UserRatingModel userRatingModel && typeof(TModel) == typeof(UserRatingDTO):
                    model = (TModel)(object)MapUserRatingModelToUserRatingDTO(userRatingModel, httpContext, linkGenerator, routeName);
                    break;
            }
            return model;
        }



        public static TModel? Spawn_DTO<TModel, TEntity>(this TEntity entity) where TEntity : class where TModel : class
        {
            if (entity == null) return null;
            var model = entity.Adapt<TModel>();
            return model;
        }

        // IMPORTANT, sometimes some values are null, but that will throw an axception when trying to set it here
        // add nullable in DTO and in here

        public static UserRatingDTO MapUserRatingModelToUserRatingDTO(this UserRatingModel userRating, HttpContext httpContext, LinkGenerator linkGenerator, string routeName)
        {
            var model = userRating.Adapt<UserRatingDTO>();
            model.PrimaryTitle = userRating.Title.PrimaryTitle;
            model.PosterUrl = userRating.Title.Poster.PosterUrl;
            model.Url = linkGenerator.GetUriByName(httpContext, routeName, new { titleId = userRating.TitleId });
            return model;
        }

        public static TitleDetailedDTO MapTitleToTitleDetailedDTO(this TitleModel title, HttpContext httpContext, LinkGenerator linkGenerator, string routeName)
        {
            var model = title?.Adapt<TitleDetailedDTO>();
            if (model == null) return null;
            model.GenresList = title?.GenresList?.Select(x => x.Genre.Name).ToList();
            model.PosterUrl = title?.Poster?.PosterUrl;
            model.WritersList = title.WritersList?.Select(x => x.Person.Name).ToList();
            model.Plot = title.Plot?.PlotOfTitle;
            model.VoteCount = title.Rating?.VoteCount;
            model.PrincipalCastList = title.PrincipalCastList?.Select(x => x.Person.Name).ToList();
            model.DirectorsList = title.DirectorsList?.Select(x => x.Person.Name).ToList();
            model.AverageRating = title.Rating?.AverageRating;
            model.Url = linkGenerator.GetUriByName(httpContext, routeName, new { id = title.Id });
            return model;
        }

        public static TitleSimpleDTO MapTitleToTitleSimpleDTO(this TitleModel title, HttpContext httpContext, LinkGenerator linkGenerator, string routeName)
        {
            var model = title.Adapt<TitleSimpleDTO>();
            if (model == null) return null;
            model.GenresList = title.GenresList?.Select(x => x.Genre.Name).ToList();
            model.PosterUrl = title.Poster?.PosterUrl;
            model.AverageRating = title.Rating?.AverageRating;
            model.Url = linkGenerator.GetUriByName(httpContext, routeName, new { id = title.Id });
            return model;
        }

        public static PersonDetailedDTO MapPersonToPersonDTO(this PersonModel person, HttpContext httpContext, LinkGenerator linkGenerator, string routeName)
        {
            var model = person.Adapt<PersonDetailedDTO>();
            if (model == null) return null;
            model.MostRelevantTitles = person?.MostRelevantTitles?.Select(x => x.Title.PrimaryTitle).ToList();
            model.PrimaryProfessions = person?.PrimaryProfessions?.Select(x => x.Profession.Name).ToList();
            model.Url = linkGenerator.GetUriByName(httpContext, routeName, new { id = person.Id });
            return model;
        }

        // this function was never used, also it's name doesn't really corelate with what it does, as the param and return type
        //public static PersonDetailedDTO MapUserTitleBookmarkToUserTitleBookmarkDTO(this PersonModel person)
        //{
        //    var model = person.Adapt<PersonDetailedDTO>();
        //    if (model == null) return null;
        //    model.MostRelevantTitles = person?.MostRelevantTitles?.Select(x => x.Title.PrimaryTitle).ToList();
        //    model.PrimaryProfessions = person?.PrimaryProfessions?.Select(x => x.Profession.Name).ToList();
        //    return model;
        //}

        public static UserBookmarkDTO MapUserTitleBookmarkToUserBookmarkDTO(this UserTitleBookmarkModel userTitleBookmark, HttpContext httpContext, LinkGenerator linkGenerator, string routeName)
        {
            var model = userTitleBookmark.Adapt<UserBookmarkDTO>();
            if (model == null) return null;
            model.Url = linkGenerator.GetUriByName(httpContext, routeName, new { titleId = userTitleBookmark.TitleId });
            model.CreatedAt = userTitleBookmark.CreatedAt;
            return model;
        }

        public static UserBookmarkDTO MapUserPersonBookmarkToUserBookmarkDTO(this UserPersonBookmarkModel userPersonBookmark, HttpContext httpContext, LinkGenerator linkGenerator, string routeName)
        {
            var model = userPersonBookmark.Adapt<UserBookmarkDTO>();
            if (model == null) return null;
            model.Url = linkGenerator.GetUriByName(httpContext, routeName, new { personId = userPersonBookmark.PersonId });
            model.CreatedAt = userPersonBookmark.CreatedAt;
            return model;
        }

        public static TitleSearchResultDTO MapTitleSearchResultModelToTitleSearchResultDTO(this TitleSearchResultTempTable titleSearchResultModel, HttpContext httpContext, LinkGenerator linkGenerator, string routeName)
        {
            var model = titleSearchResultModel.Adapt<TitleSearchResultDTO>();
            if (model == null) return null;
            model.Url = linkGenerator.GetUriByName(httpContext, routeName, new { id = titleSearchResultModel.TitleId });
            return model;
        }

        public static PersonSearchResultDTO MapPersonSearchResultModelToPersonSearchResultDTO(this PersonSearchResultTempTable personSearchResultModel, HttpContext httpContext, LinkGenerator linkGenerator, string routeName)
        {
            var model = personSearchResultModel.Adapt<PersonSearchResultDTO>();
            if (model == null) return null;
            model.Url = linkGenerator.GetUriByName(httpContext, routeName, new { id = personSearchResultModel.PersonId });
            return model;
        }
    }
}
