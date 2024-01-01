using FluentValidation;
using NovusLiberus.Api.DTOs.UserDtos;

namespace NovusLiberus.Api.Validators;

public class UserValidator: AbstractValidator<CreateUserDto>

{
    public UserValidator()
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
        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Provide a valid email address");
        // RuleFor(u => u.Phone)
        //     .NotEmpty()
        //     .WithMessage("Provide a phone number");
        
    }
}
public class UserEditValidator: AbstractValidator<EditUserDto>
{
    public UserEditValidator()
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
        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Provide a valid email address");
    }
}