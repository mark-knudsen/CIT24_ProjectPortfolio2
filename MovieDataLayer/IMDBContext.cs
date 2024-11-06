using Microsoft.EntityFrameworkCore;
using MovieDataLayer.Models.IMDB_Models;

namespace MovieDataLayer
{
    public class IMDBContext : DbContext
    {
        //Nice to have, configure context in Program.cs instead: https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/

        //IMDB tables:
        public DbSet<PersonModel> Persons { get; set; }
        public DbSet<TitleModel> Titles { get; set; }
        public DbSet<MostRelevantModel> MostRelevants { get; set; }
        public DbSet<ProfessionModel> Professions { get; set; }
        public DbSet<LocalizedTitleModel> LocalizedTitles { get; set; }
        public DbSet<PrincipalCastModel> PrincipalCasts { get; set; }
        public DbSet<GenreModel> Genres { get; set; }
        public DbSet<LocalizedDetailModel> LocalizedDetails { get; set; }
        public DbSet<PrimaryProfessionModel> PrimaryProfessions { get; set; }
        public DbSet<DirectorModel> Directors { get; set; }
        public DbSet<EpisodeFromSeriesModel> EpisodeFromSeries { get; set; }
        public DbSet<PlotModel> Plots { get; set; }
        public DbSet<TitleGenreModel> TitleGenres { get; set; }
        public DbSet<PosterModel> Posters { get; set; }
        public DbSet<RatingModel> Ratings { get; set; }
        public DbSet<WriterModel> Writers { get; set; }
        public DbSet<TitleSearchResultModel> TitleSearchResultDTO { get; set; }
        public DbSet<SimilarTitleSearchModel> SimilarTitleSearchDTO { get; set; }

        //UserFramework tables:
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserPersonBookmarkModel> UserPersonBookmarks { get; set; }
        public DbSet<UserTitleBookmarkModel> UserTitleBookmarks { get; set; }
        public DbSet<UserRatingModel> UserRatings { get; set; }
        public DbSet<UserSearchHistoryModel> UserSearchHistory { get; set; }


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
            modelBuilder.Entity<UserModel>().ToTable("customer");
            modelBuilder.Entity<UserModel>().HasKey(p => p.Id);

            modelBuilder.Entity<UserModel>().Property(p => p.Id).HasColumnName("customer_id");
            modelBuilder.Entity<UserModel>().Property(p => p.Email).HasColumnName("email");
            modelBuilder.Entity<UserModel>().Property(p => p.FirstName).HasColumnName("firstname");
            modelBuilder.Entity<UserModel>().Property(p => p.Password).HasColumnName("password");

            //modelBuilder.Entity<>().Property(p => p.Id).HasColumnName("");
        }

