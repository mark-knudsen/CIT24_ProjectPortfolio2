using Microsoft.EntityFrameworkCore;
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
        public DbSet<PrincipalCast> PrincipalCasts { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<LocalizedTitle> LocalizedTitles { get; set; }
        public DbSet<MostRelevant> MostRelevants { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Rating> Ratings { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

            optionsBuilder.UseNpgsql("host=cit.ruc.dk; db=cit06; uid=cit06; pwd=6fkEI8NdedtI;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapPerson(modelBuilder);
            MapTitle(modelBuilder);
            MapPrincipalCast(modelBuilder);
            MapGenres(modelBuilder);
            MapLocalizedTitles(modelBuilder);
            MapLocalizedDetails(modelBuilder);
            MapMostRelevant(modelBuilder);
            MapRating(modelBuilder);

        }

        private void MapRating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>().ToTable("rating");
            modelBuilder.Entity<Rating>().HasKey(r => r.Id);
            modelBuilder.Entity<Rating>().Property(r => r.Id).HasColumnName("title_id");
            modelBuilder.Entity<Rating>().Property(r => r.AverageRating).HasColumnName("average_rating");
            modelBuilder.Entity<Rating>().Property(r => r.VoteCount).HasColumnName("vote_count");
        }

        private void MapMostRelevant(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MostRelevant>().ToTable("most_relevant");
            modelBuilder.Entity<MostRelevant>().HasKey(mr => new { mr.PersonId, mr.TitleId });
            modelBuilder.Entity<MostRelevant>().Property(mr => mr.PersonId).HasColumnName("person_id");
            modelBuilder.Entity<MostRelevant>().Property(mr => mr.TitleId).HasColumnName("title_id");
        }

        private static void MapLocalizedDetails(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocalizedDetail>().ToTable("localized_detail");
            modelBuilder.Entity<LocalizedDetail>().HasKey(ld => ld.Id);
            modelBuilder.Entity<LocalizedDetail>().Property(ld => ld.Id).HasColumnName("localized_id");
            modelBuilder.Entity<LocalizedDetail>().Property(ld => ld.Title).HasColumnName("localized_title");
            modelBuilder.Entity<LocalizedDetail>().Property(ld => ld.Language).HasColumnName("language");
            modelBuilder.Entity<LocalizedDetail>().Property(ld => ld.Region).HasColumnName("region");
            modelBuilder.Entity<LocalizedDetail>().Property(ld => ld.Type).HasColumnName("type");
            modelBuilder.Entity<LocalizedDetail>().Property(ld => ld.Attribute).HasColumnName("attribute");
        }

        private static void MapLocalizedTitles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocalizedTitle>().ToTable("localized_title");

            modelBuilder.Entity<LocalizedTitle>().HasKey(lt => lt.Id);

            modelBuilder.Entity<LocalizedTitle>().Property(lt => lt.Id).HasColumnName("localized_id");

            modelBuilder.Entity<LocalizedTitle>().Property(lt => lt.TitleId).HasColumnName("title_id");




        }

        private static void MapGenres(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().ToTable("genre");
            modelBuilder.Entity<Genre>().HasKey(p => p.Id);

            modelBuilder.Entity<Genre>().Property(p => p.Id).HasColumnName("genre_id");
            modelBuilder.Entity<Genre>().Property(p => p.Name).HasColumnName("genre");
        }

        private static void MapPerson(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("person");
            modelBuilder.Entity<Person>().HasKey(p => p.Id);

            modelBuilder.Entity<Person>().Property(p => p.Id).HasColumnName("person_id");
            modelBuilder.Entity<Person>().Property(p => p.Name).HasColumnName("primary_name");
            modelBuilder.Entity<Person>().Property(p => p.BirthYear).HasColumnName("birth_year");
            modelBuilder.Entity<Person>().Property(p => p.DeathYear).HasColumnName("death_year");
            //modelBuilder.Entity<Person>().Navigation(p => p.MostRelevantTitles).AutoInclude();

            //modelBuilder.Entity<Person>().HasMany(t => t.Titles).WithMany(c => c.Persons);
            //modelBuilder.Entity<Person>().HasMany(t => t.PrimaryProfessions).WithOne(t => t.Person);

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

            modelBuilder.Entity<Title>()
     .HasOne(t => t.rating) // Navigation property in Title
     .WithOne(r => r.title) // Navigation property in Rating
     .HasForeignKey<Rating>(r => r.Id); // Foreign key in Rating

            modelBuilder.Entity<Title>()
            .HasMany(t => t.WritersList)
            .WithMany(p => p.TitlesList)
            .UsingEntity(j => j.ToTable("writers"));

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

        //private static void Map(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<PrincipalCast>().ToTable("principal_cast");
        //    modelBuilder.Entity<PrincipalCast>().HasKey(x => new { x.PersonId, x.TitleId });

        //    modelBuilder.Entity<PrincipalCast>().Property(x => x.PersonId).HasColumnName("person_id");
        //    modelBuilder.Entity<PrincipalCast>().Property(x => x.PersonId).HasColumnName("title_id");
        //}
    }
}
