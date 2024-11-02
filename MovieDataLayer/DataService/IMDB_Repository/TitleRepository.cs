using Microsoft.EntityFrameworkCore;
using MovieDataLayer.Models.IMDB_Models;
using MovieDataLayer.Models.IMDB_Models.IMDB_DTO;
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

        public async Task<IList<Title>> GetTitleByGenre(int id, int page = 0, int pageSize = 10)
        {
            const int maxPageSize = 10; //Max size of page retrieved from DB

            pageSize = pageSize > maxPageSize ? maxPageSize : pageSize; //Sets pageSize to maxPageSize if greater than maxPageSize

            return await _dbSet
                .Include(t => t.Poster)
                .Include(t => t.Plot)
                .Include(t => t.Rating)
                //.Include(t => t.LocalizedTitlesList)
                .Include(t => t.WritersList).ThenInclude(w => w.Person)
                .Include(t => t.DirectorsList).ThenInclude(d => d.Person)
                .Include(t => t.GenresList).ThenInclude(g => g.Genre)
                .Include(t => t.PrincipalCastList).ThenInclude(p => p.Person).Where(x => x.GenresList.Any(x => x.GenreId == id)).Skip(page * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<IList<TitleSearchResultDTO>> TitleSearch(int userId, string searchTerm) // also have to remember to make them async
        {
            string query = $"SELECT * FROM string_search('{userId}', '{searchTerm}');";
            return await _context.CallQuery<TitleSearchResultDTO>(query);
        }
        public async Task<IList<SimilarTitleSearchDTO>> SimilarTitles(string titleID) // also have to remember to make them async
        {
            // Should fix so the distinc is made in the function in the DB, then use the shorter version below!
            //string query = $"SELECT * FROM find_similar_movies('{titleID}') LIMIT 8;";
            string query = $"SELECT DISTINCT ON(primary_title) similar_title_id, primary_title, isadult, title_type, genres FROM find_similar_movies('{titleID}') ORDER BY primary_title DESC LIMIT 8;";
            return await _context.CallQuery<SimilarTitleSearchDTO>(query);
        }

        public async Task<int> CountByGenre(int genreId)
        {
            return await _dbSet.Where(title => title.GenresList.Any(g => g.GenreId == genreId)).CountAsync(); //should it be async?
        }



        //public IList<Title> GetAllTitleButWithLimit(int id)
        //{
        //    return _dbSet.Take(id).ToList();
        //}
    }
}
