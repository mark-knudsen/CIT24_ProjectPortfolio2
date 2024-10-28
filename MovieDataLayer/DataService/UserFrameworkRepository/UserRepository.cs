using Microsoft.EntityFrameworkCore;
using MovieDataLayer.Models.IMDB_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.DataService.UserFrameworkRepository
{
    public class UserRepository : Repository<User>
    {
        private readonly IMDBContext _context;

        public UserRepository(IMDBContext context) : base(context) { } //Constructor that calls the base constructor, ensuring both the context and dbset are initialized. Btw we now can use the same context in both sub/super class


        public async Task<IList<User>> GetAllUsers_InqludeAll()
        {
            return await _dbSet.AsNoTracking().Take(1).Include(x => x.UserSearchHistory).ToListAsync();
        }
        public async Task<IList<UserSearchHistory>> GetAllUSearchHistoryByUserId(int id)
        {
            return await _context.Set<UserSearchHistory>().AsNoTracking().Where(x => x.UserId.Equals(id)).ToListAsync();
        }
        public async Task<User> GetAll(int id)
        {
            return await _context.Set<User>().AsNoTracking().IgnoreAutoIncludes().SingleAsync(x => x.Id.Equals(id));
        }


        //**** Proof of concept, CallSql.cs: ****//

        //public async Task<EmailSearchResult> GetByEmail(string email) // also have to remember to make them async
        //{
        //    string query = $"select * from get_customer('{email}')";
        //    return (await _context.CallQuery<EmailSearchResult>(query)).Single();
        //}
    }
}
