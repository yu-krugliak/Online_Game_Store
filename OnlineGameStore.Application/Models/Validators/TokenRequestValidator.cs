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

            StringMustBeInRange(r => r.Password, 6, 50);
        }
    }
}
