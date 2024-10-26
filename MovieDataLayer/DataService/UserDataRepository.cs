using Microsoft.EntityFrameworkCore;
using MovieDataLayer.Extentions;
using MovieDataLayer.Interfaces;

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

    public IList<User> GetAllUsers_InqludeAll()
    {
       // return _dbSet.ToList();
        return _dbSet.Take(1).Include(x => x.UserSearchHistory).ToList();
    }   
    public IList<UserSearchHistory> GetAllUSearchHistoryByUserId(int id)
    {
       // return _dbSet.ToList();
        return _context.Set<UserSearchHistory>().Where(x => x.Id == id).ToList();
    }  
    public IList<User> GetAll(int id) // also have to remember to make them async
    {
       // return _dbSet.ToList();
        return _context.Set<User>().Take(20).IgnoreAutoIncludes().Where(x => x.Id == id).ToList();
    }
}

