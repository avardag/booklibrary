namespace NovusLiberus.Api.DTOs.UserDtos;

public record CreateUserDto(
    string FirstName,  
    string? MiddleName, 
    string LastName, 
    string Email, 
    DateTime DateOfBirth, 
    string Phone);