namespace ConsolePalindromicPrimeInPiLevel2;

public class PalindromicPrimeInPi : IPalindromicPrimeInPi
{
    private readonly IPiService _piService;

    public PalindromicPrimeInPi(IPiService piService)
    {
        _piService = piService;
    }

    public async Task<string?> FindAsync(int digits)
    {
        bool found = false;
        int i = 0;
        PiResponse? result = new();

        while (!found)
        {
            result = await _piService.GetPiDecimalsAsync(i, 1000);
            i += 1000 - digits;
        }

        return result?.Content;
    }
}
