namespace NovusLiberus.Api.DTOs.BookDtos;

public record CreateBookDto(
    string ISBN,
    string Title,
    DateTime PubDate,
    int CurrentStockLevel,
    List<int> GenreIds,
    List<int> AuthorIds);

