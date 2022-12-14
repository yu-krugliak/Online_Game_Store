using FluentValidation;
using OnlineGameStore.Application.Models.Requests;

namespace OnlineGameStore.Application.Models.Validators
{
    public class CommentRequestValidator : CustomValidator<CommentRequest>
    {
        public CommentRequestValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty()
                .WithMessage("Name can't be empty.")
                .MinimumLength(2)
                .WithMessage("Name must have at least 2 characters.")
                .MaximumLength(100)
                .WithMessage("Name can't be longer than 100 characters.");

            RuleFor(r => r.Body)
                .NotEmpty()
                .WithMessage("Body can't be empty.")
                .MinimumLength(2)
                .WithMessage("Body must have at least 2 characters.")
                .MaximumLength(500)
                .WithMessage("Body can't be longer than 500 characters.");
        }
    }
}
