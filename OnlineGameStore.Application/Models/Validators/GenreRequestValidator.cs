using FluentValidation;
using OnlineGameStore.Application.Models.Requests;

namespace OnlineGameStore.Application.Models.Validators
{
    public class GenreRequestValidator : CustomValidator<GenreRequest>
    {
        public GenreRequestValidator()
        {
            StringMustBeInRange(r => r.Name, 2, 60);

            StringMustBeInRange(r => r.Description, 10, 500);
        }
    }
}
