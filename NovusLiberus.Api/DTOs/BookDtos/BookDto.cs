namespace NovusLiberus.Api.DTOs.BookDtos;

public record BookDto(
    int Id,
    string ISBN,
    string Title,
    DateTime PubDate,
    double AvgRating,
    int CurrentStockLevel);