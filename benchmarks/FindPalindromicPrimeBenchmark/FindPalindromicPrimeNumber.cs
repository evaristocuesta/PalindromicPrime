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
        Number = Spigot.GetPiDecimals(new Progress<long>()).TakeToString(Digits);
    }

    [Benchmark]
    public string Find()
    {
        return PalindromicPrimeNumber.Find(Number, 9, new Progress<long>());
    }

    [Benchmark]
    public string FindParallel()
    {
        return PalindromicPrimeNumber.FindParallel(Number, 9);
    }
}
