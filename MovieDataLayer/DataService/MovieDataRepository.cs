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
            return await _dbSet.AsNoTracking().Where(x => x.Id.Equals(id)).ToListAsync();

        }

        //public IList<Person> GetWritersByMovieId(string id)
        //{
        //    var writers = _dbSet.Where(x => x.Id.Equals(id)).Include(t => t).ThenInclude(w => w.Person).SelectMany(t => t.WritersList.Select(w => w.Person)).ToList(); //Using selectmany to flatten the list of lists. Needed because we are working with nested list here!
        //    return writers;
        //}


        public T Get(object id)
        {
            return _dbSet.Find(id);
        }
    }
}
