namespace OnlineGameStore.Application.Extensions
{
    internal static class NullableExtensions
    {
        public static bool TryGetValue<T>(this T? variable, out T value) where T: struct
        {
            if (!variable.HasValue)
            {
                value = default;
                return false;
            }

            value = variable.Value;
            return true;
        }
    }
}
