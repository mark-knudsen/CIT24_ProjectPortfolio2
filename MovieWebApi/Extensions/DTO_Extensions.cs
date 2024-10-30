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

        public static TitleModel CreateTitleModel(this Title title)
        {
            var model = title.Adapt<TitleModel>();
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
    }
}
