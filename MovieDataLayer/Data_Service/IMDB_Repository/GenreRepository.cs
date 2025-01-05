using MovieDataLayer.Models.IMDB_Models;

namespace MovieDataLayer.Data_Service.IMDB_Repository
{
    public class GenreRepository : Repository<GenreModel>
    {
        public GenreRepository(IMDBContext context) : base(context)
        {
        }
    }
}
