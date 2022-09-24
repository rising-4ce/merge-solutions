namespace MergeSolutions.Core
{
    public class MergePlan
    {
        public MergePlan()
        {
            PlanName = "Untitled";
        }

        /// <summary>
        /// Excluded projects. Key is solution name, value is a project GUID
        /// </summary>
        public KeyValuePair<string, string>[] ExcludedProjects { get; set; } = Array.Empty<KeyValuePair<string, string>>();

        public string OutputSolutionPath { get; set; } = "merged.sln";

        public string PlanName { get; set; }

        /// <summary>
        /// Excluded projects. Key is solution name, value is a path to solution
        /// </summary>
        public Dictionary<string, string> Solutions { get; set; } = new();

        public bool IsExcluded(string? solutionName, string? projectGuid)
        {
            return ExcludedProjects.Any(e => e.Key == solutionName && e.Value == projectGuid);
        }
    }
}