using Microsoft.EntityFrameworkCore;
using MovieDataLayer.Models.IMDB_Models;
using MovieDataLayer.Models.IMDB_Models.IMDB_DTO;

namespace MovieDataLayer
{
    public class IMDBContext : DbContext
    {
        //Nice to have, configure context in Program.cs instead: https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/

        //IMDB tables:
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
        public DbSet<TitleGenre> TitleGenres { get; set; }
        public DbSet<Poster> Posters { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<TitleSearchResultModel> TitleSearchResultDTO { get; set; }
        public DbSet<SimilarTitleSearchModel> SimilarTitleSearchDTO { get; set; }

        //UserFramework tables:
        public DbSet<User> Users { get; set; }
        public DbSet<UserPersonBookmark> UserPersonBookmarks { get; set; }
        public DbSet<UserTitleBookmark> UserTitleBookmarks { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
        public DbSet<UserSearchHistory> UserSearchHistory { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);

            optionsBuilder.UseNpgsql(SecretData.DB_Connection.ConnectionString); // local
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the IMDB model
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
            MapTitleGenre(modelBuilder);

            // Speciel mapping for the functions in DB
            MapUserTitleSearch(modelBuilder);
            MapTitleSearch(modelBuilder);

            //Configure the UserFramework model
            MapUser(modelBuilder);
            MapUserTitleBookmark(modelBuilder);
            MapUserPersonBookmark(modelBuilder);
            MapUserRating(modelBuilder);
            MapUserSearchHistory(modelBuilder);
        }

        private void MapUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("customer");
            modelBuilder.Entity<User>().HasKey(p => p.Id);

            modelBuilder.Entity<User>().Property(p => p.Id).HasColumnName("customer_id");
            modelBuilder.Entity<User>().Property(p => p.Email).HasColumnName("email");
            modelBuilder.Entity<User>().Property(p => p.FirstName).HasColumnName("firstname");
            modelBuilder.Entity<User>().Property(p => p.Password).HasColumnName("password");

            //modelBuilder.Entity<>().Property(p => p.Id).HasColumnName("");
        }

