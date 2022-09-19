using BenchmarkDotNet.Attributes;

namespace PalindromicNumberBenchmark;

public class PalindromicNumber
{
    [Params("323", "2394862", "197363791")]
    public string Number = string.Empty;

    [Benchmark]
    public bool IsPalindromicReversing()
    {
        char[] myArr = Number.ToCharArray();
        Array.Reverse(myArr);
        var reverse = new string(myArr);
        return Number.Equals(reverse);
    }

    [Benchmark]
    public bool IsPalindrome() 
    { 
        for (int i = 0; i < Number.Length / 2; i++) 
        { 
            if (Number[i] != Number[Number.Length - 1 - i]) 
                return false; 
        } 
        return true; 
    }
}
