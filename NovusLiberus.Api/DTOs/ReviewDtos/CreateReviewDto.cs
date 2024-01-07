namespace NovusLiberus.Api.DTOs.ReviewDtos;

public record CreateReviewDto(string? Comment, int Rating, int UserId, int BookId);