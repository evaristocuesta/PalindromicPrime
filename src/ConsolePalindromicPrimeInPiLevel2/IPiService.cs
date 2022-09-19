namespace ConsolePalindromicPrimeInPiLevel2;

public interface IPiService
{
    Task<PiResponse?> GetPiDecimalsAsync(long start, int numDigits);
}
