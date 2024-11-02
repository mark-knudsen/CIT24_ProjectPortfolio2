using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieDataLayer.Models.IMDB_Models.IMDB_DTO;

namespace MovieDataLayer 
{
    public class SimilarTitleSearchModel: BaseDTO    {
        public string SimilarTitleId { get; set; } 
        public string PrimaryTitle { get; set; }
        public string[] Genres { get; set; }
        public bool IsAdult { get; set; }
        public string TitleType { get; set; }
    }
}
