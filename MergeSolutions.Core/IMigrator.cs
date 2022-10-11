namespace MergeSolutions.Core
{
    public interface IMigrator
    {
        string? Migrate(string? content, string rootDir);
    }
}