using Extensions;
using FindPalindromicPrime;
using JGSpigotPiDecimals;

namespace ConsolePalindromicPrimeInPiLevel1
{
    public class PalindromicPrimeInPi : IPalindromicPrimeInPi
    {
        private readonly IPalindromicPrimeNumber _palindromicPrimeNumber;
        private readonly ISpigot _spigot;
        private readonly Progress<long> _progressPi;
        private readonly Progress<long> _progressPalindromicPrime;
        private readonly string _path = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/palindromic-prime-pi/";

        private DateTime _lastTimeProgressReported = DateTime.Now;
        private string _file = string.Empty;
        private int _digitsInPi;
        
        public PalindromicPrimeInPi(IPalindromicPrimeNumber palindromicPrimeNumber, 
            ISpigot spigot)
        {
            _palindromicPrimeNumber = palindromicPrimeNumber;
            _spigot = spigot;

            _progressPi = new Progress<long>();
            _progressPi.ProgressChanged += ProgressPi_ProgressChanged;

            _progressPalindromicPrime = new Progress<long>();
            _progressPalindromicPrime.ProgressChanged += ProgressPalindromicPrime_ProgressChanged;
        }

        public Task<string> FindAsync(int digitsInPi, int digitsInPalindromicPrime)
        {
            _digitsInPi = digitsInPi;
            var pi = GeneratePi(digitsInPi, _progressPi);
            FindPalindromicPrime(pi, digitsInPalindromicPrime, _progressPalindromicPrime);
            return Task.FromResult(string.Empty);
        }

        private string GeneratePi(int digits, Progress<long> progressPi)
        {
            _file = $"pi-{digits}.txt";
            var pi = ReadPiFromFile(_path);

            if (!string.IsNullOrEmpty(pi))
            {
                Console.Write($"\rGenerated {digits} decimals of PI... - 100%");
                return pi;
            }

            Console.WriteLine($"Generating PI...{DateTime.Now.ToLongTimeString()}");
            pi = _spigot.GetPiDecimals(progressPi).TakeToString(digits);
            Console.Write($"\rGenerated {digits} decimals of PI... - 100%");
            SavePiToFile(_path, pi);
            return pi;
        }

        private string ReadPiFromFile(string path)
        {
            if (File.Exists($"{path}/{_file}"))
            {
                try
                {
                    return File.ReadAllText($"{path}/{_file}");
                }
                catch
                {
                    return string.Empty;
                }
            }

            return string.Empty;
        }

        private void SavePiToFile(string path, string pi)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                File.WriteAllText($"{path}/{_file}", pi);
            }
            catch
            {

            }
        }

        private void FindPalindromicPrime(string pi, int digits, Progress<long> progressPalindromicPrime)
        {
            Console.WriteLine($"\r\nFinding palindromic prime in PI...{DateTime.Now.ToLongTimeString()}");
            string palindromicPrime = _palindromicPrimeNumber.Find(pi, digits, progressPalindromicPrime);

            if (string.IsNullOrEmpty(palindromicPrime))
            {
                Console.WriteLine($"Number not found {DateTime.Now.ToLongTimeString()}");
            }
            else
            {
                Console.WriteLine($"Palindromic prime number found: {palindromicPrime} - {DateTime.Now.ToLongTimeString()}");
            }
        }

        private void ProgressPi_ProgressChanged(object? sender, long e)
        {
            var now = DateTime.Now;

            if (now.Subtract(_lastTimeProgressReported).Seconds <= 1)
            {
                return;
            }

            Console.Write($"\rGenerated {e} decimals of PI... - {e * 100 / _digitsInPi}%");
            _lastTimeProgressReported = now;
        }

        private void ProgressPalindromicPrime_ProgressChanged(object? sender, long e)
        {
            var now = DateTime.Now;

            if (now.Subtract(_lastTimeProgressReported).Seconds <= 1)
            {
                return;
            }

            Console.Write($"\rSearching palindromic prime... - {e * 100 / _digitsInPi}%");
            _lastTimeProgressReported = now;
        }
    }
}
