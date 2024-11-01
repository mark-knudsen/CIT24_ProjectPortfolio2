using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieDataLayer.DataService.UserFrameworkRepository
{
    public class UserRatingRepository : Repository<UserRating>
    {
        public UserRatingRepository(IMDBContext context) : base(context) { }

        public async Task<IList<UserRating>> GetAllUserRatingByUserId(int id)
        {
            return await _dbSet.AsNoTracking().Where(x => x.UserId.Equals(id)).ToListAsync();
        }

        public async Task<UserRating> GetUserRating(int id, string titleId)
        {
            return await _dbSet.AsNoTracking().Where(x => x.UserId.Equals(id) && x.TitleId.Equals(titleId)).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteUserRating(int id, string titleId) 
        {
            try
            {
                var entity = await GetUserRating(id, titleId);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    _context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public async Task<bool> DeleteAllUserRatings(int userId)
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
