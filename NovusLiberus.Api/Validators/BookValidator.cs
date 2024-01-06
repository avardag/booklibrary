using FluentValidation;
using NovusLiberus.Api.DTOs.BookDtos;

namespace NovusLiberus.Api.Validators;

public class BookValidator: AbstractValidator<CreateBookDto>
{
    public BookValidator()
    {
        RuleFor(b => b.ISBN)
            .NotEmpty()
            .MinimumLength(9)
            .MaximumLength(13)
            .WithMessage("ISBN must be between 9 and 13 chars");
        RuleFor(b => b.Title)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(64)
            .WithMessage("Title must be between 1 and 64 chars");
        // .Must(LastName => LastName.GetType() == typeof(string));//doesnt catch
        RuleFor(b => b.CurrentStockLevel)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Provide stock level");
        RuleFor(b => b.AuthorIds)
            .NotEmpty()
            .WithMessage("Provide at least one Author Id");
        RuleForEach(b => b.AuthorIds).NotEmpty().Must(id=> id > 0);
        RuleFor(b => b.GenreIds)
            .NotEmpty()
            .WithMessage("Provide at least one Genre Id");
        RuleForEach(b => b.GenreIds).NotEmpty().Must(id=> id > 0);
    }
}
// public record CreateBookDto(
//     string ISBN,
//     string Title,
//     DateTime PubDate,
//     int CurrentStockLevel,
//     List<int> GenreIds,
//     List<int> AuthorIds);
//     