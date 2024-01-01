using FluentValidation;
using NovusLiberus.Api.DTOs;

namespace NovusLiberus.Api.Validators;

public class AuthorValidator : AbstractValidator<CreateAuthorDto>

{
    public AuthorValidator()
    {
        RuleFor(d => d.FirstName)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(64)
            .WithMessage("First Name must be between 1 and 64 chars");
        // .Must(FirstName => FirstName is string);//doesnt catch
        RuleFor(d => d.LastName)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(64)
            .WithMessage("Last name must be between 1 and 64 chars");
        // .Must(LastName => LastName.GetType() == typeof(string));//doesnt catch

    }
}