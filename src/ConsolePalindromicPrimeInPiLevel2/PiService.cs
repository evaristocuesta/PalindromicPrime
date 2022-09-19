using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace ConsolePalindromicPrimeInPiLevel2;

public class PiService : IPiService
{
    private readonly IHttpClientFactory _httpFactory;

    public PiService(IHttpClientFactory httpFactory)
    {
        _httpFactory = httpFactory;
    }

    public async Task<PiResponse?> GetPiDecimalsAsync(long start, int numDigits)
    {
        var client = _httpFactory.CreateClient();
        client.BaseAddress = new Uri("https://api.pi.delivery/v1/");
        client.DefaultRequestHeaders.Accept.Clear();

        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await client.GetAsync($"pi?start={start}&numberOfDigits={numDigits}");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Status Code: {response.StatusCode}");
        }

        try
        {
            return await response.Content.ReadFromJsonAsync<PiResponse>();
        }
        catch
        {
            return default;
        }
    }
}
