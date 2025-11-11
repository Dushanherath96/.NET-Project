using GameStore.Api.Dtos;
namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    //why static? Because we don't need to create an instance of this class to access its members
    //why readonly? Because we don't want to reassign the list reference, 
    //but we can still modify its contents
    private static readonly List<GameDto> games = new()
{
    new (1,
    "The Witcher 3",
     "RPG",
     39.99m,
     new DateOnly(2015, 5, 19)),

    new(
        2,
        "Cyberpunk 2077",
        "RPG",
        59.99m,
        new DateOnly(2020, 12, 10)),
    new(
        3,
        "Minecraft",
        "Sandbox",
        26.95m,
        new DateOnly(2011, 11, 18))
        };

    //RouteGroupeBuilder used to group related endpoints
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        //WithParameterValidation enables automatic validation of parameters based on data annotations
        var group = app.MapGroup("games").WithParameterValidation();

        //GET /games
        group.MapGet("/", () => games);

        //GET /games/{id}
        group.MapGet("/{id}", (int id) =>
        {
            //use GameDto? to allow null values (if game is not found)
            GameDto? game = games.Find(game => game.Id == id);

            //if we search an game that does not exist, we return a 404 Not Found response
            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

        //POST /games
        group.MapPost("/", (CreateGameDto NewGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                NewGame.Name,
                NewGame.Genre,
                NewGame.Price,
                NewGame.ReleaseDate);

            games.Add(game);
            //return 201 Created response with the location of the newly created game
            //use CreatedAtRoute to point to the GetGame endpoint
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        //PUT /games/{id}
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);
            //if game not found, return 404 Not Found
            //-1 means not found
            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
                );
            return Results.NoContent();
        });

        // DELETE /games/{id}
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });
        return group;
    }
}
