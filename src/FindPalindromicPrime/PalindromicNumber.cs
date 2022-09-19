namespace FindPalindromicPrime;

public class PalindromicNumber : IPalindromicNumber
{
    public bool IsPalindrome(string number)
    {
        for (int i = 0; i < number.Length / 2; i++)
        {
            if (number[i] != number[number.Length - 1 - i])
                return false;
        }
        return true;
    }
}
