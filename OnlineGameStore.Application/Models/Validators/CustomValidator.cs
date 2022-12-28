using FluentValidation;
using FluentValidation.Internal;
using System.Linq.Expressions;

namespace OnlineGameStore.Application.Models.Validators
{
    public class CustomValidator<T> : AbstractValidator<T>
    {
        protected IRuleBuilderOptions<T, string?> StringMustBeInRange(Expression<Func<T, string?>> expression, int min, int max)
        {
            var member = expression.GetMember();
            var containerType = typeof(T);
            var propertyName = ValidatorOptions.Global.PropertyNameResolver(containerType, member, expression);

            return RuleFor(expression)
                .NotEmpty()
                .WithMessage($"{propertyName} can't be empty.")
                .MinimumLength(min)
                .WithMessage($"{propertyName} must have at least {min} characters.")
                .MaximumLength(max)
                .WithMessage($"{propertyName} can't be longer than {max} characters.");
        }
    }
}
