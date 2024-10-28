using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
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
        // public DbSet<PrincipalCast> PrincipalCasts { get; set; }   // it wants a primary key to be defined but it does!??!?!


        public DbSet<User> Users { get; set; }
        // public DbSet<UserRating> CustomerRatings { get; set; }
        //public DbSet<UserBookmark> CustomerBookmarks { get; set; }
        public DbSet<UserSearchHistory> CustomerSearchHistorys { get; set; }

        // sql func results
        public DbSet<EmailSearchResult> EmailSearchResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

            //optionsBuilder.UseNpgsql("host=cit.ruc.dk; db=cit06; uid=cit06; pwd=6fkEI8NdedtI;"); // school
            optionsBuilder.UseNpgsql(SecretData.DB_Connection.ConnectionString); // local
           // optionsBuilder.UseNpgsql("host=localhost;db=test_imdb;uid=postgres;pwd=postgres"); // local
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapPerson(modelBuilder);
            MapTitle(modelBuilder);
            //MapPrincipalCast(modelBuilder);
            MapGenre(modelBuilder);
            MapWriter(modelBuilder);
            //MapTitleGenre(modelBuilder);

            //MapPerson(modelBuilder);
            //MapTitle(modelBuilder);
            //MapPrincipalCast(modelBuilder);
            //MapProfession(modelBuilder);
            //MapMostRelevant(modelBuilder);
            //MapGenre(modelBuilder);
            //MapLocalizedTitle(modelBuilder);
            //MapLocalizedDetail(modelBuilder);
            MapPrimaryProfession(modelBuilder);
            //MapDirector(modelBuilder);
            //MapEpisodeFromSeries(modelBuilder);
            //MapPlot(modelBuilder);
            //MapPoster(modelBuilder);
            //MapRating(modelBuilder);
            //MapWriter(modelBuilder);
            //MapTitleGenre(modelBuilder);


            MapUser(modelBuilder);
            // MapUserRating(modelBuilder);
            MapUserSearchHistory(modelBuilder);
            MapEmailSearchResult(modelBuilder);
        }

        private static void MapPerson(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("person");
            modelBuilder.Entity<Person>().HasKey(p => p.Id);

            modelBuilder.Entity<Person>().Property(p => p.Id).HasColumnName("person_id");
            modelBuilder.Entity<Person>().Property(p => p.Name).HasColumnName("primary_name");
            modelBuilder.Entity<Person>().Property(p => p.BirthYear).HasColumnName("birth_year");
            modelBuilder.Entity<Person>().Property(p => p.DeathYear).HasColumnName("death_year");

            //modelBuilder.Entity<Person>().HasMany(t => t.Titles).WithMany(c => c.Persons);
            //modelBuilder.Entity<Person>().HasMany(t => t.PrimaryProfessions).WithOne(t => t.Person);

            //  modelBuilder.Entity<Person>().Navigation(p => p.PrimaryProfessions).AutoInclude();
            //modelBuilder.Entity<Person>().Navigation(p => p.Titles);
            //modelBuilder.Entity<Person>().Navigation(p => p.Titles).AutoInclude();
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

            //modelBuilder.Entity<Title>().Navigation(t => t.GenresList);

            //modelBuilder.Entity<Title>().Navigation(t => t.Persons);
            //modelBuilder.Entity<Title>().Navigation(t => t.Persons).AutoInclude();

            // Configure Many-to-Many relationship
            //modelBuilder.Entity<Title>().HasMany(t => t.Persons).WithMany(c => c.Titles); // If not working add Foreign Keys
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

        private static void MapWriter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Writer>().ToTable("writer");
            modelBuilder.Entity<Writer>().HasKey(x => new { x.PersonId, x.TitleId });
            //columns
            modelBuilder.Entity<Writer>().Property(x => x.PersonId).HasColumnName("person_id");
            modelBuilder.Entity<Writer>().Property(x => x.TitleId).HasColumnName("title_id");
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

        private static void MapTitleGenre(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<TitleGenre>().ToTable("title_genre");
            //modelBuilder.Entity<TitleGenre>().HasKey(x => new { x.Id, x.Title });

            //modelBuilder.Entity<TitleGenre>().Property(x => x.Id).HasColumnName("title_id");
            //modelBuilder.Entity<TitleGenre>().Property(x => x.Title).HasColumnName("genre_id");
        }

        static void MapUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("customer");
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            //columns
            modelBuilder.Entity<User>().Property(x => x.Id).HasColumnName("customer_id");
            modelBuilder.Entity<User>().Property(x => x.Email).HasColumnName("email");
            modelBuilder.Entity<User>().Property(x => x.FirstName).HasColumnName("firstname");
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnName("password");


            //      modelBuilder.Entity<User>()
            //.HasMany(e => e.CustomerSearchHistory)
            //.WithOne()
            //.IsRequired();

            //     modelBuilder.Entity<User>()
            //.HasMany(e => e.UserRatings)
            //.WithOne(e => e.User)
            //.HasForeignKey(e => e.UserId)
            //.IsRequired();

            //     modelBuilder.Entity<User>()
            //.HasMany(e => e.CustomerSearchHistory)
            //.WithOne(e => e.User)
            //.HasForeignKey(e => e.Id)
            //.IsRequired();

            //      modelBuilder.Entity<User>()
            //.HasMany(e => e.CustomerSearchHistory)
            //.WithOne(e => e.User)
            //.IsRequired();

            //     modelBuilder.Entity<User>()
            //.HasMany(e => e.UserRatings)
            //.WithOne(e => e.User);

            //modelBuilder.Entity<User>().HasMany(t => t.CustomerSearchHistory).WithOne();   // it is looking for the classname + id   and it can't find it, but we are specifying the key for each table, why can't u map it you dumb machine :(
            modelBuilder.Entity<User>().Navigation(x => x.UserSearchHistory).AutoInclude();
        }
        static void MapUserRating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRating>().ToTable("customer_rating");
            modelBuilder.Entity<UserRating>().HasKey(x => x.TitleId);
            //columns
            modelBuilder.Entity<UserRating>().Property(x => x.UserId).HasColumnName("customer_id");
            modelBuilder.Entity<UserRating>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<UserRating>().Property(x => x.Rating).HasColumnName("rating");
            modelBuilder.Entity<UserRating>().Property(x => x.CreatedAt).HasColumnName("created_at");
            modelBuilder.Entity<UserRating>().Property(x => x.UpdatedAt).HasColumnName("updated_at");

            //     modelBuilder.Entity<UserRating>()
            //.HasOne(e => e.User)
            //.WithMany(e => e.UserRatings)
            //.HasForeignKey(e => e.UserId)
            //;

            //        modelBuilder.Entity<UserRating>()
            //.HasOne(e => e.User)
            //.WithMany(e => e.UserRatings)
            //.HasForeignKey(e => e.UserId)
            //.IsRequired();

            //modelBuilder.Entity<UserRating>().HasOne(t => t.User);
            //  modelBuilder.Entity<UserRating>().Navigation(x => x.User);
        }
        static void MapUserSearchHistory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSearchHistory>().ToTable("customer_search_history");
            modelBuilder.Entity<UserSearchHistory>().HasKey(x => new { x.Id, x.CreatedAt }); // remember what is composite keys, super important for it to work properly
            //columns
            modelBuilder.Entity<UserSearchHistory>().Property(x => x.Id).HasColumnName("customer_id");
            modelBuilder.Entity<UserSearchHistory>().Property(x => x.SearchTerms).HasColumnName("search_terms");
            modelBuilder.Entity<UserSearchHistory>().Property(x => x.CreatedAt).HasColumnName("created_at");

            modelBuilder.Entity<UserSearchHistory>().HasOne(u => u.User).WithMany(u => u.UserSearchHistory).HasForeignKey(u => u.Id);
        }
        
        static void MapEmailSearchResult(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailSearchResult>().HasNoKey();
            //columns
            modelBuilder.Entity<EmailSearchResult>().Property(x => x.Firstname).HasColumnName("firstname");
            modelBuilder.Entity<EmailSearchResult>().Property(x => x.Email).HasColumnName("email");
        }
    }
}
