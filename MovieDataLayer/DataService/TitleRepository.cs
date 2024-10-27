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
        public TitleRepository(IMDBContext context) : base(context) { } //Constructor that calls the base constructor, ensuring both the context and dbset are initialized. Btw we now can use the same context in both sub/super class


        public IList<Person> GetWritersByMovieId(string id)
        {

            var writers = _dbSet.Where(t => t.Id == id).Include(t => t.WritersList).ThenInclude(w => w.Person).SelectMany(t => t.WritersList.Select(w => w.Person)).ToList(); //Using selectmany to flatten the list of lists. Needed because we are working with nested list here!
            return writers;

        }

        //public IList<Title> GetAllTitleButWithLimit(int id)
        //{
        //    return _dbSet.Take(id).ToList();
        //}
    }


}
