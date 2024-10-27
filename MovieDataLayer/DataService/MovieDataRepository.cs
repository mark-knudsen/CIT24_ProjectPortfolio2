using Microsoft.EntityFrameworkCore;
using MovieDataLayer.Interfaces;

namespace MovieDataLayer.DataService
{

    public class MovieDataRepository<T, U> : IMovieDataRepository<T, U> where T : Item<U>, new() where U : IComparable<U>
    {
        private readonly IMDBContext _context;
        private readonly DbSet<T> _dbSet;

        public MovieDataRepository(IMDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public IList<T> GetAll()
        {
            return _dbSet.Take(100).ToList(); //Temp, we should NOT get all
        }
        public async Task<IList<T>> GetAll(object id)
        {
            return _context.Set<T>().AsNoTracking().Where(x => x.Id.Equals(id)).ToList(); // it doesn't work when it is made async
            return await _context.Set<T>().AsNoTracking().Where(x => x.Id.Equals(id)).ToListAsync(); // it doesn't work when it is made async

            //switch (id) // it has to have asEnumerable, but the AsAsyncEnumerable doesn't have a where clause so it can't be done async argh
            //{
            //    case int:
            //        return _context.Set<T>().AsNoTracking().Where(x => x.Id.Equals(id)).ToList(); // as int
            //    default:
            //         return _context.Set<T>().AsNoTracking().AsEnumerable().Where(x => Convert.ToString(x.Id) == (string)id).ToList(); // as string
            //}
        }

        public T Get(object id)
        {
            return _dbSet.Find(id);
        }
    }
}
