﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieDataLayer
{
    public class IMDBContext : DbContext
    {
        //Nice to have, configure context in Program.cs instead: https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/

        public DbSet<Person> Persons { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Genre> Genres { get; set; }
       // public DbSet<PrincipalCast> PrincipalCasts { get; set; }   // it wants a primary key to be defined but it does!??!?!


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

            //optionsBuilder.UseNpgsql("host=cit.ruc.dk; db=cit06; uid=cit06; pwd=6fkEI8NdedtI;"); // school
            optionsBuilder.UseNpgsql("host=localhost;db=test_imdb;uid=postgres;pwd=postgres"); // local
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //MapPerson(modelBuilder);
            //MapTitle(modelBuilder);
            //MapPrincipalCast(modelBuilder);
            MapGenre(modelBuilder);
        }

        private static void MapPerson(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("person");
            modelBuilder.Entity<Person>().HasKey(p => p.Id);

            modelBuilder.Entity<Person>().Property(p => p.Id).HasColumnName("person_id");
            modelBuilder.Entity<Person>().Property(p => p.Name).HasColumnName("primary_name");
            modelBuilder.Entity<Person>().Property(p => p.BirthYear).HasColumnName("birth_year");
            modelBuilder.Entity<Person>().Property(p => p.DeathYear).HasColumnName("death_year");

            modelBuilder.Entity<Person>().HasMany(t => t.Titles).WithMany(c => c.Persons);
            modelBuilder.Entity<Person>().HasMany(t => t.PrimaryProfessions).WithOne(t => t.Person);

            //modelBuilder.Entity<Person>().Navigation(p => p.Titles).AutoInclude();
            modelBuilder.Entity<Person>().Navigation(p => p.Titles);

        }

        private static void MapTitle(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Title>().ToTable("title");
            modelBuilder.Entity<Title>().HasKey(t => t.Id);

            modelBuilder.Entity<Title>().Property(t => t.Id).HasColumnName("title_id");
            modelBuilder.Entity<Title>().Property(t => t.TitleType).HasColumnName("title_type");
            modelBuilder.Entity<Title>().Property(t => t.PrimaryTitle).HasColumnName("primary_title");
            modelBuilder.Entity<Title>().Property(t => t.OriginalTitle).HasColumnName("original_title");
            modelBuilder.Entity<Title>().Property(t => t.StartYear).HasColumnName("start_year");
            modelBuilder.Entity<Title>().Property(t => t.EndYear).HasColumnName("end_year");
            modelBuilder.Entity<Title>().Property(t => t.Runtime).HasColumnName("runtime");
            modelBuilder.Entity<Title>().Property(t => t.IsAdult).HasColumnName("isadult");

            modelBuilder.Entity<Title>().Navigation(t => t.Persons);


            // Configure Many-to-Many relationship

            //modelBuilder.Entity<Title>()
            //    .HasMany(t => t.Persons)
            //    .WithMany(c => c.Titles);
            // If not working add Foreign Keys


        }
        private static void MapPrincipalCast(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrincipalCast>().ToTable("principal_cast");
            modelBuilder.Entity<PrincipalCast>().HasKey(x => new { x.PersonId, x.TitleId });

            modelBuilder.Entity<PrincipalCast>().Property(x => x.PersonId).HasColumnName("person_id");
            modelBuilder.Entity<PrincipalCast>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<PrincipalCast>().Property(x => x.CharacterName).HasColumnName("character_name");
            modelBuilder.Entity<PrincipalCast>().Property(x => x.Category).HasColumnName("category");
            modelBuilder.Entity<PrincipalCast>().Property(x => x.Job).HasColumnName("job");
        }

        private static void MapGenre(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().ToTable("genre_list");
            modelBuilder.Entity<Genre>().HasKey(x => x.Id);

            modelBuilder.Entity<Genre>().Property(x => x.Id).HasColumnName("genre_id");
            modelBuilder.Entity<Genre>().Property(x => x.Name).HasColumnName("genre");

        }

        //private static void Map(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<PrincipalCast>().ToTable("principal_cast");
        //    modelBuilder.Entity<PrincipalCast>().HasKey(x => new { x.PersonId, x.TitleId });

        //    modelBuilder.Entity<PrincipalCast>().Property(x => x.PersonId).HasColumnName("person_id");
        //    modelBuilder.Entity<PrincipalCast>().Property(x => x.PersonId).HasColumnName("title_id");
        //}
    }
}
