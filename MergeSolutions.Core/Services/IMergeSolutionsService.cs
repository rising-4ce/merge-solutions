using MergeSolutions.Core.Parsers;

namespace MergeSolutions.Core.Services
{
    public interface IMergeSolutionsService
    {
        SolutionInfo MergeSolutions(MergePlan mergePlan);
    }
}