        private void MapUserTitleBookmark(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTitleBookmark>().ToTable("customer_title_bookmark");
            modelBuilder.Entity<UserTitleBookmark>().HasKey(p => new { p.UserId, p.TitleId });

            //columns
            modelBuilder.Entity<UserTitleBookmark>().Property(p => p.UserId).HasColumnName("customer_id");
            modelBuilder.Entity<UserTitleBookmark>().Property(p => p.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<UserTitleBookmark>().Property(p => p.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp without time zone");
            modelBuilder.Entity<UserTitleBookmark>().Property(p => p.Annotation).HasColumnName("annotation");

        }
        private void MapUserPersonBookmark(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPersonBookmark>().ToTable("customer_person_bookmark");
            modelBuilder.Entity<UserPersonBookmark>().HasKey(p => new { p.UserId, p.PersonId });

            //columns
            modelBuilder.Entity<UserPersonBookmark>().Property(p => p.UserId).HasColumnName("customer_id");
            modelBuilder.Entity<UserPersonBookmark>().Property(p => p.PersonId).HasColumnName("person_id");
            modelBuilder.Entity<UserPersonBookmark>().Property(p => p.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp without time zone");
            modelBuilder.Entity<UserPersonBookmark>().Property(p => p.Annotation).HasColumnName("annotation");

        }

        private void MapUserRating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRating>().ToTable("customer_rating");
            modelBuilder.Entity<UserRating>().HasKey(p => new { p.UserId, p.TitleId });

            //columns
            modelBuilder.Entity<UserRating>().Property(p => p.UserId).HasColumnName("customer_id");
            modelBuilder.Entity<UserRating>().Property(p => p.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<UserRating>().Property(p => p.Rating).HasColumnName("rating");
            modelBuilder.Entity<UserRating>().Property(p => p.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp without time zone");
            modelBuilder.Entity<UserRating>().Property(p => p.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp without time zone");

        }

        private void MapUserSearchHistory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSearchHistory>().ToTable("customer_search_history");
            modelBuilder.Entity<UserSearchHistory>().HasKey(p => new { p.UserId, p.CreatedAt });

            //columns
            modelBuilder.Entity<UserSearchHistory>().Property(p => p.UserId).HasColumnName("customer_id");
            modelBuilder.Entity<UserSearchHistory>().Property(p => p.SearchTerms).HasColumnName("search_terms");
            modelBuilder.Entity<UserSearchHistory>().Property(p => p.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp without time zone");

        }
        private void MapUserTitleSearch(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TitleSearchResultModel>().HasNoKey();
            //columns
            modelBuilder.Entity<TitleSearchResultModel>().Property(p => p.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<TitleSearchResultModel>().Property(p => p.PrimaryTitle).HasColumnName("primary_title");
        }
        private void MapTitleSearch(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SimilarTitleSearchModel>().HasNoKey();
            //columns
            modelBuilder.Entity<SimilarTitleSearchModel>().Property(p => p.SimilarTitleId).HasColumnName("similar_title_id");
            modelBuilder.Entity<SimilarTitleSearchModel>().Property(p => p.PrimaryTitle).HasColumnName("primary_title");
            modelBuilder.Entity<SimilarTitleSearchModel>().Property(p => p.Genres).HasColumnName("genres");
            modelBuilder.Entity<SimilarTitleSearchModel>().Property(p => p.IsAdult).HasColumnName("isadult");
            modelBuilder.Entity<SimilarTitleSearchModel>().Property(p => p.TitleType).HasColumnName("title_type");
        }

        private void MapPerson(ModelBuilder modelBuilder)
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

        private void MapTitle(ModelBuilder modelBuilder)
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

        private void MapProfession(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profession>().ToTable("profession");
            modelBuilder.Entity<Profession>().HasKey(x => new { x.Id });

            //columns
            modelBuilder.Entity<Profession>().Property(x => x.Id).HasColumnName("profession_id");
            modelBuilder.Entity<Profession>().Property(x => x.Name).HasColumnName("profession_name");

        }

        private void MapPrimaryProfession(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrimaryProfession>().ToTable("primary_profession");
            modelBuilder.Entity<PrimaryProfession>().HasKey(x => new { x.ProfessionId, x.PersonId });

            //columns
            modelBuilder.Entity<PrimaryProfession>().Property(x => x.ProfessionId).HasColumnName("profession_id");
            modelBuilder.Entity<PrimaryProfession>().Property(x => x.PersonId).HasColumnName("person_id");

            //Relations
            //modelBuilder.Entity<Person>().HasMany(x => x.PrimaryProfessions).WithOne(c => c.Person);

            modelBuilder.Entity<PrimaryProfession>()
                .HasOne(pp => pp.Person)
                .WithMany(p => p.PrimaryProfessions)
                .HasForeignKey(pp => pp.PersonId);


            modelBuilder.Entity<PrimaryProfession>()
                .HasOne(pp => pp.Profession)
                .WithMany(prof => prof.PrimaryProfession)
                .HasForeignKey(pp => pp.ProfessionId);
        }
        private void MapPrincipalCast(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrincipalCast>().ToTable("principal_cast");
            modelBuilder.Entity<PrincipalCast>().HasKey(x => new { x.PersonId, x.TitleId, x.Ordering });

            //columns
            modelBuilder.Entity<PrincipalCast>().Property(x => x.PersonId).HasColumnName("person_id");
            modelBuilder.Entity<PrincipalCast>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<PrincipalCast>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<PrincipalCast>().Property(x => x.CharacterName).HasColumnName("character_name");
            modelBuilder.Entity<PrincipalCast>().Property(x => x.Category).HasColumnName("category");
            modelBuilder.Entity<PrincipalCast>().Property(x => x.Job).HasColumnName("job");
        }

        private void MapMostRelevant(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MostRelevant>().ToTable("most_relevant");
            modelBuilder.Entity<MostRelevant>().HasKey(x => new { x.PersonId, x.TitleId });

            //columns
            modelBuilder.Entity<MostRelevant>().Property(x => x.PersonId).HasColumnName("person_id");
            modelBuilder.Entity<MostRelevant>().Property(x => x.TitleId).HasColumnName("title_id");

        }
        private void MapGenre(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().ToTable("genre_list");
            modelBuilder.Entity<Genre>().HasKey(x => x.Id);

            //columns
            modelBuilder.Entity<Genre>().Property(x => x.Id).HasColumnName("genre_id");
            modelBuilder.Entity<Genre>().Property(x => x.Name).HasColumnName("genre");
        }

        private void MapLocalizedTitle(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocalizedTitle>().ToTable("localized_title");
            modelBuilder.Entity<LocalizedTitle>().HasKey(x => new { x.Id, x.TitleId });
            modelBuilder.Entity<LocalizedTitle>().Property(x => x.Id).HasColumnName("localized_id");
            modelBuilder.Entity<LocalizedTitle>().Property(x => x.TitleId).HasColumnName("title_id");

        }

        private void MapLocalizedDetail(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocalizedDetail>().ToTable("localized_detail");
            modelBuilder.Entity<LocalizedDetail>().HasKey(x => x.Id);

            //columns
            modelBuilder.Entity<LocalizedDetail>().Property(x => x.Id).HasColumnName("localized_id");
            modelBuilder.Entity<LocalizedDetail>().Property(x => x.LocTitle).HasColumnName("localized_title");
            modelBuilder.Entity<LocalizedDetail>().Property(x => x.Language).HasColumnName("language");
            modelBuilder.Entity<LocalizedDetail>().Property(x => x.Region).HasColumnName("region");
            modelBuilder.Entity<LocalizedDetail>().Property(x => x.Type).HasColumnName("type");
            modelBuilder.Entity<LocalizedDetail>().Property(x => x.Attribute).HasColumnName("attribute");

            modelBuilder.Entity<LocalizedDetail>()
        .HasOne(ld => ld.LocalizedTitle)
        .WithOne(lt => lt.LocalizedDetail)
        .HasForeignKey<LocalizedDetail>(ld => new { ld.Id, ld.TitleId });//specify FK

        }
        private void MapDirector(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Director>().ToTable("director");
            modelBuilder.Entity<Director>().HasKey(x => new { x.TitleId, x.PersonId });

            //columns
            modelBuilder.Entity<Director>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Director>().Property(x => x.PersonId).HasColumnName("person_id");
        }
        private void MapEpisodeFromSeries(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EpisodeFromSeries>().ToTable("episode_from_series");
            modelBuilder.Entity<EpisodeFromSeries>().HasKey(x => new { x.TitleId, x.SeriesTitleId });

            //columns
            modelBuilder.Entity<EpisodeFromSeries>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<EpisodeFromSeries>().Property(x => x.SeriesTitleId).HasColumnName("series_title_id");
            modelBuilder.Entity<EpisodeFromSeries>().Property(x => x.SeasonNumber).HasColumnName("season_num");
            modelBuilder.Entity<EpisodeFromSeries>().Property(x => x.EpisodeNumber).HasColumnName("episode_num");

        }

        private void MapPlot(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plot>().ToTable("plot");
            modelBuilder.Entity<Plot>().HasKey(x => x.TitleId);
            modelBuilder.Entity<Plot>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Plot>().Property(x => x.PlotOfTitle).HasColumnName("plot");

        }
        private void MapPoster(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Poster>().ToTable("poster");
            modelBuilder.Entity<Poster>().HasKey(x => x.TitleId);

            //columns
            modelBuilder.Entity<Poster>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Poster>().Property(x => x.PosterUrl).HasColumnName("poster");

        }
        private void MapRating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>().ToTable("rating");
            modelBuilder.Entity<Rating>().HasKey(x => x.TitleId);

            //columns
            modelBuilder.Entity<Rating>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Rating>().Property(x => x.AverageRating).HasColumnName("average_rating");
            modelBuilder.Entity<Rating>().Property(x => x.VoteCount).HasColumnName("vote_count");

        }

        private void MapWriter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Writer>().ToTable("writer");
            modelBuilder.Entity<Writer>().HasKey(x => new { x.PersonId, x.TitleId });

            //columns
            modelBuilder.Entity<Writer>().Property(x => x.PersonId).HasColumnName("person_id");
            modelBuilder.Entity<Writer>().Property(x => x.TitleId).HasColumnName("title_id");

        }

        private void MapTitleGenre(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TitleGenre>().ToTable("title_genre");
            modelBuilder.Entity<TitleGenre>().HasKey(t => new { t.GenreId, t.TitleId });

            //columns
            modelBuilder.Entity<TitleGenre>().Property(t => t.GenreId).HasColumnName("genre_id");
            modelBuilder.Entity<TitleGenre>().Property(t => t.TitleId).HasColumnName("title_id");

        }
    }
}