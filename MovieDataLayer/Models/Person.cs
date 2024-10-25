﻿using MovieDataLayer.Extentions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer
{
    public class Person : Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? BirthYear { get; set; }
        public int? DeathYear { get; set; }
        //public ICollection<Title> MostRelevantTitles { get; set; }
        public ICollection<Profession> PrimaryProfessions { get; set; }
        public IEnumerable<Title> Titles { get; set; }

        public override object GetId()
        {
            return Id;
        }

        public override void SetId(object id)
        {
            Id = (string)id;
        }

    }
}
