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
}
