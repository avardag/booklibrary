using FluentValidation;
using NovusLiberus.Api.DTOs.LoanDtos;

namespace NovusLiberus.Api.Validators;

public class LoanValidator : AbstractValidator<CreateLoanDto>
{
    public LoanValidator()
    {
        RuleFor(b => b.UserId)
            .NotNull()
            .WithMessage("User Id must be entered");
        RuleFor(b => b.BookId)
            .NotNull()
            .WithMessage("Book Id must be entered");
    }
}