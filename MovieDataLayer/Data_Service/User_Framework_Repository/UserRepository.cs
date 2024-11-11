using Microsoft.EntityFrameworkCore;

namespace MovieDataLayer.Data_Service.User_Framework_Repository
{
    public class UserRepository : Repository<UserModel>
    {
        public UserRepository(IMDBContext context) : base(context) { } //Constructor that calls the base constructor, ensuring both the context and dbset are initialized. Btw we now can use the same context in both sub/super class

        public async Task UpdateUser(UserModel user)
        {
            _context.Update(user);

            await _context.SaveChangesAsync();
        }
        public async Task<UserModel> Login(string email, string password)
        {
            try
            {
                return await _dbSet.Where(u => u.Email == email && u.Password == password).SingleAsync();
            }
            catch
            {
                return null; // return error at some point
            }
        }

        public async Task<UserModel> GetUser(int id)
        {
            return await _dbSet.Where(u => u.Id == id).FirstOrDefaultAsync();
        }
    }
}
