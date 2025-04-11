using System;
using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    private static readonly List<GameDto> games = [
    new (
        1,
        "Street fighter I",
        "Fighting",
        19.99M,
        new DateOnly(1992,2,2)
    ),
    new (
        2,
        "Street fighter II",
        "Fighting",
        20.99M,
        new DateOnly(1993,2,2)
    ),
    new (
        3,
        "Street fighter III",
        "Fighting",
        21.99M,
        new DateOnly(1994,2,2)
    )];


    public static WebApplication MapGamesEndpoints(this WebApplication app)
    {
        //Get / games
        app.MapGet("games", () => games)
        .WithParameterValidation();

        //Get / games / 1
        app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id))
        .WithParameterValidation();

        //POST / games
        app.MapPost("games", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return Results.Created($"/games/{game.Id}", game);

        })
        .WithParameterValidation();

        // PUT / games / {id}
        app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);
            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        })
        .WithParameterValidation();

        // DELETE / games / {id}
        app.MapDelete("games/{id}", (int id) =>
        {

            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        })
        .WithParameterValidation();

        return app;
    }

}
