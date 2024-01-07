namespace NovusLiberus.Api.DTOs.ReviewDtos;

public record ReviewDto(int Id, string Comment, int Rating, int UserId, int BookId);
