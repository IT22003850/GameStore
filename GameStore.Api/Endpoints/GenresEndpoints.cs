using System;
using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;
using GameStore.Api.Mapping; // ← important!


namespace GameStore.Api.Endpoints;

public static class GenresEndpoints
{
    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("genres");

group.MapGet("/", async (GameStoreContext dbContext) =>
    await dbContext.Genres
                   .Select(genre => genre.ToDto())
                   .AsNoTracking()
                   .ToListAsync());

        return group;
    }
}
