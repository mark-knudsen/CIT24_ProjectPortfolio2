using Microsoft.EntityFrameworkCore;

namespace MovieDataLayer.DataService.UserFrameworkRepository
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(IMDBContext context) : base(context) { } //Constructor that calls the base constructor, ensuring both the context and dbset are initialized. Btw we now can use the same context in both sub/super class

        public async Task<IList<UserSearchHistory>> GetAllSearchHistoryByUserId(int id)
        {
            return await _context.Set<UserSearchHistory>().AsNoTracking().Where(x => x.UserId.Equals(id)).ToListAsync();
        }
        public async Task<IList<UserRating>> GetAllUserRatingByUserId(int id)
        {
            return await _context.Set<UserRating>().AsNoTracking().Where(x => x.UserId.Equals(id)).ToListAsync();
        }

        public async Task<IList<UserPersonBookmark>> GetAllPersonBookmarks(int id)
        {
            return await _context.Set<UserPersonBookmark>().AsNoTracking().Where(x => x.UserId == id).ToListAsync();
        }
        public async Task<IList<UserTitleBookmark>> GetAllTitleBookmarks(int id)
        {
            return await _context.Set<UserTitleBookmark>().AsNoTracking().Where(x => x.UserId == id).ToListAsync();
        }

        //public async Task<User> GetUser(int id)
        //{
        //    return await _dbSet.Where(u => u.Id == id).FirstOrDefaultAsync();
        //}

        //**** Proof of concept, CallSql.cs: ****//

        //public async Task<EmailSearchResult> GetByEmail(string email) // also have to remember to make them async
        //{
        //    string query = $"select * from get_customer('{email}')";
        //    return (await _context.CallQuery<EmailSearchResult>(query)).Single();
        //}
    }
}
