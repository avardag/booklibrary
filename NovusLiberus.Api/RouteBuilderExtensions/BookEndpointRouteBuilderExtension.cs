using NovusLiberus.Api.Constants;
using NovusLiberus.Api.DTOs.BookDtos;
using NovusLiberus.Api.EndpointFilters;
using NovusLiberus.Api.EndpointHandlers;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.RouteBuilderExtensions;

public static class BookEndpointRouteBuilderExtension
{
    public static void MapBooksEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        //Endpoint Groups//
        var apiVersionOneEndpoints = endpointRouteBuilder.MapGroup(RouteConstants.ApiV1);
        var booksEndpoints = apiVersionOneEndpoints.MapGroup("/books")
            .WithTags(nameof(Book) + " routes")
            .WithOpenApi();
        var bookWithIdEndpoints = booksEndpoints.MapGroup("/{bookId:int}");
        // .RequireAuthorization();
        var booksWithIntIdAndLockFilters = booksEndpoints.MapGroup("/{bookId:int}");

        //Endpoints//
        booksEndpoints.MapGet("/", BooksHandlers.GetAllBooksAsync)
            .WithName("GetBooks")
            .WithSummary("Gets all books");
        bookWithIdEndpoints.MapGet("/", BooksHandlers.GetBookByIdAsync)
            .WithName("GetBook")
            .WithSummary("Gets book by providing ID");
        booksEndpoints.MapGet("/{bookName}", BooksHandlers.GetBookByTitleAsync)
            .AllowAnonymous()
            .WithSummary("Gets book by providing name");
        booksEndpoints.MapPost("/", BooksHandlers.CreateBookAsync)
            .AddEndpointFilter<ValidationFilterGeneric<CreateBookDto>>()
            .ProducesValidationProblem(400)
            .WithSummary("Creates new book by providing book DTO");
        booksWithIntIdAndLockFilters.MapPut("/", BooksHandlers.EditBookAsync)
            .AddEndpointFilter<ValidationFilterGeneric<EditBookDto>>()
            .ProducesValidationProblem(400)
            .WithSummary("Edits, updates book by providing ID.")
            .WithDescription("Edits only book info. For editing authors and genres use respective routes");
        booksWithIntIdAndLockFilters.MapDelete("/", BooksHandlers.DeleteBookAsync)
            .WithSummary("Deletes book by providing ID");
        bookWithIdEndpoints.MapPost("/authors/add/{authorId:int}", BooksHandlers.AddOrRemoveAuthorToABookAsync)
            .WithSummary("Add an author to a book, provide book id and author id");
        bookWithIdEndpoints.MapPost("/authors/remove/{authorId:int}", BooksHandlers.AddOrRemoveAuthorToABookAsync)
            .WithSummary("Remove an author from a book, provide book id and author id");
        bookWithIdEndpoints.MapPost("/genres/add/{genreId:int}", BooksHandlers.AddOrRemoveGenreToABookAsync)
            .WithSummary("Add a genre to a book");
        bookWithIdEndpoints.MapPost("/genres/remove/{genreId:int}", BooksHandlers.AddOrRemoveGenreToABookAsync)
            .WithSummary("Remove a genre from a book, provide book Id and Genre Id");
    }
}