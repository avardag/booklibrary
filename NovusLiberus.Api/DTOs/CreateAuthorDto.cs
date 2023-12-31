namespace NovusLiberus.Api.DTOs;

public record CreateAuthorDto(
    string FirstName,
    string? MiddleName,
    string LastName,
    DateTime? DateOfBirth,
    DateTime? DateOfDeath);