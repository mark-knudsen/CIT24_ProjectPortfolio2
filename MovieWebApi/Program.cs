using MovieDataLayer.Interfaces;
using MovieDataLayer.DataService;
using MovieDataLayer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<MovieContext>();

builder.Services.AddScoped<IMovieDataService<PersonModel>, MovieDataService<PersonModel>>(); //Using AddScoped to ensure new instance of DataService is created for each HTTP request

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
