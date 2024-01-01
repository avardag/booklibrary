namespace NovusLiberus.Api.DTOs.UserDtos;

public record EditUserDto(
    string FirstName,  
    string? MiddleName, 
    string LastName, 
    string Email, 
    DateTime DateOfBirth, 
    string Phone, 
    Guid LibraryCardNumber);