using FindPalindromicPrime;

namespace PalindromicPrime.Tests;

public class FindPalindromicPrimeShould
{
    [Theory]
    [InlineData("983742", 1, "3")]
    [InlineData("987711", 2, "11")]
    [InlineData("981312", 3, "131")]
    [InlineData("98103013742", 5, "10301")]
    [InlineData("103019813013742", 5, "10301")]
    [InlineData("981030374210301", 5, "10301")]
    public void FindPalindromicPrime(string number, int digits, string expected)
    {
        string result = PalindromicPrimeNumber.Find(number, digits);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("987711", 2, "11")]
    [InlineData("981312", 3, "131")]
    [InlineData("98103013742", 5, "10301")]
    [InlineData("103019813013742", 5, "10301")]
    [InlineData("981030374210301", 5, "10301")]
    public void FindPalindromicPrimeParallel(string number, int digits, string expected)
    {
        string result = PalindromicPrimeNumber.FindParallel(number, digits);
        Assert.Equal(expected, result);
    }
}
