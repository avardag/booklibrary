namespace NovusLiberus.Api.DTOs.AuthorDtos;

public record AuthorDto(
    int Id,
    string FirstName,
    string? MiddleName,
    string LastName,
    DateTime? DateOfBirth,
    DateTime? DateOfDeath);