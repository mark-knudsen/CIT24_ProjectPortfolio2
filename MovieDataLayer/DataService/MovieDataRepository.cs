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

    public class MovieDataRepository<T> : IMovieDataRepository<T> where T : Item
    {
        private readonly IMDBContext _context;
        private readonly DbSet<T> _dbSet;

        public MovieDataRepository(IMDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet.Take(100).ToList(); //Temp, we should NOT get all
        }
        public IEnumerable<T> Get(object id)
        {
            return _dbSet.Where(x => x.GetId().Equals(id)).ToList();//Temp, we should NOT get all
        }

    }
}
