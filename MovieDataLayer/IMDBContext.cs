using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public DbSet<MostRelevant> MostRelevants { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<LocalizedTitle> LocalizedTitles { get; set; }
        public DbSet<PrincipalCast> PrincipalCasts { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<LocalizedDetail> LocalizedDetails { get; set; }
        public DbSet<PrimaryProfession> PrimaryProfessions { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<EpisodeFromSeries> EpisodeFromSeries { get; set; }
        public DbSet<Plot> Plots { get; set; }
        public DbSet<Poster> Posters { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Writer> Writers { get; set; }


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
            MapProfession(modelBuilder);
            MapMostRelevant(modelBuilder);
            MapGenre(modelBuilder);
            MapLocalizedTitle(modelBuilder);
            MapLocalizedDetail(modelBuilder);
            MapPrimaryProfession(modelBuilder);
            MapDirector(modelBuilder);
            MapEpisodeFromSeries(modelBuilder);
            MapPlot(modelBuilder);
            MapPoster(modelBuilder);
            MapRating(modelBuilder);
            MapWriter(modelBuilder);
        }

        private static void MapPerson(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Person>().ToTable("person");
            modelBuilder.Entity<Person>().HasKey(p => p.Id);
            
            //columns
            modelBuilder.Entity<Person>().Property(p => p.Id).HasColumnName("person_id");
            modelBuilder.Entity<Person>().Property(p => p.Name).HasColumnName("primary_name");
            modelBuilder.Entity<Person>().Property(p => p.BirthYear).HasColumnName("birth_year");
            modelBuilder.Entity<Person>().Property(p => p.DeathYear).HasColumnName("death_year");
            //modelBuilder.Entity<Person>().Navigation(p => p.MostRelevantTitles).AutoInclude();
            ////modelBuilder.Entity<Person>().HasMany(t => t.Titles).WithMany(c => c.Persons);


        }

        private static void MapTitle(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Title>().ToTable("title");
            modelBuilder.Entity<Title>().HasKey(t => t.Id);

            //columns
            modelBuilder.Entity<Title>().Property(t => t.Id).HasColumnName("title_id");
            modelBuilder.Entity<Title>().Property(t => t.TitleType).HasColumnName("title_type");
            modelBuilder.Entity<Title>().Property(t => t.PrimaryTitle).HasColumnName("primary_title");
            modelBuilder.Entity<Title>().Property(t => t.OriginalTitle).HasColumnName("original_title");
            modelBuilder.Entity<Title>().Property(t => t.StartYear).HasColumnName("start_year");
            modelBuilder.Entity<Title>().Property(t => t.EndYear).HasColumnName("end_year");
            modelBuilder.Entity<Title>().Property(t => t.Runtime).HasColumnName("runtime");
            modelBuilder.Entity<Title>().Property(t => t.IsAdult).HasColumnName("isadult");


            // Configure Many-to-Many relationship

            //modelBuilder.Entity<Title>()
            //    .HasMany(t => t.Persons)
            //    .WithMany(c => c.Titles);
            // If not working add Foreign Keys


        }

        private static void MapProfession(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profession>().ToTable("profession");
            modelBuilder.Entity<Profession>().HasKey(x => new { x.Id });

            //columns
            modelBuilder.Entity<Profession>().Property(x => x.Id).HasColumnName("profession_id");
            modelBuilder.Entity<Profession>().Property(x => x.Name).HasColumnName("profession_name");

        }

        private static void MapPrimaryProfession(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrimaryProfession>().ToTable("primary_profession");
            modelBuilder.Entity<PrimaryProfession>().HasKey(x => new { x.Id, x.PersonId });

            //columns
            modelBuilder.Entity<PrimaryProfession>().Property(x => x.Id).HasColumnName("profession_id");
            modelBuilder.Entity<PrimaryProfession>().Property(x => x.PersonId).HasColumnName("person_id");

            //Relations
            //modelBuilder.Entity<Person>().HasMany(x => x.PrimaryProfessions).WithOne(c => c.Person);
        }
        private static void MapPrincipalCast(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrincipalCast>().ToTable("principal_cast");
            modelBuilder.Entity<PrincipalCast>().HasKey(x => new { x.PersonId, x.TitleId, x.Ordering});

            //columns
            modelBuilder.Entity<PrincipalCast>().Property(x => x.PersonId).HasColumnName("person_id");
            modelBuilder.Entity<PrincipalCast>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<PrincipalCast>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<PrincipalCast>().Property(x => x.CharacterName).HasColumnName("character_name");
            modelBuilder.Entity<PrincipalCast>().Property(x => x.Category).HasColumnName("category");
            modelBuilder.Entity<PrincipalCast>().Property(x => x.Job).HasColumnName("job");
        }

        private static void MapMostRelevant(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MostRelevant>().ToTable("most_relevant");
            modelBuilder.Entity<MostRelevant>().HasKey(x => new { x.PersonId, x.TitleId });
            
            //columns
            modelBuilder.Entity<MostRelevant>().Property(x => x.PersonId).HasColumnName("person_id");
            modelBuilder.Entity<MostRelevant>().Property(x => x.TitleId).HasColumnName("title_id");

        }


        private static void MapGenre(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().ToTable("genre_list");
            modelBuilder.Entity<Genre>().HasKey(x => new { x.Id });

            //columns
            modelBuilder.Entity<Genre>().Property(x => x.Id).HasColumnName("genre_id");
            modelBuilder.Entity<Genre>().Property(x => x.Name).HasColumnName("genre");
        }

        private static void MapLocalizedTitle(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocalizedTitle>().ToTable("localized_title");
            modelBuilder.Entity<LocalizedTitle>().HasKey(x => new { x.Id, x.TitleId });
            modelBuilder.Entity<LocalizedTitle>().Property(x => x.Id).HasColumnName("localized_id");
            modelBuilder.Entity<LocalizedTitle>().Property(x => x.TitleId).HasColumnName("title_id");

        }

        private static void MapLocalizedDetail(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocalizedDetail>().ToTable("localized_detail");
            modelBuilder.Entity<LocalizedDetail>().HasKey(x => new { x.Id });

            //columns
            modelBuilder.Entity<LocalizedDetail>().Property(x => x.Id).HasColumnName("localized_id");
            modelBuilder.Entity<LocalizedDetail>().Property(x => x.LocTitle).HasColumnName("localized_title");
            modelBuilder.Entity<LocalizedDetail>().Property(x => x.Language).HasColumnName("language");
            modelBuilder.Entity<LocalizedDetail>().Property(x => x.Region).HasColumnName("region");
            modelBuilder.Entity<LocalizedDetail>().Property(x => x.Type).HasColumnName("type");
            modelBuilder.Entity<LocalizedDetail>().Property(x => x.Attribute).HasColumnName("attribute");





        }
        private static void MapDirector(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Director>().ToTable("director");
            modelBuilder.Entity<Director>().HasKey(x => new { x.TitleId, x.PersonId });

            //columns
            modelBuilder.Entity<Director>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Director>().Property(x => x.PersonId).HasColumnName("person_id");
        }
        private static void MapEpisodeFromSeries(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EpisodeFromSeries>().ToTable("episode_from_series");
            modelBuilder.Entity<EpisodeFromSeries>().HasKey(x => new { x.TitleId, x.SeriesTitleId });

            //columns
            modelBuilder.Entity<EpisodeFromSeries>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<EpisodeFromSeries>().Property(x => x.SeriesTitleId).HasColumnName("series_title_id");
            modelBuilder.Entity<EpisodeFromSeries>().Property(x => x.SeasonNumber).HasColumnName("season_num");
            modelBuilder.Entity<EpisodeFromSeries>().Property(x => x.EpisodeNumber).HasColumnName("episode_num");

        }

        private static void MapPlot(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plot>().ToTable("plot");
            modelBuilder.Entity<Plot>().HasKey(x => new { x.TitleId });
            modelBuilder.Entity<Plot>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Plot>().Property(x => x.PlotOfTitle).HasColumnName("plot");

        }
        private static void MapPoster(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Poster>().ToTable("poster");
            modelBuilder.Entity<Poster>().HasKey(x => new { x.TitleId });

            //columns
            modelBuilder.Entity<Poster>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Poster>().Property(x => x.PosterUrl).HasColumnName("poster");
           
        }
        private static void MapRating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>().ToTable("rating");
            modelBuilder.Entity<Rating>().HasKey(x => new { x.TitleId });

            //columns
            modelBuilder.Entity<Rating>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Rating>().Property(x => x.AverageRating).HasColumnName("average_rating");
            modelBuilder.Entity<Rating>().Property(x => x.VoteCount).HasColumnName("vote_count");

        }

        private static void MapWriter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Writer>().ToTable("writer");
            modelBuilder.Entity<Writer>().HasKey(x => new { x.PersonId, x.TitleId });

            //columns
            modelBuilder.Entity<Writer>().Property(x => x.PersonId).HasColumnName("person_id");
            modelBuilder.Entity<Writer>().Property(x => x.TitleId).HasColumnName("title_id");

        }

    }
    } 