using Microsoft.EntityFrameworkCore;
using MovieDataLayer.Models.IMDB_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.DataService.IMDB_Repository
{
    public class TitleRepository : Repository<Title>
    {
        public TitleRepository(IMDBContext context) : base(context) { } //Constructor that calls the base constructor, ensuring both the context and dbset are initialized. Btw we now can use the same context in both sub/super class


        public async Task<IList<Person>> GetWritersByMovieId(string id)
        {

            return await _dbSet.Where(t => t.Id.Equals(id)).Include(t => t.WritersList).ThenInclude(w => w.Person).SelectMany(t => t.WritersList.Select(w => w.Person)).ToListAsync(); //Using selectmany to flatten the list of lists. Needed because we are working with nested list here!


        }

        public IList<Title> GetTitleDetails(string id)
        {
            //return _dbSet.Where(t => t.Id.Equals(id)).Include(t => t.WritersList).ThenInclude(w => w.Person).Include(t => t.DirectorsList).ThenInclude(d => d.Person).Include(t => t.GenresList).ThenInclude(g => g.Genre).Include(t => t.PrincipalCastList).ThenInclude(p => p.Person).Include(t => t.Rating).FirstOrDefault(); //Using include to get all the related entities. This is a bit of a heavy query, but it is needed to get all the details of a title.
            //return _dbSet.Where(t => t.Id.Equals(id)).Include(t => t.WritersList).ThenInclude(w => w.Person).Include(t => t.DirectorsList).ThenInclude(d => d.Person).Include(t => t.GenresList).ThenInclude(g => g.Genre).FirstOrDefault();
            return _dbSet.Where(t => t.Id.Equals(id)).Include(t => t.Rating).Take(100).ToList();
        }




        //public IList<Title> GetAllTitleButWithLimit(int id)
        //{
        //    return _dbSet.Take(id).ToList();
        //}
    }


}
