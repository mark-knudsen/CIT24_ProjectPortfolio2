using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieDataLayer.Models.IMDB_Models;

namespace MovieDataLayer.DataService.IMDB_Repository
{
    public class PersonRepository : Repository<Person>
    {
        public PersonRepository(IMDBContext context) : base(context) { }


        public async Task<Person> GetPerson(string id)
        {
            var p = await _dbSet.Where(p => p.Id.Equals(id))
            .Include(p => p.PrimaryProfessions).ThenInclude(x => x.Profession)
            .Include(p => p.MostRelevantTitles).ThenInclude(t => t.Title).FirstOrDefaultAsync();
            return p;

        }
    }
}
