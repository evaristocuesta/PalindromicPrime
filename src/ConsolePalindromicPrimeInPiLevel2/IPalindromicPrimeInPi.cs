namespace ConsolePalindromicPrimeInPiLevel2;

public interface IPalindromicPrimeInPi
{
    Task<string?> FindAsync(int start, int digits);
    Task<string?> FindParallelAsync(int start, int digits);
    Task<string?> FindFromFileAsync(string filename, int digits);
    Task<string?> FindParallelFromFileAsync(string filename, int digits);
}
