using Mapster;

namespace MovieWebApi.Extensions
{
    public class DTO_Extensions
    {
        public static TModel? Spawn_DTO<TModel, TEntity>(TEntity entity) where TEntity : class where TModel : class
        {
            if (entity == null) return null;

            var model = entity.Adapt<TModel>();
            return model;
        }
    }
}
