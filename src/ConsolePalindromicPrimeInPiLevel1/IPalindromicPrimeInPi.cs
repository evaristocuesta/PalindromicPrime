namespace ConsolePalindromicPrimeInPiLevel1
{
    public interface IPalindromicPrimeInPi
    {
        Task<string> FindAsync(int digitsInPi, int digitsInPalindromicPrime);
    }
}
