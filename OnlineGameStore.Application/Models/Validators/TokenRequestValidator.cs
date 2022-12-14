using FluentValidation;
using OnlineGameStore.Application.Models.Requests;

namespace OnlineGameStore.Application.Models.Validators
{
    public class TokenRequestValidator : CustomValidator<TokenRequest>
    {
        public TokenRequestValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Email can't be empty.")
                .EmailAddress()
                .WithMessage("'Email' is not a valid email address");

            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage("Password can't be empty.")
                .MinimumLength(6)
                .WithMessage("Password must have at least 6 characters.");
        }
    }
}
