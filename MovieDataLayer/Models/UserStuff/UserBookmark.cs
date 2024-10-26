using MovieDataLayer.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer;
public class UserBookmark : Item<int>
{
    public int TitleId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Annotations { get; set; }
}