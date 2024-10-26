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

            var title = _dbSet.Where(t => t.Id == id).Include(t => t.WritersList).ThenInclude(w => w.Person).SelectMany(t => t.WritersList.Select(w => w.Person)).ToList();
            return title;

        }

        public IList<Title> GetAllTitleButWithLimit(int id)
        {
            return _dbSet.Take(id).ToList();
        }
    }


}
