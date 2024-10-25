using MovieDataLayer.Extentions;
using MovieDataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.DataService
{
    public class DataService<T> : IMovieDataRepository<T> where T : Item
    {

        //No Services required for OrderDetail as all of OrderDetail methods contains Include()
        //which we were unable to implement into our DataService class

        // CATEGORY - CRUD operations for passing tests

        IMDBContext _context;

        public DataService(IMDBContext iMDBContext)
        {
            _context = iMDBContext;
            //_dbSet = _context.Set<T>();
        }

        public T Get(int id)
        {
            return _context.Set<T>().ToList().Find(x => x.GetId() == id);
        }

        public IList<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().ToList().Find(x => x.GetId() == id);
            //return db.Set<T>().Where(x => x.GetId() == id).Single();
        }

    }
}
