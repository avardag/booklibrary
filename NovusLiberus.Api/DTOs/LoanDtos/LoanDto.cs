namespace NovusLiberus.Api.DTOs.LoanDtos;

public record LoanDto(int Id, DateTime LoanDate, DateTime DueDate, DateTime? ReturnDate, int BookId, int UserId);
