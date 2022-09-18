using Extensions;
using FindPalindromicPrime;
using JGSpigotPiDecimals;

Console.WriteLine("Finding palindromic prime in PI...");

var pi = Spigot.GetPiDecimals().TakeToString(100000);
string palindromicPrime = PalindromicPrimeNumber.FindParallel(pi, 9);

if (string.IsNullOrEmpty(palindromicPrime))
{
    Console.WriteLine("Number not found");
}
else
{
    Console.WriteLine(palindromicPrime);
}