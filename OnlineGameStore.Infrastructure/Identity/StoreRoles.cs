using System.Collections.ObjectModel;

namespace OnlineGameStore.Infrastructure.Identity;

public class StoreRoles
{
    public const string Admin = nameof(Admin);
    public const string User = nameof(User);

    public static IReadOnlyList<string> DefaultRoles = new ReadOnlyCollection<string>(new[]
    {
        Admin,
        User
    });
}