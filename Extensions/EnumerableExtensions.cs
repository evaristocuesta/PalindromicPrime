using System.Text;

namespace Extensions;

public static class EnumerableExtensions
{
    public static string TakeToString<T>(this IEnumerable<T> enumerable, int digits)
    {
        StringBuilder stringBuilder = new StringBuilder();

        foreach (T number in enumerable.Take(digits))
        {
            stringBuilder.Append(number);
        }

        return stringBuilder.ToString();
    }
}