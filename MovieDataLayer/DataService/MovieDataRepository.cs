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

    public class MovieDataRepository<T, K> : IMovieDataRepository<T, K> where T : BaseItem<K>
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
        public IList<T> Get(object id)
        {
            return _dbSet.Where(x => x.GetId().Equals(id)).ToList();//Temp, we should NOT get all
            //return _dbSet.Where(x => x.Id.Equals(id)).ToList();//Temp, we should NOT get all
            //return _dbSet.Where(x => x.Id.Equals(id)).ToList();//Temp, we should NOT get all
            //return _dbSet.Where(t => t.Id.Equals(id)).SelectMany(t => t.WritersList).ToList();
            //return _dbSet.Include(t => t.Id).FirstOrDefault(t => t.Id.Equalts(id));
        }

        //public T GetTitleWithWriters(string id)
        //{
        //    return _dbSet.Include(x => x.WritersList).FirstOrDefault(x => x.GetId().Equals(id));
        //}

    }
}
