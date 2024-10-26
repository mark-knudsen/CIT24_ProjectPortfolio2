using Microsoft.EntityFrameworkCore;
using MovieDataLayer.Extentions;
using MovieDataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.DataService
{

    public class MovieDataRepository<T, U> : IMovieDataRepository<T, U> where T : Item<U>
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
        public IList<T> GetAll(object id)
        {
            switch (id)
            {
                case int:
                    return _context.Set<T>().Where(x => Convert.ToInt32(x.Id) == (int)id).ToList();
                case string:
                    return _context.Set<T>().Where(x => x.Id.ToString() == (string)id).ToList();
                default:
                    return _context.Set<T>().Where(x => Convert.ToInt32(x.Id) == (int)id).ToList();
            }
        }

        public T Get(object id)
        {
            return _dbSet.Find(id);
        }
    }
}
