using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<int> NumberOfElementsInTable();
        Task<T> Get(object id);
        Task<IList<T>> GetAllWithPaging(int page = 0, int pageSize = 10);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(object id);
    }
}
