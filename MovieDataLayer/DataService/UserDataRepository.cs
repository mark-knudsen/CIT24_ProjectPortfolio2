using Microsoft.EntityFrameworkCore;
using MovieDataLayer.Interfaces;
using Npgsql;

namespace MovieDataLayer;
public class UserDataRepository
{
    private readonly IMDBContext _context;
    private readonly DbSet<User> _dbSet;
    public UserDataRepository(IMDBContext context)
    {
        _context = context;
        _dbSet = _context.Set<User>();
    }

    public async Task<IList<User>> GetAllUsers_InqludeAll()
    {
        return await _dbSet.AsNoTracking().Take(1).Include(x => x.UserSearchHistory).ToListAsync();
    }
    public async Task<IList<UserSearchHistory>> GetAllUSearchHistoryByUserId(int id)
    {
        return await _context.Set<UserSearchHistory>().AsNoTracking().Where(x => x.Id == id).ToListAsync();
    }
    public async Task<User> GetAll(int id)
    {
        return await _context.Set<User>().AsNoTracking().IgnoreAutoIncludes().SingleAsync(x => x.Id == id);
    }
    public async Task<EmailSearchResult> GetByEmail(string email) // also have to remember to make them async
    {
        string query = $"select * from get_customer('{email}')";
        return (await _context.CallQuery<EmailSearchResult>(query)).Single();
    }
}
