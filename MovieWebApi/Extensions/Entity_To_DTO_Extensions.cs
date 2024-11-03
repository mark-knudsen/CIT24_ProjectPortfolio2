using Mapster;
using Microsoft.AspNetCore.Http;
using MovieDataLayer.Models.IMDB_Models;
using MovieWebApi;

public class Entity_To_DTO_Extensions
{
    private readonly LinkGenerator _linkGenerator;

    public Entity_To_DTO_Extensions(LinkGenerator linkGenerator)
    {
        _linkGenerator = linkGenerator;
    }


    public TDTOModel MapToDTO<TDomainModel, TDTOModel>(TDTOModel entity, string pathName, HttpContext httpContext)
    {

        // var entityDTO = entity.Adapt<TDTOModel>();

        //Sets URL property, but only if the entityDTO implements the INeedUrl interface
        if (entity is INeedUrl dtoWithUrl)
        {
            dtoWithUrl.Url = _linkGenerator.GetUriByName(httpContext, pathName, new { id = entity.GetType().GetProperty("Id")?.GetValue(entity) });
        }

        return entity;
    }

    public TitleDetailedDTO MapTitleToTitleDetailedDTO(Title title, string pathName, HttpContext httpContext) // IMPORTANT, sometimes some values are null, but that will throw an axception when trying to set it here, add nullable in DTO and in here
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

        return MapToDTO<Title, TitleDetailedDTO>(model, pathName, httpContext);
    }
}

// This interface is used to mark DTOs that have a URL property
public interface INeedUrl
{
    string? Url { get; set; }
}
