using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer
{
    public class MostRelevant
    {
        public string PersonId { get; set; }
        public string TitleId { get; set; }
        public Title Title { get; set; } = null!; //required ref. navigation
        public Person Person { get; set; } = null!; //required ref. navigation

    }
}
