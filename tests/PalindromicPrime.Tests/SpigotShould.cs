using JGSpigotPiDecimals;
using System.Text;

namespace PalindromicPrime.Tests;

public class SpigotShould
{
    // First 1500 PI decimals
    private const string PI_1500_DECIMALS = "141592653589793238462643383279502884197169399375105820974944592307816406286208998628034825342117067982148086513282306647093844609550582231725359408128481117450284102701938521105559644622948954930381964428810975665933446128475648233786783165271201909145648566923460348610454326648213393607260249141273724587006606315588174881520920962829254091715364367892590360011330530548820466521384146951941511609433057270365759591953092186117381932611793105118548074462379962749567351885752724891227938183011949129833673362440656643086021394946395224737190702179860943702770539217176293176752384674818467669405132000568127145263560827785771342757789609173637178721468440901224953430146549585371050792279689258923542019956112129021960864034418159813629774771309960518707211349999998372978049951059731732816096318595024459455346908302642522308253344685035261931188171010003137838752886587533208381420617177669147303598253490428755468731159562863882353787593751957781857780532171226806613001927876611195909216420198938095257201065485863278865936153381827968230301952035301852968995773622599413891249721775283479131515574857242454150695950829533116861727855889075098381754637464939319255060400927701671139009848824012858361603563707660104710181942955596198946767837449448255379774726847104047534646208046684259069491293313677028989152104752162056966024058038150193511253382430035587640247496473263914199272604269922796782354781636009341721641219924586315030286182974555706749838505494588586926995690927210797509302955";

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(200)]
    [InlineData(500)]
    [InlineData(1000)]
    [InlineData(1200)]
    [InlineData(1500)]
    public void GenerateDigitsCorrectly(int decimals)
    {
        StringBuilder pi = new();
        ISpigot spigot = new Spigot();

        foreach (var digit in spigot.GetPiDecimals(new Progress<long>()).Take(decimals))
        {
            pi.Append(digit);
        }

        Assert.Equal(PI_1500_DECIMALS.Substring(0, decimals), pi.ToString());
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(200)]
    [InlineData(500)]
    [InlineData(1000)]
    [InlineData(1200)]
    [InlineData(1500)]
    public void Generate10FirstDigitsCorrectly(int decimals)
    {
        StringBuilder pi = new();
        ISpigot spigot = new Spigot();

        foreach (var digit in spigot.GetPiDecimals(new Progress<long>()).Take(decimals))
        {
            pi.Append(digit);
        }

        Assert.StartsWith(PI_1500_DECIMALS.Substring(0, decimals), pi.ToString());
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(200)]
    [InlineData(500)]
    [InlineData(1000)]
    [InlineData(1200)]
    [InlineData(1500)]
    public void Generate10LastDigitsCorrectly(int decimals)
    {
        StringBuilder pi = new();
        ISpigot spigot = new Spigot();

        foreach (var digit in spigot.GetPiDecimals(new Progress<long>()).Skip(PI_1500_DECIMALS.Length - decimals).Take(decimals))
        {
            pi.Append(digit);
        }

        Assert.EndsWith(PI_1500_DECIMALS.Substring(PI_1500_DECIMALS.Length - decimals, decimals), pi.ToString());
    }
}