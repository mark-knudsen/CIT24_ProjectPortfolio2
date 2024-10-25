using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.Interfaces
{
    public interface IMovieDataRepository<T>
    {
        IList<T> GetAll();
        T Get(int id);

    }
}
