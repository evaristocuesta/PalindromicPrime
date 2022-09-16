using System.Numerics;

namespace JGSpigotPiDecimals
{
    public class Spigot
    {
        public static IEnumerable<uint> GetPiDecimals()
        {
            BigInteger
              k = 1,
              l = 3,
              n = 3,
              q = 1,
              r = 0,
              t = 1;

            BigInteger nr = 10 * (r - t * n);
            n = 10 * (3 * q + r) / t - 10 * n;
            q *= 10;
            r = nr;

            for (; ; )
            {
                var tn = t * n;

                if (4 * q + r - t < tn)
                {
                    yield return (uint)n;
                    nr = 10 * (r - tn);
                    n = 10 * (3 * q + r) / t - 10 * n;
                    q *= 10;
                }
                else
                {
                    t *= l;
                    nr = (2 * q + r) * l;
                    var nn = (q * (7 * k) + 2 + r * l) / t;
                    q *= k;
                    l += 2;
                    ++k;
                    n = nn;
                }

                r = nr;
            }
        }
    }
}