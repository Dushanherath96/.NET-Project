
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Configure SQLite database connection
//connection string
var connString = "Data Source=GameStore.db";
builder.Services.AddSqlite<GameStore.Api.Data.GameStoreContext>(connString);

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
