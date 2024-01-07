using NovusLiberus.Api.DTOs.BookDtos;
using NovusLiberus.Api.DTOs.UserDtos;

namespace NovusLiberus.Api.DTOs.LoanDtos;

public record LoanDetailsDto(
    int Id,
    DateTime LoanDate,
    DateTime DueDate,
    DateTime? ReturnDate,
    BookDto Book,
    UserDto User
    );