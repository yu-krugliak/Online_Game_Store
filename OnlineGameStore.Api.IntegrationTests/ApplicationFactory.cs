using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineGameStore.Infrastructure.Context;
using OnlineGameStore.Infrastructure.Entities;

namespace OnlineGameStore.Api.IntegrationTests;
public class ApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<GamesContext>));

            services.Remove(descriptor!);

            services.AddDbContext<GamesContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var gamesContext = scopedServices.GetRequiredService<GamesContext>();

            gamesContext.Database.EnsureDeleted();
            gamesContext.Database.EnsureCreated();

            SeedDatabaseAsync(gamesContext);
        });
    }

    public void SeedDatabaseAsync(GamesContext context)
    {
        var genres = new List<Genre>()
        {
            new ()
            {
                Id = 1,
                Name = "Test Genre",
                Description = "test genre desc",
            }

        };

        var platformTypes = new List<PlatformType>()
        {
            new ()
            {
                Id = 1,
                Type = "Android"
            }
        };

        var game = new Game()
        {
            Id = 1,
            Name = "Test Game",
            Description = "test game desc",
            Price = 101.00M,
            Key = "game1",
            Genres = new List<Genre>(genres),
            Platforms = new List<PlatformType>(platformTypes)
        };

        var comments = new List<Comment>()
        {
            new ()
            {
                Id = 1,
                GameId = 1,
                Body = "Test Body",
                DatePosted = DateTime.Now
            }
        };

        context.Genres.AddRange(genres);
        context.PlatformTypes.AddRange(platformTypes);
        context.Games.Add(game);
        context.Comments.AddRange(comments);
        
        context.SaveChanges();
    }
}