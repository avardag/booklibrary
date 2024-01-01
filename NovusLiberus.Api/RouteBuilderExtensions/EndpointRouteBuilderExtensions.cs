using NovusLiberus.Api.DTOs;
using NovusLiberus.Api.EndpointFilters;
using NovusLiberus.Api.EndpointHandlers;

namespace NovusLiberus.Api.RouteBuilderExtensions;

public static class EndpointRouteBuilderExtensions
{
    public static void MapAuthorsEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var apiVersionOneEndpoints = endpointRouteBuilder.MapGroup("/api/v1").WithOpenApi();
        var authorsEndpoints = apiVersionOneEndpoints.MapGroup("/authors");
        var authorWithIdEndpoints = authorsEndpoints.MapGroup("/{authorId:int}");
        // .RequireAuthorization();
        var authorsWithIntIdAndLockFilters = authorsEndpoints.MapGroup("/{authorId:int}")
            .AddEndpointFilter(new AuthorIsLockedFilter(2));

        authorsEndpoints.MapGet("/", AuthorsHandlers.GetAllAuthorsAsync)
                .WithName("GetAuthors")
                .WithSummary("Gets all authors")
                .WithDescription("Optionally a name or part of name can be entered as a query paramater and the route will return a list of authors containing the query string");
        authorWithIdEndpoints.MapGet("/", AuthorsHandlers.GetAuthorByIdAsync)
                .WithName("GetAuthor")
                .WithSummary("Gets author by providing ID");
        authorsEndpoints.MapGet("/{authorName}", AuthorsHandlers.GetAuthorByNameAsync)
                .AllowAnonymous()
                .WithSummary("Gets author by providing name")
                .WithOpenApi(operation =>
                {
                    operation.Deprecated = true;
                    return operation;
                });
        authorsEndpoints.MapPost("/", AuthorsHandlers.CreateAuthorAsync)
                .AddEndpointFilter<ValidationFilterGeneric<CreateAuthorDto>>()
                .ProducesValidationProblem(400)
                .WithSummary("Creates new author by providing author DTO")
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
               .AddEndpointFilter<ValidationFilterGeneric<CreateAuthorDto>>()
               .ProducesValidationProblem(400)
               .WithSummary("Edits, updates author by providing ID");
        authorsWithIntIdAndLockFilters.MapDelete("/", AuthorsHandlers.DeleteAuthorAsync)
                .WithSummary("Deletes author by providing ID");
    }

}