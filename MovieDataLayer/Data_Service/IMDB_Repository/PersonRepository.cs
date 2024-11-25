using Microsoft.EntityFrameworkCore;
using MovieDataLayer.Data_Service;
using MovieDataLayer.Models.IMDB_Models;
using MovieDataLayer.Models.IMDB_Models.IMDB_Temp_Tables;

namespace MovieDataLayer.DataService.IMDB_Repository
{
    public class PersonRepository : Repository<PersonModel>
    {
        public PersonRepository(IMDBContext context) : base(context) { }

        public async Task<PersonModel> GetPerson(string id)
        {
            var p = await _dbSet.Where(p => p.Id.Equals(id))
            .Include(p => p.PrimaryProfessions).ThenInclude(x => x.Profession)
            .Include(p => p.MostRelevantTitles).ThenInclude(t => t.Title).FirstOrDefaultAsync();
            return p;

        }

        public async Task<IEnumerable<PersonSearchResultTempTable>> PersonSearch(int userId, string searchTerm, int page = 0, int pageSize = 10)
        {
            string query = $"SELECT * FROM person_search('{searchTerm}', '{userId}')";
            return await _context.CallQuery<PersonSearchResultTempTable>(query, page, pageSize);
        }
    }
}
