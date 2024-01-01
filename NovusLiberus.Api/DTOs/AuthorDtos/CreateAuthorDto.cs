namespace NovusLiberus.Api.DTOs.AuthorDtos;

public record CreateAuthorDto(
    string FirstName,
    string? MiddleName,
    string LastName,
    DateTime? DateOfBirth,
    DateTime? DateOfDeath);