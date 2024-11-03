using Microsoft.EntityFrameworkCore;

namespace MovieDataLayer.DataService.UserFrameworkRepository
{
    public class UserPersonBookmarkRepository : Repository<UserPersonBookmark>
    {
        public UserPersonBookmarkRepository(IMDBContext context) : base(context) { }

        public async Task<IList<UserPersonBookmark>> GetAll(int id)
        {
            return await _dbSet.AsNoTracking().Where(x => x.UserId == id).ToListAsync();
        }
        public async Task<UserPersonBookmark> Get(int userId, string personId)
        {
            return await _dbSet.Where(x => x.UserId == userId && x.PersonId.Equals(personId)).FirstOrDefaultAsync();
        }

        public async Task<bool> Delete(int userId, string titleId)
        {
            try
            {
                var entity = await Get(userId, titleId);

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
