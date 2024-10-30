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
        public async Task<Title> GetTitle(string id)
        {
            return await _dbSet.Where(t => t.Id.Equals(id))
                .Include(t => t.Poster)
                .Include(t => t.Plot)
                .Include(t => t.Rating)
                //.Include(t => t.LocalizedTitlesList)
                .Include(t => t.WritersList).ThenInclude(w => w.Person)
                .Include(t => t.DirectorsList).ThenInclude(d => d.Person)
                .Include(t => t.GenresList).ThenInclude(g => g.Genre)
                .Include(t => t.PrincipalCastList).ThenInclude(p => p.Person).FirstOrDefaultAsync();
        }  
        
        public async Task<IList<Title>> GetTitleByGenre(int id)
        {
            return await _dbSet
                .Include(t => t.Poster)
                .Include(t => t.Plot)
                .Include(t => t.Rating)
                //.Include(t => t.LocalizedTitlesList)
                .Include(t => t.WritersList).ThenInclude(w => w.Person)
                .Include(t => t.DirectorsList).ThenInclude(d => d.Person)
                .Include(t => t.GenresList).ThenInclude(g => g.Genre)
                .Include(t => t.PrincipalCastList).ThenInclude(p => p.Person).Where(x => x.GenresList.Any(x => x.GenreId == id)).Take(20).ToListAsync();
        }

        //public IList<Title> GetAllTitleButWithLimit(int id)
        //{
        //    return _dbSet.Take(id).ToList();
        //}
    }
}
