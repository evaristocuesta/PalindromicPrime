using FindPalindromicPrime;

namespace ConsolePalindromicPrimeInPiLevel2;

public class PalindromicPrimeInPi : IPalindromicPrimeInPi
{
    private readonly IPiService _piService;
    private readonly IPalindromicPrimeNumber _palindromicPrimeNumber;
    private readonly Progress<long> _progressPalindromicPrime;

    private DateTime _lastTimeProgressReported = DateTime.Now;
    private int _digitsInPi;

    public PalindromicPrimeInPi(IPiService piService, 
        IPalindromicPrimeNumber palindromicPrimeNumber)
    {
        _piService = piService;
        _palindromicPrimeNumber = palindromicPrimeNumber;

        _progressPalindromicPrime = new Progress<long>();
    }

    public async Task<string?> FindAsync(int digits)
    {
        long i = 2000000;

        while (true)
        {
            var response = await _piService.GetPiDecimalsAsync(i, 1000);
            var palindromicPrime = _palindromicPrimeNumber.Find(response?.Content, digits, _progressPalindromicPrime);
            UpdateProgress(i);

            if (!string.IsNullOrEmpty(palindromicPrime))
            {
                return palindromicPrime;
            }

            i += 1000 - digits - 1;
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
