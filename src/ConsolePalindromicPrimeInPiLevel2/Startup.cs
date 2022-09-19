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
        }).UseConsoleLifetime();

        return builder.Build();
    }
}