        private void MapUserTitleBookmark(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTitleBookmarkModel>().ToTable("customer_title_bookmark");
            modelBuilder.Entity<UserTitleBookmarkModel>().HasKey(p => new { p.UserId, p.TitleId });

            //columns
            modelBuilder.Entity<UserTitleBookmarkModel>().Property(p => p.UserId).HasColumnName("customer_id");
            modelBuilder.Entity<UserTitleBookmarkModel>().Property(p => p.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<UserTitleBookmarkModel>().Property(p => p.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp without time zone");
            modelBuilder.Entity<UserTitleBookmarkModel>().Property(p => p.Annotation).HasColumnName("annotation");

        }
        private void MapUserPersonBookmark(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPersonBookmarkModel>().ToTable("customer_person_bookmark");
            modelBuilder.Entity<UserPersonBookmarkModel>().HasKey(p => new { p.UserId, p.PersonId });

            //columns
            modelBuilder.Entity<UserPersonBookmarkModel>().Property(p => p.UserId).HasColumnName("customer_id");
            modelBuilder.Entity<UserPersonBookmarkModel>().Property(p => p.PersonId).HasColumnName("person_id");
            modelBuilder.Entity<UserPersonBookmarkModel>()
                .Property(p => p.CreatedAt)
                //.HasDefaultValueSql("CURRENT_TIMESTAMP") // Sets default to current timestamp in PostgreSQL
                //.ValueGeneratedOnAdd() // Specifies that the value is generated when the entity is added
                .HasColumnName("created_at")
                .HasColumnType("timestamp without time zone");
            modelBuilder.Entity<UserPersonBookmarkModel>().Property(p => p.Annotation).HasColumnName("annotation");

        }

        private void MapUserRating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRatingModel>().ToTable("customer_rating");
            modelBuilder.Entity<UserRatingModel>().HasKey(p => new { p.UserId, p.TitleId });

            //columns
            modelBuilder.Entity<UserRatingModel>().Property(p => p.UserId).HasColumnName("customer_id");
            modelBuilder.Entity<UserRatingModel>().Property(p => p.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<UserRatingModel>().Property(p => p.Rating).HasColumnName("rating");
            modelBuilder.Entity<UserRatingModel>().Property(p => p.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp without time zone");
            modelBuilder.Entity<UserRatingModel>().Property(p => p.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp without time zone");

        }

        private void MapUserSearchHistory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSearchHistoryModel>().ToTable("customer_search_history");
            modelBuilder.Entity<UserSearchHistoryModel>().HasKey(p => new { p.UserId, p.CreatedAt });

            //columns
            modelBuilder.Entity<UserSearchHistoryModel>().Property(p => p.UserId).HasColumnName("customer_id");
            modelBuilder.Entity<UserSearchHistoryModel>().Property(p => p.SearchTerms).HasColumnName("search_terms");
            modelBuilder.Entity<UserSearchHistoryModel>().Property(p => p.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp without time zone");

        }
        private void MapUserTitleSearch(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TitleSearchResultModel>().HasNoKey();
            //columns
            modelBuilder.Entity<TitleSearchResultModel>().Property(p => p.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<TitleSearchResultModel>().Property(p => p.PrimaryTitle).HasColumnName("primary_title");

            modelBuilder.Entity<TitleSearchResultModel>()
          .Ignore(t => t.Id);

            modelBuilder.Entity<TitleSearchResultModel>()
            .Ignore(t => t.Url);

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

            modelBuilder.Entity<PersonModel>().ToTable("person");
            modelBuilder.Entity<PersonModel>().HasKey(p => p.Id);

            //columns
            modelBuilder.Entity<PersonModel>().Property(p => p.Id).HasColumnName("person_id");
            modelBuilder.Entity<PersonModel>().Property(p => p.Name).HasColumnName("primary_name");
            modelBuilder.Entity<PersonModel>().Property(p => p.BirthYear).HasColumnName("birth_year");
            modelBuilder.Entity<PersonModel>().Property(p => p.DeathYear).HasColumnName("death_year");
            //modelBuilder.Entity<Person>().Navigation(p => p.MostRelevantTitles).AutoInclude();
            ////modelBuilder.Entity<Person>().HasMany(t => t.Titles).WithMany(c => c.Persons);

        }

        private void MapTitle(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TitleModel>().ToTable("title");
            modelBuilder.Entity<TitleModel>().HasKey(t => t.Id);

            //columns
            modelBuilder.Entity<TitleModel>().Property(t => t.Id).HasColumnName("title_id");
            modelBuilder.Entity<TitleModel>().Property(t => t.TitleType).HasColumnName("title_type");
            modelBuilder.Entity<TitleModel>().Property(t => t.PrimaryTitle).HasColumnName("primary_title");
            modelBuilder.Entity<TitleModel>().Property(t => t.OriginalTitle).HasColumnName("original_title");
            modelBuilder.Entity<TitleModel>().Property(t => t.StartYear).HasColumnName("start_year");
            modelBuilder.Entity<TitleModel>().Property(t => t.EndYear).HasColumnName("end_year");
            modelBuilder.Entity<TitleModel>().Property(t => t.Runtime).HasColumnName("runtime");
            modelBuilder.Entity<TitleModel>().Property(t => t.IsAdult).HasColumnName("isadult");


            // Configure Many-to-Many relationship

            //modelBuilder.Entity<Title>()
            //    .HasMany(t => t.Persons)
            //    .WithMany(c => c.Titles);
            // If not working add Foreign Keys
        }

        private void MapProfession(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfessionModel>().ToTable("profession");
            modelBuilder.Entity<ProfessionModel>().HasKey(x => new { x.Id });

            //columns
            modelBuilder.Entity<ProfessionModel>().Property(x => x.Id).HasColumnName("profession_id");
            modelBuilder.Entity<ProfessionModel>().Property(x => x.Name).HasColumnName("profession_name");

        }

        private void MapPrimaryProfession(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrimaryProfessionModel>().ToTable("primary_profession");
            modelBuilder.Entity<PrimaryProfessionModel>().HasKey(x => new { x.ProfessionId, x.PersonId });

            //columns
            modelBuilder.Entity<PrimaryProfessionModel>().Property(x => x.ProfessionId).HasColumnName("profession_id");
            modelBuilder.Entity<PrimaryProfessionModel>().Property(x => x.PersonId).HasColumnName("person_id");

            //Relations
            //modelBuilder.Entity<Person>().HasMany(x => x.PrimaryProfessions).WithOne(c => c.Person);

            modelBuilder.Entity<PrimaryProfessionModel>()
                .HasOne(pp => pp.Person)
                .WithMany(p => p.PrimaryProfessions)
                .HasForeignKey(pp => pp.PersonId);


            modelBuilder.Entity<PrimaryProfessionModel>()
                .HasOne(pp => pp.Profession)
                .WithMany(prof => prof.PrimaryProfession)
                .HasForeignKey(pp => pp.ProfessionId);
        }
        private void MapPrincipalCast(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrincipalCastModel>().ToTable("principal_cast");
            modelBuilder.Entity<PrincipalCastModel>().HasKey(x => new { x.PersonId, x.TitleId, x.Ordering });

            //columns
            modelBuilder.Entity<PrincipalCastModel>().Property(x => x.PersonId).HasColumnName("person_id");
            modelBuilder.Entity<PrincipalCastModel>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<PrincipalCastModel>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<PrincipalCastModel>().Property(x => x.CharacterName).HasColumnName("character_name");
            modelBuilder.Entity<PrincipalCastModel>().Property(x => x.Category).HasColumnName("category");
            modelBuilder.Entity<PrincipalCastModel>().Property(x => x.Job).HasColumnName("job");
        }

        private void MapMostRelevant(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MostRelevantModel>().ToTable("most_relevant");
            modelBuilder.Entity<MostRelevantModel>().HasKey(x => new { x.PersonId, x.TitleId });

            //columns
            modelBuilder.Entity<MostRelevantModel>().Property(x => x.PersonId).HasColumnName("person_id");
            modelBuilder.Entity<MostRelevantModel>().Property(x => x.TitleId).HasColumnName("title_id");

        }
        private void MapGenre(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GenreModel>().ToTable("genre_list");
            modelBuilder.Entity<GenreModel>().HasKey(x => x.Id);

            //columns
            modelBuilder.Entity<GenreModel>().Property(x => x.Id).HasColumnName("genre_id");
            modelBuilder.Entity<GenreModel>().Property(x => x.Name).HasColumnName("genre");
        }

        private void MapLocalizedTitle(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocalizedTitleModel>().ToTable("localized_title");
            modelBuilder.Entity<LocalizedTitleModel>().HasKey(x => new { x.Id, x.TitleId });
            modelBuilder.Entity<LocalizedTitleModel>().Property(x => x.Id).HasColumnName("localized_id");
            modelBuilder.Entity<LocalizedTitleModel>().Property(x => x.TitleId).HasColumnName("title_id");

        }

        private void MapLocalizedDetail(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocalizedDetailModel>().ToTable("localized_detail");
            modelBuilder.Entity<LocalizedDetailModel>().HasKey(x => x.Id);

            //columns
            modelBuilder.Entity<LocalizedDetailModel>().Property(x => x.Id).HasColumnName("localized_id");
            modelBuilder.Entity<LocalizedDetailModel>().Property(x => x.LocTitle).HasColumnName("localized_title");
            modelBuilder.Entity<LocalizedDetailModel>().Property(x => x.Language).HasColumnName("language");
            modelBuilder.Entity<LocalizedDetailModel>().Property(x => x.Region).HasColumnName("region");
            modelBuilder.Entity<LocalizedDetailModel>().Property(x => x.Type).HasColumnName("type");
            modelBuilder.Entity<LocalizedDetailModel>().Property(x => x.Attribute).HasColumnName("attribute");

            modelBuilder.Entity<LocalizedDetailModel>()
        .HasOne(ld => ld.LocalizedTitle)
        .WithOne(lt => lt.LocalizedDetail)
        .HasForeignKey<LocalizedDetailModel>(ld => new { ld.Id, ld.TitleId });//specify FK

        }
        private void MapDirector(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DirectorModel>().ToTable("director");
            modelBuilder.Entity<DirectorModel>().HasKey(x => new { x.TitleId, x.PersonId });

            //columns
            modelBuilder.Entity<DirectorModel>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<DirectorModel>().Property(x => x.PersonId).HasColumnName("person_id");
        }
        private void MapEpisodeFromSeries(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EpisodeFromSeriesModel>().ToTable("episode_from_series");
            modelBuilder.Entity<EpisodeFromSeriesModel>().HasKey(x => new { x.TitleId, x.SeriesTitleId });

            //columns
            modelBuilder.Entity<EpisodeFromSeriesModel>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<EpisodeFromSeriesModel>().Property(x => x.SeriesTitleId).HasColumnName("series_title_id");
            modelBuilder.Entity<EpisodeFromSeriesModel>().Property(x => x.SeasonNumber).HasColumnName("season_num");
            modelBuilder.Entity<EpisodeFromSeriesModel>().Property(x => x.EpisodeNumber).HasColumnName("episode_num");

        }

        private void MapPlot(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlotModel>().ToTable("plot");
            modelBuilder.Entity<PlotModel>().HasKey(x => x.TitleId);
            modelBuilder.Entity<PlotModel>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<PlotModel>().Property(x => x.PlotOfTitle).HasColumnName("plot");

        }
        private void MapPoster(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PosterModel>().ToTable("poster");
            modelBuilder.Entity<PosterModel>().HasKey(x => x.TitleId);

            //columns
            modelBuilder.Entity<PosterModel>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<PosterModel>().Property(x => x.PosterUrl).HasColumnName("poster");

        }
        private void MapRating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RatingModel>().ToTable("rating");
            modelBuilder.Entity<RatingModel>().HasKey(x => x.TitleId);

            //columns
            modelBuilder.Entity<RatingModel>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<RatingModel>().Property(x => x.AverageRating).HasColumnName("average_rating");
            modelBuilder.Entity<RatingModel>().Property(x => x.VoteCount).HasColumnName("vote_count");

        }

        private void MapWriter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WriterModel>().ToTable("writer");
            modelBuilder.Entity<WriterModel>().HasKey(x => new { x.PersonId, x.TitleId });

            //columns
            modelBuilder.Entity<WriterModel>().Property(x => x.PersonId).HasColumnName("person_id");
            modelBuilder.Entity<WriterModel>().Property(x => x.TitleId).HasColumnName("title_id");

        }

        private void MapTitleGenre(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TitleGenreModel>().ToTable("title_genre");
            modelBuilder.Entity<TitleGenreModel>().HasKey(t => new { t.GenreId, t.TitleId });

            //columns
            modelBuilder.Entity<TitleGenreModel>().Property(t => t.GenreId).HasColumnName("genre_id");
            modelBuilder.Entity<TitleGenreModel>().Property(t => t.TitleId).HasColumnName("title_id");

        }
    }
}