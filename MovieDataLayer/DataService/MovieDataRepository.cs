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

        public MovieDataRepository()
        {
            //_context = context;
            //_dbSet = _context.Set<T>();
        }
        public IList<T> GetAll()
        {
            return _dbSet.Take(100).ToList(); //Temp, we should NOT get all
        }
        public T Get(int id)
        {
            IMDBContext db = new IMDBContext();
            //  return _dbSet.Take(10).ToList();
            //var d =_dbSet.Take(10).ToList()[0].GetId(); // this doesn't fail, yay
            //return _dbSet.Take(100).ToList().FindAll(x => x.GetId() == id).ToList(); // this works

            //var genre = dbContext.Genres.AsEnumerable().FirstOrDefault(g => g.GetId() == __id_0);
            return _context.Set<T>().Where(x => x.GetId() == id).Single();
            //return _dbSet.Where(x => x.GetId() == id).Single();

            //return _dbSet.Where(x => x.GetId() == id).ToList();//Temp, we should NOT get all
        }





    }
}