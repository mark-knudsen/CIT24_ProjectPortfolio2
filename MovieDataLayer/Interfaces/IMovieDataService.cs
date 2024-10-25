using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.Interfaces
{
    public interface IMovieDataService<T>
    {
        IEnumerable<T> GetAll();
    }
}
