using ConsolePalindromicPrimeInPiLevel2;
using Microsoft.Extensions.DependencyInjection;

var host = Startup.Initialize();

using (var serviceScope = host.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    try
    {
        DateTime dateTime = DateTime.Now;
        var service = services.GetRequiredService<IPalindromicPrimeInPi>();
        var number = await service.FindFromFileAsync("D:\\Pi\\Pi-4.txt", 21);
        Console.WriteLine(DateTime.Now.Subtract(dateTime).TotalMilliseconds);

        if (string.IsNullOrWhiteSpace(number))
        {
            Console.WriteLine("Palindromic prime number not found");
        }
        else
        {
            Console.WriteLine($"Palindromic prime number found: {number} - {DateTime.Now.ToLongTimeString()}");
        }

    }
    catch (Exception)
    {
        Console.WriteLine("Error Occured");
    }
}