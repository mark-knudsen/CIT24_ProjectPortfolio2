using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(object id);
        Task<IList<T>> GetAll();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(object id);
    }
}
