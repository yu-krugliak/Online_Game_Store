using FluentValidation;
using OnlineGameStore.Application.Models.Requests;

namespace OnlineGameStore.Application.Models.Validators
{
    public class RefreshTokenRequestValidator : CustomValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(r => r.AccessToken)
                .NotEmpty()
                .WithMessage("Access token can't be empty.");

            RuleFor(r => r.RefreshToken)
                .NotEmpty()
                .WithMessage("Refresh token can't be empty.");
        }
    }
}
