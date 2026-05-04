using FluentValidation;
using GameStore.Api.Dtos;

namespace GameStore.Api.Application.Validators;

public class UpdateGameValidator : AbstractValidator<UpdateGameDto>
{
    public UpdateGameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must be 50 characters or fewer.");

        RuleFor(x => x.GenreId)
            .GreaterThan(0).WithMessage("GenreId must be greater than 0.")
            .LessThanOrEqualTo(50).WithMessage("GenreId must be 50 or less.");

        RuleFor(x => x.Price)
            .NotNull().WithMessage("Price is required.");

        RuleFor(x => x.ReleaseDate)
            .NotEqual(default(DateOnly)).WithMessage("Release date is required.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today)).WithMessage("Release date cannot be in the future.");
    }
}
