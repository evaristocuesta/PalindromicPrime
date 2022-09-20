using FindPalindromicPrime;

namespace ConsolePalindromicPrimeInPiLevel2;

public class PalindromicPrimeInPi : IPalindromicPrimeInPi
{
    private const int DIGITS_PI = 1000;
    private readonly IPiService _piService;
    private readonly IPalindromicPrimeNumber _palindromicPrimeNumber;
    private readonly Progress<long> _progressPalindromicPrime;

    private DateTime _lastTimeProgressReported = DateTime.Now;

    public PalindromicPrimeInPi(IPiService piService, 
        IPalindromicPrimeNumber palindromicPrimeNumber)
    {
        _piService = piService;
        _palindromicPrimeNumber = palindromicPrimeNumber;

        _progressPalindromicPrime = new Progress<long>();
    }

    public async Task<string?> FindAsync(int start, int digits)
    {
        long i = start;

        while (true)
        {
            var response = await _piService.GetPiDecimalsAsync(i, DIGITS_PI);
            
            if (response == null || string.IsNullOrEmpty(response.Content))
            {
                continue;
            }

            var palindromicPrime = _palindromicPrimeNumber.Find(response.Content, digits, _progressPalindromicPrime);
            UpdateProgress(i);

            if (!string.IsNullOrEmpty(palindromicPrime))
            {
                return palindromicPrime;
            }

            i += DIGITS_PI - digits + 1;
        }
    }

    private void UpdateProgress(long progress)
    {
        var now = DateTime.Now;

        if (now.Subtract(_lastTimeProgressReported).Seconds <= 1)
        {
            return;
        }

        Console.Write($"\rSearching palindromic prime... - {progress}");
        _lastTimeProgressReported = now;
    }
}
