using Microsoft.EntityFrameworkCore;

namespace MovieDataLayer.Data_Service.User_Framework_Repository
{
    public class UserPersonBookmarkRepository : Repository<UserPersonBookmarkModel>
    {
        public UserPersonBookmarkRepository(IMDBContext context) : base(context) { }
        public async Task<IList<UserPersonBookmarkModel>> GetAll(int id)
        {
            return await _dbSet.AsNoTracking().Where(x => x.UserId == id).ToListAsync();
        }
        public async Task<UserPersonBookmarkModel> Get(int userId, string personId)
        {
            return await _dbSet.AsNoTracking().Where(x => x.UserId == userId && x.PersonId.Equals(personId)).FirstOrDefaultAsync();
        }

        public async Task<bool> Delete(int userId, string titleId)
        {
            try
            {
                var entity = await Get(userId, titleId);

                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    await _context.SaveChangesAsync();
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
                    await _context.SaveChangesAsync();
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
