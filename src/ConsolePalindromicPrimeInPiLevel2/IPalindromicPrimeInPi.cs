﻿namespace ConsolePalindromicPrimeInPiLevel2;

public interface IPalindromicPrimeInPi
{
    Task<string?> FindAsync(int start, int digits);
}
