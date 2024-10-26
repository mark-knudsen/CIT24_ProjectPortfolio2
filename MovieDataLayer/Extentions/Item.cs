﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MovieDataLayer.Extentions
{
    public abstract class Item : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public abstract int GetId();
        public abstract void SetId(int id);

    }
}
