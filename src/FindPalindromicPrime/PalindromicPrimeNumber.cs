namespace FindPalindromicPrime;

public class PalindromicPrimeNumber
{
    public static string Find(string number, int digits)
    {
        for (int i = 0; i < number.Length - digits + 1; i++)
        {
            var subNumber = number.Substring(i, digits);
            ulong.TryParse(subNumber, out ulong result);

            if (PalindromicNumber.IsPalindrome(subNumber) && PrimeNumber.IsPrimeNumber(result))
            {
                return subNumber;
            }
        }

        return string.Empty;
    }

    public static string FindParallel(string number, int digits)
    {
        var palindromes = new List<string>();

        Parallel.For(0, number.Length - digits, i =>
        {
            var subNumber = number.Substring(i, digits);
            ulong.TryParse(subNumber, out ulong result);

            if (PalindromicNumber.IsPalindrome(subNumber)) // && PrimeNumber.IsPrimeNumber(result))
            {
                palindromes.Add(subNumber);
            }
        });

        return string.Empty;
    }
}