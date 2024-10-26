using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.DataService
{
    public class TitleRepository : Repository<Title, string>
    {
        public TitleRepository(IMDBContext context) : base(context) { }


        public IList<Person> GetWritersByMovieId(string id)
        {

            var title = _dbSet.Include(t => t.WritersList).FirstOrDefault(t => t.Id == id);
            return title.WritersList.ToList();


        }

        public IList<Title> GetAllTitleButWithLimit(int id)
        {
            return _dbSet.Take(id).ToList();
        }
    }


}
