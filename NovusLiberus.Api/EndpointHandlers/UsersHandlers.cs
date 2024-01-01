using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovusLiberus.Api.Data;
using NovusLiberus.Api.DTOs.UserDtos;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.EndpointHandlers;

public static class UsersHandlers
{
    public static async Task<Ok<IEnumerable<UserDto>>> GetAllUsersAsync (DataContext datacontext,
    IMapper mapper,
    ILogger<UserDto> logger) 
    {
        logger.LogInformation("Getting the users...");
        var users = await datacontext.Users.ToListAsync();
        var mappedUsers =  mapper.Map<IEnumerable<UserDto>>(users);
        return TypedResults.Ok(mappedUsers);
    }
    
    public static async Task<Results<NotFound, Ok<UserDetailsDto>>> GetUserByIdAsync (DataContext dataContext,
        IMapper mapper,
        int userId)
    {
        var user = await dataContext.Users.FindAsync(userId);
        // var user = await dataContext.Users.FirstOrDefaultAsync(a=> a.Id == userId);
        if (user == null)
        {
            return TypedResults.NotFound();
        }
        var mappedUser = mapper.Map<UserDetailsDto>(user);
        return TypedResults.Ok(mappedUser);
    }
    
    public static async Task<Results<NotFound, Ok<UserDto>>> GetUserByNameAsync(DataContext dataContext,
        IMapper mapper,
        string userName)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(d => d.LastName.ToLower().Contains(userName.ToLower()) || d.FirstName.ToLower().Contains(userName.ToLower()));
        if (user == null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok(mapper.Map<UserDto>(user));
    }
    
    public static async Task<CreatedAtRoute<UserDetailsDto>> CreateUserAsync(DataContext dataContext,
        IMapper mapper,
        CreateUserDto createUserDto) 
    {
        Guid newLibCardNum = Guid.NewGuid();
        var user = mapper.Map<User>(createUserDto, opts=> opts.Items["LibraryCardNumber"] = newLibCardNum);
        // dataContext.Add((object)user);
        dataContext.Add((object)user);
        await dataContext.SaveChangesAsync();
    
        var userToReturn = mapper.Map<UserDetailsDto>(user);
        return TypedResults.CreatedAtRoute(
            userToReturn,
            "GetUser",
            new {userId=userToReturn.Id}
        );
    }
    
    public static async Task<Results<NotFound, NoContent>> EditUserAsync (DataContext dataContext,
        IMapper mapper,
        [FromRoute] int userId,
        EditUserDto editUserDto) 
    { 
        var user = await dataContext.Users.FirstOrDefaultAsync(d => d.Id == userId);
        if (user == null)
        {
            return TypedResults.NotFound();
        }
    
        mapper.Map(editUserDto, user );
        // dataContext.Entry(user).State = EntityState.Modified;
        await dataContext.SaveChangesAsync();
    
        return TypedResults.NoContent();
    }
    
    public static async Task<Results<NotFound, NoContent>> DeleteUserAsync (DataContext dataContext, [FromRoute] int userId) 
    { 
        var user = await dataContext.Users.FindAsync(userId);
        // var user = await dataContext.Users.FirstOrDefaultAsync(a => a.Id == userId);
        if (user == null)
        {
            return TypedResults.NotFound();
        }
    
        // db.Users.Remove(user);
        dataContext.Remove(user);
        await dataContext.SaveChangesAsync();
    
        return TypedResults.NoContent();
    }
}