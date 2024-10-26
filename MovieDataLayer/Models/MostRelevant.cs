using MovieDataLayer.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer
{
    public class MostRelevant : Item<string> // id is personid
    {
        public string TitleId { get; set; }
        public Person person { get; set; }
        public Title title { get; set; }
    }
}
