using FluentValidation;
using OnlineGameStore.Application.Models.Requests;

namespace OnlineGameStore.Application.Models.Validators
{
    public class PlatformRequestValidator : CustomValidator<PlatformRequest>
    {
        public PlatformRequestValidator()
        {
            RuleFor(r => r.Type)
                .NotEmpty()
                .WithMessage("Type can't be empty.")
                .MinimumLength(2)
                .WithMessage("Type must have at least 2 characters.")
                .MaximumLength(60)
                .WithMessage("Type can't be longer than 60 characters.");
        }
    }
}
