using BenchmarkDotNet.Attributes;

namespace EvenNumberBenchmark;

public class EvenNumber
{
    [Params("323", "2394862", "197363791")]
    public string Number;

    [Benchmark]
    public bool IsEvenAsString()
    {
        return new List<char>() { '2', '4', '6', '8' }.Contains(Number[Number.Length - 1]);
    }

    [Benchmark]
    public bool IsEvenAsULong()
    {
        ulong.TryParse(Number, out ulong number);
        return number % 2 == 0;
    }
}
