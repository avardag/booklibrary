using FluentValidation;
using NovusLiberus.Api.DTOs.ReviewDtos;

namespace NovusLiberus.Api.Validators;

public class ReviewValidator: AbstractValidator<CreateReviewDto>
{
    public ReviewValidator()
    {
        RuleFor(b => b.Rating)
            .NotNull()
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(5)
            .WithMessage("Rating must be between 1 ad 5");
        RuleFor(b => b.Comment)
            .MinimumLength(1)
            .MaximumLength(248)
            .WithMessage("Comment must be between 1 and 248 chars");
        RuleFor(b => b.UserId)
            .NotNull()
            .WithMessage("User Id must be ");
        RuleFor(b => b.BookId)
            .NotNull()
            .WithMessage("Book Id must be ");
    }
}