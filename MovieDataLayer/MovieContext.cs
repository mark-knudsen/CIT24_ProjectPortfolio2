using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer
{
    public class MovieContext : DbContext
    {
        //Nice to have, configure context in Program.cs instead: https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/

        public DbSet<PersonModel> Persons { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

            optionsBuilder.UseNpgsql("host=cit.ruc.dk; db=cit06; uid=cit06; pwd=6fkEI8NdedtI;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapPerson(modelBuilder);
        }

        private static void MapPerson(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonModel>().ToTable("person");
            modelBuilder.Entity<PersonModel>().HasKey(p => p.Id);

            modelBuilder.Entity<PersonModel>().Property(p => p.Id).HasColumnName("person_id");
            modelBuilder.Entity<PersonModel>().Property(p => p.Name).HasColumnName("primary_name");
            modelBuilder.Entity<PersonModel>().Property(p => p.BirthYear).HasColumnName("birth_year");
            modelBuilder.Entity<PersonModel>().Property(p => p.DeathYear).HasColumnName("death_year");
        }
    }
}
