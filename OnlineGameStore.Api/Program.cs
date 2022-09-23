using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OnlineGameStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Infrastructure.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GamesContext>((provider, options) =>
{

    var connectionString = builder.Configuration.GetConnectionString("DbConnection");
    options.UseSqlServer(connectionString);
    

}).AddScoped<GamesContext>();

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
