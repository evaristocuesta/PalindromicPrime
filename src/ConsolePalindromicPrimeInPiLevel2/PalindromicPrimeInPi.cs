using FindPalindromicPrime;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;

namespace ConsolePalindromicPrimeInPiLevel2;

public class PalindromicPrimeInPi : IPalindromicPrimeInPi
{
    private const int DIGITS_PI = 1_000;
    private const int LENGTH = 100_000_000;
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

    public async Task<string?> FindParallelAsync(int start, int digits)
    {
        int i = start;
        var responses = new ConcurrentDictionary<int, string>();

        while (true)
        {
            var indexes = Enumerable.Range(i, i + 15).Select(x => x += x * (DIGITS_PI - digits));

            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = 16, 
                CancellationToken = CancellationToken.None
            };

            await Parallel.ForEachAsync(indexes, parallelOptions, async (pos, ct) =>
            {
                var response = await _piService.GetPiDecimalsAsync(pos, DIGITS_PI);

                if (response == null || string.IsNullOrEmpty(response.Content))
                {
                    responses[pos] = "ERROR";
                    return;
                }

                var palindromicPrime = _palindromicPrimeNumber.Find(response.Content, digits, _progressPalindromicPrime);
                UpdateProgress(pos);

                if (!string.IsNullOrEmpty(palindromicPrime))
                {
                    responses[pos] = palindromicPrime;
                }

                responses[pos] = string.Empty;
            });

            i++;
        }
    }

    public async Task<string?> FindFromFileAsync(string filename, int digits)
    {
        Char[] buffer = new Char[LENGTH]; 

        using (var sr = new StreamReader(filename))
        {
            int i = 0;
            string lastDigits = string.Empty;

            while (!sr.EndOfStream)
            {
                await sr.ReadAsync(buffer, 0, LENGTH);
                var pi = new String(buffer);
                var palindromicPrime = _palindromicPrimeNumber.Find(string.Concat(lastDigits, pi), digits, _progressPalindromicPrime);
                UpdateProgress(i);
                i++;

                if (!string.IsNullOrEmpty(palindromicPrime))
                {
                    return palindromicPrime;
                }

                lastDigits = pi.Substring(pi.Length - digits + 1, digits - 1);
            }
        }

        return String.Empty;
    }

    public async Task<string?> FindParallelFromFileAsync(string filename, int digits)
    {
        Char[] buffer = new Char[LENGTH];

        using (var sr = new StreamReader(filename))
        {
            int i = 0;
            string lastDigits = string.Empty;

            while (!sr.EndOfStream)
            {
                var length = sr.BaseStream.Length - sr.BaseStream.Position < LENGTH ? sr.BaseStream.Length - sr.BaseStream.Position : LENGTH;
                await sr.ReadAsync(buffer, 0, (int)length);
                var pi = new String(buffer);
                var palindromicPrime = _palindromicPrimeNumber.FindParallel(string.Concat(lastDigits, pi), digits);
                UpdateProgress(i);
                i++;

                if (!string.IsNullOrEmpty(palindromicPrime))
                {
                    return palindromicPrime;
                }

                lastDigits = pi.Substring(pi.Length - digits + 1, digits - 1);
            }
        }

        return String.Empty;
    }

    private void UpdateProgress(long progress)
    {
        var now = DateTime.Now;

        if (now.Subtract(_lastTimeProgressReported).Seconds <= 1)
        {
            return;
        }

        Console.WriteLine($"Searching palindromic prime... - {progress}");
        _lastTimeProgressReported = now;
    }
}
