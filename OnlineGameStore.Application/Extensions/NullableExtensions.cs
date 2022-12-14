using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
