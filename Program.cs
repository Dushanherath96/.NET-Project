
using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Configure SQLite database connection
//connection string
var connString = builder.Configuration.GetConnectionString("GameStor");
//register DbContext with DI container
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();


app.MapGamesEndpoints();

app.MigrateDb();

app.Run();
