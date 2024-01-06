using NovusLiberus.Api.DTOs.AuthorDtos;
using NovusLiberus.Api.DTOs.GenreDtos;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.DTOs.BookDtos;

public record BookWithAuthorsAndGenresDto(
    int Id,
    string ISBN,
    string Title,
    DateTime PubDate,
    double AvgRating,
    int CurrentStockLevel,
    List<GenreDto> Genres,
    List<AuthorDto> Authors
    );
