using Extensions;
using FindPalindromicPrime;
using JGSpigotPiDecimals;

DateTime lastTimeProgressReported = DateTime.Now;
const int DIGITS = 100000;
string path = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/palindromic-prime-pi/";
string file = $"pi-{DIGITS}.txt";

var progressPi = new Progress<long>();
progressPi.ProgressChanged += Progress_ProgressChanged;

var progressPalindromicPrime = new Progress<long>();
progressPalindromicPrime.ProgressChanged += ProgressPalindromicPrime_ProgressChanged;

var pi = GeneratePi(DIGITS, progressPi);

FindPalindromicPrime(pi, 9, progressPalindromicPrime);

string GeneratePi(int digits, Progress<long> progressPi)
{
    var pi = ReadPiFromFile(path);

    if (!string.IsNullOrEmpty(pi))
    {
        Console.Write($"\rGenerated {DIGITS} decimals of PI... - 100%");
        return pi;
    }

    Console.WriteLine($"Generating PI...{DateTime.Now.ToLongTimeString()}");
    pi = Spigot.GetPiDecimals(progressPi).TakeToString(digits);
    Console.Write($"\rGenerated {DIGITS} decimals of PI... - 100%");
    SavePiToFile(path, pi);
    return pi;
}

string ReadPiFromFile(string path)
{
    if (File.Exists($"{path}/{file}"))
    {
        try
        {
            return File.ReadAllText($"{path}/{file}");
        }
        catch
        {
            return string.Empty;
        }
    }

    return string.Empty;
}

void SavePiToFile(string path, string pi)
{
    try
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText($"{path}/{file}", pi);
    }
    catch
    {
        
    }
}

void FindPalindromicPrime(string pi, int digits, Progress<long> progressPalindromicPrime)
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