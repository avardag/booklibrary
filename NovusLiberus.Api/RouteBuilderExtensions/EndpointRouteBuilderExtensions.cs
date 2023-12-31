using NovusLiberus.Api.EndpointHandlers;

namespace NovusLiberus.Api.RouteBuilderExtensions;

public static class EndpointRouteBuilderExtensions
{
    public static void MapAuthorsEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var apiVersionOneEndpoints = endpointRouteBuilder.MapGroup("/api/v1");
        var authorsEndpoints = apiVersionOneEndpoints.MapGroup("/authors");
        var authorWithIdEndpoints = authorsEndpoints.MapGroup("/{authorId:int}");

        authorsEndpoints.MapGet("/", AuthorsHandlers.GetAllAuthorsAsync).WithName("GetAuthors");
        authorWithIdEndpoints.MapGet("/", AuthorsHandlers.GetAuthorByIdAsync).WithName("GetAuthor");
        authorsEndpoints.MapGet("/{authorName}", AuthorsHandlers.GetAuthorByNameAsync)
            .AllowAnonymous();
        authorsEndpoints.MapPost("/", AuthorsHandlers.CreateAuthorAsync)
            ;
        authorWithIdEndpoints.MapPut("/", AuthorsHandlers.EditAuthorAsync);
        authorWithIdEndpoints.MapDelete("/", AuthorsHandlers.DeleteAuthorAsync);
    }
} 