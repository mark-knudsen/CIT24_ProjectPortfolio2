using Microsoft.EntityFrameworkCore;
using MovieDataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.DataService
{

    public class MovieDataService<T> : IMovieDataService<T> where T : class
    {
        private readonly MovieContext _context;
        private readonly DbSet<T> _dbSet;

        public MovieDataService(MovieContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet.Take(100).ToList(); //Temp, we should NOT get all

        }

    }
}
