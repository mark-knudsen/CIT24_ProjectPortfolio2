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
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IMDBContext _context; //Both are protected so that they can be accessed by the derived class
        protected readonly DbSet<T> _dbSet;

        public Repository(IMDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> Get(object id)
        {
            return await _dbSet.FindAsync(id);

        }

        public async Task<IList<T>> GetAll(int page = 0, int pageSize = 10) //should not use default values when fully implemented?
        {
            const int maxPageSize = 10; //Max size of page retrieved from DB

            pageSize = pageSize > maxPageSize ? maxPageSize : pageSize; //Sets pageSize to maxPageSize if greater than maxPageSize
            return await _dbSet.AsNoTracking().Skip(page * pageSize).Take(pageSize).ToListAsync();
        }

        // TODO: Add try catch to avoid runtime error.
        public async Task<bool> Add(T entity) //This and Update, consider making it virtual?
        {
            try
            {
                _dbSet.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Delete(object id)
        {
            try
            {
                var entity = await Get(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    _context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> Update(T entity) //maybe add id, and think about serialize
        {
            try
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<int> NumberOfElementsInTable()
        {
            return await _dbSet.AsNoTracking().CountAsync();
        }


    }
}
