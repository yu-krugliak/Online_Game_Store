using FluentValidation;
using OnlineGameStore.Application.Models.Requests;

namespace OnlineGameStore.Application.Models.Validators
{
    public class RegisterRequestValidator : CustomValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage("Password can't be empty.")
                .MinimumLength(6)
                .WithMessage("Password must have at least 6 characters.");

            RuleFor(r => r.ConfirmPassword)
                .Equal(r => r.Password)
                .WithMessage("Passwords can't be different.");

            RuleFor(r => r.FirstName)
                .NotEmpty()
                .WithMessage("First name can't be empty.")
                .MinimumLength(2)
                .WithMessage("First name must have at least 2 characters.")
                .MaximumLength(100)
                .WithMessage("First name can't be longer than 100 characters.");

            RuleFor(r => r.LastName)
                .NotEmpty()
                .WithMessage("Last name can't be empty.")
                .MinimumLength(2)
                .WithMessage("Last name must have at least 2 characters.")
                .MaximumLength(100)
                .WithMessage("Last name can't be longer than 100 characters.");

            RuleFor(r => r.UserName)
                .NotEmpty()
                .WithMessage("Username can't be empty.");

            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Email can't be empty.")
                .EmailAddress()
                .WithMessage("'Email' is not a valid email address");
        }
    }
}
