using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.Application.Auth.JwtTokenServices;

public class JwtSettings : IValidatableObject
{
    public string Key { get; set; } = string.Empty;

    public int TokenExpirationInMinutes { get; set; }

    public int RefreshTokenExpirationInDays { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Key))
        {
            yield return new ValidationResult("No Key in Jwt config", new[] { nameof(Key) });
        }
    }
}