using System;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;
namespace GameStore.Api.Data;

//why (DbContextOptions<GameStoreContext> options) used here?
//It is used to pass configuration options to the DbContext,such as the database provider, connection string, and other settings.
//This allows for greater flexibility and decoupling of the DbContext from specific configurations,
//making it easier to manage and change database settings without modifying the DbContext class itself.
public class GameStoreContext(DbContextOptions<GameStoreContext> options)
: DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();

    //why OnModelCreating used here?
    //OnModelCreating is used to configure the model and its relationships using the ModelBuilder API
    //why it is important?
    //It is important because it allows for customization of the database schema,
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new { Id = 1, Name = "Action" },
            new { Id = 2, Name = "Adventure" },
            new { Id = 3, Name = "RPG" },
            new { Id = 4, Name = "Strategy" },
            new { Id = 5, Name = "Simulation" }
        );
    }
}
