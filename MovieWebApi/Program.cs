using MovieDataLayer.Interfaces;
using MovieDataLayer;
using MovieDataLayer.DataService.IMDB_Repository;
using MovieDataLayer.Data_Service.User_Framework_Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MovieWebApi.Extensions;
using MovieDataLayer.Data_Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    //options.AddPolicy(name: MyAllowSpecificOrigins,
    //                  policy =>
    //                  {
    //                      policy.WithOrigins("localhost:3000", "http://localhost:3000/",
    //                                          "https://localhost:3000/", "https://localhost:7154").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    //                  });
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});


builder.Services.AddControllers();

builder.Services.AddDbContext<IMDBContext>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); //Dependency Injection for Repository base class.
builder.Services.AddScoped<TitleRepository>(); //Dependency InjectionS for related class, concrete.
builder.Services.AddScoped<PersonRepository>();

builder.Services.AddScoped<UserRepository>(); 
builder.Services.AddScoped<UserRatingRepository>();
builder.Services.AddScoped<UserTitleBookmarkRepository>();
builder.Services.AddScoped<UserPersonBookmarkRepository>();
builder.Services.AddScoped<UserSearchHistoryRepository>();
builder.Services.AddSingleton<AuthenticatorExtension>();

//Adds JWT Authentication configuration:
builder.Services.AddAuthentication(cfg =>
{
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true; //This should be true in production
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8
            .GetBytes(builder.Configuration["ApplicationSettings:JWT_Secret"]!)
        ),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
