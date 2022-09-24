using MergeSolutions.Core.Parsers;

namespace MergeSolutions.Core.Services
{
    public class SolutionService : ISolutionService
    {
        public SolutionInfo ParseSolution(string path)
        {
            return SolutionInfo.Parse(path);
        }
    }
}