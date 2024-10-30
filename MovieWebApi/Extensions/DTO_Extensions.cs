using System.Linq;
using System.Net.NetworkInformation;
using Mapster;
using MovieDataLayer;
using MovieDataLayer.Models.IMDB_Models;

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

        public static TitleDetailedDTO MapTitleToTitleDetailedDTO(this Title title)
        {
            var model = title.Adapt<TitleDetailedDTO>();
            model.GenresList = title.GenresList.Select(x => x.Genre.Name).ToList();
            model.PosterUrl = title.Poster.PosterUrl;
            model.WritersList = title.WritersList.Select(x => x.Person.Name).ToList();
            model.Plot = title.Plot.PlotOfTitle;
            model.VoteCount = title.Rating.VoteCount;
            model.PrincipalCastList = title.PrincipalCastList.Select(x => x.Person.Name).ToList();
            model.DirectorsList = title.DirectorsList.Select(x => x.Person.Name).ToList();
            model.AverageRating = title.Rating.AverageRating;
            return model;
        }

        public static PersonDetailedDTO MapPersonToPersonDTO(this Person person)
        {
            var model = person.Adapt<PersonDetailedDTO>();
            model.MostRelevantTitles = person.MostRelevantTitles.Select(x => x.Title.PrimaryTitle).ToList();
            model.PrimaryProfessions = person.Professions.Select(x => x.Name).ToList();
            return model;
            
        }
    }
}
