namespace NovusLiberus.Api.DTOs.UserDtos;

public record UserDetailsDto(
    int Id, 
    string FirstName,  
    string? MiddleName, 
    string LastName, 
    string Email, 
    DateTime DateOfBirth, 
    string Phone, 
    Guid LibraryCardNumber);