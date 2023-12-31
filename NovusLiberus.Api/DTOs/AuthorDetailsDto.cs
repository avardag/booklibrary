namespace NovusLiberus.Api.DTOs;

public record AuthorDetailsDto(
    int Id,
    string FirstName,
    string? MiddleName,
    string LastName,
    DateTime? DateOfBirth,
    DateTime? DateOfDeath);