using OnlineGameStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;
using OnlineGameStore.Infrastructure.Repositories.Implementations;
using OnlineGameStore.Application.Services.Interfaces;
using OnlineGameStore.Application.Services.Implementation;
using MapsterMapper;
using OnlineGameStore.Application.Mapster;
using Microsoft.OpenApi.Models;
using OnlineGameStore.Application;
using OnlineGameStore.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corsapp", policyBuilder =>
{
    policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();