using FluentValidation;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;

namespace OnlineGameStore.Application.Models.Validators
{
    public class GenreRequestValidator : CustomValidator<GenreRequest>
    {
        public GenreRequestValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("Name can't be empty.")
                .MinimumLength(2)
                .WithMessage("Name must have at least 2 characters.")
                .MaximumLength(60)
                .WithMessage("Name can't be longer than 60 characters.");

            RuleFor(r => r.Description)
                .NotEmpty()
                .WithMessage("Description can't be empty.")
                .MinimumLength(10)
                .WithMessage("Description must have at least 10 characters.")
                .MaximumLength(500)
                .WithMessage("Description can't be longer than 500 characters.");
        }
    }
}
