using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer
{
    public abstract class BaseItem<T>
    {
        public abstract T Id { get; set; }
        public abstract T GetId();
        public abstract void SetId(T id);
    }
}
