using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.Interfaces
{
    public interface IRepository<T, K> //K is type, T is class. So T could be Person, and K could be string. Nice because not all ID is int
    {
        T Get(K id);
        IList<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(K id);
    }
}
