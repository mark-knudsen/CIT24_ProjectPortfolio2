using MovieDataLayer.Interfaces;
using MovieDataLayer.DataService;
using MovieDataLayer;
using Microsoft.EntityFrameworkCore;
using Mapster;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<IMDBContext>();

//builder.Services.AddScoped<MovieDataRepository>();
builder.Services.AddScoped<DataService<Genre>>();

builder.Services.AddMapster(); //Utilizing Mapster for mapping between models

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

//app.UseAuthorization();

app.MapControllers();

app.Run();