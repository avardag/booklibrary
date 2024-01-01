using NovusLiberus.Api.DTOs;
using NovusLiberus.Api.EndpointFilters;
using NovusLiberus.Api.EndpointHandlers;

namespace NovusLiberus.Api.RouteBuilderExtensions;

public static class EndpointRouteBuilderExtensions
{
    public static void MapAuthorsEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var apiVersionOneEndpoints = endpointRouteBuilder.MapGroup("/api/v1");
        var authorsEndpoints = apiVersionOneEndpoints.MapGroup("/authors");
        var authorWithIdEndpoints = authorsEndpoints.MapGroup("/{authorId:int}");
        // .RequireAuthorization();
        var authorsWithIntIdAndLockFilters = authorsEndpoints.MapGroup("/{authorId:int}")
            .AddEndpointFilter(new AuthorIsLockedFilter(2));

        authorsEndpoints.MapGet("/", AuthorsHandlers.GetAllAuthorsAsync).WithName("GetAuthors");
        authorWithIdEndpoints.MapGet("/", AuthorsHandlers.GetAuthorByIdAsync).WithName("GetAuthor");
        authorsEndpoints.MapGet("/{authorName}", AuthorsHandlers.GetAuthorByNameAsync)
            .AllowAnonymous();
        authorsEndpoints.MapPost("/", AuthorsHandlers.CreateAuthorAsync)
            .AddEndpointFilter<ValidationFilterGeneric<CreateAuthorDto>>()
            // .RequireAuthorization(policy =>
            // {
            //     policy.RequireRole("admin");
            // })
            ;
        //authorWithIdEndpoints.MapPut("/", AuthorsHandlers.EditAuthorAsync)
        //    .AddEndpointFilter<ValidationFilterGeneric<CreateAuthorDto>>();
        //authorWithIdEndpoints.MapDelete("/", AuthorsHandlers.DeleteAuthorAsync);
        //    .AddEndpointFilter<AlexIsLocked>()
        authorsWithIntIdAndLockFilters.MapPut("/", AuthorsHandlers.EditAuthorAsync)
               .AddEndpointFilter<ValidationFilterGeneric<CreateAuthorDto>>();
        authorsWithIntIdAndLockFilters.MapDelete("/", AuthorsHandlers.DeleteAuthorAsync);
    }
}