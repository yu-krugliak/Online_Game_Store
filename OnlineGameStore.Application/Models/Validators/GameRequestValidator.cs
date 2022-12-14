using FluentValidation;
using OnlineGameStore.Application.Models.Requests;

namespace OnlineGameStore.Application.Models.Validators
{
    public class GameRequestValidator : CustomValidator<GameRequest>
    {
        public GameRequestValidator()
        {
            RuleFor(r => r.Key)
                .NotEmpty()
                .WithMessage("Game key can't be empty.")
                .MinimumLength(10)
                .WithMessage("Game key must have at least 10 characters.")
                .MaximumLength(100)
                .WithMessage("Game key can't be longer than 100 characters.");

            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("Name can't be empty.")
                .MinimumLength(2)
                .WithMessage("Name must have at least 2 characters.")
                .MaximumLength(100)
                .WithMessage("Name can't be longer than 100 characters.");

            RuleFor(r => r.Description)
                .NotEmpty()
                .WithMessage("Description can't be empty.")
                .MinimumLength(10)
                .WithMessage("Description must have at least 10 characters.")
                .MaximumLength(500)
                .WithMessage("Description can't be longer than 500 characters.");

            RuleFor(r => r.Price)
                .GreaterThan(0)
                .WithMessage("Price must be positive.")
                .PrecisionScale(7, 2, false);
        }
    }
}
