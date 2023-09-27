using MergeSolutions.Core.Parsers;

namespace MergeSolutions.Core.Services
{
    public interface ISolutionService
    {
        SolutionInfo ParseSolution(string path, string? rootDir);
        bool SolutionExists(string path, string? rootDir);
    }
}