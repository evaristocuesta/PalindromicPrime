using ConsolePalindromicPrimeInPiLevel2;
using Microsoft.Extensions.DependencyInjection;

var host = Startup.Initialize();

using (var serviceScope = host.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    try
    {
        var service = services.GetRequiredService<IPalindromicPrimeInPi>();
        var number = await service.FindAsync(21);
        Console.WriteLine(number);
    }
    catch (Exception)
    {
        Console.WriteLine("Error Occured");
    }
}