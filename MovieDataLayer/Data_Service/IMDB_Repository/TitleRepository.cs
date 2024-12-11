using Microsoft.EntityFrameworkCore;
using MovieDataLayer.Data_Service;
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
                //.Include(t => t.LocalizedTitlesList) //not planning to use this.
                .Include(t => t.WritersList).ThenInclude(w => w.Person)
                .Include(t => t.DirectorsList).ThenInclude(d => d.Person)
                .Include(t => t.GenresList).ThenInclude(g => g.Genre)
                .Include(t => t.PrincipalCastList).ThenInclude(p => p.Person).FirstOrDefaultAsync();
        }

        public async Task<IList<TitleModel>> GetAllTitles(int page = 0, int pageSize = 10) // could these be reused and not duplicated? for future iterations!
        {
            return await _dbSet.AsNoTracking()
                .Include(t => t.Poster)
                .Include(t => t.Plot)
                .Include(t => t.Rating)
                .Include(t => t.GenresList).ThenInclude(g => g.Genre)
                .Skip(page * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IList<TitleModel>> GetTitleByGenre(int id, int page = 0, int pageSize = 10)
        {
            return await _dbSet
                .Include(t => t.Poster)
                .Include(t => t.Plot)
                .Include(t => t.Rating)
                //.Include(t => t.LocalizedTitlesList)  //not planning to use this.
                .Include(t => t.WritersList).ThenInclude(w => w.Person)
                .Include(t => t.DirectorsList).ThenInclude(d => d.Person)
                .Include(t => t.GenresList).ThenInclude(g => g.Genre)
                .Include(t => t.PrincipalCastList).ThenInclude(p => p.Person).Where(x => x.GenresList.Any(x => x.GenreId == id)).Skip(page * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<(IEnumerable<TitleSearchResultTempTable> SearchResult, int totalEntities)> TitleSearch(string searchTerm, int userId, int page = 0, int pageSize = 10)
        {
            string query = $"SELECT * FROM title_search('{searchTerm}', '{userId}')"; //Currently uses a title_search test function, needs to be changed if merging into main..

            var searchResult = await _context.CallQuery<TitleSearchResultTempTable>(query, page, pageSize);

            if (!searchResult.Any()) return (searchResult, 0); //this line allows for searchResult to not contain anything when returned to API/frontend.
            int totalElements = searchResult.FirstOrDefault().TotalElements;

            return (searchResult, totalElements);
        }


        public async Task<IEnumerable<SimilarTitleSearchTempTable>> SimilarTitles(string titleID)
        {
            var page = new Random().Next(1, 100);

            //Should fix so the distinct is made in the function in the DB, then use the shorter version below!
            //string query = $"SELECT * FROM find_similar_movies('{titleID}') LIMIT 8;";
            // string query = $"SELECT DISTINCT ON(primary_title) similar_title_id, primary_title, isadult, title_type, genres FROM find_similar_movies('{titleID}') ORDER BY primary_title DESC LIMIT 8";
            string query = $"SELECT * FROM find_similar_movies('{titleID}')"; // fixed the distinct in sql function
            return await _context.CallQuery<SimilarTitleSearchTempTable>(query, page, 10);
        }
        public async  Task<(IEnumerable<TitleSearchResultTempTable> SearchResult, int totalEntities)> AdvancedTitleSearch(string searchTerm, int userId, int? genreId, int? startYear, int? endYear, int page = 0, int pageSize = 10)
        {
            string query = $"SELECT * FROM advanced_search('{searchTerm}', {userId}, " +
                $"{(genreId is not null ? genreId : "null")}, " +
                $"{(startYear is not null ? startYear : "0")}, " +
                $"{(endYear is not null ? endYear : "3000")})"; 
          
            var searchResult = await _context.CallQuery<TitleSearchResultTempTable>(query, page, pageSize);
            
            if (!searchResult.Any()) return (searchResult, 0); //this line allows for searchResult to not contain anything when returned to API/frontend.
            int totalElements = searchResult.FirstOrDefault().TotalElements;

            return (searchResult, totalElements);
        }
        public async Task<int> CountByGenre(int genreId)
        {
            return await _dbSet.AsNoTracking().Where(title => title.GenresList.Any(g => g.GenreId == genreId)).CountAsync();
        }

    }
}
