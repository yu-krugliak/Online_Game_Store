using FluentValidation;
using OnlineGameStore.Application.Models.Requests;
using OnlineGameStore.Infrastructure.Repositories.Interfaces;

namespace OnlineGameStore.Application.Models.Validators
{
    public class CommentRequestValidator : CustomValidator<CommentRequest>
    {
        public CommentRequestValidator(ICommentRepository commentRepository, IGameRepository gameRepository)
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

            RuleFor(r => r.ParentCommentId)
                .MustAsync(async (r, _) => await commentRepository.ExistsAsync(r!.Value))
                .When(r => r.ParentCommentId is not null)
                .WithMessage("Parent comment doesn't exists.");

            RuleFor(r => r.GameId)
                .MustAsync(async (r, _) => await gameRepository.ExistsAsync(r!.Value))
                .When(r => r.GameId is not null)
                .WithMessage("Game doesn't exists.");
        }
    }
}
