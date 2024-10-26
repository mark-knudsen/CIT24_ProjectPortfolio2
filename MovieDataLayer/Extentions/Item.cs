using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.Extentions
{
    public abstract class Item<U>
    {
        public U Id { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
