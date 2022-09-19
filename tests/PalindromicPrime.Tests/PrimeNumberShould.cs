﻿using FindPalindromicPrime;

namespace PalindromicPrime.Tests;

public class PrimeNumberShould
{
    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(7)]
    [InlineData(11)]
    [InlineData(17)]
    [InlineData(19)]
    [InlineData(23)]
    public void ReturnIsPrime(ulong number)
    {
        IPrimeNumber primeNumber = new PrimeNumber();
        Assert.True(primeNumber.IsPrimeNumber(number));            
    }

    [Theory]
    [InlineData(6)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    [InlineData(12)]
    [InlineData(20)]
    [InlineData(27)]
    [InlineData(33)]
    [InlineData(333333333)]
    public void ReturnIsNotPrime(ulong number)
    {
        IPrimeNumber primeNumber = new PrimeNumber();
        Assert.False(primeNumber.IsPrimeNumber(number));
    }
}
