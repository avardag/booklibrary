using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovusLiberus.Api.Data;
using NovusLiberus.Api.DTOs.GenreDtos;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.EndpointHandlers;

public static class GenresHandlers
{
    public static async Task<Ok<IEnumerable<GenreDto>>> GetAllGenresAsync (DataContext datacontext,
    IMapper mapper,
    ILogger<GenreDto> logger) 
    {
        logger.LogInformation("Getting the genres...");
        var genres = await datacontext.Genres.ToListAsync();
        var mappedGenres =  mapper.Map<IEnumerable<GenreDto>>(genres);
        return TypedResults.Ok(mappedGenres);
    }
    
    public static async Task<Results<NotFound, Ok<GenreDto>>> GetGenreByIdAsync (DataContext dataContext,
        IMapper mapper,
        int genreId)
    {
        var genre = await dataContext.Genres.FindAsync(genreId);
        // var genre = await dataContext.Genres.FirstOrDefaultAsync(a=> a.Id == genreId);
        if (genre == null)
        {
            return TypedResults.NotFound();
        }
        var mappedGenre = mapper.Map<GenreDto>(genre);
        return TypedResults.Ok(mappedGenre);
    }
    
    public static async Task<Results<NotFound, Ok<GenreDto>>> GetGenreByNameAsync(DataContext dataContext,
        IMapper mapper,
        string genreName)
    {
        var genre = await dataContext.Genres.FirstOrDefaultAsync(g => g.Name.ToLower().Contains(genreName.ToLower()) );
        if (genre == null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok(mapper.Map<GenreDto>(genre));
    }
    
    public static async Task<CreatedAtRoute<GenreDto>> CreateGenreAsync(DataContext dataContext,
        IMapper mapper,
        CreateGenreDto createGenreDto) 
    {
        var genre = mapper.Map<Genre>(createGenreDto);
        // dataContext.Add((object)genre);
        dataContext.Add((object)genre);
        await dataContext.SaveChangesAsync();
    
        var genreToReturn = mapper.Map<GenreDto>(genre);
        return TypedResults.CreatedAtRoute(
            genreToReturn,
            "GetGenre",
            new {genreId=genreToReturn.Id}
        );
    }
    
    public static async Task<Results<NotFound, NoContent>> EditGenreAsync (DataContext dataContext,
        IMapper mapper,
        [FromRoute] int genreId,
        CreateGenreDto createGenreDto) 
    { 
        var genre = await dataContext.Genres.FirstOrDefaultAsync(g => g.Id == genreId);
        if (genre == null)
        {
            return TypedResults.NotFound();
        }
    
        mapper.Map(createGenreDto, genre );
        // dataContext.Entry(genre).State = EntityState.Modified;
        await dataContext.SaveChangesAsync();
    
        return TypedResults.NoContent();
    }
    
    public static async Task<Results<NotFound, NoContent>> DeleteGenreAsync (DataContext dataContext, [FromRoute] int genreId) 
    { 
        var genre = await dataContext.Genres.FindAsync(genreId);
        // var genre = await dataContext.Genres.FirstOrDefaultAsync(a => a.Id == genreId);
        if (genre == null)
        {
            return TypedResults.NotFound();
        }
    
        // db.Genres.Remove(genre);
        dataContext.Remove(genre);
        await dataContext.SaveChangesAsync();
    
        return TypedResults.NoContent();
    }
}