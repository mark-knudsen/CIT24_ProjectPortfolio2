using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer
{
    public class LocalizedDetail
    {
        public int Id { get; set; } //FK

        public string Title { get; set; }

        public string Language { get; set; }

        public string Region { get; set; }

        public string Type { get; set; }
        public string Attribute { get; set; } //We prob dont need

        // Navigation property to LocalizedTitle
        public LocalizedTitle LocalizedTitle { get; set; }
    }
}
