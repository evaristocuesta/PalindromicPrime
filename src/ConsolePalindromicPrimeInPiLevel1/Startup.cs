using FindPalindromicPrime;
using JGSpigotPiDecimals;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsolePalindromicPrimeInPiLevel1;

internal class Startup
{
    public static IHost Initialize()
    {
        var builder = new HostBuilder()
        .ConfigureServices((hostContext, services) =>
        {
            services.AddSingleton<IPalindromicPrimeInPi, PalindromicPrimeInPi>();
            services.AddSingleton<IPalindromicPrimeNumber, PalindromicPrimeNumber>();
            services.AddSingleton<IPrimeNumber, PrimeNumber>();
            services.AddSingleton<IPalindromicNumber, PalindromicNumber>();
            services.AddSingleton<ISpigot, Spigot>();
        }).UseConsoleLifetime();

        return builder.Build();
    }
}
