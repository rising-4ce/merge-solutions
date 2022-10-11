using MergeSolutions.Core.Parsers;

namespace MergeSolutions.Core.Services
{
    public class MergeSolutionService : IMergeSolutionsService
    {
        public SolutionInfo MergeSolutions(MergePlan mergePlan)
        {
            var outputSlnPath = Path.GetFullPath(mergePlan.OutputSolutionPath);

            var solutions = mergePlan.Solutions
                .Where(s => s.RelativePath != null)
                .Select(s => SolutionInfo.Parse(s.RelativePath!, mergePlan.RootDir))
                .ToArray();

            var mergedSolution = SolutionInfo.MergeSolutions(Path.GetFileNameWithoutExtension(outputSlnPath),
                Path.GetDirectoryName(outputSlnPath) ?? "",
                out var warnings,
                project => !mergePlan.IsExcluded(project.SolutionName, project.Guid),
                solutions);

            // Remove empty solution directories
            //var emptyProjectDirectories = mergedSolution.Projects.OfType<ProjectDirectory>().Where(pd => mergedSolution.NestedSection.Dirs.All(nd => nd != pd)).ToArray();
            //mergedSolution.Projects.RemoveAll(p => emptyProjectDirectories.Contains(p));

            mergedSolution.Save();
            return mergedSolution;
        }
    }
}