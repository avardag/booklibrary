namespace NovusLiberus.Api.DTOs.UserDtos;

public record UserDto(
    int Id, 
    string FirstName,  
    string LastName, 
    string Email, 
    Guid LibraryCardNumber);