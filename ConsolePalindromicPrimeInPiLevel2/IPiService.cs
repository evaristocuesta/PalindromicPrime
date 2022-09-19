namespace ConsolePalindromicPrimeInPiLevel2;

public interface IPiService
{
    Task<PiResponse?> GetPiDecimalsAsync(int start, int numDigits);
}
