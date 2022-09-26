using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OnlineGameStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Infrastructure.Entities;
using Microsoft.OpenApi.Models;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;
using OnlineGameStore.Infrastructure.Repositories.Implementations;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Application.Services.Implementation;
using OnlineGameStore.Application.Services.UnitOfWorkImplementation;
using MapsterMapper;
using OnlineGameStore.Application.Mapster;

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

builder.Services.AddMvc();
builder.Services.AddMapsterConfiguration()
        .AddTransient<IMapper, Mapper>();
builder.Services.AddTransient<IGameRepository, GameRepository>();
builder.Services.AddTransient<IGenreRepository, GenreRepository>();
builder.Services.AddTransient<IPlatformRepository, PlatformRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();

builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IGameService, GameService>();
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<IPlatformService, PlatformService>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();

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
