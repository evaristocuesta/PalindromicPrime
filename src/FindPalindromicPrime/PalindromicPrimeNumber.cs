using System.Numerics;

namespace FindPalindromicPrime;

public class PalindromicPrimeNumber : IPalindromicPrimeNumber
{
    private readonly IPrimeNumber _primeNumber;
    private readonly IPalindromicNumber _palindromicNumber;

    public PalindromicPrimeNumber(IPrimeNumber primeNumber, IPalindromicNumber palindromicNumber)
    {
        _primeNumber = primeNumber;
        _palindromicNumber = palindromicNumber;
    }

    public string Find(string number, int digits, IProgress<long> progress)
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

    public string FindParallel(string number, int digits)
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

    private bool HasPalindromicPrime(string number, int digits, int i, out string palindromicPrime)
    {
        palindromicPrime = string.Empty;
        var subNumber = number.Substring(i, digits);

        if (_palindromicNumber.IsPalindrome(subNumber) && _primeNumber.IsPrimeNumber(BigInteger.Parse(subNumber)))
        {
            palindromicPrime = subNumber;
            return true;
        }

        return false;
    }
}