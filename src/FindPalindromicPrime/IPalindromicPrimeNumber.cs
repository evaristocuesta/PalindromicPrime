namespace FindPalindromicPrime;

public interface IPalindromicPrimeNumber
{
    string Find(string number, int digits, IProgress<long> progress);
    string FindParallel(string number, int digits);
}
