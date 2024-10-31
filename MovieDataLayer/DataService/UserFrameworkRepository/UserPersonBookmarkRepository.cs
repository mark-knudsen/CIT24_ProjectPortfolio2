using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer.DataService.UserFrameworkRepository
{
    public class UserPersonBookmarkRepository : Repository<UserPersonBookmark>
    {
        public UserPersonBookmarkRepository(IMDBContext context) : base(context) { }

        public async Task<IList<UserPersonBookmark>> GetAllPersonBookmarks(int id)
        {
            return await _dbSet.AsNoTracking().Where(x => x.UserId == id).ToListAsync();
        }
        public async Task<UserPersonBookmark> GetPersonBookmark(int userId, string personId)
        {
            return await _dbSet.Where(x => x.UserId == userId && x.PersonId.Equals(personId)).FirstOrDefaultAsync();
        }

        public async Task<bool> DeletePersonBookmark(int userId, string titleId)
        {
            try
            {
                var entity = await GetPersonBookmark(userId, titleId);

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
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> DeleteAllPersonBookmarks(int userId)
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
            catch (Exception)
            {

                return false;
            }
        }
    }
}
