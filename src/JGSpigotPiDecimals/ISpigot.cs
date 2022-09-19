namespace JGSpigotPiDecimals
{
    public interface ISpigot
    {
        IEnumerable<ulong> GetPiDecimals(IProgress<long> progress);
    }
}