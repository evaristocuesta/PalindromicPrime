using Extensions;
using FindPalindromicPrime;
using JGSpigotPiDecimals;
using System.Globalization;

DateTime lastTimeProgressReported = DateTime.Now;
const int DIGITS = 1000000;

var progressPi = new Progress<long>();
progressPi.ProgressChanged += Progress_ProgressChanged;

var progressPalindromicPrime = new Progress<long>();
progressPalindromicPrime.ProgressChanged += ProgressPalindromicPrime_ProgressChanged;

var pi = GeneratePi(DIGITS, progressPi);

FindPalindromicPrime(pi, 9, progressPalindromicPrime);

static string GeneratePi(int digits, Progress<long> progressPi)
{
    var pi = ReadPiFromFile(digits);

    if (!string.IsNullOrEmpty(pi))
    {
        Console.Write($"\rGenerated {DIGITS} decimals of PI... - 100%");
        return pi;
    }

    Console.WriteLine($"Generating PI...{DateTime.Now.ToLongTimeString()}");
    pi = Spigot.GetPiDecimals(progressPi).TakeToString(digits);
    Console.Write($"\rGenerated {DIGITS} decimals of PI... - 100%");
    SavePiToFile(DIGITS, pi);
    return pi;
}

static string ReadPiFromFile(int digits)
{
    string path = $"./pi-{digits}.txt";

    if (File.Exists(path))
    {
        try
        {
            return File.ReadAllText(path);
        }
        catch
        {
            return string.Empty;
        }
    }

    return string.Empty;
}

static void SavePiToFile(int digits, string pi)
{
    string path = $"./pi-{digits}.txt";

    try
    {
        File.WriteAllText(path, pi);
    }
    catch
    {
        
    }
}

static void FindPalindromicPrime(string pi, int digits, Progress<long> progressPalindromicPrime)
{
    Console.WriteLine($"\r\nFinding palindromic prime in PI...{DateTime.Now.ToLongTimeString()}");
    string palindromicPrime = PalindromicPrimeNumber.Find(pi, digits, progressPalindromicPrime);

    if (string.IsNullOrEmpty(palindromicPrime))
    {
        Console.WriteLine($"Number not found {DateTime.Now.ToLongTimeString()}");
    }
    else
    {
        Console.WriteLine($"Palindromic prime number found: {palindromicPrime} - {DateTime.Now.ToLongTimeString()}");
    }
}

void Progress_ProgressChanged(object? sender, long e)
{
    var now = DateTime.Now;

    if (now.Subtract(lastTimeProgressReported).Seconds <= 1)
    {
        return;
    }

    Console.Write($"\rGenerated {e} decimals of PI... - {e * 100 / DIGITS}%");
    lastTimeProgressReported = now;
}

void ProgressPalindromicPrime_ProgressChanged(object? sender, long e)
{
    var now = DateTime.Now;

    if (now.Subtract(lastTimeProgressReported).Seconds <= 1)
    {
        return;
    }

    Console.Write($"\rSearching palindromic prime... - {e * 100 / DIGITS}%");
    lastTimeProgressReported = now;
};