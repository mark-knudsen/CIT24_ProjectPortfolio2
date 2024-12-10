using Microsoft.EntityFrameworkCore;
using MovieDataLayer.Models.IMDB_Models;

namespace MovieDataLayer.Data_Service.User_Framework_Repository
{
    public class UserTitleBookmarkRepository : Repository<UserTitleBookmarkModel>
    {
        public UserTitleBookmarkRepository(IMDBContext context) : base(context) { }
        public async Task<IList<UserTitleBookmarkModel>> GetAll(int id, int page = 0, int pageSize = 10)
        {
            return await _dbSet.AsNoTracking().Where(x => x.UserId == id).Include(p => p.Title).Skip(page * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<UserTitleBookmarkModel> Get(int userId, string titleId)
        {
            return await _dbSet.AsNoTracking().Where(x => x.UserId == userId && x.TitleId.Equals(titleId)).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteTitleBookmark(int userId, string titleId)
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

        public async Task<bool> DeleteAllTitleBookmarks(int userId)
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
