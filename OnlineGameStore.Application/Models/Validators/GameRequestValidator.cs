using FluentValidation;
using OnlineGameStore.Application.Models.Requests;

namespace OnlineGameStore.Application.Models.Validators
{
    public class GameRequestValidator : CustomValidator<GameRequest>
    {
        public GameRequestValidator()
        {
            StringMustBeInRange(r => r.Key, 10, 100);

            StringMustBeInRange(r => r.Name, 2, 100);

            StringMustBeInRange(r => r.Description, 10, 500);

            RuleFor(r => r.Price)
                .GreaterThan(0)
                .WithMessage("Price must be positive.")
                .PrecisionScale(7, 2, false);
        }
    }
}
