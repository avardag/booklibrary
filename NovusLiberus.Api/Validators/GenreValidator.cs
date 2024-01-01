using FluentValidation;
using NovusLiberus.Api.DTOs.GenreDtos;

namespace NovusLiberus.Api.Validators;

public class GenreValidator: AbstractValidator<CreateGenreDto>

{
    public GenreValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(64)
            .WithMessage("Genre name must be between 1 and 64 chars");
    }
}