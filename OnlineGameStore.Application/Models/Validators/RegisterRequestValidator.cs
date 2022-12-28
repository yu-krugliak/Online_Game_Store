using FluentValidation;
using OnlineGameStore.Application.Models.Requests;

namespace OnlineGameStore.Application.Models.Validators
{
    public class RegisterRequestValidator : CustomValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            StringMustBeInRange(r => r.FirstName, 2, 120);

            StringMustBeInRange(r => r.LastName, 2, 120);

            StringMustBeInRange(r => r.Password, 6, 50);

            RuleFor(r => r.ConfirmPassword)
                .Equal(r => r.Password)
                .WithMessage("Passwords can't be different.");

            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Email can't be empty.")
                .EmailAddress()
                .WithMessage("'Email' is not a valid email address");

            RuleFor(r => r.UserName)
                .NotEmpty()
                .WithMessage("Username can't be empty.");
        }
    }
}
