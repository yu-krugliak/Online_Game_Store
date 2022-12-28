using FluentValidation;
using OnlineGameStore.Application.Models.Requests;

namespace OnlineGameStore.Application.Models.Validators
{
    public class PlatformRequestValidator : CustomValidator<PlatformRequest>
    {
        public PlatformRequestValidator()
        {
            StringMustBeInRange(r => r.Type, 2, 60);
        }
    }
}
