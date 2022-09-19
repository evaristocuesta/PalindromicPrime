using BenchmarkDotNet.Attributes;
using Extensions;
using FindPalindromicPrime;
using JGSpigotPiDecimals;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FindPalindromicPrimeBenchmark;

public class FindPalindromicPrimeNumber
{
    private IPalindromicPrimeNumber? _palindromicPrimeNumber;
    private string _number = String.Empty;

    [Params(100, 1000, 10000, 100000)]
    public int Digits;

    [GlobalSetup]
    public void Setup()
    {
        var builder = new HostBuilder()
            .ConfigureServices((hostContext, services) =>
        {
            services.AddSingleton<IPalindromicPrimeNumber, PalindromicPrimeNumber>();
            services.AddSingleton<ISpigot, Spigot>();
            services.AddSingleton<IPrimeNumber, PrimeNumber>();
            services.AddSingleton<IPalindromicNumber, PalindromicNumber>();
        }).UseConsoleLifetime();

        var host = builder.Build();
        
        using (var serviceScope = host.Services.CreateScope())
        {
            var services = serviceScope.ServiceProvider;
            var spigot = services.GetRequiredService<ISpigot>();
            _number = spigot.GetPiDecimals(new Progress<long>()).TakeToString(Digits);
            _palindromicPrimeNumber = services.GetRequiredService<IPalindromicPrimeNumber>();
        }
    }

    [Benchmark]
    public string? Find()
    {
        return _palindromicPrimeNumber?.Find(_number, 9, new Progress<long>());
    }

    [Benchmark]
    public string? FindParallel()
    {
        return _palindromicPrimeNumber?.FindParallel(_number, 9);
    }
}
