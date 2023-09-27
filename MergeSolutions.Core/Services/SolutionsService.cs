using MergeSolutions.Core.Parsers;

namespace MergeSolutions.Core.Services
{
    public class SolutionService : ISolutionService
    {
        public SolutionInfo ParseSolution(string path, string? rootDir)
        {
            return SolutionInfo.Parse(path, rootDir);
        }

        public bool SolutionExists(string path, string? rootDir)
        {
            return SolutionInfo.TryGetPathToExistingSolution(path, rootDir, out _);
        }
    }
}