using FindPalindromicPrime;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsolePalindromicPrimeInPiLevel2;

internal class Startup
{
    public static IHost Initialize()
    {
        var builder = new HostBuilder()
        .ConfigureServices((hostContext, services) =>
        {
            services.AddHttpClient();
            services.AddSingleton<IPiService, PiService>();
            services.AddSingleton<IPalindromicPrimeInPi, PalindromicPrimeInPi>();
            services.AddSingleton<IPrimeNumber, PrimeNumber>();
            services.AddSingleton<IPalindromicNumber, PalindromicNumber>();
            services.AddSingleton<IPalindromicPrimeNumber, PalindromicPrimeNumber>();
        }).UseConsoleLifetime();

        return builder.Build();
    }
}
