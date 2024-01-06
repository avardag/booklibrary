using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.DTOs.BookDtos;

public record EditBookDto(
        string ISBN,
        string Title,
        DateTime PubDate,
        int CurrentStockLevel);
