using Microsoft.EntityFrameworkCore;


namespace MovieDataLayer;
public static class CallSql
{
    public static async Task<IEnumerable<T>> CallQuery<T>(this IMDBContext context, string query) where T : class
    {
        return await context.Set<T>().FromSqlRaw(query).ToListAsync();

    }
    public static bool CallProcedure(this IMDBContext context, string query) // wouldn't all procedures be called by triggers? Don't think we would ever call a procedure manually like this, might be wrong
    {
        try
        {
            context.Database.ExecuteSqlRaw(query);
            return true;
        }
        catch
        {
            return false;
        }
    }
}