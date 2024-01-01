using NovusLiberus.Api.Constants;
using NovusLiberus.Api.DTOs.GenreDtos;
using NovusLiberus.Api.EndpointFilters;
using NovusLiberus.Api.EndpointHandlers;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.RouteBuilderExtensions;

public static class GenreEndpointRouteBuilderExtensions
{
    public static void MapGenresEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        //Endpoint Groups//
        var apiVersionOneEndpoints = endpointRouteBuilder.MapGroup(RouteConstants.ApiV1);
        var genresEndpoints = apiVersionOneEndpoints.MapGroup("/genres")
            .WithTags(nameof(Genre) + " routes")
            .WithOpenApi();
        var genreWithIdEndpoints = genresEndpoints.MapGroup("/{genreId:int}");
        // .RequireAuthorization();
        var genresWithIntIdAndLockFilters = genresEndpoints.MapGroup("/{genreId:int}");

        //Endpoints//
        genresEndpoints.MapGet("/", GenresHandlers.GetAllGenresAsync)
            .WithName("GetGenres")
            .WithSummary("Gets all genres");
        genreWithIdEndpoints.MapGet("/", GenresHandlers.GetGenreByIdAsync)
            .WithName("GetGenre")
            .WithSummary("Gets genre by providing ID");
        genresEndpoints.MapGet("/{genreName}", GenresHandlers.GetGenreByNameAsync)
            .AllowAnonymous()
            .WithSummary("Gets genre by providing name");
        genresEndpoints.MapPost("/", GenresHandlers.CreateGenreAsync)
            .AddEndpointFilter<ValidationFilterGeneric<CreateGenreDto>>()
            .ProducesValidationProblem(400)
            .WithSummary("Creates new genre by providing genre DTO");
        genresWithIntIdAndLockFilters.MapPut("/", GenresHandlers.EditGenreAsync)
            .AddEndpointFilter<ValidationFilterGeneric<CreateGenreDto>>()
            .ProducesValidationProblem(400)
            .WithSummary("Edits, updates genre by providing ID");
        genresWithIntIdAndLockFilters.MapDelete("/", GenresHandlers.DeleteGenreAsync)
            .WithSummary("Deletes genre by providing ID");
    }
}