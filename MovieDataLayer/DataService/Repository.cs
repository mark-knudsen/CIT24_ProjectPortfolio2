using Microsoft.EntityFrameworkCore;
using MovieDataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.DataService
{
    //This Repository class is a generic class that implements the IRepository interface
    public class Repository<T, K> : IRepository<T, K> where T : class
    {
        protected readonly IMDBContext _context; //Both are protected so that they can be accessed by the derived class
        protected readonly DbSet<T> _dbSet;

        public Repository(IMDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity) //This and Update, consider making it virtual?
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(K id)
        {
            var entity = Get(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public T Get(K id)
        {
            return _dbSet.Find(id);
        }

        public IList<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Update(T entity) //maybe add id, and think about serialize
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}
