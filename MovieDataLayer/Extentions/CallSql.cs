using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer;
public static class CallSql
{
    public static async Task<IList<T>> CallQuery<T>(this IMDBContext context, string query) where T : class
    {
        return await context.Set<T>().FromSqlRaw(query).ToListAsync();

        //using var connection = (NpgsqlConnection)context.Database.GetDbConnection();
        //connection.Open();

        //using var command = new NpgsqlCommand();
        //command.Connection = connection;

        //command.CommandText = query;

        ////command.ExecuteNonQuery();
        //using var reader = command.ExecuteReader(); 

        //while (reader.Read())  // how would you map the attributes for different kind of objects? To make it generic!
        //{
        //    return new User(){ Email = reader.GetString(0), FirstName = reader.GetString(1)};
        //}

        //return new User();
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
