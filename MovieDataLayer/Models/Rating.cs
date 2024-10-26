using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer
{
    public class Rating
    {
        public string Id { get; set; }
        public int AverageRating { get; set; }
        public int VoteCount { get; set; }

        public Title title { get; set; }
    }
}
