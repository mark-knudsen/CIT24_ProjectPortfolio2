using Microsoft.EntityFrameworkCore;

namespace MovieDataLayer.Data_Service.User_Framework_Repository
{
    public class UserRatingRepository : Repository<UserRatingModel>
    {
        public UserRatingRepository(IMDBContext context) : base(context) { }

        public async Task<IList<UserRatingModel>> GetAllUserRatingByUserId(int id)
        {
            return await _dbSet.AsNoTracking().Where(x => x.UserId == id).Include(t => t.Title).ThenInclude(p => p.Poster).ToListAsync();
        }

        public async Task<UserRatingModel> GetUserRating(int id, string titleId)
        {
            return await _dbSet.AsNoTracking().Where(x => x.UserId == id && x.TitleId.Equals(titleId)).Include(t => t.Title).ThenInclude(p => p.Poster).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteUserRating(int id, string titleId)
        {
            try
            {
                var entity = await GetUserRating(id, titleId);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    await _context.SaveChangesAsync();
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
