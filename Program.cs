
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Configure SQLite database connection
//connection string
var connString = builder.Configuration.GetConnectionString("GameStor");
//register DbContext with DI container
builder.Services.AddSqlite<GameStore.Api.Data.GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
