using MovieDataLayer.Extentions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer
{
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? BirthYear { get; set; }
        public int? DeathYear { get; set; }
        public IList<MostRelevant> MostRelevantTitles { get; set; }
        public IList<Profession> PrimaryProfessions { get; set; }
        public IList<Title> TitlesList { get; set; }



    }
}
