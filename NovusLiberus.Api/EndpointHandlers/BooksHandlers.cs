using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovusLiberus.Api.Data;
using NovusLiberus.Api.DTOs.BookDtos;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.EndpointHandlers;

public class BooksHandlers
{
    public static async Task<Ok<IEnumerable<BookWithAuthorsAndGenresDto>>> GetAllBooksAsync (DataContext dataContext,
    IMapper mapper,
    ILogger<BookWithAuthorsAndGenresDto> logger,
    [FromQuery] string? title) 
    {
        logger.LogInformation("Getting the books...");
        var books = await dataContext.Books
            .Where(b => title == null || b.Title.ToLower().Contains(title.ToLower()) )
            .ToListAsync();
        var mappedBooks =  mapper.Map<IEnumerable<BookWithAuthorsAndGenresDto>>(books);
        return TypedResults.Ok(mappedBooks);
    }
    
    public static async Task<Results<NotFound, Ok<BookWithAuthorsAndGenresDto>>> GetBookByIdAsync (DataContext dataContext,
        IMapper mapper,
        int bookId)
    {
        var book = await dataContext.Books
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .FirstOrDefaultAsync(b => b.Id == bookId);
        // var book = await dataContext.Books.FirstOrDefaultAsync(a=> a.Id == bookId);
        if (book == null)
        {
            return TypedResults.NotFound();
        }
        var mappedBook = mapper.Map<BookWithAuthorsAndGenresDto>(book);
        return TypedResults.Ok(mappedBook);
    }
    
    public static async Task<Results<NotFound, Ok<BookWithAuthorsAndGenresDto>>> GetBookByTitleAsync(DataContext dataContext,
        IMapper mapper,
        string bookTitle)
    {
        var book = await dataContext.Books
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .FirstOrDefaultAsync(d => d.Title.ToLower().Contains(bookTitle.ToLower()) );
        if (book == null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok(mapper.Map<BookWithAuthorsAndGenresDto>(book));
    }
    
    public static async Task<Results<ProblemHttpResult, CreatedAtRoute<BookWithAuthorsAndGenresDto>>> CreateBookAsync(DataContext dataContext,
        IMapper mapper,
        CreateBookDto createBookDto) 
    {
        var book = mapper.Map<Book>(createBookDto);
        
        dataContext.Add((object)book);
        
        foreach (int authorId in createBookDto.AuthorIds)
        {
            book.Authors = book.Authors is null ? new List<Author>() : book.Authors;
            var author = await dataContext.Authors.FindAsync(authorId);
            if (author == null) return TypedResults.Problem("Author doesn't exist");
            book.Authors.Add(author);
        }
        foreach (int genreId in createBookDto.GenreIds)
        {
            book.Genres = book.Genres ?? new List<Genre>() ;
            var genre = await dataContext.Genres.FindAsync(genreId);
            if (genre == null) return TypedResults.Problem("Genre doesn't exist");
            book.Genres.Add(genre);
        }
        
        await dataContext.SaveChangesAsync();
    
        var bookToReturn = mapper.Map<BookWithAuthorsAndGenresDto>(book);
        return TypedResults.CreatedAtRoute(
            bookToReturn,
            "GetBook",
            new {bookId=bookToReturn.Id}
        );
    }
    
    //Edits only book info. For editing authors and genres use respective routes
    public static async Task<Results<NotFound, NoContent>> EditBookAsync (DataContext dataContext,
        IMapper mapper,
        [FromRoute] int bookId,
        EditBookDto editBookDto) 
    { 
        var book = await dataContext.Books.FirstOrDefaultAsync(d => d.Id == bookId);
        if (book == null)
        {
            return TypedResults.NotFound();
        }
    
        mapper.Map(editBookDto, book);
        // dataContext.Entry(book).State = EntityState.Modified;
        await dataContext.SaveChangesAsync();
    
        return TypedResults.NoContent();
    }
    
    public static async Task<Results<NotFound, NoContent>> DeleteBookAsync (DataContext dataContext, [FromRoute] int bookId) 
    { 
        var book = await dataContext.Books.FindAsync(bookId);
        // var book = await dataContext.Books.FirstOrDefaultAsync(a => a.Id == bookId);
        if (book == null)
        {
            return TypedResults.NotFound();
        }
    
        // db.Books.Remove(book);
        dataContext.Remove(book);
        await dataContext.SaveChangesAsync();
    
        return TypedResults.NoContent();
    }
    //Add an author to a book
    public static async Task<Results<NotFound, NoContent>> AddOrRemoveAuthorToABookAsync (
        HttpRequest request,
        DataContext dataContext,
        [FromRoute] int bookId,
        [FromRoute] int authorId
        ) 
    { 
        var book = await dataContext.Books.Include(b=>b.Authors).FirstOrDefaultAsync(d => d.Id == bookId);
        var author = await dataContext.Authors.FirstOrDefaultAsync(d => d.Id == authorId);
        if (book == null || author == null)
        {
            return TypedResults.NotFound();
        }
        
        if (request.Path.Value.Contains("add"))
        {
            book.Authors.Add(author);
        }
        else if (request.Path.Value.Contains("remove"))
        {
            book.Authors.Remove(author);
        }
        
        dataContext.Entry(book).State = EntityState.Modified;
        await dataContext.SaveChangesAsync();
    
        return TypedResults.NoContent();
    }
    public static async Task<Results<NotFound, NoContent>> AddOrRemoveGenreToABookAsync (DataContext dataContext,
        HttpRequest request,
        [FromRoute] int bookId,
        [FromRoute] int genreId
        ) 
    { 
        var book = await dataContext.Books.Include(b=>b.Genres).FirstOrDefaultAsync(d => d.Id == bookId);
        var genre = await dataContext.Genres.FirstOrDefaultAsync(d => d.Id == genreId);
        if (book == null || genre == null)
        {
            return TypedResults.NotFound();
        }

        if (request.Path.Value.Contains("add"))
        {
            book.Genres.Add(genre);
        }
        else if (request.Path.Value.Contains("remove"))
        {
            book.Genres.Remove(genre);
        }
        // dataContext.Entry(book).State = EntityState.Modified;
        await dataContext.SaveChangesAsync();
    
        return TypedResults.NoContent();
    }
}