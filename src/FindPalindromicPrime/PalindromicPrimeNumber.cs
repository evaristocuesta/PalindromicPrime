namespace FindPalindromicPrime;

public class PalindromicPrimeNumber
{
    public static string Find(string number, int digits, IProgress<long> progress)
    {
        for (int i = 0; i < number.Length - digits + 1; i++)
        {
            if (HasPalindromicPrime(number, digits, i, out string palindromicPrime))
            {
                return palindromicPrime;
            }

            progress.Report(i);
        }

        return string.Empty;
    }

    public static string FindParallel(string number, int digits)
    {
        string result = string.Empty;

        Parallel.For(0, number.Length - digits + 1, i =>
        {
            if (HasPalindromicPrime(number, digits, i, out string palindromicPrime))
            {
                result = palindromicPrime;
                return;
            }
        });

        return result;
    }

    private static bool HasPalindromicPrime(string number, int digits, int i, out string palindromicPrime)
    {
        palindromicPrime = string.Empty;
        var subNumber = number.Substring(i, digits);
        ulong.TryParse(subNumber, out ulong result);

        if (PalindromicNumber.IsPalindrome(subNumber) && PrimeNumber.IsPrimeNumber(result))
        {
            palindromicPrime = subNumber;
            return true;
        }

        return false;
    }
}