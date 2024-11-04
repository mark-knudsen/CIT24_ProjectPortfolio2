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

        public async Task<IList<UserPersonBookmark>> GetAllPersonBookmarks(int id)
        {
            return await _context.Set<UserPersonBookmark>().AsNoTracking().Where(x => x.UserId == id).ToListAsync();
        }
        public async Task<IList<UserTitleBookmark>> GetAllTitleBookmarks(int id)
        {
            return await _context.Set<UserTitleBookmark>().AsNoTracking().Where(x => x.UserId == id).ToListAsync();
        }

        public async Task<User> Login(string email, string password)
        {

            var user = await _dbSet.Where(u => u.Email == email && u.Password == password).SingleAsync();

            if (user == null)
            {
                return null;
            }
            return user;
        }

        //public async Task<User> GetUser(int id)
        //{
        //    return await _dbSet.Where(u => u.Id == id).FirstOrDefaultAsync();
        //}

        //**** Proof of concept, CallSql.cs: ****//


    }
}
