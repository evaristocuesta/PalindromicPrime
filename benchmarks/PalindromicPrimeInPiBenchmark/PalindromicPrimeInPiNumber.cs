using BenchmarkDotNet.Attributes;
using ConsolePalindromicPrimeInPiLevel2;
using FindPalindromicPrime;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PalindromicPrimeInPiBenchmark
{
    public class PalindromicPrimeInPiNumber
    {
        private IPalindromicPrimeInPi _palindromicPrimeInPi;

        [GlobalSetup]
        public void Setup()
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

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                _palindromicPrimeInPi = services.GetRequiredService<IPalindromicPrimeInPi>();
            }
        }

        [Benchmark]
        public async Task<string> Find()
        {
            return await _palindromicPrimeInPi.FindFromFileAsync("C:\\Users\\evari\\Downloads\\Pi-Dec-Chudnovsky-4.txt", 13);
        }

        [Benchmark]
        public async Task<string> FindParallel()
        {
            return await _palindromicPrimeInPi.FindParallelFromFileAsync("C:\\Users\\evari\\Downloads\\Pi-Dec-Chudnovsky-4.txt", 13);
        }
    }
}
