using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.Models.IMDB_Models.IMDB_Temp_Tables
{
    public class PersonSearchResultTempTable
    {
        public string PersonId { get; set; }
        public string PrimaryName { get; set; }
        public int? BirthYear { get; set; }
        public int? DeathYear { get; set; }
        public int? PersonAverageRating { get; set; }
        public int TotalElements { get; set; }

    }
}
