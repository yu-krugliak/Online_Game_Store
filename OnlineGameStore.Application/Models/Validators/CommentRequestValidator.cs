using OnlineGameStore.Application.Models.Requests;

namespace OnlineGameStore.Application.Models.Validators
{
    public class CommentRequestValidator : CustomValidator<CommentRequest>
    {
        public CommentRequestValidator()
        {
            StringMustBeInRange(r => r.Body, 2, 500);
        }
    }
}
