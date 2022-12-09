using MergeSolutions.Core.Parsers;

namespace MergeSolutions.Core.Services
{
    public class MergeSolutionService : IMergeSolutionsService
    {
        public SolutionInfo MergeSolutions(MergePlan mergePlan)
        {
            var solutions = mergePlan.Solutions
                .Where(s => s.RelativePath != null)
                .Select(s => SolutionInfo.Parse(s.RelativePath!, mergePlan.RootDir, s.NodeName))
                .ToArray();

            if (mergePlan.OutputSolutionPath == null)
            {
                throw new InvalidOperationException($"Merge plan does not contain {nameof(mergePlan.OutputSolutionPath)}");
            }

            var outputSlnPath = Path.Combine(mergePlan.RootDir!, mergePlan.OutputSolutionPath);
            var mergedSolution = SolutionInfo.MergeSolutions(Path.GetFileNameWithoutExtension(outputSlnPath),
                Path.GetDirectoryName(outputSlnPath) ?? "",
                out var warnings,
                project => !mergePlan.IsExcluded(project),
                mergePlan.ConfigurationFallback,
                solutions);

            if (!string.IsNullOrWhiteSpace(warnings))
            {
                throw new InvalidOperationException(warnings);
            }

            mergedSolution.Save();
            return mergedSolution;
        }
    }
}