using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer
{
    public class PersonModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? BirthYear { get; set; }
        public int? DeathYear { get; set; }
        //public ICollection<TitleModel> MostRelevantTitles { get; set; }
        //public ICollection<ProfessionModel> PrimaryProfessions { get; set; }


    }
}
