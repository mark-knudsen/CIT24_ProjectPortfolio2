using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.Interfaces;
public interface IUserFrameworkRepository<T>
{
    IList<T> GetAll();
    T Get(object id);
    IList<T> GetAll(object id);
}

