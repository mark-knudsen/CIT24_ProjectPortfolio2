using Microsoft.EntityFrameworkCore;

namespace MovieDataLayer.Data_Service.User_Framework_Repository
{
    public class UserSearchHistoryRepository : Repository<UserSearchHistoryModel>
    {
        public UserSearchHistoryRepository(IMDBContext context) : base(context) { }

        public async Task<IList<UserSearchHistoryModel>> GetAllSearchHistoryByUserId(int id)
        {
            return await _dbSet.AsNoTracking().Where(x => x.UserId == id).ToListAsync();
        }
        public async Task<UserSearchHistoryModel> Get(int userId, DateTime createdAt)
        {
            return await _dbSet.AsNoTracking().Where(x => x.UserId == userId && x.CreatedAt.Equals(createdAt)).FirstOrDefaultAsync();
        }
        public async Task<bool> Delete(int userId, DateTime createdAt)
        {
            try
            {
                var entity = await Get(userId, createdAt);

                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAll(int userId)
        {
            try
            {
                var entity = await _dbSet.Where(x => x.UserId == userId).ToListAsync();
                if (entity.Any())
                {
                    _dbSet.RemoveRange(entity);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
