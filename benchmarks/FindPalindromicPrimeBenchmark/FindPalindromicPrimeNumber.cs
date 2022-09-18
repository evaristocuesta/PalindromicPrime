using BenchmarkDotNet.Attributes;
using Extensions;
using FindPalindromicPrime;
using JGSpigotPiDecimals;

namespace FindPalindromicPrimeBenchmark;

public class FindPalindromicPrimeNumber
{
    public string Number;

    [Params(100, 1000, 10000, 100000)]
    public int Digits;

    [GlobalSetup]
    public void Setup()
    {
        Number = Spigot.GetPiDecimals().TakeToString(Digits);
    }

    [Benchmark]
    public string Find()
    {
        return PalindromicPrimeNumber.Find(Number, 9);
    }

    [Benchmark]
    public string FindParallel()
    {
        return PalindromicPrimeNumber.FindParallel(Number, 9);
    }
}
