using MovieDataLayer.Interfaces;
using MovieDataLayer.DataService;
using MovieDataLayer;
using Microsoft.EntityFrameworkCore;
using MovieDataLayer.DataService.IMDB_Repository;
using MovieDataLayer.DataService.UserFrameworkRepository;
using MovieDataLayer.Models.IMDB_Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<IMDBContext>();

//builder.Services.AddScoped<IMovieDataRepository<Person, string>, MovieDataRepository<Person, string>>(); //Using AddScoped to ensure new instance of DataService is created for each HTTP request
//builder.Services.AddScoped<IMovieDataRepository<Title, string>, MovieDataRepository<Title, string>>(); //Using AddScoped to ensure new instance of DataService is created for each HTTP request

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); //Currently not needed, maybe should be removed? //Dependency Injection for Repository base class.
builder.Services.AddScoped<TitleRepository>(); //Dependency Injection for TitleRepository class, concrete.
builder.Services.AddScoped<UserRepository>(); //Dependency Injection for UserRepository class, concrete.
builder.Services.AddScoped<PersonRepository>();
//builder.Services.AddScoped<MovieContext>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
