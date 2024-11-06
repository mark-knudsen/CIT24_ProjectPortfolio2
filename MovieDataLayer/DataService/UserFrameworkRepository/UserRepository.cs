using Microsoft.EntityFrameworkCore;

namespace MovieDataLayer.DataService.UserFrameworkRepository
{
    public class UserRepository : Repository<UserModel>
    {
        public UserRepository(IMDBContext context) : base(context) { } //Constructor that calls the base constructor, ensuring both the context and dbset are initialized. Btw we now can use the same context in both sub/super class

        public async Task<IList<UserSearchHistoryModel>> GetAllSearchHistoryByUserId(int id)
        {
            return await _context.Set<UserSearchHistoryModel>().AsNoTracking().Where(x => x.UserId.Equals(id)).ToListAsync();
        }

        public async Task<IList<UserPersonBookmarkModel>> GetAllPersonBookmarks(int id)
        {
            return await _context.Set<UserPersonBookmarkModel>().AsNoTracking().Where(x => x.UserId == id).ToListAsync();
        }
        public async Task<IList<UserTitleBookmarkModel>> GetAllTitleBookmarks(int id)
        {
            return await _context.Set<UserTitleBookmarkModel>().AsNoTracking().Where(x => x.UserId == id).ToListAsync();
        }
        public async Task UpdateUser(UserModel user)
        {
            _context.Update(user);

            await _context.SaveChangesAsync();
        }
        public async Task<UserModel> Login(string email, string password)
        {

            var user = await _dbSet.Where(u => u.Email == email && u.Password == password).SingleAsync();

            if (user == null)
            {
                return null;
            }
            return user;
        }



        public async Task<UserModel> GetUser(int id)
        {
            return await _dbSet.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        //**** Proof of concept, CallSql.cs: ****//


    }
}
