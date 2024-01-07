namespace NovusLiberus.Api.DTOs.ReviewDtos;

public record EditReviewDto(int Id, string Comment, int Rating, int UserId, int BookId);