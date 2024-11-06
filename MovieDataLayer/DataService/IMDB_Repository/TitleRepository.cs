using Microsoft.EntityFrameworkCore;
using MovieDataLayer.Models.IMDB_Models;

namespace MovieDataLayer.DataService.IMDB_Repository
{
    public class TitleRepository : Repository<TitleModel>
    {
        public TitleRepository(IMDBContext context) : base(context) { } //Constructor that calls the base constructor, ensuring both the context and dbset are initialized. Btw we now can use the same context in both sub/super class

        public async Task<IList<PersonModel>> GetWritersByMovieId(string id)
        {
            return await _dbSet.Where(t => t.Id.Equals(id)).Include(t => t.WritersList).ThenInclude(w => w.Person).SelectMany(t => t.WritersList.Select(w => w.Person)).ToListAsync(); //Using selectmany to flatten the list of lists. Needed because we are working with nested list here!
        }
        public async Task<TitleModel> GetTitle(string id)
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

        public async Task<IList<TitleModel>> GetAllTitles(int page = 0, int pageSize = 10) // could these be reused and not duplicated?
        {
            return await _dbSet.AsNoTracking()
                .Include(t => t.Poster)
                .Include(t => t.Plot)
                .Include(t => t.Rating)
                .Include(t => t.GenresList).ThenInclude(g => g.Genre)
                .Skip(page * pageSize).Take(pageSize).ToListAsync();
        }
        //AsNoTracking().Skip(page* pageSize).Take(pageSize).ToListAsync()

        public async Task<IList<TitleModel>> GetTitleByGenre(int id, int page = 0, int pageSize = 10)
        {
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
        public async Task<IEnumerable<TitleSearchResultModel>> TitleSearch(int userId, string searchTerm, int page = 0, int pageSize = 10) // also have to remember to make them async
        {
            string query = $"SELECT * FROM string_search('{userId}', '{searchTerm}')";
            return await _context.CallQuery<TitleSearchResultModel>(query, page, pageSize);
        }
        public async Task<IEnumerable<SimilarTitleSearchModel>> SimilarTitles(string titleID, int page = 0, int pageSize = 10) // also have to remember to make them async
        {
            // Should fix so the distinc is made in the function in the DB, then use the shorter version below!
            //string query = $"SELECT * FROM find_similar_movies('{titleID}') LIMIT 8;";
            string query = $"SELECT DISTINCT ON(primary_title) similar_title_id, primary_title, isadult, title_type, genres FROM find_similar_movies('{titleID}') ORDER BY primary_title DESC LIMIT 8";
            return await _context.CallQuery<SimilarTitleSearchModel>(query, page, pageSize);
        }
        public async Task<int> CountByGenre(int genreId)
        {
            return await _dbSet.AsNoTracking().Where(title => title.GenresList.Any(g => g.GenreId == genreId)).CountAsync(); //should it be async?
        }
        //public IList<Title> GetAllTitleButWithLimit(int id)
        //{
        //    return _dbSet.Take(id).ToList();
        //}
    }
}
