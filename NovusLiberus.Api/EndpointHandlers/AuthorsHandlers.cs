using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovusLiberus.Api.Data;
using NovusLiberus.Api.DTOs;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.EndpointHandlers;

public static class AuthorsHandlers
{
    public static async Task<Ok<IEnumerable<AuthorDto>>> GetAllAuthorsAsync (DataContext dishesBdContext,
    IMapper mapper,
    ILogger<AuthorDto> logger,
    [FromQuery] string? name) 
    {
        logger.LogInformation("Getting the authors...");
        var dishes = await dishesBdContext.Authors.Where(d => name == null || d.FirstName.Contains(name) || d.LastName.Contains(name)).ToListAsync();
        var mappedAuthors =  mapper.Map<IEnumerable<AuthorDto>>(dishes);
        return TypedResults.Ok(mappedAuthors);
    }
    
    public static async Task<Results<NotFound, Ok<AuthorDetailsDto>>> GetAuthorByIdAsync (DataContext dataContext,
        IMapper mapper,
        int authorId)
    {
        var author = await dataContext.Authors.FindAsync(authorId);
        // var author = await dataContext.Authors.FirstOrDefaultAsync(a=> a.Id == authorId);
        if (author == null)
        {
            return TypedResults.NotFound();
        }
        var mappedAuthor = mapper.Map<AuthorDetailsDto>(author);
        return TypedResults.Ok(mappedAuthor);
    }
    
    //TODO: modify the handler , or get rid of it
    public static async Task<Results<NotFound, Ok<AuthorDto>>> GetAuthorByNameAsync(DataContext dataContext,
        IMapper mapper,
        string authorName)
    {
        var author = await dataContext.Authors.FirstOrDefaultAsync(d => d.LastName.ToLower() == authorName.ToLower());
        if (author == null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok(mapper.Map<AuthorDto>(author));
    }
    
    public static async Task<CreatedAtRoute<AuthorDetailsDto>> CreateAuthorAsync(DataContext dataContext,
        IMapper mapper,
        CreateAuthorDto createAuthorDto) 
    {
        var author = mapper.Map<Author>(createAuthorDto);
        // dataContext.Add((object)author);
        dataContext.Add(author);
        await dataContext.SaveChangesAsync();
    
        var authorToReturn = mapper.Map<AuthorDetailsDto>(author);
        return TypedResults.CreatedAtRoute(
            authorToReturn,
            "GetAuthor",
            new {authorId=authorToReturn.Id}
        );
    }
    
    public static async Task<Results<NotFound, NoContent>> EditAuthorAsync (DataContext dataContext,
        IMapper mapper,
        [FromRoute] int authorId,
        CreateAuthorDto createAuthorDto) 
    { 
        var author = await dataContext.Authors.FirstOrDefaultAsync(d => d.Id == authorId);
        if (author == null)
        {
            return TypedResults.NotFound();
        }
    
        mapper.Map(createAuthorDto, author);
        // dataContext.Entry(author).State = EntityState.Modified;
        await dataContext.SaveChangesAsync();
    
        return TypedResults.NoContent();
    }
    
    public static async Task<Results<NotFound, NoContent>> DeleteAuthorAsync (DataContext dataContext, [FromRoute] int authorId) 
    { 
        var author = await dataContext.Authors.FindAsync(authorId);
        // var author = await dataContext.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
        if (author == null)
        {
            return TypedResults.NotFound();
        }
    
        // db.Authors.Remove(author);
        dataContext.Remove(author);
        await dataContext.SaveChangesAsync();
    
        return TypedResults.NoContent();
    }
}