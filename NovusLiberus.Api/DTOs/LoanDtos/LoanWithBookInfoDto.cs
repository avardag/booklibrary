using NovusLiberus.Api.DTOs.BookDtos;

namespace NovusLiberus.Api.DTOs.LoanDtos;

public record LoanWithBookInfoDto(
    int Id,
    DateTime LoanDate,
    DateTime DueDate,
    DateTime? ReturnDate,
    BookDto Book,
    int UserId
    );