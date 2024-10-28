using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.Interfaces
{
    public interface IRepository<T> //T is class. So T could be Person.
    {
        T Get(object id);
        IList<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(object id);
    }
}
