using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.Interfaces
{
    public interface IMovieDataRepository<T, K> where T : BaseItem<K>
    {
        IList<T> GetAll();
        IList<T> Get(object id);
        //T GetTitleWithWriters(string id);

    }
}
